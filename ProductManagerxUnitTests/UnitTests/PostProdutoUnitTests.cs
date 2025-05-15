using Microsoft.AspNetCore.Mvc;
using ProductManagerAPI.Controllers;
using ProductManagerAPI.DTOs;
using ProductManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerxUnitTests.UnitTests
{
    public class PostProdutoUnitTests:IClassFixture<ProdutosUnitTestsController>
    {
        private readonly ProdutosController _produtosController;

        public PostProdutoUnitTests(ProdutosUnitTestsController produtosUnitTestsController)
        {
            _produtosController = new ProdutosController(produtosUnitTestsController.produtoService,
                                                        produtosUnitTestsController.mapper);
        }

        [Fact]
        public async Task PostProduto_Return_CreatedResult()
        {
            //arrange
            var produto = new ProdutoCreateDTO
            {
                Nome = "Café Pilão",
                Descricao = "Café Tradicional em pó 500g",
                CategoriaId = 1,
                Preco = 30
            };

            //act 
            var data = await _produtosController.Post(produto);
            var result = Assert.IsType<CreatedAtRouteResult>(data.Result);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public async Task PostProduto_Return_BadRequest()
        {
            //arrange
            ProdutoCreateDTO? produto = null;

            //act
            var data = await _produtosController.Post(produto);

            //assert
            var result = Assert.IsType<BadRequestResult>(data.Result);
            Assert.Equal(400, result.StatusCode);


        }
    }
}
