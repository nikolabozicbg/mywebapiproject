using Microsoft.AspNetCore.Mvc;
using MyWebApiProject.Data;
using MyWebApiProject.Models.dtos;

namespace MyWebApiProject.Controllers;

[ApiController]
[Route("api/villa")]
public class VillaAPIController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        return Ok(VillaStore.villaList);
    }

    [HttpGet("{id}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
            return NotFound();
        return Ok(VillaStore.villaList.FirstOrDefault(v => v.Id == id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villa)
    {
        if (villa == null)
            return BadRequest(villa);
        villa.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

        VillaStore.villaList.Add(villa);

        return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
    }
}