


using Fhi.HelseId.Web;
using Fhi.HelseId.Web.Hpr;
using Fhi.HelseId.Web.Services;
using Microsoft.Extensions.Configuration;

var whitelist = new Whitelist();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;



var services = builder.Services;
var redirectPagesKonfigurasjonSeksjon = configuration.GetSection(nameof(RedirectPagesKonfigurasjon));
var redirectPagesKonfigurasjon = redirectPagesKonfigurasjonSeksjon.Get<RedirectPagesKonfigurasjon>();

var hprKonfigurasjonSeksjon = configuration.GetSection(nameof(HprKonfigurasjon));
var hprKonfigurasjon = hprKonfigurasjonSeksjon.Get<HprKonfigurasjon>();

var helseIdKonfigurasjonSeksjon = configuration.GetSection(nameof(HelseIdWebKonfigurasjon));
var helseIdWebKonfigurasjon = helseIdKonfigurasjonSeksjon.Get<HelseIdWebKonfigurasjon>();
services.Configure<HelseIdWebKonfigurasjon>(helseIdKonfigurasjonSeksjon);
services.Configure<RedirectPagesKonfigurasjon>(redirectPagesKonfigurasjonSeksjon);
services.Configure<HprKonfigurasjon>(hprKonfigurasjonSeksjon);
builder.Services.AddSingleton<IWhitelist>(whitelist);
builder.Services.AddScoped<IGodkjenteHprKategoriListe, HprListe>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();



public class HprListe: GodkjenteHprKategoriListe
{}
