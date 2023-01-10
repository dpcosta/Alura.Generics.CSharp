namespace Alura.Generics.CSharp;

internal class Musica
{
    public Musica(string nome, string autor)
    {
        Nome = nome;
        Autor = autor;
    }

    public string Nome { get; }
    public string Autor { get; }
}

/// <summary>
/// Lista de músicas para uma playlist qualquer.
/// </summary>
internal class Playlist
{
    public void Incluir(Musica musica)
    {

    }
}
