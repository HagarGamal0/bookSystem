using bookSystem.Models;

namespace bookSystem.Repositries
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(LibraryContext context) : base(context) { }
    }
}
