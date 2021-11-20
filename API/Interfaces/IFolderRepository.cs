using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IFolderRepository
    {
         Task<bool> SaveAllAsync();
    }
}