using System.Collections.Generic;
using System.Threading.Tasks;

namespace Substitution.Business.Interfaces
{
    public interface ITypedHttpClient
    {
        Task<IEnumerable<User>> GetUsers();
    }
}
