using Polyglot.DataAccess.Entities;
using Polyglot.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.QueryTypes
{
    public class UserRights : QueryType
    {
        public int UserId { get; set; }
        public RightDefinition RightDefinition { get; set; }
        public int ProjectId { get; set; }
    }
}
