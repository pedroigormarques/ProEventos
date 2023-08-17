using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces;
public interface IAccountService
{
    Task<bool> UserExists(string userName);
    Task<UserDto?> GetUserByUserNameAsync(string userName);
    Task<SignInResult> CheckUserPasswordAsync(UserDto userDto, string password);
    Task<UserDto> CreateAccountAsync(UserCreateDto userCreateDto);
    Task<UserDto> UpdateAccount(UserUpdateDto userDto);
}
