using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManagerAPI.DTOs;
using ProductManagerAPI.Models;
using ProductManagerAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        // GET: api/<ProdutosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoResponseDTO>>> GetAll()
        {
            var produtos = await _produtoService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ProdutoResponseDTO>>(produtos));
        }

        // GET api/<ProdutosController>/5
        [Authorize]
        [HttpGet("{id}",Name ="ObterProduto")]
        public async Task<ActionResult<ProdutoResponseDTO>> Get(int id)
        {
            var produto = await _produtoService.GetAsync(p=>p.Id == id);

            if(produto == null) 
                return NotFound("Produto não encontrado!");

            return Ok(_mapper.Map<ProdutoResponseDTO>(produto));
        }

        [Authorize]
        [HttpGet("EmEstoque")]
        public async Task<ActionResult<IEnumerable<ProdutoEstoqueDTO>>> GetProdutosEmEstoque()
        {
            var produtos = await _produtoService.GetProdutosEmEstoqueAsync();

            if (produtos == null)
                return NotFound("Produtos não encontrados!");

            return Ok(_mapper.Map<IEnumerable<ProdutoResponseDTO>>(produtos));
        }

        [Authorize]
        [HttpGet("categorias/{id}")]
        public async Task<ActionResult<IEnumerable<ProdutoResponseDTO>>> GetProdutosPorCategoria(int id)
        {
            var produtos = await _produtoService.GetProdutosPorCategoriaAsync(id);

            if (produtos == null)
                return NotFound("Produtos não encontrados!");

            return Ok(_mapper.Map<IEnumerable<ProdutoResponseDTO>>(produtos));
        }



        // POST api/<ProdutosController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProdutoResponseDTO>> Post([FromBody] ProdutoCreateDTO produtoDTO)
        {
            var produtoEntity = _mapper.Map<Produto>(produtoDTO);

            var produtoCriado = await _produtoService.CreateAsync(produtoEntity);

            var result = _mapper.Map<ProdutoResponseDTO>(produtoCriado);

            return new CreatedAtRouteResult("ObterProduto", new { id = produtoCriado.Id }, result);
        }

        // PUT api/<ProdutosController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoResponseDTO>> Put(int id, [FromBody] ProdutoUpdateDTO produtoDTO)
        {
            if (produtoDTO.Id != id)
                return BadRequest("id fornecido é inválido!");

            var produtoEntity = _mapper.Map<Produto>(produtoDTO);

            var produtoAtualizado = await _produtoService.UpdateAsync(produtoEntity);

            return Ok(_mapper.Map<ProdutoResponseDTO>(produtoAtualizado));
        }

        [Authorize(Policy ="Administrador")]
        [Authorize(Policy ="Funcionario")]
        [HttpPatch("{id}/UpdateEstoque")]
        public async Task<ActionResult<ProdutoUpdateEstoqueResponseDTO>> Patch(int id , [FromBody] JsonPatchDocument<ProdutoUpdateEstoqueRequestDTO> patchProdutoDTO)
        {
            if (patchProdutoDTO == null || id < 0)
                return BadRequest();

            var produto = await _produtoService.GetAsync(p=>p.Id == id);

            if (produto == null)
                return NotFound();

            var produtoUpdateEstoqueRequest = _mapper.Map<ProdutoUpdateEstoqueRequestDTO>(produto);

            patchProdutoDTO.ApplyTo(produtoUpdateEstoqueRequest, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(produtoUpdateEstoqueRequest))
                return BadRequest(ModelState);

            _mapper.Map(produtoUpdateEstoqueRequest, produto);

            if (produtoUpdateEstoqueRequest.Quantidade > 0)
                produto.Status = Status.EmEstoque;

            await _produtoService.UpdateAsync(produto);

            return Ok(_mapper.Map<ProdutoUpdateEstoqueResponseDTO>(produto));

        }


        // DELETE api/<ProdutosController>/5
        [Authorize(Policy ="Administrador")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoResponseDTO>> Delete(int id)
        {
            var produto = await _produtoService.GetAsync(p=> p.Id == id);

            if(produto == null)
                return NotFound("Produto não encontrado!");

            var produtoDeletado = await _produtoService.DeleteAsync(produto);

            return Ok(_mapper.Map<ProdutoResponseDTO>(produtoDeletado));
        }
    }
}
