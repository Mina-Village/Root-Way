namespace Root_Way.Models;

public class FileModel
{
    public string Name { get; set; }
    public FileType FileType { get; set; }
}

public enum FileType
{
    Cloud,
    Local,
    Both
}