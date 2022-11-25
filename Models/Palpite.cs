namespace API_Copa.Models
{
	public class Palpite
	{
		public int Id { get; set; }
		public int GolA { get; set; }
		public int GolB { get; set; }
		public virtual Jogo Jogo { get; set; }
	}
}