namespace BausCode.Api.Models
{
    using ServiceStack;
    using ServiceStack.DataAnnotations;

    public class Tag : IInsertFilter, IUpdateFilter
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Index(true)]
        public string Lowercase { get; set; }

        [Index(false)]
        public string Name { get; set; }

        private void OnUpsert()
        {
            Lowercase = Name.ToLowerSafe();
        }

        public void OnBeforeInsert()
        {
            OnUpsert();
        }

        public void OnBeforeUpdate()
        {
            OnUpsert();
        }
    }
}
