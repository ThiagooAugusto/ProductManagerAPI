# ProductManagerAPI :file_folder:

Api Rest para gerenciamento de um catálogo de produtos.

## Requisitos :pencil:

* Qualquer pessoa pode Consultar todos os Produtos cadastrados
* Qualquer pessoa pode Consultar os Produtos em Estoque
* Gerente do sistema pode Excluir um Produto
* O Gerente e o Funcionário podem Atualizar estoque de um produto

## Tecnologias e ferramentas :computer:
* Autenticação com JWT
* Autorização com políticas do Asp .net core
* Validação de dados
* Testes unitários
* Padrões de projeto

## Execução e exemplo 

Para rodar a api digite o comando `dotnet run` na pasta do projeto

Exemplo de cadastro de produto
 
POST api/v1/produtos
```
{
  "nome": "string",
  "descricao": "string",
  "preco": 9999,
  "categoriaId": 0
}
```
