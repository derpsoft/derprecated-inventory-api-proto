namespace BausCode.Api.Models
{
    public interface IUpdatableField<T>
    {
        int Id { get; set; }
        string FieldName { get; }
        T Value { get; set; }
    }
}