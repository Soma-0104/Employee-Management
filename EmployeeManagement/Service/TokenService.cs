using EmployeeManagement.Core.IRepository;
using EmployeeManagement.Core.IService;
using EmployeeManagement.Model;

namespace EmployeeManagement.Service
{
    public class TokenService : ITokenService
    {
        public readonly ITokenRepo tokenRepo;
        public TokenService(ITokenRepo _tokenRepo)
        {
            tokenRepo = _tokenRepo;
        }
        public string CreateToken(Employees employee)
        {
            return tokenRepo.CreateToken(employee);
        }
    }
}
