using Entities.DataTransferObjects;
using System.Threading.Tasks;

namespace Contract
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserAuthenticationDto userAuth);
        Task<string> CreateToken();
    }
}
