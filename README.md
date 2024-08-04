# OVB.Demos.InvestmentPortfolio

[![SonarCloud](https://sonarcloud.io/images/project_badges/sonarcloud-white.svg)](https://sonarcloud.io/summary/new_code?id=OtavioVB_OVB.Demos.PortfolioInvestment)

Esse é o repositório do teste técnico de desenvolvimento de um Sistema de Gestão de Portfólio de Investimentos. 

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=OtavioVB_OVB.Demos.PortfolioInvestment&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=OtavioVB_OVB.Demos.PortfolioInvestment)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=OtavioVB_OVB.Demos.PortfolioInvestment&metric=bugs)](https://sonarcloud.io/summary/new_code?id=OtavioVB_OVB.Demos.PortfolioInvestment)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=OtavioVB_OVB.Demos.PortfolioInvestment&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=OtavioVB_OVB.Demos.PortfolioInvestment)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=OtavioVB_OVB.Demos.PortfolioInvestment&metric=coverage)](https://sonarcloud.io/summary/new_code?id=OtavioVB_OVB.Demos.PortfolioInvestment)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=OtavioVB_OVB.Demos.PortfolioInvestment&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=OtavioVB_OVB.Demos.PortfolioInvestment)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=OtavioVB_OVB.Demos.PortfolioInvestment&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=OtavioVB_OVB.Demos.PortfolioInvestment)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=OtavioVB_OVB.Demos.PortfolioInvestment&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=OtavioVB_OVB.Demos.PortfolioInvestment)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=OtavioVB_OVB.Demos.PortfolioInvestment&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=OtavioVB_OVB.Demos.PortfolioInvestment)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=OtavioVB_OVB.Demos.PortfolioInvestment&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=OtavioVB_OVB.Demos.PortfolioInvestment)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=OtavioVB_OVB.Demos.PortfolioInvestment&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=OtavioVB_OVB.Demos.PortfolioInvestment)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=OtavioVB_OVB.Demos.PortfolioInvestment&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=OtavioVB_OVB.Demos.PortfolioInvestment)

### :chains: Dependências

As dependências do projeto está listado a seguir:
- [PostgreeSQL](https://www.postgresql.org/);
- [Postgres Admin](https://www.pgadmin.org/);
- [.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0);
- [Entity Framework Core 8](https://learn.microsoft.com/pt-br/ef/core/get-started/overview/install);
- [Docker e Docker-Compose](https://docs.docker.com/);
- [Azure Functions e Azurite](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer?tabs=python-v2%2Cisolated-process%2Cnodejs-v4&pivots=programming-language-csharp)
- [k6](https://k6.io/docs)
- [SonarCloud](https://sonarcloud.io/summary/overall?id=OtavioVB_OVB.Demos.PortfolioInvestment)

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

Agora para realizarmos a compilação e execução do Scheduler para Alerta via E-mail de Vencimento Próximo de Ativos financeiros, será necessário seguir as etapas seguintes:

6. Instalar as dependências do npm

```
npm install -g azure-functions-core-tools@4
```

7. Executar a compilação e execução também do Function as a Service para Alerta do Vencimento Próximo de Ativos financeiros

```
cd "functions/OVB.Demos.InvestmentPortfolio.Scheduler/"
func start
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

Além disso, o serviço de gerenciamento do Postgres (PgAdmin) está up no endpoint `http://localhost:17000` para gerenciamento do banco de dados. Você pode usar o email `suporte@postgres.com.br` e a senha `postgres@123` para ter acesso ao banco de dados.

Como não há endpoint para cadastro de cliente e cadastro de operadores, utilize as queries a seguir para fazer o cadastro:

```sql
INSERT INTO operator.operators(
	idoperator, code, name, email, password_hash, password_salt, document)
	VALUES ('d96a228c-96fb-477e-bed4-1d28575f088e','OPER0KHF75','Otávio Carmanini','otaviovb.developer@gmail.com','8b440d3cfef73bda67eb80626dcb359b0ddd1cd319656c3eadc5bd452414d676','82J2347KHA4K','54627477805');
INSERT INTO customer.customers(
	idcustomer, code, name, document, email, created_at, password_hash, password_salt)
	VALUES ('ed1d721d-62ba-44ef-b887-0c0b812ed37d','CST94KDJNX3','Otávio Carmanini','54627477805','otaviovb.developer@gmail.com','2024-08-04 16:29:45+00','8b440d3cfef73bda67eb80626dcb359b0ddd1cd319656c3eadc5bd452414d676','82J2347KHA4K');
```

### Relatório de Teste de Carga

Você consegue obter informações de um pequeno e mínimo teste de carga realizado, através do arquivo em .xlsx disponibilizado com nome `RESULTADOS_TESTES_DE_CARGA.xlsx`.