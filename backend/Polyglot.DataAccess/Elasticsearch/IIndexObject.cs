using System;

namespace Polyglot.DataAccess.Elasticsearch
{
    public interface IIndexObject
    {
        /// <summary>
        /// The mandatory Key, normally Guid
        /// </summary>
        string Id { get; set; }

        ///// <summary>
        ///// Version number - used in confilcts
        ///// </summary>
        //int VersionNumber { get; set; }

        /// <summary>
        /// Updated at time
        /// </summary>
        DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Created at time
        /// </summary>
        DateTime CreatedAt { get; set; }

        ///// <summary>
        ///// The HTTP-action used when communicating on the HTTP-level.
        ///// You should not typically use this property directly - it is used by the underlying infra.
        ///// </summary>
        //string SearchActionStr { get; set; }

    }
}