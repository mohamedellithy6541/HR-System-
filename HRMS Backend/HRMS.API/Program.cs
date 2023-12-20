using HRMS.API;
using HRMS.Application;
using HRMS.Application.Services.UsereService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HRMS.Application.Context;
using HRMS.Application.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using HRMS.Application.Repository.SalaryRepository;
using Microsoft.AspNetCore.Authorization;

string MyAllowSpecificOrigins = "m";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<DBContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DB")));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<DBContext>();

builder.Services.AddScoped(typeof(IGenaricrepository<>), typeof(GenaricRepository<>));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.Load("HRMS.Application")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();

builder.Services.AddScoped<ISeasonalVacationRepository, SeasonalVacationRepository>();
builder.Services.AddScoped<IGeneralSettingRepository, GeneralSettingRepository>();


builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

builder.Services.AddScoped<ISalaryRepository,SalaryRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(options =>
{
	options.AddPolicy(MyAllowSpecificOrigins,
	builder =>
	{
		builder.AllowAnyOrigin().WithExposedHeaders("next"); ;
		builder.AllowAnyMethod();
		builder.AllowAnyHeader();
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
	
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);
/*using (var serviceScope = app.Services.CreateScope())
{

    var dbContext = serviceScope.ServiceProvider.GetRequiredService<DBContext>();
    var serviceProvider = serviceScope.ServiceProvider;
    
    SeedContext.Seed(dbContext, serviceProvider);
}*/
app.MapControllers();

SeedDataBase.SeedAdminAndRoles(app).Wait();

app.Run();
