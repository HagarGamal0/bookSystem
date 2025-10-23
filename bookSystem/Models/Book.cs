namespace bookSystem.Models
{
    public class Book : BaseModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
