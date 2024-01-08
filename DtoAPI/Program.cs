using DtoAPI;
using DtoAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingConfig));

var key = Encoding.ASCII.GetBytes(JWTProperties.SecretKey); // Buscando a SecretKey do JWT.
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Em geral, esta sendo configurado o processo/passos que deve ser feito na autenticao! Nesse caso o Bearer eh para verificar a toda requisicao o Token, no Header da requisicao!
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Aqui eh configurado o processo/passos que devem ser feitos caso o usuario nao esteja autenticado!
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; // Nao precisa ser HTTPS
    x.SaveToken = true; // Deve salvar o Token (Aonde, nao sei)
    x.TokenValidationParameters = new TokenValidationParameters // Quais validacoes serao feitas
    {
        ValidateIssuerSigningKey = true, // Deve validar a assinatura do Token
        IssuerSigningKey = new SymmetricSecurityKey(key), // Qual chave ele vai usar para validar
        ValidateIssuer = false, // Esse e o Audience sao mais complexos e nao vao ser usados, caem mais na parte de oAuth e etc e esta relacionadno ao DefaultChallengeScheme
        ValidateAudience = false
    };
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RetailStoreManagement"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
