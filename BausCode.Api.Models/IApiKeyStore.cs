using System;
using System.Collections.Generic;

namespace BausCode.Api.Models
{
    public interface IApiKeyStore
    {
        bool IsValid(Guid key);

        /// <summary>
        ///     Get a single ApiKey belonging to a user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        ApiKey Get(int id, int userId);

        /// <summary>
        ///     Get all ApiKeys belonging to a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<ApiKey> Get(int userId);

        ApiKey Create(ApiKey apiKey);
    }
}