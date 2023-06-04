using System;
namespace Travlr.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public string FirebaseId { get; set; }
    public string Email { get; set; }
    public Trip? Trip { get; set; }
  
}
