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
        BaseResponse create(RequestAnswerReq answerQuestionChoseReq);        
    }
    public class AnswerQuestionChoseService : IAnswerQuestionChoseService
    {
        IAnswerQuestionChoseRepo _answerQuestionChoseRepo;

        public AnswerQuestionChoseService(IAnswerQuestionChoseRepo answerQuestionChoseRepo)
        {
            _answerQuestionChoseRepo = answerQuestionChoseRepo;
        }

        public BaseResponse create(RequestAnswerReq req)
        {
            _answerQuestionChoseRepo.create(req);
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
                AnswerChoseId =data.AnswerChoseId,
                HistoryExamId = data.HistoryExamId,
                CreatedAt = data.CreatedAt
            };
            return new BaseResponse(format);
        }
    }

}
