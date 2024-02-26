var builder = WebApplication.CreateBuilder(args);

//Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

//configurar y agregar un cliente HTTP con nombre "CRMAPI"
builder.Services.AddHttpClient("CRMAP1", c =>
{
    //configurar la direccion base del cliente HTTP desde la configuracion
    c.BaseAddress = new Uri(builder.Configuration["UrlsAPI1:CRM"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
