using AutoMapper;
using ProductManagerAPI.DTOs;
using ProductManagerAPI.Models;

namespace ProductManagerAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Produto, ProdutoCreateDTO>().ReverseMap();
            CreateMap<Produto,ProdutoResponseDTO>().ReverseMap();
            CreateMap<Produto, ProdutoUpdateEstoqueResponseDTO>().ReverseMap();
            CreateMap<Produto, ProdutoUpdateEstoqueRequestDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaResponseDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaCreateDTO>().ReverseMap();
        }
    }
}
