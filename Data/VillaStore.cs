using MyWebApiProject.Models.dtos;

namespace MyWebApiProject.Data;

public class VillaStore
{
    public static List<VillaDTO> villaList = new()
    {
        new() { Id = 1, Name = "Villa 1" },
        new() { Id = 2, Name = "Villa 2" }
    };
}