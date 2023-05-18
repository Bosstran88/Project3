using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface IAnswerQuestionChoseService
    {
        BaseResponse getOne(long id);
       
        BaseResponse createOrUpdate(AddAnswerQuestionChoseReq answerQuestionChoseReq);

        BaseResponse deleteAnswerQuestionChose(long id);

    }
    public class AnswerQuestionChoseService : IAnswerQuestionChoseService
    {
        IAnswerQuestionChoseRepo _answerQuestionChoseRepo;
        AnswerQuestionChose answerQuestionChose;

        public AnswerQuestionChoseService(IAnswerQuestionChoseRepo answerQuestionChoseRepo, AnswerQuestionChose answerQuestionChose)
        {
            _answerQuestionChoseRepo = answerQuestionChoseRepo;
            this.answerQuestionChose = answerQuestionChose;
        }

        public BaseResponse createOrUpdate(AddAnswerQuestionChoseReq answerQuestionChoseReq)
        {
  
            if(answerQuestionChoseReq.Id == null)
            {
                this.answerQuestionChose = new AnswerQuestionChose();
                this.answerQuestionChose.CreatedAt = DateTime.Now;
            }
            else
            {
                this.answerQuestionChose = _answerQuestionChoseRepo.getOne((long)answerQuestionChoseReq.Id);
                if(this.answerQuestionChose == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }
             
            }
            _answerQuestionChoseRepo.createOrUpdateAnswerQuestionChose(this.answerQuestionChose);
            return new BaseResponse();
        }

        public BaseResponse deleteAnswerQuestionChose(long id)
        {
            var data = _answerQuestionChoseRepo.getOne(id);
            if (data == null)
            {
                throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            }
            
            _answerQuestionChoseRepo.deleteAnswerQuestionChose(data);

            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        {
            var data = _answerQuestionChoseRepo.getOne(id);
            if (data == null)
            {
                return new BaseResponse();
            }
            var format = new VAnswerQuestionChoseOne
            {
                Id = data.Id,
                QuestionId = data.QuestionId,
                AnswerChose =data.AnswerChose,
                HistoryExamId = data.HistoryExamId,
                CreatedAt = data.CreatedAt
            };
            return new BaseResponse(format);
        }
    }

}
