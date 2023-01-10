# Generics no C#

### Motiva��o

Extra�do do [documento da Microsoft](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics#generics-overview):

- Maximizar reuso de c�digo, �type safety� e performance
- Uso mais comum � para projetar cole��es de classes
- Generics pode ser usado em interfaces, classes, m�todos, eventos e delegates
- � poss�vel restringir a declara��o de um generic para facilitar acesso a propriedades e m�todos
- Utilize System.Reflection para obter informa��o sobre o tipo usado em execu��o

# Contexto

Temos um projeto console com algumas cole��es. A classe `Program` e seu m�todo `Main()` � utilizado como *sandbox*. As cole��es declaradas s�o: registro de anivers�rios, hist�rico de compras de ra��o para os cachorros aqui de casa, a playlist de m�sicas favoritas e alguns perfis que sigo no twitter. Temos anivers�rios, hist�rico de compras, m�sicas e perfis do twitter (repare no plural). Sim, temos aqui uma cole��o para cada um destes *conceitos*, novamente: anivers�rio, m�sica, hist�rico de compra, perfil twitter (aqui no singular!).

Vejam o c�digo que escrevi referente a estas cole��es. 

Lindo! Mas� incompleto. Imaginem que enjoei de alguma m�sica e queira remov�-la da playlist. Ou deixei de seguir algu�m. Preciso criar um m�todo para remover o elemento.

Ok. Mas parece que meu projeto est� estranho. O que acham?

# Problema

Todas estas classes que representam cole��es possuem comportamento igual ou bem parecido. Percorrer, adicionar, remover. Poderia pensar em outros como atualizar, recuperar um elemento espec�fico, dentre outros.

Um dos problemas disso � o retrabalho. Para quatro cole��es, escrevi quatro m�todos com o mesmo comportamento. E se houvessem muitas cole��es? Tamb�m para corrigir defeitos no c�digo eu precisaria revisar todas as cole��es.

Outro problema � que para estender minhas cole��es eu precisaria criar nova classe e da� copiar e colar o c�digo com este comportamento de cole��es.

Acho que concordamos que seria importante isolar, centralizar, combinar este comportamento em um classe �nica. E que permita gerar novos tipos de cole��es sem stress.

S� que agora eu quebrei os exemplos que estavam funcionando! 

Ser� que consigo achar alguma solu��o para seguir com esta ideia de ter uma classe �nica para representar a lista?

Vamos explorar algumas alternativas�

# Solu��es alternativas

Recapitulando: quero isolar o comportamento de uma cole��o em uma classe �nica E quero que o projeto volte a compilar.

### Alternativa 1

Primeira ideia seria usar o tipo mais ancestral da linguagem, o `object`. Vamos ver. Conseguimos fazer o projeto voltar a compilar! 

Qual a desvantagem desta abordagem?

Eu perco a especificidade da minha lista. Numa mesma inst�ncia consigo adicionar objetos de qualquer tipo. E depois para manipul�-los?

### Alternativa 2

Segunda ideia seria utilizar uma hierarquia de classes para os elementos. Se eu tenho cole��o tamb�m poderia ter item, certo? Deste jeito tamb�m consigo fazer o projeto compilar.

Qual a desvantagem desta abordagem?

Eu for�o que cada poss�vel elemento seja descendente deste ancestral. Do ponto de vista conceitual, � uma informa��o equivocada (*uma m�sica n�o � um elemento*). Costumamos dizer que existe grande acoplamento.

Al�m disso, restrinjo a solu��o de cole��o para projetos propriet�rios (aqueles em que eu tenho controle do c�digo) porque eu n�o conseguiria imp�r esta hierarquia em classes que n�o tenho acesso.

Usando interfaces ajudaria a aliviar a quest�o hier�rquica discutida acima mas ainda manteria nossa solu��o restrita a projetos propriet�rios. Mas existe ainda outro tipo de relacionamento entre tipos.

### Alternativa 3

Nessa alternativa eu criaria a classe de cole��o e adicionaria um par�metro de tipo para o elemento. A sintaxe para isso � a nota��o diamante `<>`. e dentro do diamante um texto que pudesse ser usado em outras partes da classe. 

```csharp
class Colecao<T>
{
	public void Incluir(T item) { }
	public void Remover(T item) { }
	public T Recuperar(int index) { }
}
```

Em geral usamos uma letra �nica em caixa alta, e a letra mais usada para isso � o T. Outros cen�rios exigem que usemos palavras como TInput, TOutput, dentre outras.

Observem bem as classes de cole��o originais. Reparem que as �nicas partes que diferem em todas estas classes de cole��o � a indica��o do tipo nos m�todos e propriedades! O que fizemos foi extrair este tipo para um par�metro. Baseado nisso, o nome do recurso de linguagem que estamos utilizando � chamado **Generic Type Parameter**.

Usando este recurso do C# a gente garante uma classe �nica para representar o conceito de cole��es al�m de manter m�xima flexibilidade no tipo do elemento.

E como eu uso um tipo assim?

# Utilizando generics

No meu m�todo Main eu preciso declarar qual o tipo do elemento que minha cole��o vai ter.

```csharp
Colecao<DateTime> aniversarios = new Colecao<DateTime>();
Colecao<string> perfis = new Colecao<string>();
```

Tamb�m posso criar propriedades destes tipos em outras classes.

```csharp
class PlayerMusica
{
	public Colecao<Musica> Playlist { get; set; }
}
```

Vale lembrar que tipos gen�ricos podem ser usados tanto em classes quanto interfaces, al�m de m�todos e delegates.

# Exemplos de Generics em projetos reais

O principal exemplo est� justamente nos tipos que representam cole��es, quase todos declarados no namespace **System.Collection.Generics**. Temos diversas classes e interfaces que utilizam este recurso para garantir reuso de c�digo de forma extens�vel. Alguns exemplos:

- `ICollection<T>`
- `List<T>`
- `IEnumerable<T>`
- `Set<T>`
- `Dictionary<TKey,TValue>`

Contudo, existem in�meros outros exemplos dentro da biblioteca de tipos do C# que utilizam generics. Sempre com este objetivo: reuso de c�digo e extensibilidade.

System - IComparable<T>

Aspnet Core - PageModel<T>

Entity Framework - DbSet<T>

Microsoft.Extensions.Logging - ILogger<T>

System - Func<T,TResult>

System.Threading.Tasks - Task<T>