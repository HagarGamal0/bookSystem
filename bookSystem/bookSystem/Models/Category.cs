namespace bookSystem.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
