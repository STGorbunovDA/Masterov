using AutoMapper;
using Masterov.API.Extensions;
using Masterov.API.Models.User;
using Masterov.Domain.Masterov.UserFolder.ChangeAccountLoginDateUserById;
using Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser;
using Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser.Command;
using Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser;
using Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser.Command;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById.Command;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin.Command;
using Masterov.Domain.Masterov.UserFolder.ChangeUpdatedAtUserById.Command;
using Masterov.Domain.Masterov.UserFolder.DeleteUserById;
using Masterov.Domain.Masterov.UserFolder.DeleteUserById.Command;
using Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin;
using Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin.Command;
using Masterov.Domain.Masterov.UserFolder.GetUserById;
using Masterov.Domain.Masterov.UserFolder.GetUserById.Query;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin;
using Masterov.Domain.Masterov.UserFolder.GetUserByLogin.Query;
using Masterov.Domain.Masterov.UserFolder.GetUsers;
using Masterov.Domain.Masterov.UserFolder.GetUsersByAccountLoginDate;
using Masterov.Domain.Masterov.UserFolder.GetUsersByAccountLoginDate.Query;
using Masterov.Domain.Masterov.UserFolder.GetUsersByCreatedAt;
using Masterov.Domain.Masterov.UserFolder.GetUsersByCreatedAt.Query;
using Masterov.Domain.Masterov.UserFolder.GetUsersByRole;
using Masterov.Domain.Masterov.UserFolder.GetUsersByRole.Query;
using Masterov.Domain.Masterov.UserFolder.GetUsersByUpdatedAt;
using Masterov.Domain.Masterov.UserFolder.GetUsersByUpdatedAt.Query;
using Masterov.Domain.Masterov.UserFolder.UpdateUser;
using Masterov.Domain.Masterov.UserFolder.UpdateUser.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masterov.API.Controllers;

[Route("api/users")]
public class UserController(IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить список всех пользователей
    /// </summary>
    /// <param name="useCase"></param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Список пользователей</returns>
    [HttpGet("getUsers")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UserResponse>))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> GetUsers(
        [FromServices] IGetUsersUseCase useCase,
        CancellationToken cancellationToken)
    {
        var users = await useCase.Execute(cancellationToken);
        return Ok(users.Select(mapper.Map<UserResponse>));
    }
    
    /// <summary>
    /// Получить пользователя по логину
    /// </summary>
    /// <param name="request">Логин пользователя</param>
    /// <param name="useCase">Сценарий получения пользователя по логину</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Данные пользователя</returns>
    [HttpGet("getUserByLogin")]
    [ProducesResponseType(200, Type = typeof(UserResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> GetUserByLogin(
        [FromQuery] GetUserByLoginRequest request,
        [FromServices] IGetUserByLoginUseCase useCase,
        CancellationToken cancellationToken)
    {
        var user = await useCase.Execute(new GetUserByLoginQuery(request.Login), cancellationToken);
        return Ok(mapper.Map<UserResponse>(user));
    }

    /// <summary>
    /// Получить пользователя по Id
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="useCase">Сценарий получения пользователя по Id</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Данные пользователя</returns>
    [HttpGet("getUserById/{userId:guid}")]
    [ProducesResponseType(200, Type = typeof(UserResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> GetUserById(
        Guid userId,
        [FromServices] IGetUserByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var user = await useCase.Execute(new GetUserByIdQuery(userId), cancellationToken);
        return Ok(mapper.Map<UserResponse>(user));
    }
    
    /// <summary>
    /// Получить пользователей по роли
    /// </summary>
    /// <param name="request">Роль пользователей</param>
    /// <param name="useCase">Сценарий получения пользователей по роли</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Данные пользователей</returns>
    [HttpGet("getUsersByRole")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UserResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> GetUsersByRole(
        [FromQuery] GetUsersByRoleRequest request,
        [FromServices] IGetUsersByRoleUseCase useCase,
        CancellationToken cancellationToken)
    {
        var users = await useCase.Execute(new GetUsersByRoleQuery(EnumTypeHelper.FromExtensionRoleMethod(request.Role)), cancellationToken);
        return Ok(users?.Select(mapper.Map<UserResponse>) ?? Array.Empty<UserResponse>());
    }
    
    /// <summary>
    /// Получить пользователей по дате регистрации
    /// </summary>
    /// <param name="request">Дата регистрации пользователей</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о пользователях</returns>
    [HttpGet("getUsersByCreatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UserResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> GetUsersByCreatedAt(
        [FromQuery] GetUsersByCreatedAtRequest request,
        [FromServices] IGetUsersByCreatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var users = await useCase.Execute(new GetUsersByCreatedAtQuery(request.CreatedAt.ToDateTime()), cancellationToken);
        return Ok(users?.Select(mapper.Map<UserResponse>) ?? Array.Empty<UserResponse>());
    }
    
    /// <summary>
    /// Получить пользователей по дате входа
    /// </summary>
    /// <param name="request">Дата входа пользователей</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о пользователях</returns>
    [HttpGet("getUsersByAccountLoginDate")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UserResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> GetUsersByAccountLoginDate(
        [FromQuery] GetUsersByAccountLoginDateRequest request,
        [FromServices] IGetUsersByAccountLoginDateUseCase useCase,
        CancellationToken cancellationToken)
    {
        var users = await useCase.Execute(new GetUsersByAccountLoginDateQuery(request.AccountLoginDate.ToDateTime()), cancellationToken);
        return Ok(users?.Select(mapper.Map<UserResponse>) ?? Array.Empty<UserResponse>());
    }
    
    /// <summary>
    /// Получить пользователей по дате обновления
    /// </summary>
    /// <param name="request">Дата обновления для пользователей</param>
    /// <param name="useCase">Сценарий использования</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о пользователях</returns>
    [HttpGet("getUsersByUpdatedAt")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UserResponse>))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> GetUsersByUpdatedAt(
        [FromQuery] GetUsersByUpdatedAtRequest request,
        [FromServices] IGetUsersByUpdatedAtUseCase useCase,
        CancellationToken cancellationToken)
    {
        var users = await useCase.Execute(new GetUsersByUpdatedAtQuery(request.UpdatedAt.ToDateTime()), cancellationToken);
        return Ok(users?.Select(mapper.Map<UserResponse>) ?? Array.Empty<UserResponse>());
    }
    
    /// <summary>
    /// Обновить пользователя по Id
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Данные для обновления пользователя</param>
    /// <param name="useCase">Сценарий обновления пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обновления пользователя</returns>
    [HttpPatch("updateUser/{userId:guid}")]
    [ProducesResponseType(200, Type = typeof(UserResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> UpdateUser(
        [FromRoute] Guid userId,
        [FromBody] UpdateUserRequest request,
        [FromServices] IUpdateUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        var updateUser = await useCase.Execute(
            new UpdateUserCommand(
                userId, 
                request.Login, 
                EnumTypeHelper.FromExtensionRoleMethod(request.Role),
                request.NewPassword, 
                request.OldPassword, 
                request.CreatedAt?.ToDateTime(),
                request.CustomerId),
            cancellationToken);
        return Ok(mapper.Map<UserResponse>(updateUser));
    }
    
    /// <summary>
    /// Изменить дату входа пользователя по Id.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Новая дата входа для пользователя</param>
    /// <param name="changeAccountLoginDateUserByIdUseCase">Сценарий обновления даты входа для пользователя</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Результат обновления пользователя</returns>
    [HttpPatch("changeAccountLoginDateUserById")]
    [ProducesResponseType(200, Type = typeof(UserResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> ChangeAccountLoginDateUserById(
        [FromRoute] Guid userId,
        [FromQuery] ChangeAccountLoginDateUserByIdRequest request,
        [FromServices] IChangeAccountLoginDateUserByIdUseCase changeAccountLoginDateUserByIdUseCase,
        CancellationToken cancellationToken)
    {
        var user = await changeAccountLoginDateUserByIdUseCase.Execute(
            new ChangeAccountLoginDateUserByIdCommand(userId, request.AccountLoginDate.ToDateTime()), cancellationToken);

        return Ok(mapper.Map<UserResponse>(user));
    }

    /// <summary>
    /// Изменить роль для пользователя по логину
    /// </summary>
    /// <param name="request">Запрос с данными для изменения роли пользователя</param>
    /// <param name="useCase">Сценарий для изменения роли пользователя</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Результат обновления пользователя</returns>
    [HttpPatch("changeRoleUserByLogin")]
    [ProducesResponseType(200, Type = typeof(UserResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> ChangeRoleUserByLogin(
        [FromQuery] ChangeRoleUserByLoginRequest request,
        [FromServices] IChangeRoleUserByLoginUseCase useCase,
        CancellationToken cancellationToken)
    {
        var user = await useCase.Execute(
            new ChangeRoleUserByLoginCommand(request.Name, 
                EnumTypeHelper.FromExtensionRoleMethod(request.Role)),
            cancellationToken);

        return Ok(mapper.Map<UserResponse>(user));
    }
    
    /// <summary>
    /// Изменить роль для пользователя по id
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Запрос с данными для изменения роли пользователя</param>
    /// <param name="useCase">Сценарий для изменения роли пользователя</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Результат обновления пользователя</returns>
    [HttpPatch("changeRoleUserById/{userId:guid}")]
    [ProducesResponseType(200, Type = typeof(UserResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> ChangeRoleUserById(
        [FromRoute] Guid userId,
        [FromQuery] ChangeRoleUserByIdRequest request,
        [FromServices] IChangeRoleUserByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var user = await useCase.Execute(
            new ChangeRoleUserByIdCommand(userId, 
                EnumTypeHelper.FromExtensionRoleMethod(request.Role)),
            cancellationToken);

        return Ok(mapper.Map<UserResponse>(user));
    }

    /// <summary>
    /// Изменить у пользователя ссыль на заказчика.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Запрос с данными для изменения заказчика для пользователя</param>
    /// <param name="useCase">Сценарий изменения заказчика для пользователя</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Результат обновления пользователя</returns>
    [HttpPatch("changeCustomerFromUser")]
    [ProducesResponseType(200, Type = typeof(UserResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> ChangeCustomerFromUser(
        [FromRoute] Guid userId,
        [FromQuery] ChangeCustomerFromUserRequest request,
        [FromServices] IChangeCustomerFromUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        var user = await useCase.Execute(
            new ChangeCustomerFromUserCommand(userId, request.CustomerId),
            cancellationToken);

        return Ok(mapper.Map<UserResponse>(user));
    }

    /// <summary>
    /// Изменить пароль у пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="request">Запрос с данными для изменения пароля у пользователя</param>
    /// <param name="useCase">Сервис для изменения пароля у пользователя</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Результат обновления пользователя</returns>
    [HttpPatch("changePasswordFromUser")]
    [ProducesResponseType(200, Type = typeof(UserResponse))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> ChangePasswordFromUser(
        [FromRoute] Guid userId,
        [FromBody] ChangePasswordFromUserRequest request,
        [FromServices] IChangePasswordFromUserUseCase useCase,
        CancellationToken cancellationToken)
    {
        var success = await useCase.Execute(
            new ChangePasswordFromUserCommand(userId, request.OldPassword, request.NewPassword),
            cancellationToken);

        return success ? Ok() : NotFound("Password not change");
    }

    /// <summary>
    /// Удалить пользователя по Id
    /// </summary>
    /// <param name="userId">Идентификатор пользователя для удаления</param>
    /// <param name="useCase">Сервис для удаления пользователя по Id</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Статус операции удаления пользователя</returns>
    [HttpDelete("deleteUserById/{userId:guid}")]
    [ProducesResponseType(204, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> DeleteUserById(
        [FromRoute] Guid userId,
        [FromServices] IDeleteUserByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(new DeleteUserByIdCommand(userId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удалить пользователя по логину.
    /// </summary>
    /// <param name="login">Логин пользователя для удаления.</param>
    /// <param name="useCase">Сервис для удаления пользователя по логину.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Статус операции удаления пользователя.</returns>
    [HttpDelete("deleteUserByLogin/{login}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(ProblemDetails))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> DeleteUserByLoginAsync(
        string login,
        [FromServices] IDeleteUserByLoginUseCase useCase,
        CancellationToken cancellationToken)
    {
        var result = await useCase.Execute(new DeleteUserByLoginCommand(login), cancellationToken);
        return Ok(result);
    }
}