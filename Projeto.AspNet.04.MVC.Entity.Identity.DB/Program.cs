using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projeto.AspNet._04.MVC.Entity.Identity.DB.Models;

var builder = WebApplication.CreateBuilder(args);

// 1º passo: adiconar o service que "aciona" a string de conexão com o servidor e o db configurados no arquivo appsettings.json
builder.Services.AddDbContext<AppEntityIdentityDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])); //(options) => ....

// 2º passo: indicar o "conjunto de regras" devidamente referenciado para - posteriormente ser possivel executar a total manipulação da base de dados
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppEntityIdentityDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
//0º passo: adicionar o método que auxiliará a aplicação nos processos de autenticação de usuarios para acesso a áreas restritas
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
