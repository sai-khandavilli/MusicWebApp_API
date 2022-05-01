using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicWebApp.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AccountDbContext>(config =>
{
    config.UseInMemoryDatabase("Memory");
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(config =>
{
    //config.Password.RequiredLength = 4;
    //config.Password.RequireDigit = false;
    //config.Password.RequireLowercase = false;
    //config.Password.RequireUppercase = false;
    //config.Password.RequireNonAlphanumeric = false;
    //config.SignIn.RequireConfirmedEmail = true;
    config.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AccountDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "Itentity.Cookie";
    config.LoginPath = "/Account/Login";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc(res =>
            res.EnableEndpointRouting = false);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();
