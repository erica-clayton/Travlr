using Travlr.Models;

namespace Travlr.Repositories
{
    public interface IActivityRepository
    {
        void Add(Activity category);
        void Delete(int id);
        List<Activity> GetAll();
        Activity GetById(int id);
        void Update(Activity activity);
    }
}