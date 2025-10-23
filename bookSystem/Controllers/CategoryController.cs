using bookSystem.Dtos;
using bookSystem.Models;
using bookSystem.Repositries;
using Microsoft.AspNetCore.Mvc;

namespace bookSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repo;

        public CategoryController(CategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _repo.GetAllAsync();
            var dtos = categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name }).ToList();
            return View(dtos);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _repo.AddAsync(new Category { Name = dto.Name });
            await _repo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(new CategoryDto { Id = category.Id, Name = category.Name });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDto dto)
        {
            var category = await _repo.GetByIdAsync(dto.Id);
            if (category == null) return NotFound();
            category.Name = dto.Name;
            _repo.Update(category);
            await _repo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return NotFound();
            _repo.Delete(category);
            await _repo.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
