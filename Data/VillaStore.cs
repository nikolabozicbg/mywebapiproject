using MyWebApiProject.Models.dtos;

namespace MyWebApiProject.Data;

public class VillaStore
{
    public static List<VillaDTO> villaList = new()
    {
        new VillaDTO { Id = 1, Name = "BeachVilla", SQft = 100, Occupancy = 4 },
        new VillaDTO { Id = 2, Name = "PollVilla", SQft = 101, Occupancy = 5 }
    };
}