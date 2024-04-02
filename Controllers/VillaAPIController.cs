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
[Route("api/villa")]
public class VillaAPiController : ControllerBase
{
    private readonly IVillaRepository _dbVilla;
    private readonly IMapper _mapper;
    protected APIResponse _response;

    public VillaAPiController(IVillaRepository dbVilla, IMapper mapper)
    {
        _dbVilla = dbVilla;
        _mapper = mapper;
        _response = new APIResponse();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetVillas()
    {
        IEnumerable<Villa> villas = await _dbVilla.GetAllAsync();
        _response.Result = _mapper.Map<IEnumerable<VillaCreateDto>>(villas);
        _response.isSuccess = true;
        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }

    [HttpGet("{id}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> GetVilla(int id)
    {
        if (id == 0) return NotFound();

        var villa = await _dbVilla.GetAsync(v => v.Id == id);

        _response.Result = _mapper.Map<VillaCreateDto>(villa);
        _response.isSuccess = true;
        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDto? villaCreateDto)
    {
        // Check for existing villa with the same name
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (await _dbVilla.GetAsync(v => villaCreateDto != null && v.Name == villaCreateDto.Name) != null)
        {
            ModelState.AddModelError("Name", "Villa name already exists.");
            return BadRequest(ModelState);
        }

        // Check if the received DTO is null
        if (villaCreateDto == null) return BadRequest("Received villa data is null.");

        // Map DTO to the entity model
        var villa = _mapper.Map<Villa>(villaCreateDto);

        await _dbVilla.CreateAsync(villa);

        _response.Result = _mapper.Map<VillaCreateDto>(villa);
        _response.isSuccess = true;
        _response.StatusCode = HttpStatusCode.OK;


        return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
    }

    [HttpPut("{id}", Name = "PutVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> UpadateVilla(int id, [FromBody] VillaUpdateDto? villaUpdateDto)
    {
        if (villaUpdateDto == null || id != villaUpdateDto.Id)
            return BadRequest(villaUpdateDto);

        var model = _mapper.Map<Villa>(villaUpdateDto);

        await _dbVilla.UpdateAsync(model);


        _response.isSuccess = true;
        _response.StatusCode = HttpStatusCode.NoContent;
        return Ok(_response);
    }

    [HttpPatch("{id}", Name = "PatchVilla")]
    public async Task<ActionResult<APIResponse>> PatchVilla(int id, [FromBody] JsonPatchDocument<VillaUpdateDto> patch)
    {
        var existingVilla = await _dbVilla.GetAsync(v => v.Id == id);

        var villaCreateDto = _mapper.Map<VillaUpdateDto>(existingVilla);

        patch.ApplyTo(villaCreateDto, ModelState);


        var model = _mapper.Map<Villa>(villaCreateDto);


        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _dbVilla.UpdateAsync(model);
        _response.isSuccess = true;
        _response.StatusCode = HttpStatusCode.NoContent;
        return Ok(_response);
    }
}