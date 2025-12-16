using CommunityNoticeBoard.Application.Behaviors;
using CommunityNoticeBoard.Application.Features.User.Commands.CreateUser;
using CommunityNoticeBoard.Application.IRepository;
using CommunityNoticeBoard.Domain.Entities;
using CommunityNoticeBoard.Infrastructure.Persistence.Data;
using CommunityNoticeBoard.Infrastructure.Repository;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add DbContext
builder.Services.AddDbContext<AppDbContext>();
// Add Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserReposistory, UserRepository>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IRefreshReposistary, RefreshReposistary>();
// Add PasswordHasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
// Add MediatR
builder.Services.AddMediatR(cfg => {

cfg.RegisterServicesFromAssembly(Assembly.Load("CommunityNoticeBoard.Application")
    );
    });
// Add FluentValidation
builder.Services.AddControllers();
// Add ValidationBehavior to MediatR pipeline
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(Assembly.Load("CommunityNoticeBoard.Application"));
// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowAngularOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAngularOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
