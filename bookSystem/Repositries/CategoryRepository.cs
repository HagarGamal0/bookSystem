using bookSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace bookSystem.Repositries
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(LibraryContext context) : base(context) { }




        public async Task<List<Category>> GetCategoryWithBooksAsync(int catid)
        {
            return await _table
                .Include(c => c.Books)
                .Where(c => c.Id == catid)
                .ToListAsync();
        }
    }
}
