using ECommerceMVC.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Lấy connection string từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("Hshop2023Context");

// 🔹 Đăng ký DbContext
builder.Services.AddDbContext<Hshop2023Context>(options =>
    options.UseSqlServer(connectionString));
// 🔹 Kích hoạt Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 🔹 Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔹 Cấu hình pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();
// ✅ Kích hoạt routing cho Areas (Admin, ...)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


// 🔹 Định tuyến mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
