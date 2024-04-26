using System.Text.Json;
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

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateAnimal([FromBody] JsonElement JSONnewAnimal)
    {
        var success = _animalService.CreateAnimal(JsonSerializer.Serialize(JSONnewAnimal));
        if (success)
            return Created("", JSONnewAnimal);
        else
            return BadRequest();
    }
    
    [HttpPut("{idAnimal}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateAnimal(int idAnimal,[FromBody] JsonElement JSONnewAnimal)
    {
        var success = _animalService.UpdateAnimal(idAnimal,JsonSerializer.Serialize(JSONnewAnimal));
        if (success)
            return Ok();
        else
            return BadRequest("Nie można modyfikowac ID rekordu");
    }
    
    [HttpDelete("{idAnimal}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        var success = _animalService.DeleteAnimal(idAnimal);
        if (success)
            return Ok();
        else
            return NotFound();
    }
}
