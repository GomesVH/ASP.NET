using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projeto.AspNet._04.MVC.Entity.Identity.DB.Models;

var builder = WebApplication.CreateBuilder(args);

// 1� passo: adiconar o service que "aciona" a string de conex�o com o servidor e o db configurados no arquivo appsettings.json
builder.Services.AddDbContext<AppEntityIdentityDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])); //(options) => ....

// 2� passo: indicar o "conjunto de regras" devidamente referenciado para - posteriormente ser possivel executar a total manipula��o da base de dados
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
//0� passo: adicionar o m�todo que auxiliar� a aplica��o nos processos de autentica��o de usuarios para acesso a �reas restritas
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
