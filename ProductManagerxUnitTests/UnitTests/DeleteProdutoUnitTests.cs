using Microsoft.AspNetCore.Mvc;
using ProductManagerAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerxUnitTests.UnitTests
{
    public class DeleteProdutoUnitTests:IClassFixture<ProdutosUnitTestsController>
    {
        private readonly ProdutosController _produtosController;

        public DeleteProdutoUnitTests(ProdutosUnitTestsController produtosUnitTestsController)
        {
            _produtosController = new ProdutosController(produtosUnitTestsController.produtoService,
                                                         produtosUnitTestsController.mapper);
        }

        [Fact]
        public async Task DeleteProduto_Return_OkResult()
        {
            //arrange
            var produtoId = 7;

            //act
            var data = await _produtosController.Delete(produtoId);
            Assert.NotNull(data);
            var result = Assert.IsType<OkObjectResult>(data.Result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task DeleteProduto_Return_NotFoundResult()
        {
            var prodId = 999;

            var data = await _produtosController.Delete(prodId);
            Assert.NotNull(data);
            var result = Assert.IsType<NotFoundObjectResult>(data.Result);
            Assert.Equal(404, result.StatusCode);
        }
    }
}
