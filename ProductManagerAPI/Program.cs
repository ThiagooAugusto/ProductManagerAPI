using Microsoft.EntityFrameworkCore;
using ProductManagerAPI.Context;
using ProductManagerAPI.Mapping;
using ProductManagerAPI.Repositories;
using ProductManagerAPI.Repositories.Interfaces;
using ProductManagerAPI.Services;
using ProductManagerAPI.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
builder.Services.AddScoped<ICategoriaRepository,ICategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository,IProdutoRepository>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProdutoService,ProdutoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
