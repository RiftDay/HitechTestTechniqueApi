using System.ComponentModel.DataAnnotations;
using HitechSoftware.TestTechnique.Donnees.Validation;

namespace HitechSoftware.TestTechnique.Modeles;

[ValidationDepense]
public class Depense
{
    [Key]
    public Guid Id { get; init; }

    public required decimal Montant { get; set; }
    public required DateOnly Date { get; set; }
    public required string Commentaire { get; set; }
    public required TypeDepense Type { get; set; }

    public uint? DistanceKm { get; set;}
    public ushort? NombreInvites { get; set; }
}
