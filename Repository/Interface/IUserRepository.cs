using ECinemaDomain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECinema.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<ECinemaUser> GetAll();
        ECinemaUser Get(string id);
        void Insert(ECinemaUser entity);
        void Update(ECinemaUser entity);
        void Delete(ECinemaUser entity);
    }
}
