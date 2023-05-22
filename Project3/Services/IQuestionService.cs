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
        BaseResponse getPagin(QuestionReq filter);
        BaseResponse createOrUpdate(AddQuestionReq questionReq);
    }
    public class QuestionService : IQuestionService
    {
        IQuestionRepo _questionRepo;
        Question question;

        public QuestionService(IQuestionRepo questionRepo)
        {
            _questionRepo = questionRepo;
        }

        public BaseResponse createOrUpdate(AddQuestionReq questionReq)
        {
           if(questionReq.Id == null)
            {
                this.question = new Question();
                this.question.IsDelete = Constants.IsDelete.False;
                this.question.CreatedAt = DateTime.Now;
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

        public BaseResponse getPagin(QuestionReq filter)
        {
            var data = _questionRepo.paginations(filter);
            return new BaseResponse(data);
        }
    }
}
