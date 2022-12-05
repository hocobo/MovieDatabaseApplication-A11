using ConsoleTables;
using MovieDatabaseApplication_A11.Context;
using MovieDatabaseApplication_A11.Dao;
using MovieDatabaseApplication_A11.Dto;
using MovieDatabaseApplication_A11.Mappers;
using MovieDatabaseApplication_A11.Models;
using Spectre.Console;
using System;


namespace MovieDatabaseApplication_A11.Drivers
{
    // This is just a fun implementation of Spectre.Console to present more interesting menus
    // Feel free to use it and read more at https://spectreconsole.net if you would like
    // If not, just use your own regular Console.Writeline menus as we have in the past
    public class Menu
    {
        public enum MenuOptions
        {
            ListFromDb,
            ListFromFile,
            AddMovie,
            Update,
            Delete,
            Search,
            AddUser,
            DisplayUsers,
            AddNewUserRating,
            Exit
        }
        public Menu() // default constructor
        {
        }     
        public MenuOptions ChooseAction()
        {
            var menuOptions = Enum.GetNames(typeof(MenuOptions));

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose your [green]menu action[/]?")
                    .AddChoices(menuOptions));

            return (MenuOptions)Enum.Parse(typeof(MenuOptions), choice);
        }
        public void Exit()
        {
            AnsiConsole.Write(
                new FigletText("Thanks!")
                    .LeftAligned()
                    .Color(Color.Green));
        }
        public string GetUserResponse(string question, string highlightedText, string highlightedColor)
        {
            return AnsiConsole.Ask<string>($"{question} [{highlightedColor}]{highlightedText}[/]");
        }
        public void AddNewMovie(IRepository repository, IGenreMapper genreMapper)
        {
            using (var db = new MovieContext())
            {
                Console.WriteLine("Enter the movie title ");
                var movieTitle = Console.ReadLine();
                if (string.IsNullOrEmpty(movieTitle))
                    Console.WriteLine("\nInvalid Input");
                else
                {
                    var movie = new Movie();
                    movie.Title = movieTitle;

                    Console.WriteLine("Enter the release date ");
                    var date = Console.ReadLine();
                    if (string.IsNullOrEmpty(date))
                        Console.WriteLine("\nInvalid Input");
                    else
                    {
                        try
                        {
                            var releaseDate = DateTime.Parse(date);
                            movie.ReleaseDate = releaseDate;
                        }

                        catch (FormatException)
                        {
                            Console.WriteLine("\nInvalid Input");
                        }
                    }
                    /*var allGenres = repository.GetAllGenres();
                    var genres = genreMapper.Map(allGenres);
                    ConsoleTable.From<GenreDto>(genres).Write();
                    Console.WriteLine("Select a genre.");                   
                    var userSelectedGenre = Convert.ToInt64(Console.ReadLine());                    
                    var movieGenre = new MovieGenre();
                    var genre = allGenres.Where(x => x.Id == userSelectedGenre).FirstOrDefault();
                    movieGenre.Genre = genre;
                    movieGenre.Movie = movie;
                    db.MovieGenres.Add(movieGenre);   */               
                    db.Movies.Add(movie);
                    db.SaveChanges();

                }
            }

        }
        public void UpdateMovie()
        {
            using (var db = new MovieContext())
            {
                try
                {
                    Console.WriteLine("Type entire movie title to update ");
                    var movieUpdate = Console.ReadLine();
                    if (string.IsNullOrEmpty(movieUpdate))
                        Console.WriteLine("\nInvalid Input");
                    else
                    {
                        var movie = db.Movies.Where(x => x.Title.ToLower() == movieUpdate.ToLower())
                                            .FirstOrDefault();
                        Console.WriteLine("Enter new title ");
                        var title = Console.ReadLine();
                        Console.WriteLine("Enter new release date ");
                        var releaseDate = DateTime.Parse(Console.ReadLine());
                        movie.Title = title;
                        movie.ReleaseDate = releaseDate.ToUniversalTime();                        
                        db.SaveChanges();
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid Date");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("\nNot a valid movie. Make sure to input entire movie title. ");
                }

            }
        }
        public void DeleteMovie()
        {
            using (var db = new MovieContext())
            {
                try
                {
                    Console.WriteLine("Type entire movie title to delete ");
                    var movieDelete = Console.ReadLine();
                    if (string.IsNullOrEmpty(movieDelete))
                        Console.WriteLine("\nInvalid Input");
                    else
                    {
                        var movie = db.Movies.Where(x => x.Title.ToLower() == movieDelete.ToLower())
                                            .FirstOrDefault();
                        db.Movies.Remove(movie);
                        db.SaveChanges();
                    }

                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("\nNot a valid movie. Make sure to input entire movie title. ");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("\nNot a valid movie. Make sure to input entire movie title. ");
                }

            }

        }
        public void AddNewUser()
        {
            using(var db = new MovieContext())
            {
                long age;
                Console.WriteLine("Enter age");
                long.TryParse(Console.ReadLine(), out age);
                Console.WriteLine("Enter gender");
                var gender = Console.ReadLine();
                Console.WriteLine("Enter zip code");
                var zipCode = Console.ReadLine();
                Console.WriteLine("Enter occupation");
                var occupation = Console.ReadLine();
                Occupation userOccupation;
                if(db.Occupations.Any(x => x.Name == occupation))
                {

                    userOccupation = db.Occupations.Where(x => x.Name == occupation).FirstOrDefault();
                }
                else
                {
                    userOccupation = new Occupation();
                    userOccupation.Name = occupation;

                }

                var newUser = new User();
                newUser.Age = age;
                newUser.Gender = gender;
                newUser.ZipCode = zipCode;
                newUser.Occupation = userOccupation;
                db.Users.Add(newUser);
                db.SaveChanges();
                Console.WriteLine($"\nNew user successfully added\n" +
                    $"UserID: {newUser.Id}\n" +
                    $"Age: {newUser.Age}\nGender: {newUser.Gender}\n" +
                    $"ZipCode: {newUser.ZipCode} \nOccupation: {userOccupation.Name}");
            }
        }
        public void AddNewRating()
        {
            
            using (var db = new MovieContext())
            {
                Console.WriteLine("Enter user ID ");
                var userId = Convert.ToInt64(Console.ReadLine());
                var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                Console.WriteLine("Enter the name of the movie");
                var movie = Console.ReadLine();
                var uMovie = db.Movies.Where(x => x.Title == movie).FirstOrDefault();
                Console.WriteLine("Enter a rating");
                var userRating = Convert.ToInt64(Console.ReadLine());

                var userMovie = new UserMovie();
                userMovie.User = user;
                userMovie.Movie = uMovie;
                userMovie.Rating = userRating;
                userMovie.RatedAt = DateTime.Now;
                db.UserMovies.Add(userMovie);
                db.SaveChanges();

                Console.WriteLine($"UserID: {user.Id}\n" +
                    $"UserAge: {user.Age}\n" +
                    $"UserGender: {user.Gender}\n" +
                    $"UserZipCode: {user.ZipCode}\n" +
                    $"MovieId: {uMovie.Id}\n" +
                    $"MovieTitle: {uMovie.Title}\n" +
                    $"MovieReleaseDate: {uMovie.ReleaseDate}\n" +
                    $"MovieRating: {userRating}");
                

            }
        }
    }
}