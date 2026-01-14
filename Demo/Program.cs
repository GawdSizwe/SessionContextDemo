using System.Net.NetworkInformation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


// In memory cache
builder.Services.AddDistributedMemoryCache();

// SQL Server cache
// to check run command in PowerShell or CMD: dotnet sql-cache
// To install SQL Server: dotnet tool install --global dotnet-sql-cache
// create session table: dotnet sql-cache create "Data Source=(local);Initial Catalog=m02;TrustServerCertificate=true;Trusted_Connection=True;MultipleActiveResultSets=true" dbo SessionState
//builder.Services.AddDistributedSqlServerCache(options =>
//{
//    options.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//    options.SchemaName = "dbo";
//    options.TableName = "SessionState";
//});


//Redis Cache: Using the Azure portal, add the cache to your subscription.
//Grab the connection string (AccessKey) and place it in appsettings.json
//Add Microsoft.Extensions.Caching.StackExchange NuGet Package, then add:
// builder.Services.AddStackExchangeRedisCache(options => {
//    options.Configuration = builder.Configuration.GetConnectionString("AzureRedis");
//});


builder.Services.AddSession();

// This is to access the HttpContext in the Razor Page 
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
