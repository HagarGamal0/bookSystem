using bookSystem.Models;
using bookSystem.Repositries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;                
    options.Password.RequireLowercase = false;          
    options.Password.RequireUppercase = false;         
    options.Password.RequireNonAlphanumeric = false;    
    options.Password.RequiredLength = 6;              
}).AddEntityFrameworkStores<LibraryContext>();

// ✅ Register the DbContext
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
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
