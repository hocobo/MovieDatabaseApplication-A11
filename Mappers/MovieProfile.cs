using AutoMapper;
using MovieLibraryEntities.Models;
using MovieDatabaseApplication_A11.Dto;

namespace MovieDatabaseApplication_A11.Mappers
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>();
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
        }
    }
}
