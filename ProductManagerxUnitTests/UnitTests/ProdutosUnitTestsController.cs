using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagerAPI.Context;
using ProductManagerAPI.Repositories.Interfaces;
using ProductManagerAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagerAPI.Mapping;
using ProductManagerAPI.Services;
using ProductManagerAPI.Services.Interfaces;

namespace ProductManagerxUnitTests.UnitTests
{
    public class ProdutosUnitTestsController
    {
        public IUnitOfWork repository;
        public IMapper mapper;
        public IProdutoService produtoService;
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }
        public static string connectionString =
          "Server=(localdb)\\mssqllocaldb;Database=ProductManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        static ProdutosUnitTestsController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectionString).Options;
        }
        public ProdutosUnitTestsController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            mapper = config.CreateMapper();

            var context = new AppDbContext(dbContextOptions);
            repository = new UnitOfWork(context);
            produtoService = new ProdutoService(repository);
        }
    }
}
