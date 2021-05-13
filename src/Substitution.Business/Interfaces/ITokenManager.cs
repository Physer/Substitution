using System.Threading.Tasks;

namespace Substitution.Business.Interfaces
{
    public interface ITokenManager
    {
        Task<string> GenerateAccessToken(string id, string secret, string signingSecret);
        Task<string> GetAccessToken();
    }
}
