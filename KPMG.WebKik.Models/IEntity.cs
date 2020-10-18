namespace KPMG.WebKik.Models
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
