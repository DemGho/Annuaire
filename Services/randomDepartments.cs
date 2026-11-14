using System.Net.Http;
using System.Net.Http.Json;
using Annuaire.Models;

namespace Annuaire.Services;

public class RandomUserService
{
    // Un seul HttpClient réutilisé (et non un par clic)
    private static readonly HttpClient Http = new()
    {
        BaseAddress = new Uri("https://randomuser.me/")
    };

    private static readonly string[] DepartmentNames =
        { "Informatique", "Ressources humaines", "Comptabilité", "Marketing", "Commercial" };

    public async Task<List<Employee>> GetEmployeesAsync(int count = 20)
    {
        var url = $"api/?results={count}&nat=fr&inc=name,email,phone,picture,location";
        var response = await Http.GetFromJsonAsync<ApiResponse>(url)
                       ?? throw new InvalidOperationException("Réponse vide de l'API.");

        var random = new Random();
        var departments = DepartmentNames
            .Select((name, i) => new Department { Id = i + 1, Name = name })
            .ToList();

        return response.Results.Select((u, i) => new Employee
        {
            Id = i + 1,
            FirstName = u.Name.First,
            LastName = u.Name.Last,
            Email = u.Email,
            Phone = u.Phone,
            PhotoUrl = u.Picture.Medium,
            Department = departments[random.Next(departments.Count)],
            Location = new Location { Id = i + 1, City = u.Location.City, Country = u.Location.Country }
        }).ToList();
    }

    private record ApiResponse(List<ApiUser> Results);
    private record ApiUser(ApiName Name, ApiLocation Location, string Email, string Phone, ApiPicture Picture);
    private record ApiName(string First, string Last);
    private record ApiLocation(string City, string Country);
    private record ApiPicture(string Medium);
}