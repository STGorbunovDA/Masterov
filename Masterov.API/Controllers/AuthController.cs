using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Masterov.API.Models.Auth;
using Masterov.API.Models.User;
using Masterov.Domain.Masterov.UserFolder.LoginUser;
using Masterov.Domain.Masterov.UserFolder.LoginUser.Query;
using Masterov.Domain.Masterov.UserFolder.RegisterUser;
using Masterov.Domain.Masterov.UserFolder.RegisterUser.Command;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Masterov.API.Controllers;

[Route("api/auth")]
public class AuthController(IConfiguration configuration, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="request">Данные для регистрации пользователя (логин и пароль).</param>
    /// <param name="userUseCase">Сценарий для выполнения регистрации пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Ответ с данными пользователя, если регистрация прошла успешно.</returns>
    [HttpPost("register")]
    [ProducesResponseType(201, Type = typeof(UserRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(409)]
    public async Task<IActionResult> RegisterUserAsync(
        [FromBody] AuthUserRequest request,
        [FromServices] IRegisterUserUseCase userUseCase,
        CancellationToken cancellationToken)
    {
        var user = await userUseCase.Execute(new RegisterUserCommand(request.Login, request.Password),
            cancellationToken);
        return Ok(mapper.Map<UserRequest>(user));
    }

    /// <summary>
    /// Получить токен для авторизации пользователя.
    /// </summary>
    /// <param name="request">Данные для аутентификации (логин и пароль).</param>
    /// <param name="userUseCase">Сценарий для выполнения аутентификации и получения токена.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Токен авторизации, если аутентификация прошла успешно.</returns>
    /// <exception cref="UnauthorizedAccessException">Если аутентификация не удалась.</exception>
    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(string))]
    [ProducesResponseType(401)]
    public async Task<IActionResult> LoginUserAsync(
        [FromBody] AuthUserRequest request,
        [FromServices] ILoginUserUseCase userUseCase,
        CancellationToken cancellationToken)
    {
        var user = await userUseCase.Execute(new GetLoginUserQuery(request.Login, request.Password), cancellationToken);
    
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Role, user.Role)
        };
    
        var secretKey = configuration["JwtSettings:SecretKey"];
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey ?? throw new Exception("SecretKey is missing")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:IssuerKey"],
            audience: configuration["JwtSettings:AudienceKey"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: creds
        );
    
        return Ok( new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}