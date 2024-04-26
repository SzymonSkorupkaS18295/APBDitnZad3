namespace APBDcw3.Animals;
public interface IAnimalService
{
    public IEnumerable<Animal> GetAllAnimals(string orderBy);
    public bool AddAnimal(AnimalDTO dto);
    bool UpdateAnimal(int idAnimal, string jsoNnewAnimal);
    bool CreateAnimal(string serialize);
    bool DeleteAnimal(int idAnimal);
}

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        return _animalRepository.FetchAllAnimals(orderBy);
    }
    public bool AddAnimal(AnimalDTO dto)
    {
        throw new NotImplementedException();
    }

    public bool UpdateAnimal(int idAnimal,string JSONnewAnimal)
    {
        return _animalRepository.UpdateAnimal(idAnimal, JSONnewAnimal);
    }

    public bool CreateAnimal(string JSONnewAnimal)
    {
        return _animalRepository.CreateAnimal(JSONnewAnimal);
    }

    public bool DeleteAnimal(int idAnimal)
    {
        return _animalRepository.DeleteAnimal(idAnimal);
    }
}