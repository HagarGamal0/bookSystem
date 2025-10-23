using System.ComponentModel.DataAnnotations;

namespace bookSystem.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }
        public string CategoryName { get; set; }
    }
}
