using System.Threading.Tasks;

namespace API.Data
{
    public class FolderRepository
    {
        private readonly DataContext context;
        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}