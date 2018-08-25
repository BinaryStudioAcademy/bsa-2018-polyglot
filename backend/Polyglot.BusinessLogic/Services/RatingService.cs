using AutoMapper;
using Polyglot.BusinessLogic.Interfaces;
using Polyglot.Common.DTOs;
using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.SqlRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Polyglot.BusinessLogic.Services
{
    class RatingService : CRUDService<Rating, RatingDTO>, IRatingService
    {
        IUserService _userService;
        public RatingService(IUnitOfWork uow, IMapper mapper, IUserService userService)
            : base(uow, mapper)
        {
            _userService = userService;
        }

        public override async Task<RatingDTO> PostAsync(RatingDTO entity)
        {
            if (uow != null)
            {
                var target = await uow.GetRepository<Rating>()
                    .CreateAsync(mapper.Map<Rating>(entity));
                if (target != null)
                {
                    await uow.SaveAsync();
                    var rate =  mapper.Map<RatingDTO>(target);
                    rate.CreatedBy = await _userService.GetOneAsync(rate.CreatedById);
                    return rate;
                }
            }
            return null;
        }
    }
}
