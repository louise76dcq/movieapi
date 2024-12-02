using System.ComponentModel.DataAnnotations;

public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom du film est requis.")]
    public string? MovieName { get; set; }

    [Range(1888, 2100, ErrorMessage = "L'année du film doit être comprise entre 1888 et 2100.")]
    public int MovieYear { get; set; }

    [Required(ErrorMessage = "Le nom du réalisateur est requis.")]
    public string? MovieDirector { get; set; }
}
