using System.ComponentModel.DataAnnotations;

public class ReservaCreateDto
{
    [Required]
    public int ClienteId { get; set; }

    public DateTime FechaReserva { get; set; } = DateTime.UtcNow;

    public string? Observaciones { get; set; }

    // Correcta relación muchos a muchos
    public List<int> ServicioIds { get; set; } = new();
}
