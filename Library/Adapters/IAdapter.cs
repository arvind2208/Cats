using Models;

namespace Library.Adapters
{
    public interface IAdapter
    {
        void Fill(string filePath, PetsContext petsContext);
    }
}
