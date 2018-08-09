using MongoDB.Driver;
using Polyglot.DataAccess.NoSQL_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Interfaces
{
    public interface IMongoDataContext
    {
        IMongoCollection<ComplexString> ComplexStrings { get;}
        
    }
}
