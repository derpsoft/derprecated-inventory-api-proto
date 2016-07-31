using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BausCode.Api.Models;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace BausCode.Api.Stores
{
    public class ApiKeyStore : IApiKeyStore
    {
        public ApiKeyStore(IDbConnectionFactory contextFactory)
            : this(contextFactory.Open())
        {
        }

        public ApiKeyStore(IDbConnection context)
        {
            Context = context;
        }

        private IDbConnection Context { get; }
        private bool IsDisposed { get; set; }

        /// <summary>
        ///     Get a user's ApiKeys from the store.
        /// </summary>
        /// <param name="userAuthId"></param>
        /// <returns></returns>
        public List<ApiKey> Get(int userAuthId)
        {
            return Context.Where<ApiKey>(new {UserAuthId = userAuthId});
        }

        public ApiKey Get(int id, int userId)
        {
            return Context.Single<ApiKey>(new {Id = id, UserAuthId = userId});
        }

        /// <summary>
        ///     Create a new ApiKey
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ApiKey Create(ApiKey key)
        {
            Context.Insert(key);
            return key;
        }

        /// <summary>
        ///     Returns true iff the key exists and not IsDeleted.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsValid(Guid key)
        {
            return Context.Exists<ApiKey>(new {Key = key, IsDeleted = false});
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                Dispose(true);
                IsDisposed = true;
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        ///     Get a user's ApiKeys from the store.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<ApiKey> Get(UserAuth user)
        {
            return Get(user.Id);
        }

        /// <summary>
        ///     Get an ApiKey from the store.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ApiKey Get(Guid key)
        {
            return Context.Where<ApiKey>(new {Key = key}).FirstOrDefault();
        }

        /// <summary>
        ///     Returns true if the give key exists in the database.
        ///     Does not check if the key is deleted, for that functionality use <seealso cref="IsValid" />.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(Guid key)
        {
            return Context.Exists<ApiKey>(new {Key = key});
        }

        /// <summary>
        ///     Soft-delete the given API key. This is the equivalent of setting IsDeleted to false and saving.
        /// </summary>
        /// <param name="key"></param>
        public void Invalidate(Guid key)
        {
            Delete(key);
        }

        /// <summary>
        ///     Soft-delete the given API key. This is the equivalent of setting IsDeleted to false and saving.
        /// </summary>
        /// <param name="key"></param>
        public void Delete(Guid key)
        {
            Context.Update<ApiKey>(new {IsDeleted = true}, existing => existing.Key == key);
        }

        private void Dispose(bool managed)
        {
            if (managed)
            {
                if (null != Context)
                {
                    Context.Dispose();
                }
            }
        }
    }
}