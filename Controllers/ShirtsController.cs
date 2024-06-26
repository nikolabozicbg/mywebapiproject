using Microsoft.AspNetCore.Mvc;
using MyWebApiProject.Models;

namespace MyWebApiProject.Controllers;

[ApiController]
[Route("api/shirts")]
public class ShirtsController : ControllerBase
{
    [HttpGet]
    public string GetShirts()
    {
        return "Reading all the shirts";
    }

    [HttpGet("{id}")]
    public string GetShirtById(int id)
    {
        return $"Reading shirt: {id}";
    }

    [HttpPost]
    public string CreateShirt([FromBody] Shirt shirt)
    {
        return "Creating a shirt";
    }

    [HttpPut("{id}")]
    public string UpdateShirt(int id)
    {
        return $"Updating shirt: {id}";
    }

    [HttpDelete("{id}")]
    public string DeleteShirt(int id)
    {
        return $"Deleting  shirt: {id}";
    }
}