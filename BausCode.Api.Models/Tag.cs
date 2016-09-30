using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models
{
    public class Tag: IInsertFilter, IUpdateFilter
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Index(false)]
        public string Name { get; set; }

        [Index(true)]
        public string Lowercase { get; set; }

        public void OnBeforeInsert()
        {
            OnUpsert();
        }

        public void OnBeforeUpdate()
        {
            OnUpsert();
        }

        private void OnUpsert()
        {
            Lowercase = Name.ToLowerSafe();
        }
    }
}