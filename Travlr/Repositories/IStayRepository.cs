using Travlr.Models;

namespace Travlr.Repositories
{
    public interface IStayRepository
    {
        void Add(Stay category);
        void Delete(int id);
        List<Stay> GetAll();
        Stay GetById(int id);
        void Update(Stay stay);
    }
}