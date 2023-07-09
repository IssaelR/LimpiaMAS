using LimpiaMAS.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Add(new ServiceDescriptor(typeof(iCliente), new ClienteRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(iUsuario), new UserRepository()));
builder.Services.AddControllersWithViews();
builder.Services.Add(new ServiceDescriptor(typeof(iRegister), new RegisterRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(iLimpiador), new LimpiadorRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(iDetalleServicio), new DetalleServicioRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(iLogeo), new LogeoRepository()));
builder.Services.Add(new ServiceDescriptor(typeof(iServicio), new ServicioRepository()));

builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(3600);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession(); //Activar el uso de sesiones

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Limpia}/{action=Index}/{id?}");

app.Run();
