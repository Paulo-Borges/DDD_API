using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class NoticiaModel
    {
        public int IdNoticia { get; set; }
       
        public string Titulo { get; set; } = string.Empty;
        
        public string Informacao { get; set; } = string.Empty;
        public string IdUsuario { get; set; }
    }
}
