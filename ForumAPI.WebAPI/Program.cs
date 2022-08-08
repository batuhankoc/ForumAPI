using FluentValidation.AspNetCore;
using ForumAPI.Data.Abstract;
using ForumAPI.Data.Concrete;
using ForumAPI.Data.Entity;
using ForumAPI.Service.Abstract;
using ForumAPI.Service.Concrete;
using ForumAPI.Service.Mapping;
using ForumAPI.Validation.FluentValidation;
using ForumAPI.WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter= true);

builder.Services.AddFluentValidation(u => u.RegisterValidatorsFromAssemblyContaining<AddUserContractValidator>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(x =>
x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();

builder.Services.AddTransient<IAnswerRepository, AnswerRepository>();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IQuestionService, QuestionService>();

builder.Services.AddTransient<IAnswerService, AnswerService>();

builder.Services.AddAutoMapper(typeof(MapProfile));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
