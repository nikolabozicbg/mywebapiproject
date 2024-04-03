using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyWebApiProject.Models;
using MyWebApiProject.Models.dtos;
using MyWebApiProject.Repository.IRepository;

namespace MyWebApiProject.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/VillaNumberAPI")]
public class VillaNumberAPiController : ControllerBase
{
    private readonly IVillaNumberRepository _dbNumberVilla;
    private readonly IMapper _mapper;
    protected APIResponse _response;

    public VillaNumberAPiController(IVillaNumberRepository dbNumberVilla, IMapper mapper)
    {
        _dbNumberVilla = dbNumberVilla;
        _mapper = mapper;
        _response = new APIResponse();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetVillaNumbers()
    {
        IEnumerable<VillaNumber> villaNumber = await _dbNumberVilla.GetAllAsync();
        _response.Result = _mapper.Map<IEnumerable<VillaNumberCreateDTO>>(villaNumber);
        _response.isSuccess = true;
        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }

    [HttpGet("{id}", Name = "GetVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
    {
        if (id == 0) return NotFound();

        var villa = await _dbNumberVilla.GetAsync(v => v.VillaNo == id);

        _response.Result = _mapper.Map<VillaNumberCreateDTO>(villa);
        _response.isSuccess = true;
        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> CreateVillaNumber(
        [FromBody] VillaNumberCreateDTO? villaNumberCreateDto)
    {
        // Check for existing villa with the same name
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (await _dbNumberVilla.GetAsync(
                v => villaNumberCreateDto != null && v.VillaNo == villaNumberCreateDto.VillaNo) != null)
        {
            ModelState.AddModelError("Name", "Villa number already exists.");
            return BadRequest(ModelState);
        }

        // Check if the received DTO is null
        if (villaNumberCreateDto == null) return BadRequest("Received villa data is null.");

        // Map DTO to the entity model
        var villaNumber = _mapper.Map<VillaNumber>(villaNumberCreateDto);

        await _dbNumberVilla.CreateAsync(villaNumber);

        _response.Result = _mapper.Map<VillaNumberCreateDTO>(villaNumber);
        _response.isSuccess = true;
        _response.StatusCode = HttpStatusCode.OK;


        return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, _response);
    }

    [HttpPut("{id}", Name = "PutVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> UpadateVilla(int id,
        [FromBody] VillaNumberUpdateDTO? villaNumberUpdateDto)
    {
        if (villaNumberUpdateDto == null || id != villaNumberUpdateDto.VillaNo)
            return BadRequest(villaNumberUpdateDto);

        var model = _mapper.Map<VillaNumber>(villaNumberUpdateDto);

        await _dbNumberVilla.UpdateAsync(model);


        _response.isSuccess = true;
        _response.StatusCode = HttpStatusCode.NoContent;
        return Ok(_response);
    }

    [HttpPatch("{id}", Name = "PatchVillaNumber")]
    public async Task<ActionResult<APIResponse>> PatchVilla(int id, [FromBody] JsonPatchDocument<VillaUpdateDto> patch)
    {
        var existingVilla = await _dbNumberVilla.GetAsync(v => v.VillaNo == id);

        var villaCreateDto = _mapper.Map<VillaUpdateDto>(existingVilla);

        patch.ApplyTo(villaCreateDto, ModelState);


        var model = _mapper.Map<VillaNumber>(villaCreateDto);


        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _dbNumberVilla.UpdateAsync(model);
        _response.isSuccess = true;
        _response.StatusCode = HttpStatusCode.NoContent;
        return Ok(_response);
    }
}