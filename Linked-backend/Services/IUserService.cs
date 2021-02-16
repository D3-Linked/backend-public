using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Linked_backend.Models;

namespace Linked_backend.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string paswoord);
    }
}
