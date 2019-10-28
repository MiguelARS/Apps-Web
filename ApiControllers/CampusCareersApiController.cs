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
    [Route("api/CampusesCareers")]
    [ApiController]
    [Produces("application/json")]
    public class CampusCareersApiController : ControllerBase
    {
        private readonly SchoolContext _context;

        public CampusCareersApiController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/CampusCareersApi
       /// <summary>
       /// Api REST que devuelve los campuses
       /// </summary>
       /// <returns>JSON de los campuses</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampusCareer>>> GetcampusCareers()
        {
            return await _context.campusCareers.ToListAsync();
        }

        // GET: api/CampusCareersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CampusCareer>> GetCampusCareer(int id)
        {
            var campusCareer = await _context.campusCareers.FindAsync(id);

            if (campusCareer == null)
            {
                return NotFound();
            }

            return campusCareer;
        }

        // PUT: api/CampusCareersApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampusCareer(int id, CampusCareer campusCareer)
        {
            if (id != campusCareer.CampusID)
            {
                return BadRequest();
            }

            _context.Entry(campusCareer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampusCareerExists(id))
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

        // POST: api/CampusCareersApi
        [HttpPost]
        public async Task<ActionResult<CampusCareer>> PostCampusCareer(CampusCareer campusCareer)
        {
            _context.campusCareers.Add(campusCareer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CampusCareerExists(campusCareer.CampusID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCampusCareer", new { id = campusCareer.CampusID }, campusCareer);
        }

        // DELETE: api/CampusCareersApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CampusCareer>> DeleteCampusCareer(int id)
        {
            var campusCareer = await _context.campusCareers.FindAsync(id);
            if (campusCareer == null)
            {
                return NotFound();
            }

            _context.campusCareers.Remove(campusCareer);
            await _context.SaveChangesAsync();

            return campusCareer;
        }

        private bool CampusCareerExists(int id)
        {
            return _context.campusCareers.Any(e => e.CampusID == id);
        }
    }
}
