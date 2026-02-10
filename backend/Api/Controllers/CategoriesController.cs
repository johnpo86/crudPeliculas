using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;
using Api.Dtos;
using Api.Filters;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKey]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            return await _context.Categories
                .Select(c => new CategoryDto { Id = c.Id, Nombre = c.Nombre, Descripcion = c.Descripcion })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return new CategoryDto { Id = category.Id, Nombre = category.Nombre, Descripcion = category.Descripcion };
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> PostCategory(CategoryDto categoryDto)
        {
            var category = new Category { Nombre = categoryDto.Nombre, Descripcion = categoryDto.Descripcion };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            
            categoryDto.Id = category.Id;
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.Id) return BadRequest();
            
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            category.Nombre = categoryDto.Nombre;
            category.Descripcion = categoryDto.Descripcion;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
