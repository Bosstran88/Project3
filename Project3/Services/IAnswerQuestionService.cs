using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface IAnswerQuestionService
    {
        BaseResponse getOne(long id);
        BaseResponse deleteAnswerQuestion(long id);
        BaseResponse createOrUpdate(AddAnswerQuestionReq answerQuestionReq);
        BaseResponse listAnswerQuestion(long idQuestion);
        BaseResponse pagination(AnswerQuestionPageReq answerQuestionReq);
    }
    public class AnswerQuestionService : IAnswerQuestionService
    {
        IAnswerQuestionRepo _answerQuestionRepo;
        AnswerQuestion answerQuestion;

        public AnswerQuestionService(IAnswerQuestionRepo answerQuestionRepo)
        {
            _answerQuestionRepo = answerQuestionRepo;
        }

        public BaseResponse createOrUpdate(AddAnswerQuestionReq answerQuestionReq)
        {
            if(answerQuestionReq.Id == null)
            {
                if(_answerQuestionRepo.countAnswerOfQuestion((long)answerQuestionReq.QuestionId) == 4)
                {
                    throw new ValidateException(MESSAGE.VALIDATE.ANSWER_QUESTION_NOT_BIG_THAN_4);
                }
                this.answerQuestion = new AnswerQuestion();
                this.answerQuestion.IsDelete = Constants.IsDelete.False;
                this.answerQuestion.CreatedAt = DateTime.Now;
            }
            else
            {
                this.answerQuestion = _answerQuestionRepo.getOne((long)answerQuestionReq.Id);
                if (this.answerQuestion == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }
                this.answerQuestion.UpdateAt = DateTime.Now;
            }
            ConvertFromDtoModel(answerQuestionReq);
            _answerQuestionRepo.addOrUpdateAnswerQuestion(this.answerQuestion);
            return new BaseResponse();
        }

        private void ConvertFromDtoModel(AddAnswerQuestionReq answerQuestions)
        {
            answerQuestion.QuestionId = answerQuestions.QuestionId;
            answerQuestion.Answer = answerQuestions.AnswerQuestion;
            answerQuestion.Score = answerQuestions.Score;
                
        }

        public BaseResponse deleteAnswerQuestion(long id)
        {
            var data = _answerQuestionRepo.getOne(id);
            if(data == null)
            {
                throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            }
            data.IsDelete = 1;
            data.UpdateAt = DateTime.Now;
            _answerQuestionRepo.deleteAnswerQuestion(data);

            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        {
            var data = _answerQuestionRepo.getOne(id);
            if (data == null)
            {
                return new BaseResponse();
            }
            var format = new VAnswerQuestionOne
            {
                Id = data.Id,
                QuestionId = data.QuestionId,
                Score = data.Score,
                CreatedAt = data.CreatedAt,
                UpdateAt = data.UpdateAt
            };
            return new BaseResponse(format);
        }

        public BaseResponse listAnswerQuestion(long idQuestion)
        {
            return new BaseResponse(_answerQuestionRepo.GetAnswerQuestionList(idQuestion));
        }

        public BaseResponse pagination(AnswerQuestionPageReq answerQuestionReq)
        {
            return new BaseResponse(_answerQuestionRepo.pagination(answerQuestionReq));
        }
    }
}
