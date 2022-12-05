using AutoMapper;
using MovieDatabaseApplication_A11.Dto;
using MovieDatabaseApplication_A11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabaseApplication_A11.Mappers
{
    internal class GenreMapper : IGenreMapper
    {
        public readonly IMapper _mapper;
        public GenreMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IEnumerable<GenreDto> Map(IEnumerable<Genre> genres)
        {
            IEnumerable<GenreDto> dto = _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(genres);
            return dto;
        }
        public GenreDto Map(Genre genre)
        {
            GenreDto dto = _mapper.Map<Genre, GenreDto>(genre);
            return dto;
        }
    }
}
