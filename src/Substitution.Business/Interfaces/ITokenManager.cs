using System.Threading.Tasks;

namespace Substitution.Business.Interfaces
{
    public interface ITokenManager
    {
        Task<string> GetAccessToken();
    }
}
