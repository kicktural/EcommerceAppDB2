using AutoMapper;
using Business.Abstract;
using Business.AutoMapper;
using Business.Concreate;
using DataAccess.Abstract;
using Business.DependencyResolver;
using DataAccess.Conceate.EntityFramework;
using Entities.Concreate;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddDefaultIdentity<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDBContext>();


builder.Services.Create();

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
//app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}/{SeoUrl?}"
    );
});




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{SeoUrl?}");

app.Run();
