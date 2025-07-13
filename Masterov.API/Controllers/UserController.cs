using AutoMapper;
using Masterov.API.Models.Auth;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUser;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUser.Command;
using Masterov.Domain.Masterov.UserFolder.DeleteUserById;
using Masterov.Domain.Masterov.UserFolder.DeleteUserById.Command;
using Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin;
using Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin.Command;
using Masterov.Domain.Masterov.UserFolder.GetUserById;
using Masterov.Domain.Masterov.UserFolder.GetUserById.Query;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin.Query;
using Masterov.Domain.Masterov.UserFolder.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

[Route("api/user")]
public class UserController(IMapper mapper) : ControllerBase
{
    //TODO сделать изменение пароля
    //TODO добавить метод изменение заказчика
    
    /// <summary>
    /// Получить пользователя по логину.
    /// </summary>
    /// <param name="login">Логин пользователя.</param>
    /// <param name="getUserByLoginUseCase">Сценарий для получения пользователя по логину.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Данные пользователя.</returns>
    [HttpGet("getUserByLogin/{login}")]
    [ProducesResponseType(200, Type = typeof(UserRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> GetUserByLoginAsync(
        string login,
        [FromServices] IGetUserByLoginUseCase getUserByLoginUseCase,
        CancellationToken cancellationToken)
    {
        var user = await getUserByLoginUseCase.Execute(new GetUserByLoginQuery(login), cancellationToken);
        return Ok(mapper.Map<UserRequest>(user));
    }

    /// <summary>
    /// Получить список всех пользователей.
    /// </summary>
    /// <param name="useCase"></param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список пользователей.</returns>
    [HttpGet(Name = nameof(GetUsersAsync))]
    [ProducesResponseType(200, Type = typeof(UserRequest[]))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> GetUsersAsync(
        [FromServices] IGetUsersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var users = await useCase.Execute(cancellationToken);
        return Ok(users.Select(mapper.Map<UserRequest>));
    }
    
    /// <summary>
    /// Получить пользователя по Id.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="getUserByIdUseCase">Сервис для получения пользователя по ID.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Пользователь, если найден, или ошибка, если не найден.</returns>
    [HttpGet("getUserById/{userId:guid}")]
    [ProducesResponseType(200, Type = typeof(UserRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> GetUserByIdAsync(
        Guid userId,
        [FromServices] IGetUserByIdUseCase getUserByIdUseCase,
        CancellationToken cancellationToken)
    {
        var user = await getUserByIdUseCase.Execute(new GetUserByIdQuery(userId), cancellationToken);
        return Ok(mapper.Map<UserRequest>(user));
    }
    
    /// <summary>
    /// Изменить роль для пользователя.
    /// </summary>
    /// <param name="request">Запрос с данными для изменения роли пользователя.</param>
    /// <param name="changeRoleUserUseCase">Сервис для изменения роли пользователя.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Обновлённый пользователь или ошибка в случае неудачи.</returns>
    [HttpPatch("changeRoleUser")]
    [ProducesResponseType(200, Type = typeof(UserRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> ChangeRoleUserAsync(
        [FromBody] ChangeRoleUserRequest request,
        [FromServices] IChangeRoleUserUseCase changeRoleUserUseCase,
        CancellationToken cancellationToken)
    {
        // Преобразуем строку роли в Enum UserRole
        if (!Enum.TryParse<UserRole>(request.Role, true, out var userRole))
        {
            return BadRequest("Неверная роль пользователя.");
        }

        // Создаем команду с преобразованной ролью
        var user = await changeRoleUserUseCase.Execute(
            new ChangeRoleUserCommand(request.Name, userRole), 
            cancellationToken);

        return Ok(mapper.Map<UserRequest>(user));
    }
    
    /// <summary>
    /// Удалить пользователя по Id.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя для удаления.</param>
    /// <param name="byIdUseCase">Сервис для удаления пользователя по Id.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Статус операции удаления пользователя.</returns>
    [HttpDelete("deleteUserById/{userId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> DeleteUserByIdAsync(
        Guid userId,
        [FromServices] IDeleteUserByIdUseCase byIdUseCase,
        CancellationToken cancellationToken)
    {
        var result = await byIdUseCase.Execute(new DeleteUserByIdCommand(userId), cancellationToken);
        return Ok(result);
    }
   
    /// <summary>
    /// Удалить пользователя по логину.
    /// </summary>
    /// <param name="login">Логин пользователя для удаления.</param>
    /// <param name="byLoginUseCase">Сервис для удаления пользователя по логину.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Статус операции удаления пользователя.</returns>
    [HttpDelete("deleteUserByLogin/{login}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> DeleteUserByLoginAsync(
        string login,
        [FromServices] IDeleteUserByLoginUseCase byLoginUseCase,
        CancellationToken cancellationToken)
    {
        var result = await byLoginUseCase.Execute(new DeleteUserByLoginCommand(login), cancellationToken);
        return Ok(result);
    }
}