
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos;
public class EventoDto
{
    public int Id { get; set; }

    [Required]
    public string Local { get; set; }

    public DateTime? DataEvento { get; set; }

    [Required]
    public string Tema { get; set; }

    [Range(1, 10000, ErrorMessage = "O valor deve estar entre 1 e 10000")]
    public int QtdPessoas { get; set; }

    [Required]
    public string ImagemUrl { get; set; }

    [Required]
    [Phone]
    public string Telefone { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public IEnumerable<LoteDto> Lotes { get; set; }
    public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
    public IEnumerable<PalestranteDto> Palestrantes { get; set; }
}
