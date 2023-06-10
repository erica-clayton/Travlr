using System;
namespace Travlr.Models;

public class Trip
	{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string TripName { get; set; } = "";
    public bool PastTrip { get; set; }
    public string Description { get; set; } = "";
    public int Budget { get; set; }
    public List<Dine> DineOptions { get; set; } = new();
    public List<Stay> StayOptions { get; set; } = new();
    public List<Activity> ActvitityOptions { get; set; } = new();

}


