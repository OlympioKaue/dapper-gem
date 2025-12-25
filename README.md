## 📌 Estudo de Dapper com C# e SQL Server
![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge)
![Windows](https://img.shields.io/badge/Windows-0078D4?logo=windows&logoColor=fff&style=for-the-badge)
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=fff&style=for-the-badge)
![Azure Data Studio](https://img.shields.io/badge/Azure%20Data%20Studio-0078D4?logo=microsoftazure&logoColor=fff&style=for-the-badge)


Este projeto tem como objetivo **estudo prático do uso do Dapper em C#**, utilizando **.NET 8 e SQL puro (Raw SQL)** para executar operações CRUD e Stored Procedure em um banco de dados SQL Server.

O foco é entender como o **Dapper** funciona na prática, sem abstrações pesadas como ORM completo (ex: Entity Framework), mantendo **controle total sobre as queries SQL**.

__________________________________________________________________________________________________________________________________

### 🚀 Tecnologias Utilizadas

- C# (.NET).

- Dapper.

- SQL Server.

- Microsoft.Data.SqlClient.

- Console Application.

__________________________________________________________________________________________________________________________________

### 📂 Estrutura do Projeto

- Program.cs
Contém:
  - Leitura da string de conexão via variável de ambiente.
  - Menu interativo no console.
  - Execução dos métodos de banco de dados.


- Models
  - Musicos → Representa a tabela [Musicos].


- Enums
  - Categoria → Enum para categorização dos músicos (ex: Cordas, Madeiras, Metais etc.)

 __________________________________________________________________________________________________________________________________

### 🔐 String de Conexão

  ```sh
  A conexão com o banco é feita através de uma variável de ambiente.
  ```
__________________________________________________________________________________________________________________________________

### 🧠 Fluxo da Aplicação

- Ao iniciar a aplicação, o usuário escolhe uma opção no console:

```sh
    1 - SELECT MUSICOS
    2 - INSERT MUSICOS
    3 - DELETE MUSICOS
    4 - UPDATE MUSICOS
    5 - PROCEDURE MUSICOS
```
__________________________________________________________________________________________________________________________________

### 🛠️ Métodos Implementados

1️⃣ SelectMusicsList — Buscar músicos (SELECT)

Responsável por listar todos os músicos cadastrados no banco.

📌 Conceitos aplicados:

  - Query<T>().
  - Mapeamento automático para o modelo Musicos.
  - SQL puro.
```sh
connection.Query<Musicos>("SELECT ... FROM [Musicos]");
```


2️⃣ InsertMusic — Inserir músico (INSERT)

Insere um novo músico no banco de dados.

📌 Conceitos aplicados:

  - Execute().
  - Parâmetros nomeados (@Parametro).
  - Proteção contra SQL Injection.
```sh
connection.Execute(sqlInsert, new { music.Id, music.Nome, ... });
```



3️⃣ UpdateMusic — Atualizar músico (UPDATE)

Atualiza o nome de um músico existente usando o Id.

📌 Conceitos aplicados:

  - SQL puro.
  - Atualização direta por parâmetro.
  - Retorno de linhas afetadas.
```sh
UPDATE [Musicos] SET [Nome] = @Nome WHERE [Id] = @Id
```


4️⃣ DeleteMusic — Remover músico (DELETE)

Remove um músico do banco através do Id.

📌 Conceitos aplicados:

  - Comando DELETE.
  - Execução segura com parâmetros.
  - Retorno de linhas afetadas.
```sh
DELETE FROM [Musicos] WHERE [Id] = @Id
```

5️⃣ ExecuteProcedureMusic — Executar Stored Procedure

Executa uma Stored Procedure responsável por deletar um músico.

📌 Conceitos aplicados:

   - Stored Procedure.
   - CommandType.StoredProcedure.
   - Parâmetros nomeados.
```sh
connection.Execute(
    "procedureMusic",
    param,
    commandType: CommandType.StoredProcedure
);
```
__________________________________________________________________________________________________________________________________

### 📌 Observações Importantes

- O Dapper não é um ORM completo, mas sim um Micro-ORM.

Ele foca em:
  - Performance.
  - Simplicidade.
  - Controle total do SQL.
