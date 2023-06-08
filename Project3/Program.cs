using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project3.Migrations;
using Project3.Repositories;
using Project3.Services;
using Project3.Utils;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
#region Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
// Add Jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
               .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

#region Blog
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogRepo, BlogRepo>();
#endregion

#region Security
builder.Services.AddScoped<ISecurityService, SecurityService>();
#endregion

#region CategoryBlog
builder.Services.AddScoped<ICategoryBlogService, CategoryBlogService>();
builder.Services.AddScoped<ICategoryBlogRepo, CategoryBlogRepo>();
#endregion 

#region Role
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();
#endregion

#region User
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
#endregion

#region Subject
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ISubjectRepo, SubjectRepo>();
#endregion

#region AnswerQuestion
builder.Services.AddScoped<IAnswerQuestionService, AnswerQuestionService>();
builder.Services.AddScoped<IAnswerQuestionRepo, AnswerQuestionRepo>();
#endregion

#region AnswerQuestionChose
builder.Services.AddScoped<IAnswerQuestionChoseService, AnswerQuestionChoseService>();
builder.Services.AddScoped<IAnswerQuestionChoseRepo, AnswerQuestionChoseRepo>();
#endregion

#region Infomation
builder.Services.AddScoped<IInformationStudentService, InformationStudentService>();
builder.Services.AddScoped<IInformationStudentRepo, InformationStudentRepo>();
#endregion

#region Exam
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IExamRepo, ExamRepo>();
#endregion

#region Question
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionRepo, QuestionRepo>();
#endregion

#region Course
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseRepo, CourseRepo>();
#endregion

builder.Services.AddScoped<IUserRoleRepo, UserRoleRepo>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<Project3Context>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
