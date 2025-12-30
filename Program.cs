using Microsoft.Data.SqlClient;
using Dapper;
using Gem.Models;
using System.Data;
using Gem.Enums;


var connectionString = Environment.GetEnvironmentVariable("CONNECT_GEM");
if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("A conexão não foi encontrada");
    return;
}

Console.WriteLine("Conexão encontrada com sucesso !\n");

using (var connection = new SqlConnection(connectionString))
{
    Console.Write("Selecione o item a seguir que desejar:\n" +
                      "1 - [INSERT\n" +
                      "2 - [INSERT MANY]\n" +
                      "3 - [EXECUTE SCALAR]\n" +
                      "4 - [EXECUTE SCALAR VIOLINO]\n" +
                      "5 - [EXECUTE SCALAR MADEIRAS\n" +
                      "6 - [EXECUTE SCALAR METAIS\n" +
                      "7 - [EXECUTE SCALAR TECLADOS\n");
    int key = int.Parse(Console.ReadLine()!);
    Console.WriteLine();

    if (key is 1)
    {
        var id = Guid.NewGuid();

        Console.Write("Nome do músico(a): ");
        var nome = Console.ReadLine() ?? "";
        Console.Write("Instrumento do músico(a): ");
        var instrumento = Console.ReadLine() ?? "";

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(instrumento))
        {
            Console.WriteLine("Nome inválido");
            return;
        }

        Console.Write("Categoria do instrumento [1- Cordas, 2- Madeiras, 3- Metais, 4- Teclado]: ");
        var categoriaInput = int.Parse(Console.ReadLine()!);
        if (categoriaInput < 1 || categoriaInput > 4)
        {
            Console.WriteLine("Categoria inválida");
            return;
        }

        Console.Write("Data de nascimento do músico(a) [dd/mm/aaaa]: ");
        var dataNascimento = Console.ReadLine();
        if (!DateTime.TryParse(dataNascimento, out DateTime nascimento))
        {
            Console.WriteLine("Data de nascimento inválida");
            return;
        }

        var musician = new Musicos(id, nome, instrumento, (Categoria)categoriaInput, nascimento);

        InsertMusicians(connection, id, musician.Nome, musician.Instrumento, (int)musician.Categoria, musician.Nascimento);
    }

    if (key is 2)
    {
        ExecuteMany(connection);
        Console.WriteLine();
    }

    if (key is 3)
    {
        ExecuteScalar(connection);
        Console.WriteLine();
        return;
    }

    if(key is 4)
    {
        ExecuteScalarCordas(connection);
        Console.WriteLine();
        return;
    }

    if(key is 5)
    {
        ExecuteScalarMadeiras(connection);
        Console.WriteLine();
        return;
    }

    if(key is 6)
    {
        ExecuteScalarMetais(connection);
        Console.WriteLine();
        return;
    }

    if(key is 7)
    {
        ExecuteScalarTeclados(connection);
        Console.WriteLine();
        return;
    }

    SelectListMusicians(connection);
}

static void SelectListMusicians(SqlConnection connection)
{
    var musics = connection.Query<Musicos>("SELECT [Id], [Nome], [Instrumento], [Categoria], [Nascimento] FROM [Musicos] ORDER BY [Categoria] ASC");
    foreach (var item in musics)
    {
        Console.WriteLine($" Id: {item.Id}\n Nome: {item.Nome}\n Instrumento: {item.Instrumento}\n");
    }
    Console.WriteLine();
}

static void InsertMusicians(SqlConnection connection, Guid id, string nome, string instrumento, int categoria, DateTime nascimento)
{
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
        id,
        nome,
        instrumento,
        categoria,
        nascimento
    });

    Console.WriteLine($"{rows} Linhas Inseridas !");
    Console.WriteLine();
}

static void ExecuteMany(SqlConnection connection)
{
    var sql = "INSERT INTO [Participacoes] VALUES (@MusicoId, @EnsaioId, @Presente)";

    var rows = connection.Execute(sql, new[]
    {
        new {MusicoId = "86721973-a869-4706-8e5e-170bf5385830", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "fadf3761-dc5c-4fb2-bcd0-2514942266f6", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "b4f324b8-0977-4f24-b9d6-2b12e77a794c", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "47381e6d-df47-4b43-bac4-5530d76f3b0c", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "87befe4a-631c-4062-8c98-5d304ebc5e2c", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "1375734f-54ea-4040-81e3-5d57c311ddbf", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "81df5541-8151-4b6f-9c26-69af2759525b", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "5724301b-0745-4da2-b4f8-72aa14d73b3c", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "2749d81e-8a14-42a1-8e6a-95fd25b4d8c2", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "67dde5c1-da49-4f11-9aaa-b583146d09eb", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "5dfeffb4-581d-4ad0-bca7-b7a2677951d8", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "f7cf8d12-1aa3-468a-897b-c55b0abee89d", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "825800ab-d320-4f37-a7da-d0bccbf3c1c7", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "193f6e02-03b1-4fdb-8c6d-edb3483bcc85", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
        new {MusicoId = "9ec0b5a6-d8fe-4637-a9b5-fbff129a5942", EnsaioId = "ee454b66-0abe-46f1-a7d2-5148a0ecbd30", Presente = true},
    });

    Console.WriteLine($"{rows} Linhas inseridas no Execute Many");
}

static void ExecuteScalar(SqlConnection connection)
{
    var sql = "SELECT COUNT([Nome]) AS [Quantidade_Musicos] FROM [Musicos]";
    var quantity = connection.ExecuteScalar<int>(sql);
    Console.WriteLine($"Total de músicos: {quantity}");
}

static void ExecuteScalarCordas(SqlConnection connection)
{
    var sql = @"SELECT COUNT([Nome]) AS [Quantidade_Musicos] FROM [Musicos] WHERE [Categoria] = @Categoria";
    var quantity = connection.ExecuteScalar<int>(sql, new
    {
        Categoria = (int)Categoria.Cordas
    });
    Console.WriteLine($"Total de músicos que tocam cordas: {quantity}");
}

static void ExecuteScalarMadeiras(SqlConnection connection)
{
    var sql = @"SELECT COUNT([Nome]) AS [Quantidade_Musicos] FROM [Musicos] WHERE [Categoria] = @Categoria";
    var quantity = connection.ExecuteScalar<int>(sql, new
    {
        Categoria = (int)Categoria.Madeiras
    });
    Console.WriteLine($"Total de músicos que tocam madeiras: {quantity}");
}

static void ExecuteScalarMetais(SqlConnection connection)
{
    var sql = @"SELECT COUNT([Nome]) AS [Quantidade_Musicos] FROM [Musicos] WHERE [Categoria] = @Categoria";
    var quantity = connection.ExecuteScalar<int>(sql, new
    {
        Categoria = (int)Categoria.Metais
    });
    Console.WriteLine($"Total de músicos que tocam metais: {quantity}");
}

static void ExecuteScalarTeclados(SqlConnection connection)
{
    var sql = @"SELECT COUNT([Nome]) AS [Quantidade_Musicos] FROM [Musicos] WHERE [Categoria] = @Categoria";
    var quantity = connection.ExecuteScalar<int>(sql, new
    {
        Categoria = (int)Categoria.Teclados
    });
    Console.WriteLine($"Total de músicos que tocam teclado: {quantity}");
}

static void UpdateMusicians(SqlConnection connection)
{
    var sqlUpdate = "UPDATE [Musicos] SET [Nome] = @Nome WHERE [Id] = @Id";
    var rows = connection.Execute(sqlUpdate, new
    {
        Id = "6b18d2f4-4681-4aec-9665-25039b173348",
        Nome = "Reginaldo Felix Soares",
    });

    Console.WriteLine($"{rows} Linhas afetadas no update");

}

static void DeleteMusicians(SqlConnection connection)
{
    var sqlDelete = "DELETE FROM [Musicos] WHERE [Id] = @Id";
    var rows = connection.Execute(sqlDelete, new { Id = "f1d3a05a-8243-4964-a64e-a531cef7751b" });

    Console.WriteLine($"{rows} Linhas afetadas no delete");
}

static void ProcedureDeleteMusicians(SqlConnection connection)
{
    var procedure = "[procedureDeleteMusicians]";
    var param = new { IdMusic = "6b18d2f4-4681-4aec-9665-25039b173348" };
    var rows = connection.Execute(
        procedure,
        param,
        commandType: CommandType.StoredProcedure);

    Console.WriteLine($"{rows} Linhas afetadas na procedure");
}