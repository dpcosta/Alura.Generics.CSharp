# Generics no C#

### Motivação

Extraído do [documento da Microsoft](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics#generics-overview):

- Maximizar reuso de código, “type safety” e performance
- Uso mais comum é para projetar coleções de classes
- Generics pode ser usado em interfaces, classes, métodos, eventos e delegates
- É possível restringir a declaração de um generic para facilitar acesso a propriedades e métodos
- Utilize System.Reflection para obter informação sobre o tipo usado em execução

# Contexto

Temos um projeto console com algumas coleções. A classe `Program` e seu método `Main()` é utilizado como *sandbox*. As coleções declaradas são: registro de aniversários, histórico de compras de ração para os cachorros aqui de casa, a playlist de músicas favoritas e alguns perfis que sigo no twitter. Temos aniversários, histórico de compras, músicas e perfis do twitter (repare no plural). Sim, temos aqui uma coleção para cada um destes *conceitos*, novamente: aniversário, música, histórico de compra, perfil twitter (aqui no singular!).

Vejam o código que escrevi referente a estas coleções. 

Lindo! Mas… incompleto. Imaginem que enjoei de alguma música e queira removê-la da playlist. Ou deixei de seguir alguém. Preciso criar um método para remover o elemento.

Ok. Mas parece que meu projeto está estranho. O que acham?

# Problema

Todas estas classes que representam coleções possuem comportamento igual ou bem parecido. Percorrer, adicionar, remover. Poderia pensar em outros como atualizar, recuperar um elemento específico, dentre outros.

Um dos problemas disso é o retrabalho. Para quatro coleções, escrevi quatro métodos com o mesmo comportamento. E se houvessem muitas coleções? Também para corrigir defeitos no código eu precisaria revisar todas as coleções.

Outro problema é que para estender minhas coleções eu precisaria criar nova classe e daí copiar e colar o código com este comportamento de coleções.

Acho que concordamos que seria importante isolar, centralizar, combinar este comportamento em um classe única. E que permita gerar novos tipos de coleções sem stress.

Só que agora eu quebrei os exemplos que estavam funcionando! 

Será que consigo achar alguma solução para seguir com esta ideia de ter uma classe única para representar a lista?

Vamos explorar algumas alternativas…

# Soluções alternativas

Recapitulando: quero isolar o comportamento de uma coleção em uma classe única E quero que o projeto volte a compilar.

### Alternativa 1

Primeira ideia seria usar o tipo mais ancestral da linguagem, o `object`. Vamos ver. Conseguimos fazer o projeto voltar a compilar! 

Qual a desvantagem desta abordagem?

Eu perco a especificidade da minha lista. Numa mesma instância consigo adicionar objetos de qualquer tipo. E depois para manipulá-los?

### Alternativa 2

Segunda ideia seria utilizar uma hierarquia de classes para os elementos. Se eu tenho coleção também poderia ter item, certo? Deste jeito também consigo fazer o projeto compilar.

Qual a desvantagem desta abordagem?

Eu forço que cada possível elemento seja descendente deste ancestral. Do ponto de vista conceitual, é uma informação equivocada (*uma música não é um elemento*). Costumamos dizer que existe grande acoplamento.

Além disso, restrinjo a solução de coleção para projetos proprietários (aqueles em que eu tenho controle do código) porque eu não conseguiria impôr esta hierarquia em classes que não tenho acesso.

Usando interfaces ajudaria a aliviar a questão hierárquica discutida acima mas ainda manteria nossa solução restrita a projetos proprietários. Mas existe ainda outro tipo de relacionamento entre tipos.

### Alternativa 3

Nessa alternativa eu criaria a classe de coleção e adicionaria um parâmetro de tipo para o elemento. A sintaxe para isso é a notação diamante `<>`. e dentro do diamante um texto que pudesse ser usado em outras partes da classe. 

```csharp
class Colecao<T>
{
	public void Incluir(T item) { }
	public void Remover(T item) { }
	public T Recuperar(int index) { }
}
```

Em geral usamos uma letra única em caixa alta, e a letra mais usada para isso é o T. Outros cenários exigem que usemos palavras como TInput, TOutput, dentre outras.

Observem bem as classes de coleção originais. Reparem que as únicas partes que diferem em todas estas classes de coleção é a indicação do tipo nos métodos e propriedades! O que fizemos foi extrair este tipo para um parâmetro. Baseado nisso, o nome do recurso de linguagem que estamos utilizando é chamado **Generic Type Parameter**.

Usando este recurso do C# a gente garante uma classe única para representar o conceito de coleções além de manter máxima flexibilidade no tipo do elemento.

E como eu uso um tipo assim?

# Utilizando generics

No meu método Main eu preciso declarar qual o tipo do elemento que minha coleção vai ter.

```csharp
Colecao<DateTime> aniversarios = new Colecao<DateTime>();
Colecao<string> perfis = new Colecao<string>();
```

Também posso criar propriedades destes tipos em outras classes.

```csharp
class PlayerMusica
{
	public Colecao<Musica> Playlist { get; set; }
}
```

Vale lembrar que tipos genéricos podem ser usados tanto em classes quanto interfaces, além de métodos e delegates.

# Exemplos de Generics em projetos reais

O principal exemplo está justamente nos tipos que representam coleções, quase todos declarados no namespace **System.Collection.Generics**. Temos diversas classes e interfaces que utilizam este recurso para garantir reuso de código de forma extensível. Alguns exemplos:

- `ICollection<T>`
- `List<T>`
- `IEnumerable<T>`
- `Set<T>`
- `Dictionary<TKey,TValue>`

Contudo, existem inúmeros outros exemplos dentro da biblioteca de tipos do C# que utilizam generics. Sempre com este objetivo: reuso de código e extensibilidade.

System - IComparable<T>

Aspnet Core - PageModel<T>

Entity Framework - DbSet<T>

Microsoft.Extensions.Logging - ILogger<T>

System - Func<T,TResult>

System.Threading.Tasks - Task<T>