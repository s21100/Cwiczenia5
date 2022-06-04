using cwiczenia5.Models.DTO;
using cwiczenia5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public TripsController(IDbService dbService) {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips() {

            var trips = await _dbService.GetTrips();
            return Ok(trips);
        }

        [HttpDelete]
        [Route("/api/clients/{idClient:int}")]
        public async Task<IActionResult> RemoveClient(int idClient) {

            var deleted = await _dbService.RemoveClient(idClient);

            if (!deleted) {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        [Route("/api/trips/{idTrip:int}/clients")]
        public async Task<IActionResult> RegisterClientForTrip(SomeSortOfRegistration registration) {
            var registered = await _dbService.RegisterClientForTrip(registration);

            if (!registered) {
                return BadRequest();
            }
            
            return Ok();
        }

    }
}
