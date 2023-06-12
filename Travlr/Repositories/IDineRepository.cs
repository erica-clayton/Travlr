using Travlr.Models;

namespace Travlr.Repositories
{
    public interface IDineRepository
    {
        void Add(Dine category);
        void Delete(int id);
        List<Dine> GetAll();
        Dine GetById(int id);
        void Update(Dine dine);
    }
}