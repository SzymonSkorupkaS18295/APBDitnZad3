namespace APBDcw3.Animals;
public interface IAnimalService
{
    public IEnumerable<Animal> GetAllAnimals(string orderBy);
    public bool AddAnimal(AnimalDTO dto);
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
}