using HealthCare.API.Models;
using HealthCare.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HealthCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IRepository<Doctor> _repository;
        private readonly IGetRepository<Doctor> _getRepository;
        private readonly ApplicationDBContext _dbContext;

        public DoctorController(IRepository<Doctor> repository, IGetRepository<Doctor> getRepository, ApplicationDBContext dbContext)
        {
            _repository = repository;
            _getRepository = getRepository;
            _dbContext = dbContext;
        }
        [HttpGet("GetAllDoctors")]
        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _getRepository.GetAll();
        }

        //[Authorize(Roles ="Admin,Doctor")]

        [HttpGet("GetDoctorById/{id}", Name = "GetDoctorById")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _getRepository.GetById(id);
            if (doctor != null)
            {
                return Ok(doctor);
            }
            return NotFound("Doctor not found");
        }
        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost("CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest();
            }
           
            await _repository.Create(doctor);

            return CreatedAtRoute("GetDoctorById", new { id = doctor.ID }, doctor);

        }
        [Authorize(Roles = "Admin,Doctor")]

        [HttpPut("UpdateDoctor/{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _repository.Update(id, doctor);
            if (result != null)
            {
                return NoContent();
            }
            return NotFound("Doctor not found");
        }

        //[Authorize(Roles = "Admin")]

        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var result = await _repository.Delete(id);
            if (result != null)
            {
                return Ok();
            }
            return NotFound("Doctor not found");
        }
    }
}