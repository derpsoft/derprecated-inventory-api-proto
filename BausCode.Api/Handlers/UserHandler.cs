﻿using System.Data;
using BausCode.Api.Models;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.OrmLite;

namespace BausCode.Api.Handlers
{
    public class UserHandler
    {
        public UserHandler(IDbConnection db, IUserAuthRepository userAuthRepository, UserSession user)
        {
            Db = db;
            User = user;
            UserAuthRepository = userAuthRepository;
        }

        private IUserAuthRepository UserAuthRepository { get; }
        private IDbConnection Db { get; }
        private UserSession User { get; }

        private UserAuth GetUser(int id)
        {
            id.ThrowIfLessThan(1);
            return UserAuthRepository.GetUserAuth(id) as UserAuth;
        }

        public UserAuth Update<T>(int id, IUpdatableField<T> update)
        {
            update.ThrowIfNull();
            var user = GetUser(id);
            user.SetProperty(update.FieldName, update.Value);
            Db.UpdateOnly(user, new[] {update.FieldName}, p => p.Id == user.Id);
            return user;
        }
    }
}