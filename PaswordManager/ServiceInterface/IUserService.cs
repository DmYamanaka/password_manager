using Microsoft.AspNetCore.Mvc.ModelBinding;
using PasswordManager.Models.User;

namespace PasswordManager.ServiceInterface
{
    /// <summary>
    /// Сервис для работы с пользоватиелями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Настройка учетный данных пользователя
        /// </summary>
        /// <param name="userEdit">Объект с настройками пользователя</param>
        /// <returns>Объект User</returns>
        public Task<UserInput> Edit(UserInput userEdit);

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="registrationUser">Пользователькские данные</param>
        public Task<string> Registration(HttpContext httpContext, ModelStateDictionary modelState, UserInput registrationUser);

        /// <summary>
        /// Текущий пользователь
        /// </summary>
        /// <param name="id">ID пользоавтеля</param>
        /// <returns></returns>
        public Task<UserInput> DeteilsCurrentUser(Guid id);
    }
}
