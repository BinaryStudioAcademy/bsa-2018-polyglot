using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Implementations
{
    public class UserService : CRUDService, IUserService
    {
        public UserService(IUnitOfWork uow, IMapper mapper)
            :base(uow, mapper)
        {

        }
    }
}
