using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface IHistoryExamService
    {
        BaseResponse getOne(long id);
        BaseResponse create(AddHistoryExamReq historyExamReq);
        BaseResponse listHistoryExam(long userId);
        BaseResponse updateExam(long id);
        BaseResponse getResultExam(string IdCardStudent, long ExamId);
    }

    public class HistoryExamService : IHistoryExamService
    {
        IHistoryExamRepo _historyExamRepo;
        IInformationStudentRepo _informationStudentRepo;
        IAnswerQuestionChoseRepo _answerQuestionChoseRepo;
        HistoryExam history;

        public HistoryExamService(IHistoryExamRepo historyExamRepo, IInformationStudentRepo informationStudentRepo, IAnswerQuestionChoseRepo answerQuestionChoseRepo)
        {
            _informationStudentRepo = informationStudentRepo;
            _answerQuestionChoseRepo = answerQuestionChoseRepo;
        }

        public BaseResponse create(AddHistoryExamReq historyExamReq)
        {
            this.history = new HistoryExam();
            this.history.CreatedAt = DateTime.Now;
            this.history.InformationStudentId = historyExamReq.studentId;
            this.history.StartTime = historyExamReq.StartTime;
            this.history.EndTime = historyExamReq.EndTime;

            _historyExamRepo.addOrUpdate(this.history);
            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        {
            var data = _historyExamRepo.getOne(id);
            if (data == null)
            {
                return new BaseResponse();
            }
            var format = new VHistoryExamOne
            {
                Id = data.Id,
                InfostudentId = data.InformationStudentId,
                StartTime = data.StartTime,
                EndTime = data.EndTime,
                CreatedAt = data.CreatedAt
            };
            return new BaseResponse(format);
        }

        public BaseResponse getResultExam(string IdCardStudent, long ExamId)
        {
            var userData = _informationStudentRepo.findByIdCardStudent(IdCardStudent);
            if (userData == null)
            {
                return new BaseResponse(MESSAGE.STATUS_RESPONSE.INTERNAL_SERVER, MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            }
            var data = _historyExamRepo.findByInformationStudentIdAdnExamId(userData.Id, ExamId);
            if (data == null)
            {
                return new BaseResponse(MESSAGE.STATUS_RESPONSE.INTERNAL_SERVER, MESSAGE.VALIDATE.ANSWER_QUESTION_NOT_FOUNT);
            }
            return new BaseResponse(new ResponseResult
            {
                fullName = userData.FullName,
                idCardStudent = userData.IdCardStudent,
                score = data.Score
            });
        }

        public BaseResponse listHistoryExam(long userId)
        {
            return new BaseResponse(_historyExamRepo.GetHistoryExamList(userId));
        }

        public BaseResponse updateExam(long id)
        {
            var data = _historyExamRepo.getOne(id);
            // Lấy ra số điểm từng môn 
            List<AnswerQuestionChose> dataQuestion = _answerQuestionChoseRepo.getListByExamId(data.Id);
            if(dataQuestion.Count() == 0)
            {
                return new BaseResponse(MESSAGE.STATUS_RESPONSE.INTERNAL_SERVER, MESSAGE.VALIDATE.ANSWER_QUESTION_NOT_FOUNT);
            }
            var count = 0;
            foreach(AnswerQuestionChose dto in dataQuestion)
            {
                count = count + (int)dto.Score;
            }
            // cập nhập lại điểm vào bài kiểm tra 
            data.Score = count;
            _historyExamRepo.addOrUpdate(data);
            return new BaseResponse();
        }
    }
}
