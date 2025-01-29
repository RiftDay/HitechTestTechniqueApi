using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

using HitechSoftware.TestTechnique.Modeles;
using HitechSoftware.TestTechnique.Donnees;


string route = "/depenses";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration["ChaineConnexionBdd"]);
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.ConfigureHttpJsonOptions(opts => opts.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(opts =>
{
    opts
        .WithTitle("API Test technique HitechSoftware")
        .WithTheme(ScalarTheme.Moon)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

app.MapGet(route, async ([FromQuery(Name = "numPage")] int numPage,
                         [FromQuery(Name = "nbreElemsPage")] int nbreElemsPage,
                         [FromServices] AppDbContext db) =>
{
    int decalage = (numPage - 1) * (nbreElemsPage);

    return await db.Depenses
        .OrderBy(d => d.Date)
        .Skip(decalage)
        .Take(nbreElemsPage)
        .ToListAsync();
}).WithName("GetDepenses");


app.MapGet(route + "/{id:guid}", async ([FromRoute] Guid id, [FromServices] AppDbContext db) =>
    await db.Depenses.FindAsync(id) is Depense depense ?
        Results.Ok(depense) : Results.NotFound()).WithName("GetDepense");


app.MapPost(route, async (Depense depense, [FromServices] AppDbContext db) =>
{
    ValidationContext contexte = new(depense);
    List<ValidationResult> resultats = new();

    if (!Validator.TryValidateObject(depense, contexte, resultats, validateAllProperties: false))
    {
        var erreurs = resultats
            .GroupBy(e => e.MemberNames.FirstOrDefault() ?? "Erreur")
            .ToDictionary(
                g => g.Key,
                g => g.Select(r => r.ErrorMessage ?? "Échec de la validation").ToArray()
            );

        return Results.ValidationProblem(erreurs);
    }

    db.Depenses.Add(depense);
    await db.SaveChangesAsync();

    return Results.Created($"{ route }/{ depense.Id }", depense);
}).WithName("AjouterDepense");


app.MapDelete(route + "/{id:guid}", async ([FromRoute] Guid id, [FromServices] AppDbContext db) =>
{
    if (await db.Depenses.FindAsync(id) is Depense depense)
    {
        db.Depenses.Remove(depense);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
}).WithName("SupprimerDepense");


app.Run();