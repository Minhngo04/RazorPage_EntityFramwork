using Microsoft.EntityFrameworkCore;
using PRN221PE_FA22_TrialTest_NgoQuangMinh.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSession(); // Chuyển dòng này vào builder.Services
builder.Services.AddSignalR();
builder.Services.AddDbContext<CandidateManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnect")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
