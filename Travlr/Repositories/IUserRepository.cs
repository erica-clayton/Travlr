using Travlr.Models;

namespace Travlr.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(int id);
        List<User> GetAll();
        User GetByFirebaseId(string uid);
        User GetById(int id);
        void Update(User user);
    }
}