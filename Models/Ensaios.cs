namespace Gem.Models;

public class Ensaios
{
    public Guid Id { get; set; }
    public DateTime DataEnsaio { get; set; }
    public string Local { get;set; } = string.Empty;
}
