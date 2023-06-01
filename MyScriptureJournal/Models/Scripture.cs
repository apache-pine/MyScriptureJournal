using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyScriptureJournal.Models
{
    public class Scripture
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Book field is required.")]
        public string? Book { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "The Chapter field must be a positive number.")]
        public int Chapter { get; set; }

        [StringLength(50, ErrorMessage = "The Verse field cannot exceed 50 characters."), RegularExpression(@"^\d+(-\d+)?$", ErrorMessage = "Invalid verse format.")]
        public string? Verse { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "The Notes field cannot exceed 500 characters.")]
        public string? Notes { get; set; } = string.Empty;

        [ScaffoldColumn(false), Display(AutoGenerateField = false, Name = "Entry Date"), DataType(DataType.Date)]
        public DateTime EntryDate { get; set; } = DateTime.Now;
    }
}
