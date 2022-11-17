using Microsoft.AspNetCore.Authentication.Cookies;
using RabbitMQMvc.Controllers;
using RabbitMQMvc.Data;
using RabbitMQMvc.RabitMQ;
using RabitMqMessageAPI.RabitMQ;
using RabitMqMessageAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IRabitMQProducer, RabitMQProducer>();
builder.Services.AddDbContext<DbContextClass>();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => {x.LoginPath= "/Home/Login";});
builder.Services.AddSignalR();
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
app.MapHub<Chat>("/chat");
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
