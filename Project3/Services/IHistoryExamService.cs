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
        BaseResponse deleteHistoryExam(long id);
        BaseResponse createOrUpdate(AddHistoryExamReq historyExamReq);
    }

    public class HistoryExamService : IHistoryExamService
    {
        IHistoryExamRepo _historyExamRepo;
        HistoryExam history;

        public HistoryExamService(IHistoryExamRepo historyExamRepo)
        {
            _historyExamRepo = historyExamRepo;
        }

        public BaseResponse createOrUpdate(AddHistoryExamReq historyExamReq)
        {
            if(historyExamReq.Id == null)
            {
                this.history = new HistoryExam();
                this.history.IsDelete = Constants.IsDelete.False;
                this.history.CreatedAt = DateTime.Now;
            }
            else
            {
                this.history = _historyExamRepo.getOne((long)historyExamReq.Id);
                if(this.history == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }
            }
            convertFromDtoToModel(historyExamReq);
            _historyExamRepo.addOrUpdateHistotyExams(this.history);
            return new BaseResponse();
        }

        private void convertFromDtoToModel(AddHistoryExamReq historyExamReq)
        {
            history.ExamId = historyExamReq.ExamId;
            history.InformationStudentsId = historyExamReq.InformationStudentsId;
            history.StartTime = historyExamReq.StartTime;
            history.EndTime = historyExamReq.EndTime;
        }

        public BaseResponse deleteHistoryExam(long id)
        {
            var data = _historyExamRepo.getOne(id);
            if(data == null)
            {
                throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            }
            data.IsDelete = 1;
            _historyExamRepo.deleteHistoryExam(data);

            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        {
            var data = _historyExamRepo.getOne(id);
            if(data == null)
            {
                return new BaseResponse();
            }
            var format = new VHistoryExamOne
            {
                Id = data.Id,
                ExamId = data.ExamId,
                InformationStudentsId = data.InformationStudentsId,
                StartTime = data.StartTime,
                EndTime = data.EndTime,
                CreatedAt = data.CreatedAt
            };
            return new BaseResponse(format);
        }
    }
}
