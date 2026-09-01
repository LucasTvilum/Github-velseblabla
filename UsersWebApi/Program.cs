using UsersWebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Registrerer repository i DI-containeren, så Controlleren kan bruge den via constructor injection
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger er altid slået til her (uanset miljø) - nemmere til læring/test.
// I et rigtigt produktionsprojekt vil man normalt kun have det tændt i Development.
app.UseSwagger();
app.UseSwaggerUI();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
app.Run();