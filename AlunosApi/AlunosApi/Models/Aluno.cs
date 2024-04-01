using System.ComponentModel.DataAnnotations;

namespace AlunosApi.Models
{
    public partial class Aluno
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(80, ErrorMessage = "")]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "")]
        public string Email { get; set; }

        [Required]
        public int Idade { get; set; }

        [Required]
        [Range(1,9999)]
        public int Senha { get; set; }

        public string Foto { get; set; }
    }
}
