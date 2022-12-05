using AutoMapper;
using MovieDatabaseApplication_A11.Models;
using MovieDatabaseApplication_A11.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabaseApplication_A11.Mappers
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDto>();
        }

    }
}
