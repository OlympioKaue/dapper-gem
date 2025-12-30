using Gem.Enums;

namespace Gem.Models;
public class Musicos
{
    public Musicos(Guid id, string nome, string instrumento, Categoria categoria, DateTime nascimento)
    {
        Id = id;
        Nome = nome;
        Instrumento = instrumento;
        Categoria = categoria;
        Nascimento = nascimento;
    }
    public Musicos()
    {
        
    }
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Instrumento { get; set; } = string.Empty;
    public Categoria Categoria { get; set; }
    public DateTime Nascimento { get; set; }
}

