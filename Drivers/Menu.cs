using MovieDatabaseApplication_A11.Context;
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
            Add,
            Update,
            Delete,
            Search,
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

        public void GetUserInput()
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

        /*public void SchoolGetUserInput()
        {
            var name = AnsiConsole.Ask<string>("What is your [green]name[/]?");
            var semester = AnsiConsole.Prompt(
                new TextPrompt<string>("For which [green]semester[/] are you registering?")
                    .InvalidChoiceMessage("[red]That's not a valid semester[/]")
                    .DefaultValue("Spring 2022")
                    .AddChoice("Fall 2022")
                    .AddChoice("Spring 2023"));
            var classes = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("For which [green]classes[/] are you registering?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more classes)[/]")
                    .InstructionsText(
                        "[grey](Press [blue]<space>[/] to toggle a class, " +
                        "[green]<enter>[/] to accept)[/]")
                    .AddChoices("History", "English", "Spanish", "Math", "Computer", "Literature", "Science",
                        "Chemistry", "Economics"));
        }*/

    }
}