using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ProductMeta
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Product), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int ProductId { get; set; }

        public string Key { get; set; }

        // ReSharper disable once MemberCanBePrivate.Global
        public string Value { get; set; }

        public void Set(object value)
        {
            Value = value.ToJsv();
        }

        public T Get<T>()
        {
            return Value.FromJsv<T>();
        }
    }
}