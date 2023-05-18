using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using BHDStarBooking.Config;
using BHDStarBooking.Config.MongoConfig;
using BHDStarBooking.Config.SharePointConfig;
using BHDStarBooking.Entity;
using BHDStarBooking.Entity.SharePoint;
using BHDStarBooking.ExceptionHandler;
using BHDStarBooking.Mapper;
using BHDStarBooking.Repository;
using BHDStarBooking.Repository.CustomRepository;
using BHDStarBooking.Service.IService;
using BHDStarBooking.Service.ServiceImpl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

// Add mongoIdentityConfiguration
var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings
    {
        ConnectionString = builder.Configuration.GetSection("myDB:ConnectionString").Value,
        DatabaseName = builder.Configuration.GetSection("myDB:DatabaseName").Value

    },
    IdentityOptionsAction = options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;

        // lock out
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.User.RequireUniqueEmail = true;
    }
};


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWT_Token_V1",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token with Bearer formatter"
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

// Configure Database Resource
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("myDB"));

builder.Services.Configure<SharePointSetting>(
    builder.Configuration.GetSection("mySharePoint")
    );


builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JWTConfig"));
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JWTConfig:Secretkey").Value);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };

});


//Add mapper to DI Container
builder.Services.AddAutoMapper(typeof(MovieProfile));

// Add Repository Layer to DI Container
builder.Services.AddSingleton<IMongoBaseContext,MongoContext>();
builder.Services.AddSingleton<ISharePointBaseContext, SharePointContext>();
builder.Services.AddScoped<IMongoRepository<AccountEntity>,MongoRepository<AccountEntity>>();
builder.Services.AddScoped<IMongoRepository<RoleEntity>, MongoRepository<RoleEntity>>();
builder.Services.AddScoped<IMongoRepository<UserEntity>, MongoRepository<UserEntity>>();
builder.Services.AddScoped<IMongoRepository<MovieEntity>, MongoRepository<MovieEntity>>();
builder.Services.AddScoped<IMongoRepository<CinemaEntity>, MongoRepository<CinemaEntity>>();
builder.Services.AddScoped<IMongoRepository<RoomEntity>, MongoRepository<RoomEntity>>();
builder.Services.AddScoped<IMongoRepository<SeatEntity>, MongoRepository<SeatEntity>>();
builder.Services.AddScoped<IMongoRepository<SessionEntity>, MongoRepository<SessionEntity>>();
builder.Services.AddScoped<IMongoRepository<SeatOnSessionEntity>, MongoRepository<SeatOnSessionEntity>>();
builder.Services.AddScoped<IMongoRepository<ServiceEntity>, MongoRepository<ServiceEntity>>();
builder.Services.AddScoped<IMongoRepository<Receipt>, MongoRepository<Receipt>>();
builder.Services.AddScoped<ISharePointRepository<RoleEntity>, SharePointRepository<RoleEntity>>();
builder.Services.AddScoped<ISharePointRepository<SPAccountEntity>, SharePointRepository<SPAccountEntity>>();
builder.Services.AddScoped<ISharePointRepository<Role_Account>, SharePointRepository<Role_Account>>();
builder.Services.AddScoped<ISharePointRepository<MovieEntity>, SharePointRepository<MovieEntity>>();
builder.Services.AddScoped<ISharePointRepository<SPSessionEntity>, SharePointRepository<SPSessionEntity>>();
builder.Services.AddScoped<CustomAccountRepository>();
builder.Services.AddScoped<CustomRole_AccountRepository>();
builder.Services.AddScoped<ISharePointRepository<SPUserEntity>, SharePointRepository<SPUserEntity>>();
builder.Services.AddScoped<ISharePointRepository<CinemaEntity>, SharePointRepository<CinemaEntity>>();
builder.Services.AddScoped<ISharePointRepository<SPRoomEntity>, SharePointRepository<SPRoomEntity>>();
builder.Services.AddScoped<ISharePointRepository<SPSeatEntity>, SharePointRepository<SPSeatEntity>>();
builder.Services.AddScoped<ISharePointRepository<ServiceEntity>, SharePointRepository<ServiceEntity>>();
builder.Services.AddScoped<ISharePointRepository<SPAccount>, SharePointRepository<SPAccount>>();

// Add Service Layer to DI Container

builder.Services.AddScoped<SharePointIndex>();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<IRoleService,RoleService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<ICinemaService, CinemaService>(); 
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<ISeatOnSessionService, SeatOnSessionService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IReceipt, ReceiptService>();
builder.Services.AddScoped<IRole_Account, Role_AccountService>();
builder.Services.AddScoped<ITestService, TestService>();

// Add unique index to DI Container
MongoIndex mongoIndex = new MongoIndex(builder.Configuration);
mongoIndex.CreateUniqueIndex();


// Add Share Point index to DI Container
var serviceProvider = builder.Services.BuildServiceProvider();
var sharePointIndex = serviceProvider.GetService<SharePointIndex>();
sharePointIndex.createPrimary();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleWare>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();/*
app.UseCors("CorsPolicy");*/
app.UseCors(builder => builder.
    WithOrigins("http://localhost:3000")
   .WithExposedHeaders("Authorization")
    .AllowCredentials()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.Run();
