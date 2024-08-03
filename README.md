# OVB.Demos.InvestmentPortfolio

### :chains: Dependências

As dependências do projeto está listado a seguir:
- [PostgreeSQL](https://www.postgresql.org/);
- [Postgres Admin](https://www.pgadmin.org/);
- [.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0);
- [Entity Framework Core 8](https://learn.microsoft.com/pt-br/ef/core/get-started/overview/install);
- [Docker e Docker-Compose](https://docs.docker.com/);

### :gear: Como executar a aplicação?

Para executar a aplicação associada ao OVB.Demos.InvestmentPortfolio desenvolvido por [Otávio Carmanini](https://www.linkedin.com/in/otaviovillasboassimoncinicarmanini/) é necessário a primeiro momento `Instalar as Dependências Associadas ao Projeto`.

Você pode utilizar as dependências utilizadas por meio do arquivo `docker compose` disponibilizado nesse repositório. Siga as etapas a seguir:

1. Clone o projeto do GitHub

```
git clone https://github.com/OtavioVB/OVB.Demos.PortfolioInvestment.git
```

2. Entre na pasta associada ao projeto e execute a construção do setup do arquivo compose

```
cd OVB.Demos.PortfolioInvestment
docker compose -f "./devops/docker-compose.Local.yaml" up -d --build
```

Agora, com as dependências do projeto de API instalados e sendo executados por meio do `Docker` será necessário executar a aplicação das migrações da estrutura do banco de dados para o `PostgreeSQL`, assim como, a execução da aplicação .NET.

3. Instale a ferramenta do Entity Framework Core 8

```
dotnet tool install --global dotnet-ef
```

4. Execute a migração da estrutura do banco de dados

```
dotnet ef database update --project "src/OVB.Demos.InvestmentPortfolio.WebApi/OVB.Demos.InvestmentPortfolio.WebApi.csproj"
```

5. Executar a compilação e execução da aplicação API

```
dotnet run --project "src/OVB.Demos.InvestmentPortfolio.WebApi/OVB.Demos.InvestmentPortfolio.WebApi.csproj"
```


### :rocket: Como utilizar a aplicação?

Para utilizar a aplicação e realizar requisições nos endpoints expostos pela API, você pode utilizar o arquivo de coleção de API's de requisições presentes no arquivo postman, através do diretório:

```
./postman/PortfolioInvestment.postman_collection.json
```

Ou, utilizar a documentação disponibilizada no swagger através do endpoint:

```
https://localhost:5000/swagger/index.html
```