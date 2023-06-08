using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface IQuestionService
    {
        BaseResponse getOne(long id);
        BaseResponse deleteQuestion(long id);
        BaseResponse createOrUpdate(AddQuestionReq questionReq);
        BaseResponse pagination(QuestionReq req);
        BaseResponse getListByExamId(long id);
        BaseResponse startExam(StartExamReq req);
    }
    public class QuestionService : IQuestionService
    {
        IQuestionRepo _questionRepo;
        IHistoryExamRepo _historyExamRepo;
        Question question;

        public QuestionService(IQuestionRepo questionRepo, IHistoryExamRepo historyExamRepo)

        {
            _questionRepo = questionRepo;
            _historyExamRepo = historyExamRepo;
        }

        public BaseResponse createOrUpdate(AddQuestionReq questionReq)
        {
           if(questionReq.Id == null)
            {
                this.question = new Question();
                this.question.IsDelete = Constants.IsDelete.False;
            }
            else
            {
                this.question = _questionRepo.getOne((long)questionReq.Id);
                    if(this.question == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }

            }
            convertFromDtoToModel(questionReq);
            _questionRepo.addOrUpdateQuestion(this.question);
            return new BaseResponse();
        }

        private void convertFromDtoToModel(AddQuestionReq questions)
        {
            question.ExamId = questions.ExamId;
            question.NameQuestion = questions.NameQuestion;
        }

        public BaseResponse deleteQuestion(long id)
        {
            var data = _questionRepo.getOne(id);
            if(data == null)
            {
                throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            }
            data.IsDelete = 1;
            data.UpdateAt = DateTime.Now;
            _questionRepo.deleteQuestion(data);
            return new BaseResponse();
            
        }

        public BaseResponse getOne(long id)
        {
            var data = _questionRepo.getOne(id);
            if(data == null)
            {
                return new BaseResponse();
            }
            var format = new VQuestionOne
            {
                Id = data.Id,
                ExamId = data.ExamId,
                NameQuestion = data.NameQuestion
            };
            return new BaseResponse(format);
        }

        public BaseResponse pagination(QuestionReq req)
        {
            return new BaseResponse(_questionRepo.pagination(req));
        }

        public BaseResponse getListByExamId(long id)
        {
            var data = _questionRepo.getQuestionList(id);
            if(data.Count != 10)
            {
                throw new Exception(MESSAGE.VALIDATE.TOTAL_QUESTION_INVALID);
            }
            return new BaseResponse(data);
        }

        public BaseResponse startExam(StartExamReq req)
        {
            int hours = req.totalTime / 60;
            int minutes = req.totalTime % 60;
            DateTime currentTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            endTime.AddHours(hours);
            endTime.AddMinutes(minutes);
            var dto = new HistoryExam();
            dto.ExamId = req.idExam;
            dto.StartTime = currentTime;
            dto.EndTime = endTime;
            dto.InformationStudentId = req.idStudent;
            return new BaseResponse(new InfoAllOfStarExamRespon
            {
                examInfo = _historyExamRepo.startExam(dto),
                questionInfo = _questionRepo.startExams(req.idExam)
            }); ;
        }
    }
}
