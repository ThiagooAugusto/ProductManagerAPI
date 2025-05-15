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
    public class GetProdutoUnitTests:IClassFixture<ProdutosUnitTestsController>
    {
        private readonly ProdutosController _produtosController;

        public GetProdutoUnitTests(ProdutosUnitTestsController produtosUnitTestsController)
        {
            _produtosController = new ProdutosController(produtosUnitTestsController.produtoService,produtosUnitTestsController.mapper);
        }

        [Fact]
        public async Task GetProdutById_Return_OkObjectResult()
        {
            //arrange
            var prodId = 2;

            //act
            var data = await _produtosController.Get(prodId);

            //arrange
            var result = Assert.IsType<OkObjectResult>(data.Result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetProdutoById_Return_NotFoundResult()
        {
            //arrange
            var prodId = -999;

            //act
            var data = await _produtosController.Get(prodId);

            //arrange
            var result = Assert.IsType<NotFoundObjectResult>(data.Result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetProdutos_Return_ProdutosDtoList()
        {
            //act
            var data = await _produtosController.GetAll();

            //arrange
            var result = Assert.IsType<OkObjectResult>(data.Result);
            var produtosList = Assert.IsAssignableFrom<IEnumerable<ProdutoResponseDTO>>(result.Value);
            Assert.NotNull(produtosList);
            
        }
    }
}
