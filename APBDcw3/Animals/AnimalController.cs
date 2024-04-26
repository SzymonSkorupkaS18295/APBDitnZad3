using Microsoft.AspNetCore.Mvc;
namespace APBDcw3.Animals;


[ApiController]
[Route("/api/animals")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalService _animalService;
    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllAnimals([FromQuery] string orderBy)
    {
        var animals = _animalService.GetAllAnimals(orderBy);
        return Ok(animals);
    }
}