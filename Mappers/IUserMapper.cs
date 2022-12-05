using Microsoft.SqlServer.Server;
using MovieDatabaseApplication_A11.Dto;
using MovieDatabaseApplication_A11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabaseApplication_A11.Mappers
{
    public interface IUserMapper
    {
        IEnumerable<UserDto> Map(IEnumerable<User> users);
    }
}
