using AutoMapper;
using Fiap.Web.Alunos.Data;
using Fiap.Web.Alunos.Data.Repository;
using Fiap.Web.Alunos.Logging;
using Fiap.Web.Alunos.Models;
using Fiap.Web.Alunos.Services;
using Fiap.Web.Alunos.ViewModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region RegistroIServiceColletction

builder.Services.AddSingleton<ICustomLogger, FileLogger>();


builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IRepresentanteRepository, RepresentanteRepository>();
builder.Services.AddScoped<IRepresentanteService, RepresentanteService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

#region AutoMapper

var mappeConfig = new AutoMapper.MapperConfiguration(c =>
    {
        c.AllowNullCollections = true;
        c.AllowNullDestinationValues = true;

        c.CreateMap<ClienteModel, ClienteCreateViewModel>();
        c.CreateMap<ClienteCreateViewModel, ClienteModel>();
    }
  );

IMapper mapper = mappeConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

//Inicializando o banco de dados
#region Configuracao do DB
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(

    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)

);
#endregion

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
