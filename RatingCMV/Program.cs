
using CommunicationAppApi;
//using CommunicationAppApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSignalR();
builder.Services.AddDbContext<RatingsContext>();
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
    pattern: "{controller=Ratings}/{action=Index}/{id?}");
//app.UseEndpoints(endpoints =>
//{ 
//endpoints.MapHub<MyHub>("/myHub");
//});
app.Run();
