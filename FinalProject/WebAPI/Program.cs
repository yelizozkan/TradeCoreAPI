using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.IdentityModel.Tokens;
using Core.Utilities.Security.JWT;
using Core.Utilities.Security.Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Core.Utilities.IoC;
using Core.Extensions;
using Core.DependencyResolvers;


var builder = WebApplication.CreateBuilder(args);

//Autofac entegrasyonu 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule());
});

builder.Services.AddCors();

// TokenOptions yapýlandýrmasýný appsettings.json'dan okuma
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

// JWT kimlik doðrulama ayarlarýný yapýlandýrma
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,    
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });


// AOP ve DI yapýlandýrmasý
ServiceTool.Create(builder.Services);

//AOP ve DI yapýlandýrmasý 
//builder.Services.AddSingleton<IProductService, ProductManager>();
//builder.Services.AddSingleton<IProductDal, EfProductDal>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Configure additional services or dependency resolvers
builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.ConfigureCustomExceptionMiddleware();

app.UseCors(builder=>builder.WithOrigins("http://localhost:8080").AllowAnyHeader());
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// JWT kimlik doðrulamayý aktif etme
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();
