using System;

namespace Annuaire.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string PhotoUrl { get; set; } = "";
    public Department Department { get; set; } = new();
    public Location Location { get; set; } = new();
}
