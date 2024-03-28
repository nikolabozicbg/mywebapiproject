using Microsoft.AspNetCore.Mvc;
using MyWebApiProject.Data;
using MyWebApiProject.Models.dtos;

namespace MyWebApiProject.Controllers;

[ApiController]
[Route("api/villa")]
public class VillaAPIController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        return Ok(VillaStore.villaList);
    }

    [HttpGet("{id}")]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
            return NotFound();
        return Ok(VillaStore.villaList.FirstOrDefault(v => v.Id == id));
    }
}