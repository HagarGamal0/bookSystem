using bookSystem.Dtos;
using bookSystem.Models;
using bookSystem.Repositries;
using Microsoft.AspNetCore.Mvc;

namespace bookSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepo;
        private readonly CategoryRepository _categoryRepo;

        public BookController(BookRepository bookRepo, CategoryRepository categoryRepo)
        {
            _bookRepo = bookRepo;
            _categoryRepo = categoryRepo;
        }

        //  Get All
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepo.GetAllWithCategoryAsync();


            var dtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                CategoryName = b.Category?.Name
            });

            return View(dtos);
        }

        // ➕ Create (GET)
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepo.GetAllAsync();
            return View();
        }

        // ➕ Create (POST)
        [HttpPost]
        public async Task<IActionResult> Create(BookDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryRepo.GetAllAsync();
                return View(dto);
            }

            var category = await _categoryRepo.GetByIdAsync(
                (await _categoryRepo.GetAllAsync())
                    .FirstOrDefault(c => c.Name == dto.CategoryName)?.Id ?? 0
            );

            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                CategoryId = category.Id
            };

            await _bookRepo.AddAsync(book);
            await _bookRepo.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        // ✏️ Edit (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepo.GetByIdWithCategoryAsync(id);
            if (book == null) return NotFound();

            ViewBag.Categories = await _categoryRepo.GetAllAsync();
            var dto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                CategoryName = book.Category.Name
            };
            return View(dto);
        }

        // ✏️ Edit (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(BookDto dto)
        {
            var book = await _bookRepo.GetByIdAsync(dto.Id);
            if (book == null) return NotFound();

            var category = (await _categoryRepo.GetAllAsync())
                .FirstOrDefault(c => c.Name == dto.CategoryName);

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.CategoryId = category.Id;

            _bookRepo.Update(book);
            await _bookRepo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Test(int age , string name) 
        {

            return Content( name + age);
        
        
        
        }

        // 👁️ Details
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepo.GetByIdWithCategoryAsync(id);


            if (book == null) return NotFound();

            var dto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                CategoryName = book.Category?.Name
            };

            return View(dto);
        }

        // ❌ Delete
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null) return NotFound();

            _bookRepo.Delete(book);
            await _bookRepo.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
    