﻿using AutoMapper;
using Masterov.API.Models.User;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser;
using Masterov.Domain.Masterov.UserFolder.ChangeCustomerFromUser.Command;
using Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser;
using Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser.Command;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById.Command;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin.Command;
using Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByName;
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
    //TODO сделать изменение роли по id

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
    /// Изменить роль для пользователя по логину.
    /// </summary>
    /// <param name="request">Запрос с данными для изменения роли пользователя.</param>
    /// <param name="changeRoleUserByLoginUseCase">Сервис для изменения роли пользователя.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Обновлённый пользователь или ошибка в случае неудачи.</returns>
    [HttpPatch("changeRoleUserByLogin")]
    [ProducesResponseType(200, Type = typeof(UserRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> ChangeRoleUserByLogin(
        [FromBody] ChangeRoleUserByLoginRequest request,
        [FromServices] IChangeRoleUserByLoginUseCase changeRoleUserByLoginUseCase,
        CancellationToken cancellationToken)
    {
        // Преобразуем строку роли в Enum UserRole
        if (!Enum.TryParse<UserRole>(request.Role, true, out var userRole))
        {
            return BadRequest("Неверная роль пользователя.");
        }

        // Создаем команду с преобразованной ролью
        var user = await changeRoleUserByLoginUseCase.Execute(
            new ChangeRoleUserByLoginCommand(request.Name, userRole),
            cancellationToken);

        return Ok(mapper.Map<UserRequest>(user));
    }
    
    /// <summary>
    /// Изменить роль для пользователя по id.
    /// </summary>
    /// <param name="byIdRequest">Запрос с данными для изменения роли пользователя.</param>
    /// <param name="changeRoleUserByIdUseCase">Сервис для изменения роли пользователя.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Обновлённый пользователь или ошибка в случае неудачи.</returns>
    [HttpPatch("changeRoleUserById")]
    [ProducesResponseType(200, Type = typeof(UserRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> ChangeRoleUserById(
        [FromBody] ChangeRoleUserByIdRequest byIdRequest,
        [FromServices] IChangeRoleUserByIdUseCase changeRoleUserByIdUseCase,
        CancellationToken cancellationToken)
    {
        // Преобразуем строку роли в Enum UserRole
        if (!Enum.TryParse<UserRole>(byIdRequest.Role, true, out var userRole))
        {
            return BadRequest("Неверная роль пользователя.");
        }

        // Создаем команду с преобразованной ролью
        var user = await changeRoleUserByIdUseCase.Execute(
            new ChangeRoleUserByIdCommand(byIdRequest.UserId, userRole),
            cancellationToken);

        return Ok(mapper.Map<UserRequest>(user));
    }

    /// <summary>
    /// Изменить у пользователя ссыль на заказчика.
    /// </summary>
    /// <param name="request">Запрос с данными для изменения заказчика для пользователя.</param>
    /// <param name="changeCustomerFromUserUseCase">Сервис для изменения заказчика для пользователя.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Обновлённый пользователь с другим заказчиком или ошибка в случае неудачи.</returns>
    [HttpPatch("changeCustomerFromUser")]
    [ProducesResponseType(200, Type = typeof(UserRequest))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(410)]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> ChangeCustomerFromUser(
        [FromBody] ChangeCustomerFromUserRequest request,
        [FromServices] IChangeCustomerFromUserUseCase changeCustomerFromUserUseCase,
        CancellationToken cancellationToken)
    {
        var user = await changeCustomerFromUserUseCase.Execute(
            new ChangeCustomerFromUserCommand(request.UserId, request.CustomerId),
            cancellationToken);

        return Ok(mapper.Map<UserRequest>(user));
    }

    /// <summary>
    /// Изменить пароль у пользователя.
    /// </summary>
    /// <param name="request">Запрос с данными для изменения пароля у пользователя.</param>
    /// <param name="changePasswordFromUserUseCase">Сервис для изменения пароля у пользователя.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Обновлённый пользователь с другим заказчиком или ошибка в случае неудачи.</returns>
    [HttpPatch("changePasswordFromUser")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404)]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public async Task<IActionResult> ChangePasswordFromUser(
        [FromBody] ChangePasswordFromUserRequest request,
        [FromServices] IChangePasswordFromUserUseCase changePasswordFromUserUseCase,
        CancellationToken cancellationToken)
    {
        var success = await changePasswordFromUserUseCase.Execute(
            new ChangePasswordFromUserCommand(request.UserId, request.OldPassword, request.NewPassword),
            cancellationToken);

        return success ? Ok() : NotFound("Password not change");
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