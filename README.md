# ecommerce-api-dotnet
Teste técnico - Rota das Oficinas

## Tecnologias Utilizadas
- **.NET 8.0**: Framework para desenvolvimento da API.
- **Entity Framework Core**: ORM utilizado para manipulação do banco de dados.
- **JWT (JSON Web Tokens)**: Utilizado para autenticação e autorização.

## Padrões de Projeto
- **CQRS (Command Query Responsibility Segregation)**: Separação de comandos (escrita) e consultas (leitura) para maior organização e escalabilidade.
- **Repository Pattern**: Abstração do acesso ao banco de dados para facilitar a manutenção e testes.
- **Dependency Injection**: Utilizado para gerenciar dependências e promover o desacoplamento entre as camadas.

## Estrutura do Projeto
O projeto segue uma estrutura modular, com separação clara entre as camadas:
- **Domain**: Contém as entidades e regras de negócio.
- **Application**: Contém os casos de uso, validações e contratos.
- **Infrastructure**: Contém implementações de serviços externos, como autenticação.
- **Persistence**: Contém o contexto do banco de dados e repositórios.
- **WebApi**: Contém os controladores e configurações da API.

## Como Executar o Projeto
1. Certifique-se de ter o **.NET 8.0 SDK** instalado.
2. Configure a string de conexão no arquivo `appsettings.Development.json` para apontar para o seu banco de dados PostgreSQL.
3. Execute os seguintes comandos:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project RO.DevTest.WebApi
