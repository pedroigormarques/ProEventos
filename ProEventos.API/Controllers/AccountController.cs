using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;

namespace ProEventos.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService, ITokenService tokenService)
    {
        _accountService = accountService;
        _tokenService = tokenService;
    }

    [HttpPost("Register", Name = "createUser")]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> CreateUser(UserCreateDto userCreateDto)
    {
        try
        {
            if (await _accountService.UserExists(userCreateDto.UserName))
                return BadRequest("Nome de usuário já em uso");

            UserDto user = await _accountService.CreateAccountAsync(userCreateDto);
            return Ok(user);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao carregar usuário. Erro: {e.Message}");
        }
    }

    [HttpPost("Login", Name = "loginUser")]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> LoginUser(UserLoginDto userloginDto)
    {
        try
        {
            UserDto? user = await _accountService.GetUserByUserNameAsync(userloginDto.UserName);
            if (user == null) return Unauthorized("Usuário ou senha inválida");

            var result = await _accountService.CheckUserPasswordAsync(user, userloginDto.Password);
            if (!result.Succeeded) return Unauthorized("Usuário ou senha inválida");

            user.Token = await _tokenService.CreateToken(user);
            return Ok(user);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao carregar usuário. Erro: {e.Message}");
        }
    }

    [HttpGet("GetUser", Name = "getUser")]
    public async Task<ActionResult<UserDto>> GetUser()
    {
        try
        {
            string userName = User.GetUserName();
            UserDto user = await _accountService.GetUserByUserNameAsync(userName);
            return Ok(user);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao carregar usuário. Erro: {e.Message}");
        }
    }

    [HttpPut("Update", Name = "updateUser")]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> UpdateUser(UserUpdateDto userDto)
    {
        try
        {
            UserDto? user = await _accountService.GetUserByUserNameAsync(User.GetUserName());
            if (user == null) return Unauthorized("Usuário inválido");

            UserDto userRetorno = await _accountService.UpdateAccount(userDto);
            return Ok(userRetorno);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao carregar usuário. Erro: {e.Message}");
        }
    }
}
