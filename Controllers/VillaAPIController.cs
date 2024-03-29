using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyWebApiProject.Data;
using MyWebApiProject.Logging;
using MyWebApiProject.Models.dtos;

namespace MyWebApiProject.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/villa")]
public class VillaAPiController : ControllerBase
{
    private readonly ILogging _logger;

    public VillaAPiController(ILogging logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        _logger.Log("Getting all villas", "info");
        return Ok(VillaStore.villaList);
    }

    [HttpGet("{id}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
        {
            _logger.Log("illa id is required.", "error");
            return NotFound();
        }

        return Ok(VillaStore.villaList.FirstOrDefault(v => v.Id == id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villa)
    {
        if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villa.Name) != null)
        {
            ModelState.AddModelError("Name", "Villa name already exists.");
            return BadRequest(ModelState);
        }


        if (villa == null)
            return BadRequest(villa);

        villa.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

        VillaStore.villaList.Add(villa);

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

        var existingVilla = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

        if (existingVilla == null)
            return NotFound();

        existingVilla.Name = villa.Name;
        existingVilla.SQft = villa.SQft;
        existingVilla.Occupancy = villa.Occupancy;

        return NoContent();
    }

    [HttpPatch("{id}", Name = "PatchVilla")]
    public IActionResult PatchVilla(int id, [FromBody] JsonPatchDocument<VillaDTO> patch)
    {
        if (patch == null || id == 0)
            return BadRequest();

        var existingVilla = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

        if (existingVilla == null)
            return NotFound();

        patch.ApplyTo(existingVilla, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return NoContent();
    }
}