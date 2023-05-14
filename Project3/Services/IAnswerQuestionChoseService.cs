using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;

namespace Project3.Services
{
    public interface IAnswerQuestionChoseService
    {
        BaseResponse getOne(long id);
        BaseResponse create(AddAnswerQuestionChoseReq answerQuestionChoseReq);

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

        public BaseResponse create(AddAnswerQuestionChoseReq answerQuestionChoseReq)
        {
            throw new NotImplementedException();
        }

        public BaseResponse getOne(long id)
        {
            throw new NotImplementedException();
        }
    }

}
