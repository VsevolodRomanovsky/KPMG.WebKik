namespace KPMG.WebKik.Models.Directories
{
    public interface IDirectoryEntry : IEntity<int>
    {
        string Code { get; set; }
        string Name { get; set; }
    }
}
