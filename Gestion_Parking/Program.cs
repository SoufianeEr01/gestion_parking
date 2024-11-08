using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure la cha�ne de connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Ajoutez AppDbContext avec la configuration de la base de donn�es
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Ajouter les contr�leurs et vues
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure le pipeline de requ�tes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
