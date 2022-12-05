using ConsoleTables;
using Microsoft.Extensions.Logging;
using MovieDatabaseApplication_A11.Dao;
using MovieDatabaseApplication_A11.Drivers;
using MovieDatabaseApplication_A11.Dto;
using MovieDatabaseApplication_A11.Mappers;
using System;

namespace MovieDatabaseApplication_A11.Services
{
    public class MainService : IMainService
    {
        private readonly ILogger<MainService> _logger;
        private readonly IMovieMapper _movieMapper;
        private readonly IGenreMapper _genreMapper;
        private readonly IUserMapper _userMapper;
        private readonly IRepository _repository;
        private readonly IFileService _fileService;

        public MainService(ILogger<MainService> logger, IMovieMapper movieMapper, IGenreMapper genreMapper, IUserMapper userMapper, IRepository repository, IFileService fileService)
        {
            _logger = logger;
            _movieMapper = movieMapper;
            _genreMapper = genreMapper;
            _userMapper = userMapper;
            _repository = repository;
            _fileService = fileService;
        }

        public void Invoke()
        {
            var menu = new Menu();

            Menu.MenuOptions menuChoice;
            do
            {
                menuChoice = menu.ChooseAction();

                switch (menuChoice)
                {
                    case Menu.MenuOptions.ListFromDb:
                        _logger.LogInformation("Listing movies from database");
                        var allMovies = _repository.GetAll();
                        var movies = _movieMapper.Map(allMovies);
                        ConsoleTable.From<MovieDto>(movies).Write();
                        break;

                    case Menu.MenuOptions.ListFromFile:
                        _fileService.Read();
                        _fileService.Display();
                        break;

                    case Menu.MenuOptions.AddMovie:
                        _logger.LogInformation("Adding a new movie");                                        
                        menu.AddNewMovie(_repository, _genreMapper);
                        _logger.LogInformation("Movie added");
                        break;

                    case Menu.MenuOptions.Update:
                        _logger.LogInformation("Updating an existing movie");
                        menu.UpdateMovie();
                        break;

                    case Menu.MenuOptions.Delete:
                        _logger.LogInformation("Deleting a movie");
                        menu.DeleteMovie();
                        break;

                    case Menu.MenuOptions.Search:
                        _logger.LogInformation("Searching for a movie");
                        var userSearchTerm = menu.GetUserResponse("Enter the", "movie title:", "green");
                        var searchedMovies = _repository.Search(userSearchTerm);
                        movies = _movieMapper.Map(searchedMovies);
                        ConsoleTable.From<MovieDto>(movies).Write();
                        break;

                    case Menu.MenuOptions.AddUser:
                        _logger.LogInformation("Adding new user");
                        menu.AddNewUser();
                        break;
                    case Menu.MenuOptions.DisplayUsers:
                        _logger.LogInformation("Displaying users");
                        var allUsers = _repository.GetAllUsers();
                        var users = _userMapper.Map(allUsers);
                        ConsoleTable.From(users).Write();                        
                        break;
                    case Menu.MenuOptions.AddNewUserRating:
                        _logger.LogInformation("Adding new rating");
                        menu.AddNewRating();
                        break;
                }
            }
            while (menuChoice != Menu.MenuOptions.Exit);

            menu.Exit();


            Console.WriteLine("\nThanks for using the Movie Library!");

        }
    }
}