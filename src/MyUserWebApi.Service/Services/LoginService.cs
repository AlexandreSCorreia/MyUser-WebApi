using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyUserWebApi.Domain.Dtos;
using MyUserWebApi.Domain.Entities;
using MyUserWebApi.Domain.Interfaces.Services.User;
using MyUserWebApi.Domain.Repository;
using MyUserWebApi.Domain.Security;

namespace MyUserWebApi.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        private IConfiguration _configuration { get; }

        public LoginService(IUserRepository repository,
                            SigningConfigurations signingConfigurations,
                            TokenConfigurations tokenConfigurations,
                            IConfiguration configuration)
        {
            this._repository = repository;
            this._signingConfigurations = signingConfigurations;
            this._tokenConfigurations = tokenConfigurations;
            this._configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            var baseUser = new UserEntity();
            if(user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
               baseUser =  await _repository.FindByLogin(user.Email);
                if(baseUser == null)
                {
                    return new {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                        }
                    );

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                    var handle = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handle);
                    return SuccessObject(createDate, expirationDate, token, user);
                }
            }
            else
            {
                return null;
            }     
        }
    
    
        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate,
        JwtSecurityTokenHandler handle)
        {
            var securityToken = handle.CreateToken(new SecurityTokenDescriptor{
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handle.WriteToken(securityToken);
            return token;
        }
    
        private object SuccessObject(DateTime createDate, DateTime experationDate, string token,
        LoginDto user)
        {
            return new {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = experationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                message = "Usuário Logado com sucesso!"
            };
        }
    
    }
}