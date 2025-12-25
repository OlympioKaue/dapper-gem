using Microsoft.Data.SqlClient;
using Dapper;
using Gem.Models;
using System.Data;


var connectionString = Environment.GetEnvironmentVariable("CONNECT_GEM");
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("A conexão não foi encontrada");
    return;
}

Console.WriteLine("Conexão encontrada com sucesso !\n");

using (var connection = new SqlConnection(connectionString))
{
    Console.WriteLine("Selecione o item a seguir que desejar:\n" +
                      "1 - [SELECT MUSICOS]\n" +
                      "2 - [INSERT MUSICOS]\n" +
                      "3 - [DELETE MUSICOS]\n" +
                      "4 - [UPDATE MUSICOS]\n" +
                      "5 - [PROCEDURE MUSICOS]");
    int key = int.Parse(Console.ReadLine() ?? string.Empty);
    switch (key)
    {
        case 1:
            SelectMusicsList(connection);
            break;
        case 2:
            InsertMusic(connection);
            break;
        case 3:
            DeleteMusic(connection);
            break;
        case 4:
            UpdateMusic(connection);
            break;
        case 5:
            ExecuteProcedureMusic(connection);
            break;
        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}

static void SelectMusicsList(SqlConnection connection)
{
    var musics = connection.Query<Musicos>("SELECT [Id], [Nome], [Instrumento], [Categoria] FROM [Musicos] ORDER BY [Categoria] ASC");
    foreach (var item in musics)
    {
        Console.WriteLine($"Id: {item.Id} | Nome: {item.Nome} | Instrumento: {item.Instrumento}");
    }
}

static void InsertMusic(SqlConnection connection)
{
    var music = new Musicos
    {
        Id = Guid.NewGuid(),
        Nome = "Luan Pereira Soares",
        Instrumento = "Violoncelo",
        Categoria = Gem.Enums.Categoria.Cordas,
        Nascimento = new DateTime(1995, 10, 25)
    };

    var sqlInsert = @"INSERT INTO [Musicos] 
                     VALUES
                    (
                      @Id,
                      @Nome,
                      @Instrumento,
                      @Categoria,
                      @Nascimento
                    )";

    var rows = connection.Execute(sqlInsert, new
    {
        music.Id,
        music.Nome,
        music.Instrumento,
        music.Categoria,
        music.Nascimento
    });

    Console.WriteLine($"{rows} Linhas afetadas no insert");
}

static void UpdateMusic(SqlConnection connection)
{
    var sqlUpdate = "UPDATE [Musicos] SET [Nome] = @Nome WHERE [Id] = @Id";
    var rowns = connection.Execute(sqlUpdate, new
    {
        Id = "6b18d2f4-4681-4aec-9665-25039b173348",
        Nome = "Reginaldo Felix Soares",
    });

    Console.WriteLine($"{rowns} Linhas afetadas no update");

}

static void DeleteMusic(SqlConnection connection)
{
    var sqlDelete = "DELETE FROM [Musicos] WHERE [Id] = @Id";
    var rowns = connection.Execute(sqlDelete, new { Id = "f1d3a05a-8243-4964-a64e-a531cef7751b" });

    Console.WriteLine($"{rowns} Linhas afetadas no delete");
}

static void ExecuteProcedureMusic(SqlConnection connection)
{
    var procedure = "[procedureMusic]";  
    var param = new {IdMusic = "6b18d2f4-4681-4aec-9665-25039b173348"};
    var rowns = connection.Execute(
        procedure,
        param,
        commandType: CommandType.StoredProcedure);

    Console.WriteLine($"{rowns} Linhas afetadas na procedure");
}