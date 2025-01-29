using System.ComponentModel.DataAnnotations;
using HitechSoftware.TestTechnique.Modeles;

namespace HitechSoftware.TestTechnique.Donnees.Validation;

public class ValidationDepense : ValidationAttribute
{
    protected override ValidationResult IsValid(object? valeur, ValidationContext contexteValidation)
    {
        Depense depense = (Depense) contexteValidation.ObjectInstance;

        if (depense.Montant == 0)
            return new ValidationResult("Le montant ne peut pas être nul.", [ "Montant" ]);

        if (depense.Montant < 0)
            return new ValidationResult("Le montant doit être strictement positif.", [ "Montant" ]);


        if (depense.Commentaire.Length == 0)
            return new ValidationResult("Un commentaire doit être fourni", [ "Commentaire" ]);
        
        if (depense.Commentaire.Length > 100)
            return new ValidationResult("Le commentaire de doit pas excéder 100 caractères.", [ "Commentaire" ]);


        if (depense.Type == TypeDepense.Deplacement && depense.DistanceKm is null)
            return new ValidationResult("Toute dépense de type Déplacement doit préciser la distance (en kilomètres).", [ "DistanceKm" ]);

        if (depense.Type == TypeDepense.Deplacement && !(depense.NombreInvites is null))
            return new ValidationResult("Une dépense de type Déplacement ne peut pas contenir une valeur pour le nombre d’invités.", [ "NombreInvites" ]);


        if (depense.Type == TypeDepense.Restaurant && depense.NombreInvites is null)
            return new ValidationResult("Toute dépense de type Restaurant doit préciser le nombre d’invités.", [ "NombreInvites" ]);

        if (depense.Type == TypeDepense.Restaurant && !(depense.DistanceKm is null))
            return new ValidationResult("Une dépense de type Restaurant ne peut pas contenir une valeur pour la distance parcourue.", [ "DistanceKm" ]);


        if (!(depense.DistanceKm is null))
            if (depense.DistanceKm! == 0)
                return new ValidationResult("La distance parcourue ne peut pas être égale à zéro.", [ "DistanceKm" ]);

        return ValidationResult.Success!;
    }
}