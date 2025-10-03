using AutoMapper;
using Masterov.API.Models.Auth;
using Masterov.API.Models.User;
using Masterov.Domain.Masterov.JwtService;
using Masterov.Domain.Masterov.UserFolder.LoginUser;
using Masterov.Domain.Masterov.UserFolder.LoginUser.Query;
using Masterov.Domain.Masterov.UserFolder.RegisterUser;
using Masterov.Domain.Masterov.UserFolder.RegisterUser.Command;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

/// <summary>
/// Регистрация и получение токена
/// </summary>
/// <param name="mapper"></param>
/// <param name="jwtService"></param>
[Route("api/auth")]
public class AuthController(IMapper mapper, IJwtService jwtService) : ControllerBase
{
    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(201, Type = typeof(UserRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(409)]
    public async Task<IActionResult> RegisterUser(
        [FromBody] AuthRequest request,
        [FromServices] IRegisterUserUseCase userUseCase,
        CancellationToken cancellationToken)
    {
        var user = await userUseCase.Execute(new RegisterUserCommand(request.Email, request.Password, request.Phone),
            cancellationToken);
        return Ok(mapper.Map<UserRequest>(user));
    }

    /// <summary>
    /// Получить токен для авторизации пользователя.
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(LoginResponse))]
    [ProducesResponseType(401)]
    public async Task<IActionResult> LoginUser(
        [FromBody] LoginRequest request,
        [FromServices] ILoginUserUseCase userUseCase,
        CancellationToken cancellationToken)
    {
        var user = await userUseCase.Execute(new GetLoginUserQuery(request.Login, request.Password), cancellationToken);
    
        var token = jwtService.GenerateToken(user);
        
        var response = new LoginResponse
        {
            Auth = new AuthResponse
            {
                Token = "Bearer " + token,
                Expires = DateTime.Now.AddHours(24)
            },
            User = mapper.Map<UserRequest>(user)
        };

        return Ok(response);
    }
}