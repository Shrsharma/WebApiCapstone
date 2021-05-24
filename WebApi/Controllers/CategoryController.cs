using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using ModelLayer;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoryModel()
        {
            return await _context.CategoryModel.ToListAsync();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategoryModel(string id)
        {
            var categoryModel = await _context.CategoryModel.FindAsync(id);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return categoryModel;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryModel(string id, CategoryModel categoryModel)
        {
            if (id != categoryModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoryModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryModel>> PostCategoryModel(CategoryModel categoryModel)
        {
            _context.CategoryModel.Add(categoryModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CategoryModelExists(categoryModel.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCategoryModel", new { id = categoryModel.Id }, categoryModel);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryModel(string id)
        {
            var categoryModel = await _context.CategoryModel.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            _context.CategoryModel.Remove(categoryModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryModelExists(string id)
        {
            return _context.CategoryModel.Any(e => e.Id == id);
        }
    }
}
