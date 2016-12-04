namespace BausCode.Api.Models
{
    public interface IUpdatableField<T>
    {
        string FieldName { get; }
        int Id { get; set; }
        T Value { get; set; }
    }
}
