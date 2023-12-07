using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiffingApiTask.Models;
using DiffingApiTask.Classes;

namespace DiffingApiTask.Controllers
{
    [Route("v1/diff")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        private readonly DiffingApiTaskDbContext _context;

        public DiffController(DiffingApiTaskDbContext context)
        {
            _context = context;
        }

        // GET: api/Diff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entry>>> GetDiffEntries()
        {
            return await _context.Entries.ToListAsync();
        }

        // GET: api/Diff/1
        [HttpGet("{id}")]
        public async Task<ActionResult<DiffResult>> GetDiffResultById(int id)
        {
            if (id < 1) return BadRequest();

            var entryFromDb = await _context.Entries.FindAsync(id);

            if (entryFromDb == null) return NotFound();

            DiffResult diffResult = DiffDataComparison.DiffTwoStringsAndGetTheResult(entryFromDb.Left, entryFromDb.Right);

            return Ok(diffResult); // return diffResult;
        }

        // PUT: api/Diff/1/left || api/Diff/1/right
        [HttpPut("{id}/{side}")]
        public async Task<IActionResult> PutDiffEntry(int id, string side, /*[FromBody]*/ Data2Diff data2Diff) // "The JSON value could not be converted to System.String
        {
            if (id < 1 || side != "left" && side != "right" || data2Diff.Data == null) return BadRequest();

            // Check if there is an existing db entry
            var entryFromDb = await _context.Entries.FindAsync(id);
            // Update the db entry with the provided data from the request
            if (entryFromDb != null)
            {
                if (side == "left") entryFromDb.Left = data2Diff.Data;
                else if (side == "right") entryFromDb.Right = data2Diff.Data;
                //_context.Entry(entryFromDb).State = EntityState.Modified;
                //_context.SaveChanges();
                _context.Entries.Attach(entryFromDb);
            }
            // If there is no db entry, create one
            else if (entryFromDb == null)
            {
                _context.Entries.Add(new Entry
                {
                    Id = id,
                    Left = side == "left" ? data2Diff.Data : null,
                    Right = side == "right" ? data2Diff.Data : null
                });
            }

            try {
                await _context.SaveChangesAsync();
                return new CreatedResult("No Location", "No Response Body");
            }
            catch (DbUpdateConcurrencyException) { throw; }
        }
    }
}
