# Tech Challenge - Serviço de Pedido

Este repositório contém o código do serviço de pedidos para o Tech Challenge.

## Pré-requisitos

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Configuração

1. Clone o repositório:

    ```bash
    git clone https://github.com/edsjunior/tech-challenge-pedido.git
    cd tech-challenge-pedido
    ```

2. Configure o banco de dados em memória e as dependências:

    No arquivo `appsettings.json`, ajuste as configurações conforme necessário.

## Executando o Serviço

### Localmente

1. Restaure as dependências:

    ```bash
    dotnet restore
    ```

2. Execute a aplicação:

    ```bash
    dotnet run
    ```

    A API estará disponível em `http://localhost:5001`.

### Usando Docker

1. Certifique-se de que o Docker e o Docker Compose estão instalados.

2. Construa a imagem Docker:

    ```bash
    docker-compose build
    ```

3. Suba os containers:

    ```bash
    docker-compose up
    ```

    A API estará disponível em `http://localhost:5001`.

## Testes

Para executar os testes, use o comando:

```bash
dotnet test
