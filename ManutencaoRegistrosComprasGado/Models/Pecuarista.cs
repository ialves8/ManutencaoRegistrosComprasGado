using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManutencaoRegistrosComprasGado.Models
{
    public class Pecuarista
    {
        public int PecuaristaId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(50, ErrorMessage = "Use menos caracteres.")]
        public string Nome { get; set; }

        public ICollection<CompraGado> CompraGados { get; set; }
    }
}
