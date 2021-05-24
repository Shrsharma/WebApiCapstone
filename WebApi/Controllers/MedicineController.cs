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
    [EnableCors("SiteCorsPolicy")]

    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicineController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Medicine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineModel>>> GetMedicineModel()
        {
            return await _context.MedicineModel.ToListAsync();
        }

        // GET: api/Medicine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicineModel>> GetMedicineModel(string id)
        {
            var medicineModel = await _context.MedicineModel.FindAsync(id);

            if (medicineModel == null)
            {
                return NotFound();
            }

            return medicineModel;
        }

        // PUT: api/Medicine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicineModel(string id, MedicineModel medicineModel)
        {
            if (id != medicineModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicineModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineModelExists(id))
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

        // POST: api/Medicine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MedicineModel>> PostMedicineModel(MedicineModel medicineModel)
        {
            _context.MedicineModel.Add(medicineModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MedicineModelExists(medicineModel.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMedicineModel", new { id = medicineModel.Id }, medicineModel);
        }

        // DELETE: api/Medicine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicineModel(string id)
        {
            var medicineModel = await _context.MedicineModel.FindAsync(id);
            if (medicineModel == null)
            {
                return NotFound();
            }

            _context.MedicineModel.Remove(medicineModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicineModelExists(string id)
        {
            return _context.MedicineModel.Any(e => e.Id == id);
        }
    }
}
