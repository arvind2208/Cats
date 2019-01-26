namespace Library.FileReaders
{
    public interface IFileSystem
    { 
        string ReadAllText(string path);
    }
}
