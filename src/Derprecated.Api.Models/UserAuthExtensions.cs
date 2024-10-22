﻿namespace Derprecated.Api.Models
{
    using System;
    using System.Reflection;
    using ServiceStack;
    using ServiceStack.Auth;

    public static class UserAuthExtensions
    {
        public static Type UserAuthType = typeof (UserAuth);

        public static UserAuth SetProperty<T>(this UserAuth source, string propertyName, T value)
        {
            propertyName.ThrowIfNullOrEmpty();
            value.ThrowIfNull();

            var prop = UserAuthType.GetProperty(propertyName, typeof (T));
            if (null == prop)
                throw new ArgumentException("No property found with propertyName", propertyName);
            prop.SetValue(source, value);

            return source;
        }
    }
}
