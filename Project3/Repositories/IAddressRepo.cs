using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Project3.Entity.Dto;
using Project3.Entity.Request;
using Project3.Entity.Response;
using Project3.Migrations;
using Project3.Models;
using System.Data;
using System.Reflection.Metadata;
using System.Text;

namespace Project3.Repositories
{
    public interface IAddressRepo
    {
        List<Address> getAddressList();
        void addOrUpdateAddress(Address address);
        void deleteAddress(Address address);
        Address getOne(long id);
    }

    public class AddressRepo : IAddressRepo
    {
        Project3Context _dbContext;
        public AddressRepo(Project3Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void addOrUpdateAddress(Address address)
        {
            if (address.Id == null)
            {
                _dbContext.Addresses.Add(address);
            }
            else
            {
                _dbContext.Addresses.Update(address);
            }
            _dbContext.SaveChanges();
        }

        public void deleteAddress(Address address)
        {
            _dbContext.Addresses.Update(address);
            _dbContext.SaveChanges();
        }

        public List<Address> getAddressList()
        {
            return _dbContext.Addresses.Where(r => r.IsDelete == 0).ToList();
        }

        public Address getOne(long id)
        {
            return _dbContext.Addresses.Where(r => r.Id == id).First();
        }
    }
}
