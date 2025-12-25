namespace Gem.Models;

public class Participacoes
{
    public Musicos MusicoId { get; set; } = default!;
    public Ensaios EnsaioId { get; set; } = default!;
    public bool Presente { get; set; }
}
