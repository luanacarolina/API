using System.ComponentModel.DataAnnotations;

namespace Backend.ViewModels
{
    public class OfertaViewModel
    {
        public string Produto { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "Informa a quantidade de dias")]          
        public string Validade { get; set; }

    }
}   