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
    }

    public class HistoryExamService : IHistoryExamService
    {
        IHistoryExamRepo _historyExamRepo;
        HistoryExam history;

        public HistoryExamService(IHistoryExamRepo historyExamRepo)
        {
            _historyExamRepo = historyExamRepo;
        }

        public BaseResponse create(AddHistoryExamReq historyExamReq)
        {
            this.history = new HistoryExam();
            this.history.IsDelete = Constants.IsDelete.False;
            this.history.CreatedAt = DateTime.Now;
            this.history.InfostudentId = historyExamReq.studentId;
            this.history.StartTime = historyExamReq.StartTime;
            this.history.EndTime = historyExamReq.EndTime;

            _historyExamRepo.add(this.history);
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
                InfostudentId = data.InfostudentId,
                StartTime = data.StartTime,
                EndTime = data.EndTime,
                CreatedAt = data.CreatedAt
            };
            return new BaseResponse(format);
        }

        public BaseResponse listHistoryExam(long userId)
        {
            return new BaseResponse(_historyExamRepo.GetHistoryExamList(userId));
        }
    }
}
