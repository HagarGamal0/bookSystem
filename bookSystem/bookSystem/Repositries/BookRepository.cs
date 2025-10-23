using bookSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace bookSystem.Repositries
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(LibraryContext context) : base(context) { }


        public async Task<List<Book>> GetAllWithCategoryAsync()
        {
            return await _table
                .Include(b => b.Category)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdWithCategoryAsync(int id)
        {
            return await _table
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
         
    }
}

