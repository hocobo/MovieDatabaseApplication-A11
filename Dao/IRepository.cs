using MovieDatabaseApplication_A11.Models;

namespace MovieDatabaseApplication_A11.Dao
{
    public interface IRepository
    {
        IEnumerable<Movie> GetAll();
        IEnumerable<Genre> GetAllGenres();
        IEnumerable<User> GetAllUsers();
        IEnumerable<Movie> Search(string searchString);
    }
}
