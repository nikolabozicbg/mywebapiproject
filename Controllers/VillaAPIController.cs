using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyWebApiProject.Data;
using MyWebApiProject.Logging;
using MyWebApiProject.Models;
using MyWebApiProject.Models.dtos;

namespace MyWebApiProject.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/villa")]
public class VillaAPiController : ControllerBase
{
    private readonly AppliationDbContext _db;

    public VillaAPiController(AppliationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
    
        return Ok(_db.Villas.ToList());
    }

    [HttpGet("{id}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
        {
    
            return NotFound();
        }
        
        var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
        
        if(villa == null) return NotFound();

        return Ok(villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
    {
        // Check for existing villa with the same name
        if (_db.Villas.Any(v => v.Name == villaDTO.Name))
        {
            ModelState.AddModelError("Name", "Villa name already exists.");
            return BadRequest(ModelState);
        }

        // Check if the received DTO is null
        if (villaDTO == null)
        {
            return BadRequest("Received villa data is null.");
        }

        // Map DTO to the entity model
        Villa villa = new Villa
        {
            Occupancy = villaDTO.Occupancy,
            Name = villaDTO.Name,
            SQft = villaDTO.SQft,
            Rate = villaDTO.Rate,
            Amenity = villaDTO.Amenity,
            Details = villaDTO.Details,
            ImageUrl = villaDTO.ImageUrl,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
            
        };

        // Add the entity model to the database, not the DTO
        _db.Villas.Add(villa);
        _db.SaveChanges();

     
        return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
    }

    [HttpPut("{id}", Name = "PutVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpadateVilla(int id, [FromBody] VillaDTO villa)
    {
        if (villa == null || id != villa.Id)
            return BadRequest(villa);
  
        Villa model = new()
        {
   
            Occupancy = villa.Occupancy,
            Id = villa.Id,
            Name = villa.Name,
            SQft = villa.SQft,
            Rate = villa.Rate,
            Amenity = villa.Amenity
                
        };
        _db.Update(model);
        _db.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}", Name = "PatchVilla")]
    public IActionResult PatchVilla(int id, [FromBody] JsonPatchDocument<VillaDTO> patch)
    {
     
        var existingVilla = _db.Villas.FirstOrDefault(v => v.Id == id);
        
        VillaDTO villaDto = new()
        {
   
            Occupancy = existingVilla.Occupancy,
            Id = existingVilla.Id,
            Name = existingVilla.Name,
            SQft = existingVilla.SQft,
            Rate = existingVilla.Rate,
            Amenity = existingVilla.Amenity
                
        };

        patch.ApplyTo(villaDto, ModelState);
        
        Villa model = new()
        {
   
            Occupancy = villaDto.Occupancy,
            Id = villaDto.Id,
            Name = villaDto.Name,
            SQft = villaDto.SQft,
            Rate = villaDto.Rate,
            Amenity = villaDto.Amenity
                
        };

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        _db.Update(model);
        _db.SaveChanges();

        return NoContent();
    }
}