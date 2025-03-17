using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagerAPI.DTOs;
using ProductManagerAPI.Models;
using ProductManagerAPI.Repositories.Interfaces;
using ProductManagerAPI.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;
        public CategoriasController(ICategoriaService categoriaService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        // GET: api/<CategoriasController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
        {
            var categorias = await _categoriaService.GetAllAsync();

            if(categorias == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<CategoriaResponseDTO>>(categorias));
        }

        // GET api/<CategoriasController>/5
        [Authorize]
        [HttpGet("{id}",Name="ObterProdutoPorId")]
        public async Task<ActionResult<CategoriaResponseDTO>> Get(int id)
        {
            var categoria = await _categoriaService.GetAsync(c=>c.Id == id);

            if (categoria == null) 
                return NotFound();

            return Ok(_mapper.Map<CategoriaResponseDTO>(categoria));
        }

        // POST api/<CategoriasController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CategoriaResponseDTO>> Post([FromBody] CategoriaCreateDTO categoriaDTO)
        {
            var categoriaEntity = _mapper.Map<Categoria>(categoriaDTO);

            var categoriaCriada = await _categoriaService.CreateAsync(categoriaEntity);

            return new CreatedAtRouteResult("ObterProdutoPorId", new { id = categoriaCriada.Id }, _mapper.Map<CategoriaResponseDTO>(categoriaCriada));
        }

        // PUT api/<CategoriasController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaResponseDTO>> Put(int id, [FromBody] CategoriaDTO categoriaDTO)
        {
            if(categoriaDTO.Id != id)
                return BadRequest();

            var categoriaEntity = _mapper.Map<Categoria>(categoriaDTO);

            var categoriaAtualizada = await _categoriaService.UpdateAsync(categoriaEntity);

            return Ok(_mapper.Map<CategoriaResponseDTO>(categoriaAtualizada));
        }

        // DELETE api/<CategoriasController>/5
        [Authorize(Policy ="Administrador")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaResponseDTO>> Delete(int id)
        {
            var categoria = await _categoriaService.GetAsync(c=>c.Id == id);

            if (categoria == null)
                return BadRequest();

            await _categoriaService.DeleteAsync(categoria);

            return Ok(_mapper.Map<CategoriaResponseDTO>(categoria));
        }
    }
}
