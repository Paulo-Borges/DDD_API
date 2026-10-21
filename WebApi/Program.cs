using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.Genericos;
using Dominio.Interfaces.InterfaceServicos;
using Dominio.Servicos;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.Token;

var builder = WebApplication.CreateBuilder(args);

// As ConectionsStrings são configuradas no arquivo appsettings.json, e podem ser acessadas através do objeto Configuration do builder.

builder.Services.AddDbContext<Contexto>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<Contexto>();

// INTERFACE E IMPLEMENTAÇÃO DE REPOSITÓRIO
builder.Services.AddSingleton(typeof(IGenericos<>), typeof(RepositorioGenerico<>));
builder.Services.AddSingleton<INoticia, RepositorioNoticia>();
builder.Services.AddSingleton<IUsuario, RepositorioUsuario>();

// SErvico DOMINIO
builder.Services.AddSingleton<IServicoNoticia, ServicoNoticia>();

// INTERFACE E IMPLEMENTAÇÃO DE APLICAÇÃO
builder.Services.AddSingleton<IAplicacaoNoticia, AplicacaoNoticia>();
builder.Services.AddSingleton<IAplicacaoUsuario, AplicacaoUsuario>();

// Incluindo o token JWT__________________________________________________________________________________________________________

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "Teste.Securiry.Bearer",
            ValidAudience = "Teste.Securiry.Bearer",
            IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
        };

        options.Events = new JwtBearerEvents
         {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                return Task.CompletedTask;
            }
        };
    });


        
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapSwagger();
    app.MapSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
