using AutoMapper;
using MovieLibraryEntities.Models;
using MovieDatabaseApplication_A11.Dto;
using System.Collections.Generic;

namespace MovieDatabaseApplication_A11.Mappers
{
    public class MovieMapper : IMovieMapper
    {
        private readonly IMapper _mapper;

        public MovieMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<MovieDto> Map(IEnumerable<Movie> movies)
        {
            IEnumerable<MovieDto> dto = _mapper.Map<IEnumerable<Movie>, IEnumerable<MovieDto>>(movies);
            return dto;
        }

        public MovieDto Map(Movie movie)
        {
            MovieDto dto = _mapper.Map<Movie, MovieDto>(movie);
            return dto;
        }
    }
}
