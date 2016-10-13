using System;
using ServiceStack;

namespace BausCode.Api.Models
{
    public static class ProductExtensions
    {
        public static Type ProductType = typeof (Product);

        public static Product SetProperty<T>(this Product source, string propertyName, T value)
        {
            propertyName.ThrowIfNullOrEmpty();
            value.ThrowIfNull();

            var prop = ProductType.GetProperty(propertyName, typeof (T));
            if (null == prop)
                throw new ArgumentException("No property found with propertyName", propertyName);
            prop.SetValue(source, value);

            return source;
        }
    }
}