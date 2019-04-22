using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManutencaoRegistrosComprasGado.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(300, ErrorMessage = "Use menos caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "Preço inválido.")]
        [Display(Name = "Preço")]
        public double Preco { get; set; }

        public ICollection<CompraGadoItem> CompraGadoItens { get; set; }
    }
}
