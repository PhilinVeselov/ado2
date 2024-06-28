var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Добавляем маршруты для контроллера TestController
app.MapControllerRoute(
    name: "text",
    pattern: "Test/Text",
    defaults: new { controller = "Test", action = "Text" });

app.MapControllerRoute(
    name: "html",
    pattern: "Test/Html",
    defaults: new { controller = "Test", action = "Html" });

app.MapControllerRoute(
    name: "json",
    pattern: "Test/Json",
    defaults: new { controller = "Test", action = "Json" });

app.MapControllerRoute(
    name: "file",
    pattern: "Test/File",
    defaults: new { controller = "Test", action = "File" });

app.MapControllerRoute(
    name: "status",
    pattern: "Test/Status",
    defaults: new { controller = "Test", action = "Status" });

app.MapControllerRoute(
    name: "cookie",
    pattern: "Test/Cookie",
    defaults: new { controller = "Test", action = "Cookie" });

app.Run();
