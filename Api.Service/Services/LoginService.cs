using System.Security.Principal;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService: ILoginService
    {
        private IUserRepository _repository;
        private SigningConfigurations _signingConfigurations;
        private TokenConfiguration _tokenConfiguration;
        private IConfiguration _configuration {get;}
        public LoginService(
            IConfiguration configuration,
            IUserRepository repository,
            SigningConfigurations signingConfigurations,
            TokenConfiguration tokenConfiguration
        )
        {
            
            _signingConfigurations = signingConfigurations;
            _tokenConfiguration = tokenConfiguration;
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            var baseUser = new UserEntity();

            if(user != null && !string.IsNullOrEmpty(user.Email))
            {
                baseUser = await _repository.FindByLogin(user.Email);

                if(baseUser == null){
                    return new {
                        autenticated = false,
                        message = "Falha na autenticação."
                    };
                }
                //Implementação JWT
                else
                {
                    var identity= new ClaimsIdentity(
                        new GenericIdentity(user.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //jti id do token
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                        }
                    );

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds); //60 segundos

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    return SuccessObject(createDate, expirationDate, token, user);
                }
            }
            else
            {
                return new 
                {
                    autenticated = false,
                    message = "Falha ao atenticar"
                };
            }
            
        }



        private  string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDto user)
        {
            return new 
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                userEmail = user.Email,
                message = $"{user.Email} logado com sucesso!"
            };
        }
    }
}