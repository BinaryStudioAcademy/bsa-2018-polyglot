
using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;

namespace Polyglot.BusinessLogic.Services
{
    class TagService : CRUDService<Tag, TagDTO>, ITagService
    {


        public TagService(IUnitOfWork uow, IMapper mapper) : base(uow,mapper)
        {
            
        }
    }
}
