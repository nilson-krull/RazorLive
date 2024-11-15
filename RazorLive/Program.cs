using RazorLive.Data;
using RazorLive.Infra;

var builder = WebApplication.CreateBuilder(args);

// Configurações de autenticação para páginas Razor
builder.Services.AddRazorPages();

// Adiciona o filtro AuthorizationFilter globalmente
builder.Services.AddMvc(options =>
{
    options.Filters.Add<AuthorizationFilter>(); // Adiciona o filtro como global
});

// Adiciona os serviços necessários
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<AuthorizationFilter>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Middleware para sessões
app.UseSession();

// Configuração do pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Sem `app.UseAuthorization()`, pois não estamos usando autenticação formal

// Mapeamento dos Hubs e das Páginas Razor
app.MapHub<ChatHub>("/chatHub");
app.MapHub<VideoHub>("/videoHub"); // Novo hub para a transmissão de vídeo

app.MapRazorPages();

app.Run();
