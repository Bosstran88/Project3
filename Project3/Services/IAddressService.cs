using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Models;
using Project3.Repositories;
using Project3.Utils;

namespace Project3.Services
{
    public interface IAddressService
    {
        BaseResponse getOne(long id);
        BaseResponse deleteAddress(long id);
        BaseResponse createOrUpdate(AddAddressReq addressReq);
         List<Address> getListAddress();
    }

    public class AddressService : IAddressService
    {
        IAddressRepo _addressRepo;
        Address address;

        public AddressService(IAddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        public BaseResponse createOrUpdate(AddAddressReq addressReq)
        {
            if (addressReq.Id == null)
            {
                this.address = new Address();
                this.address.IsDelete = Constants.IsDelete.False;
                this.address.CreatedAt = DateTime.Now;
            }
            else
            {
                this.address = _addressRepo.getOne((long)addressReq.Id);
                if (this.address == null)
                {
                    throw new DataNotFoundException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
                }
                this.address.UpdatedAt = DateTime.Now;
            }
            convertFromDtoToModel(addressReq);
            _addressRepo.addOrUpdateAddress(this.address);
            return new BaseResponse();
        }

        private void convertFromDtoToModel(AddAddressReq addressi)
        {
            address.Name = addressi.Name;
            address.Addresses = addressi.Addresses;
        }

        public BaseResponse deleteAddress(long id)
        {
            var data = _addressRepo.getOne(id);
            if (data == null) throw new ValidateException(MESSAGE.VALIDATE.OBJECT_NOT_FOUND);
            data.IsDelete = Constants.IsDelete.True;
            data.UpdatedAt = DateTime.Now;
            _addressRepo.deleteAddress(data);
            return new BaseResponse();
        }

        public BaseResponse getOne(long id)
        {
            var data = _addressRepo.getOne(id);
            return new BaseResponse(new VAddressOne
            {
                Id = data.Id,
                Name = data.Name,
                Addresses = data.Addresses,
                CreatedAt = data.CreatedAt,
                UpdatedAt = data.UpdatedAt
            });
        }

         List<Address> IAddressService.getListAddress()
        {
            var data = _addressRepo.getAddressList();
            return data;
        }
    }
}
