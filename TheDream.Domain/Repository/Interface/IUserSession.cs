using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDream.Domain.Repository.Interface
{
    public interface IUserSession
    {
        string BearerToken { get; }
    }
}
