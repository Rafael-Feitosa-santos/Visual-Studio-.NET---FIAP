using Asp.Versioning;
using AutoMapper;
using Fiap.Api.Alunos.Models;
using Fiap.Api.Alunos.Service;
using Fiap.Api.Alunos.ViewModel;
using Fiap.Web.Alunos.Data;
using Fiap.Web.Alunos.Data.Contexts;
using Fiap.Web.Alunos.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//#region versionamento
//builder.Services.AddApiVersioning(options =>
//{
//    options.DefaultApiVersion = new ApiVersion(1);
//    options.ReportApiVersions = true;
//    options.AssumeDefaultVersionWhenUnspecified = true;
//    options.ApiVersionReader = ApiVersionReader.Combine(
//        new UrlSegmentApiVersionReader(),
//        new HeaderApiVersionReader("X-Api-Version"));
//}).AddApiExplorer(options =>
//{
//    options.GroupNameFormat = "'v'V";
//    options.SubstituteApiVersionInUrl = true;
//});

////// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();

//builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
//builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
//#endregion


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

#endregion

#region Repositorios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IRepresentanteRepository, RepresentanteRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
#endregion

#region Services
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IRepresentanteService, RepresentanteService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
#endregion

#region AutoMapper
// Configuração do AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(c => {
    // Permite que coleções nulas sejam mapeadas
    c.AllowNullCollections = true;
    // Permite que valores de destino nulos sejam mapeados
    c.AllowNullDestinationValues = true;

    c.CreateMap<ClienteModel, ClienteViewModel>();
    c.CreateMap<FornecedorModel, FornecedorViewModel>();
    c.CreateMap<LojaModel, LojaViewModel>();
    c.CreateMap<PedidoModel, PedidoViewModel>();
    c.CreateMap<PedidoProdutoModel, PedidoProdutoViewModel>();
    c.CreateMap<ProdutoModel, ProdutoViewModel>();
    c.CreateMap<RepresentanteModel, RepresentanteViewModel>();
    c.CreateMap<ClienteViewModel, ClienteModel>();
    c.CreateMap<FornecedorViewModel, FornecedorModel>();
    c.CreateMap<LojaViewModel, LojaModel>();
    c.CreateMap<PedidoModel, PedidoViewModel>();
    c.CreateMap<PedidoViewModel, PedidoModel>();
    c.CreateMap<PedidoProdutoViewModel, PedidoProdutoModel>();
    c.CreateMap<ProdutoViewModel, ProdutoModel>();
    c.CreateMap<RepresentanteViewModel, RepresentanteModel>();


    c.CreateMap<CreatePedidoViewModel, PedidoModel>()
            .ForMember(dest => dest.PedidoProdutos, opt => opt.MapFrom(src =>
                src.ProdutoIds.Select(id => new PedidoProdutoModel { ProdutoId = id }).ToList()));


});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

#region Configuracao do DB
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(

    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)

);
#endregion

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

