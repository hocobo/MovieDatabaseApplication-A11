using MovieDatabaseApplication_A11.Models;
using MovieDatabaseApplication_A11.Dto;
using System.Collections.Generic;

namespace MovieDatabaseApplication_A11.Mappers
{
    public interface IMovieMapper
    {
        IEnumerable<MovieDto> Map(IEnumerable<Movie> movies);
    }
}