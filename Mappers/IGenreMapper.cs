using MovieDatabaseApplication_A11.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDatabaseApplication_A11.Models;

namespace MovieDatabaseApplication_A11.Mappers
{
    public interface IGenreMapper
    {
        IEnumerable<GenreDto> Map(IEnumerable<Genre> genres);
    }
}
