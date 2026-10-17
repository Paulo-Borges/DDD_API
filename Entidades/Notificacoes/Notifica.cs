using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.Notificacoes
{
    public class Notifica
    {
        public Notifica()
        {
            Notificacoes = new List<Notifica>();
        }

        [NotMapped]
        public string NomePropriedade { get; set; } = string.Empty;
        [NotMapped]
        public string Mensagem { get; set; } = string.Empty;
        [NotMapped]
        public List<Notifica> Notificacoes;

        public bool ValidarPropriedadeString(string valor, string nomePropriedade, string mensagem)
        {
            if (string.IsNullOrEmpty(valor))
            {
                Notificacoes.Add(new Notifica { NomePropriedade = nomePropriedade, Mensagem = mensagem });
                return false;
            }
            return true;
        }
    }
}
