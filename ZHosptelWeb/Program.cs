using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using ZHosptel.Models;
using ZHosptel.Reposetories;
using ZHosptel.Reposetories.DataHalper;
using ZHosptelWeb.DTOs;

internal class Program
{
    private static byte[] GenerateSymmetricKey(string secret)
    {
        // Convert the string secret to bytes using UTF-8 encoding
        byte[] keyBytes = Encoding.UTF8.GetBytes(secret);

        // Ensure the key is at least 256 bits (32 bytes) long
        if (keyBytes.Length < 32)
        {
            // If the key is too short, pad it with zeros or handle it as appropriate for your security requirements
            Array.Resize(ref keyBytes, 32);
        }

        return keyBytes;
    }
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddDbContext<HosptelWebDb>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("HosptelDbConnection")));
        builder.Services.AddIdentity<HosptelUser, IdentityRole>().AddEntityFrameworkStores<HosptelWebDb>();

        builder.Services.AddScoped<IDoctorHalper, DoctorRepo>();
        builder.Services.AddScoped<IEmployeeHalper, EmployeeRepo>();
        builder.Services.AddScoped<IPetienHalper, PatientRepo>();
        builder.Services.AddScoped<IRoomHalper, RoomRepo>();
        builder.Services.AddScoped<IMedicationHalper, MedicationRepo>();
        builder.Services.AddScoped<IAppointmentHalper, AppointmentRepo>();
        builder.Services.AddScoped<IReservationHalper, ReservationRepo>();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{ Title = "API WSVAP (WebSmartView)", Version = "v1" });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                });
        });
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JWT:ValidAudiance"],
                IssuerSigningKey =
                new SymmetricSecurityKey(GenerateSymmetricKey(builder.Configuration["JWT:Secret"]))
            };
        });

        builder.Services.AddCors(corsOptions => {
            corsOptions.AddPolicy("MyPolicy", corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("Doctor", policy => policy.RequireRole("Doctor"));
            options.AddPolicy("Patient", policy => policy.RequireRole("Patient"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "My API V1"); //originally "./swagger/v1/swagger.json"

            });
        }


        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
    
}