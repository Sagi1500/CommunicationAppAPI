
using CommunicationAppApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using CommunicationAppApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSignalR();


builder.Services.AddDbContext<RatingsContext>();
//builder.Services.AddDbContext<ContactsContext>();
builder.Services.AddDbContext<UsersContext>();
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
    pattern: "{controller=Users}/{action=Index}/{id?}");


//app.UseEndpoints(endpoints =>
//{ 
//endpoints.MapHub<MyHub>("/myHub");
//});
app.Run();
