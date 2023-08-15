using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos;
public class LoteDto
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [Range(0.01, 10000)]
    [Required]
    public decimal Preco { get; set; }

    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }

    [Required]
    [Range(1, 1000)]
    public int Quantidade { get; set; }

    [Required]
    public int EventoId { get; set; }

    public EventoDto? Evento { get; set; }
}
