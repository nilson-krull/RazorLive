using RazorLive.Data;
using RazorLive.Infra;

var builder = WebApplication.CreateBuilder(args);

// Configura��es de autentica��o para p�ginas Razor
builder.Services.AddRazorPages();

// Adiciona o filtro AuthorizationFilter globalmente
builder.Services.AddMvc(options =>
{
    options.Filters.Add<AuthorizationFilter>(); // Adiciona o filtro como global
});

// Adiciona os servi�os necess�rios
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<AuthorizationFilter>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Middleware para sess�es
app.UseSession();

// Configura��o do pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Sem `app.UseAuthorization()`, pois n�o estamos usando autentica��o formal

// Mapeamento dos Hubs e das P�ginas Razor
app.MapHub<ChatHub>("/chatHub");
app.MapHub<VideoHub>("/videoHub"); // Novo hub para a transmiss�o de v�deo

app.MapRazorPages();

app.Run();
