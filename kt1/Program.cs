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

app.MapControllerRoute(
    name: "Welcome",
    pattern: "{controller=Pages}/{action=Welcome}/{id?}");
app.MapControllerRoute(
    name: "Greet",
    pattern: "{controller=Pages}/{action=Greet}/{id}");
app.MapControllerRoute(
    name: "edit",
    pattern: "{controller=Pages}/{action=Edit}/{id?}");

app.Run();
