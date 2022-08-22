using FluentValidation.AspNetCore;
using ForumAPI.Cache.Concrete;
using ForumAPI.Cache.Interfaces;
using ForumAPI.Cache.Redis;
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
builder.Services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true;
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(x =>
x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IQuestionRepository, QuestionRepository>();
builder.Services.AddTransient<IAnswerRepository, AnswerRepository>();
builder.Services.AddTransient<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddTransient<IVoteRepository, VoteRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<IAnswerService, AnswerService>();
builder.Services.AddTransient<IVoteService, VoteService>();
builder.Services.AddTransient<IRedisCache, RedisCacheManager>();
builder.Services.AddTransient<IFavoriteCache, FavoriteCache>();
builder.Services.AddTransient<IQuestionDetailCache, QuestionDetailCache>();
builder.Services.AddTransient<IQuestionsCache, QuestionsCache>();
builder.Services.AddTransient<IVoteCache, VoteCache>();

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
