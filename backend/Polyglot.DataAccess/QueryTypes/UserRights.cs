using Polyglot.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.QueryTypes
{
    public class UserRights
    {
        public int UserId { get; set; }
        public RightDefinition RightDefinition { get; set; }
        public int ProjectId { get; set; }
    }
}
