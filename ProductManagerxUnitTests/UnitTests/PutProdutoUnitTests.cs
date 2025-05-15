using Microsoft.AspNetCore.Mvc;
using ProductManagerAPI.Controllers;
using ProductManagerAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerxUnitTests.UnitTests
{
    public class PutProdutoUnitTests:IClassFixture<ProdutosUnitTestsController>
    {
        private readonly ProdutosController _produtosController;

        public PutProdutoUnitTests(ProdutosUnitTestsController produtosUnitTestsController)
        {
            _produtosController = new ProdutosController(produtosUnitTestsController.produtoService,
                                                         produtosUnitTestsController.mapper);
        }

        [Fact]
        public async Task PutProduto_Return_OkResult()
        {
            //arrange
            var produtoId = 1;
            ProdutoUpdateDTO produto = new ProdutoUpdateDTO
            {
                Id = 1,
                Nome = "Cerveja Budweiser 330ml long neck",
                Descricao = "Cerveja garrafa long neck 330ml",
                Preco = 25m,
                CategoriaId = 1
            };

            //act
            var data = await _produtosController.Put(produtoId, produto);

            //assert
            var result = Assert.IsType<OkObjectResult>(data.Result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task PutProduto_Return_BadRequest()
        {
            //arrange
            var produtoId = 1000;
            ProdutoUpdateDTO produto = new ProdutoUpdateDTO
            {
                Id = 1,
                Nome = "Cerveja Budweiser 330ml long neck",
                Descricao = "Cerveja garrafa long neck puro malte 330ml",
                Preco = 25m,
                CategoriaId = 1
            };

            //act
            var data = await _produtosController.Put(produtoId, produto);

            //assert
            var result = Assert.IsType<BadRequestObjectResult>(data.Result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
