using Gem.Enums;

namespace Gem.Models;
public class Musicos
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Instrumento { get; set; } = string.Empty;
    public Categoria Categoria { get; set; }
    public DateTime Nascimento { get; set; }
}

