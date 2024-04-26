using System.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace APBDcw3.Animals;

public interface IAnimalRepository
{
    public IEnumerable<Animal> FetchAllAnimals(string orderBy);
    public bool CreateAnimal(string JSONnewAnimal);
    public bool DeleteAnimal(int idAnimal);
    public bool UpdateAnimal(int idAnimal, string JSONnewAnimal);
}
public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;
    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<Animal> FetchAllAnimals(string orderBy)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        var safeOrderBy = new string[] { "Description", "Category","Area" }.Contains(orderBy) ? orderBy : "Name";
        var command = new SqlCommand($"SELECT * FROM Animal ORDER BY {safeOrderBy} ASC", connection);
        using var reader = command.ExecuteReader();

        var animals = new List<Animal>();
        while (reader.Read())
        {
            var animal = new Animal()
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()!,
                Description = reader["Description"].ToString()!,
                Category = reader["Category"].ToString()!,
                Area = reader["Area"].ToString()!
            };
            animals.Add(animal);
        }

        return animals;
    }
    public bool CreateAnimal(string JSONnewAnimal)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        Animal newAnimal = JsonSerializer.Deserialize<Animal>(JSONnewAnimal, options);
        
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        var com = new SqlCommand("INSERT INTO Animal (Name, Description, CATEGORY, AREA) VALUES (@name, @description, @category, @area)", connection);
        com.Parameters.AddWithValue("name", newAnimal.Name);
        com.Parameters.AddWithValue("description", newAnimal.Description);
        com.Parameters.AddWithValue("category", newAnimal.Category);
        com.Parameters.AddWithValue("area", newAnimal.Area);
        var result = com.ExecuteNonQuery();
        return result == 1;
    }
    public bool UpdateAnimal(int idAnimal,string JSONnewAnimal)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        Animal animal = JsonSerializer.Deserialize<Animal>(JSONnewAnimal, options);
        
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        var com = new SqlCommand("UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE IdAnimal = @idAnimal", connection);
        com.Parameters.AddWithValue("name", animal.Name);
        com.Parameters.AddWithValue("description", animal.Description);
        com.Parameters.AddWithValue("category", animal.Category);
        com.Parameters.AddWithValue("area", animal.Area);
        com.Parameters.AddWithValue("IdAnimal", animal.IdAnimal);       
        var result = com.ExecuteNonQuery();
        return result == 1;
    }
    public bool DeleteAnimal(int idAnimal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        var com = new SqlCommand("DELETE FROM Animal WHERE idAnimal = @idAnimal", connection);
        com.Parameters.AddWithValue("idAnimal", idAnimal);
        var result = com.ExecuteNonQuery();
        return result == 1;
            
    }
}