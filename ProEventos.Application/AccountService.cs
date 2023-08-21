
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;
public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserPersist _userPersist;
    private readonly IMapper _mapper;
    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IUserPersist userPersist, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userPersist = userPersist;
        _mapper = mapper;

    }
    public async Task<SignInResult> CheckUserPasswordAsync(UserDto userDto, string password)
    {
        try
        {
            User? user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == userDto.UserName.ToLower());
            if (user == null) throw new Exception("Usuário não encontrado");

            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao validar senha. Erro: {e.Message}");
        }
    }

    public async Task<UserDto> CreateAccountAsync(UserCreateDto userCreateDto)
    {
        try
        {
            User user = _mapper.Map<User>(userCreateDto);

            var result = await _userManager.CreateAsync(user, userCreateDto.Password);

            if (!result.Succeeded) throw new Exception(result.Errors.ToString()); //-----------

            return _mapper.Map<UserDto>(user);
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao criar usuário. Erro: {e.Message}");
        }
    }

    public async Task<UserDto?> GetUserByUserNameAsync(string userName)
    {
        try
        {
            User? user = await _userPersist.GetUserByUserNameAsync(userName);
            return _mapper.Map<UserDto>(user);
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao buscar usuário. Erro: {e.Message}");
        }
    }

    public async Task<UserDto> UpdateAccount(UserUpdateDto userDto)
    {
        try
        {
            User? user = await _userPersist.GetUserByUserNameAsync(userDto.UserName);
            if (user == null) throw new Exception("Usuário não encontrado");

            _mapper.Map(userDto, user);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, userDto.Password);

            _userPersist.Update<User>(user);
            await _userPersist.SaveChangesAsync();

            User? userResult = await _userPersist.GetUserByUserNameAsync(user.UserName);
            if (userResult == null) throw new Exception("Erro ao carregar dados após atualização");

            return _mapper.Map<UserDto>(userResult);
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao atualizar usuário. Erro: {e.Message}");
        }
    }

    public async Task<bool> UserExists(string userName)
    {
        try
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == userName.ToLower());
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao validar usuário. Erro: {e.Message}");
        }
    }
}
