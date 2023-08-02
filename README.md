## ToroApi

Este repositório contém a ToroApi, uma aplicação desenvolvida em C# utilizando .NET 7 com MongoDB para armazenamento de dados e JWT para autenticação. A arquitetura utilizada é baseada em Domain-Driven Design (DDD) e Onion Architecture, com a utilização da biblioteca MediatR para a implementação de CQRS.

### Pré-requisitos

- .NET SDK 7.0
- MongoDB instalado e configurado

### Instalação e Execução

1. Clone este repositório em sua máquina local:

```
git clone https://github.com/gsembraski/ToroApi.git
```

2. Navegue para o diretório do projeto:

```
cd ToroApi
```

3. Restaure as dependências:

```
dotnet restore
```

4. Configure as variáveis de ambiente necessárias para a conexão com o MongoDB e a chave secreta do JWT.

5. Execute a aplicação:

```
dotnet run
```

A API estará disponível em `https://localhost:7099` ou `http://localhost:5065`.

### Estrutura do Projeto

A arquitetura do projeto segue o padrão Onion e a organização DDD, o que resulta em uma separação clara de responsabilidades e facilita a manutenção do código.

```
ToroApi/
|-- src/
|   |-- Toro.API/
|   |-- Toro.API.Domain/
|   |-- Toro.API.Infrastructure/
|   |-- Toro.API.Domain.Tests/
|-- .gitignore
|-- Toro.sln
|-- README.md
```

### Autenticação com JWT

A autenticação é feita utilizando JSON Web Tokens (JWT). Para obter um token de acesso, faça uma requisição para o endpoint de autenticação fornecendo as credenciais corretas. O token será retornado e deverá ser incluído no header de todas as requisições subsequentes como:

```
Authorization: Bearer <seu-token>
```

### Testes Automatizados

O projeto inclui testes automatizados para garantir a integridade do código. Para executar os testes, utilize o seguinte comando:

```
dotnet test
```
