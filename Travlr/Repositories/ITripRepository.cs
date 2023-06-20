using Travlr.Models;

namespace Travlr.Repositories
{
    public interface ITripRepository
    {
        void Add(Trip category);
        void Delete(int id);
        List<Trip> GetAll();
        Trip GetById(int id);
        List<Trip> GetByUserId(int tripUserId);
        List<Dine> GetDineOptionsByTripId(int tripId);
        void Update(Trip trip);
    }
}