using System.Collections.Generic;
using System.Threading.Tasks;

namespace Substitution.Business.Interfaces
{
    public interface IGenericHttpClient
    {
        Task<IEnumerable<User>> GetUsers();
    }
}
