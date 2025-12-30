using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SimulasiAkhir.DTOs;
using SimulasiAkhir.Models;

namespace SimulasiAkhir.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltiesController(EsemkaOnePlusContext _contex, IConfiguration _config) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> getloyality()
        {
            var data = await _contex.Loyalties
                .OrderBy(g => g.Id) 
                .Select(g => new LoyalitiesDTO { Id = g.Id,
                    Name = g.Name,
                    RequiredPoint = g.RequiredPoint,
                    Multiplier = g.Multiplier,
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpPost]
        public IActionResult postloyality(LoyalitiesDTO Ldto)
        { 
            var id = Ldto.Id;
            var name = Ldto.Name;
            var requiredPoint = Ldto.RequiredPoint;
            var multiplier = Ldto.Multiplier;

            Loyalty newLoyality = new Loyalty()
            {
                Id = id,
                Name = name,
                RequiredPoint = requiredPoint,
                Multiplier = multiplier,
            };

            _contex.Loyalties.Add(newLoyality);
            _contex.SaveChanges();

            return Ok(new
            {
                Message = "Successfuly add loyality",
                Data = new
                {
                    Id = id,
                    Name = name,
                    RequiredPoint = requiredPoint,
                    Multiplier = multiplier,
                }
            });
        }

        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLoyalityId(int id)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            var loyality = await _contex.Loyalties.FirstOrDefaultAsync(m => m.Id == id);
            if (loyality == null)
                return NotFound(new { message = "Movie Not Found" });

            var Loyalityy = await _contex.Loyalties
                .Where(mg => mg.Id == id)
                .Join(_contex.Loyalties, mg => mg.Id, g => g.Id, (mg, g) => g.Name)
                .ToListAsync();

            if (loyality == null)
            {
                NotFound();
            }

            return Ok(new LoyalitiesDTO
            {
                Id = id,
                Name = loyality.Name,
                RequiredPoint = loyality.RequiredPoint,
                Multiplier = loyality.Multiplier
            });
        }
    }
}
