namespace Alura.Generics.CSharp;

class Program 
{
    public static void Main(string[] args)
    {
        Aniversarios aniversarios = new();
        aniversarios.Incluir(new DateTime(1974, 4, 25));

        Playlist playlist = new();
        playlist.Incluir(new Musica("Blood Brothers", "Iron Maiden"));

        PerfisTwitter perfis = new();
        perfis.Incluir("@algumperfilqualquer");
        
        Console.WriteLine("Aperte qualquer tecla para finalizar o programa...");
        Console.ReadKey();
    }
}
