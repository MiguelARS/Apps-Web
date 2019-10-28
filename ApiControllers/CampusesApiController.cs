using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.ApiControllers
{
    [Produces("application/json")]
    [Route("api/Campuses")]
    [ApiController]    
    public class CampusesApiController : ControllerBase
    {
        private readonly SchoolContext _context;

        public CampusesApiController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Campuses
        /// <summary>
        /// Get all Campuses
        /// </summary>
        /// <returns>All Campuses</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Campus>>> GetCampuses()
        {
            return await _context.Campuses.ToListAsync();
        }

        // GET: api/CampusesApi/5
        /// <summary>
        /// Get a specific Campus.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One campus</returns>
        /// <response code="404">The id not found in Campuses</response>
        /// <response code="200">OK - One campus</response>
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Campus>> GetCampus(int id)
        {
            var campus = await _context.Campuses.FindAsync(id);

            if (campus == null)
            {
                return NotFound();
            }

            return campus;
        }

        // PUT: api/Campuses/5
        /// <summary>
        /// Update a specific Campus.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/Campuses
        ///     {
        ///         "campusID": 0,
        ///         "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="campus"></param>        
        /// <response code="400">the id is different to campus.CampusID</response>   
        /// <response code="404">The campus not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutCampus(int id, Campus campus)
        {
            if (id != campus.CampusID)
            {
                return BadRequest();
            }

            _context.Entry(campus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampusExists(id))
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

        // POST: api/Campuses
        /// <summary>
        /// Creates a Campus.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Campuses
        ///     {
        ///        "name": "string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="campus"></param>
        /// <returns>A newly created Campus</returns>
        /// <response code="201">Returns the newly created campus</response>
        /// <response code="400">If the campus is null or invalid</response>            
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Campus>> PostCampus([FromBody] Campus campus)
        {
            _context.Campuses.Add(campus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCampus", new { id = campus.CampusID }, campus);
        }

        // DELETE: api/Campuses/5
        /// <summary>
        /// Deletes a specific Campus.
        /// </summary>
        /// <param name="id"></param>   
        [HttpDelete("{id}")]
        public async Task<ActionResult<Campus>> DeleteCampus(int id)
        {
            var campus = await _context.Campuses.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }

            _context.Campuses.Remove(campus);
            await _context.SaveChangesAsync();

            return campus;
        }

        private bool CampusExists(int id)
        {
            return _context.Campuses.Any(e => e.CampusID == id);
        }
    }
}