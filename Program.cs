using HR_API.Services;
using HR_API.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "HR API",
            Description = "API's",
            TermsOfService = new Uri("https://editor.swagger.io/")
        })
);
builder.Services.AddDbContext<HR_API.Models.ErptestingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("hrDB"));
});

//API Services
builder.Services.AddScoped<IEmployeeManagementServices, EmployeeManagementServices>();
builder.Services.AddScoped<IPayrollProccessingServices, PayrollProccessing>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;

});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller?}/{action?}/{id?}"
 ).WithStaticAssets();

app.Run();
