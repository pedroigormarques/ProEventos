using ProEventos.Domain.Identity;

namespace ProEventos.Application.Dtos;
public class UserCreateDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PrimeiroNome { get; set; }
    public string UltimoNome { get; set; }
}
