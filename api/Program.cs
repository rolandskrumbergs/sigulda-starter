using ApiDemoProject;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("All", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("All");

// GET
// Izgūt visas skolas kā sarakstu no datubāzes
app.MapGet("/skolas", () =>
{
    SkolaRepository skolaRepository = new SkolaRepository();

    List<Skola> skolas = skolaRepository.GetAll();

    return skolas;
});


// GET
// Izgūt visas skolas kā sarakstu no datubāzes
app.MapGet("/skolas/{id}", (int id) =>
{
    SkolaRepository skolaRepository = new SkolaRepository();

    Skola skola = skolaRepository.FindById(id);

    return skola;
});

// POST
// Pievienot jaunu skolu
app.MapPost("/skolas", (Skola skola) =>
{
    SkolaRepository skolaRepository = new SkolaRepository();

    skolaRepository.Add(skola);
});


// GET
// Izgūt visus sludinājumus kā sarakstu no datubāzes
app.MapGet("/sludinajumi", () =>
{
    SludinajumuRepository sludinajumiRepository = new SludinajumuRepository();

    List<Sludinajums> sludinajumi = sludinajumiRepository.GetAll();

    return sludinajumi;
});

app.MapPost("/sludinajumi", (Sludinajums sludinajums) =>
{
    SludinajumuRepository sludinajumiRepository = new SludinajumuRepository();

    sludinajumiRepository.Add(sludinajums);
});


app.MapPost("/post", () => "Hello from POST!");

// PUT
app.MapPut("/", () => "Hello from root PUT!");
app.MapPut("/put", () => "Hello from PUT!");

// DELETE
app.MapDelete("/", () => "Hello from root DELETE!");
app.MapDelete("/delete", () => "Hello from DELETE!");

app.Run();
