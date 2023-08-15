using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using VitaLabAPI;
using VitaLabAPI.DataBaseAccsess;
using VitaLabAPI.Repositories.OrderDatas;
using VitaLabAPI.Repositories.Orders;
using VitaLabAPI.Repositories.Products;
using VitaLabAPI.Repositories.UserRoles;
using VitaLabAPI.Repositories.Users;
using VitaLabAPI.Services.AuthorizationServices;
using VitaLabAPI.Services.OrderDatas;
using VitaLabAPI.Services.Orders;
using VitaLabAPI.Services.Products;
using VitaLabAPI.Services.UserRoles;
using VitaLabAPI.Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
    .AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "VitaLadApiProvider",
        ValidAudience = "VitaLabApiUsers",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("Tokens:AuthToken").Value)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy(IdentityData.AdministratorPolicyName, p =>
    {
        p.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        p.RequireAuthenticatedUser();
        p.RequireRole(IdentityData.AdministratorClaimName);
    });
    opt.AddPolicy(IdentityData.ProductManagerPolicyName, p =>
    {
        p.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        p.RequireAuthenticatedUser();
        p.RequireRole(IdentityData.ProductManagerClaimName);
    });
});

//Add database context
builder.Services.AddSingleton<DapperContext>();

//Add repositories
builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderDataRepository, OrderDataRepository>();

//Add services
builder.Services.AddTransient<IUserRoleService, UserRoleService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderDataService, OrderDataService>();
builder.Services.AddTransient<IVitaLabAuthorizationService, VitaLabAuthorizationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SchemaFilter<ExampleSchemaFilter>();
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/");

app.Run();
