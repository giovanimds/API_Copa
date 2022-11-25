using System;

namespace API_Copa.Models
{
    public class Jogo
    {
        public Jogo()
        {
            CriadoEm = DateTime.Now;
        }
        public int Id { get; set; }
        public virtual Selecao SelecaoA { get; set; }
        public virtual Selecao SelecaoB { get; set; }
        
        public int GolA { get; set; }
        public int GolB { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
