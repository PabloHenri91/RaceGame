RaceGame
========

MonoGame Windows Race Game

Race Game1
Iniciado 3/12/2014

⦁	Ideia principal:

A ideia principal do jogo é, poder montar carros, personalizar, lutar e correr contra inimigos de igual pra igual, as regras que se aplicam ao jogador também se aplicam aos outros corredores. Um jogo de corrida e tiro entre carros, não um jogo fácil onde o seu carro destrói centenas de inimigos fracos.

Está sendo desenvolvido inicialmente para Windows com o MonoGame e vai ser portado para outras plataformas com o Xamarin.

⦁	Resolução da tela:
A resolução virtual do jogo vai ser 1000 x 700 px.

Esta resolução virtual, no modo tela cheia, vai se adaptar, sem perda de proporção, à qualquer resolução real de tela.

Para testes vou utilizar meu notebook com a resolução de 1366 x 768 e um Windows Phone com 800 x 480.
⦁	Corridas:

No inicio de cada corrida o jogador ganha munição máxima para suas armas que usam munição e sua blindagem e escudo de energia são restaurados.

⦁	Level Up:

O level up é feito automaticamente(???) conforme o jogagor ganha pontos de experiencia matando inimigos.

Após subir o level o jogador aumenta automaticamente(???) os atributos base do carro, baseado no tipo de carro que foi escolhido no inicio da corrida.
Os pontos de atributos podem ser redistribuidos pelo jogador na garagem.

⦁	Level e Peso:

Quanto maior o level, mais peso o carro pode carregar. Com esse peso disponivel o jogador pode, equipar armas, aumentar a blindagem, aumentar o escudo de energia, fazer upgrades no motor, para aumentar a velocidade máxima e aceleração, guardar munição para as armas de munição e equipar baterias de energia. Todos esses itens tem um peso e podem ser equipados e desequipados conforme o peso máximo e a estratégia do jogador.
Com isso tenho as variáveis: peso disponivel, peso máximo e peso atual.

⦁	Armas:

Três tipos básicos de armas: Laser ou Arma de plasma ou Canhão de Plasma, Lança Míssil e Metralhadora ou Canhão de Gauss.
Aqui fiz uma tabela com as armas que vou colocar no jogo:

nome, dano, peso, energia, tempo de recarga, preço, peso da munição, munição por carregador
Small Laser	1	100	1	1s	$1000		-	1
Medium Laser	5	200	6	3s	$5000		-	1
Large Laser	25	1000	12	5s	$25000	-	1

Essas três são armas de energia.

Autocannon		5	1600	4	2s	$5000		2	1
Autocannon x2	15x2	2600	12	4s	$30000	5	2
Autocannon x3	20x3	3200	36	6s	$60000	10	3

Missile		15	600	24	6s	$15000	20	1
Missile x2	15x2	1200	48	6s	$30000	20	2
Missile x3	15x3	1600	60	6s	$45000	20	3

Missile x4	15x4	2200	72	6s	$60000	20	4

Estes numeros provavelmente vão ser alterados para equilibrar a dificuldade do jogo.

⦁	Defesas:
1.	Escudo de energia: O escudo de energia se regenera com o tempo usando a energia das baterias do carro e protege contra armas de energia.

É visualmente invisivel mas brilha quando atingido

Tem dois atributos, poder e recarga. Poder define o número de pontos de vida do escudo e recarga define o intervalo de recarga.
Este intervalo é calculado em milisegundos, da seguinte forma:
(100 - Atributo Recarga do Escudo)  * 1s / 60

Cada ponto em poder de escudo aumenta cinco pontos de vida no escudo de energia e o jogador ganha um ponto de escudo por level.
Durante a missão, a cada recarga o escudo ganha um ponto de vida se não estiver no máximo.
2.	Blindagem: são os pontos de vida do carro, defendem contra impactos, e danos de armas de qualquer tipo. A blindagem não se regenera durante a corrida.
Quanto mais blindagem, mais pontos de vida e mais peso.

O jogador ganha um ponto de de vida por level e cinco pontos de vida por ponto de blindagem.
3.	Tipo de Blindagem
 Quanto melhor o tipo de blindagem, maior é a porcentagem de dano que vai ser absorvido pela blindagem. Este valor vai de 0% até 85%.

⦁	Cristais de energia

São a unica forma de restaurar a energia dos carros durante uma missão.
Pequenos cristais podem ser coletados durante as corridas para restaurar uma pequena quantidade de energia.

Oito cristais muito raros são equipados automáticamente depois de coletados, tem energia infinita e são indestrutíveis. Cada cristal restaura lentamente a energia da nava e não são perdidos depois do fim da corrida.

Interface

⦁	Novo jogo



Quando iniciado um novo jogo, o jogador escolhe entre seis carros diferentes e vai para a garagem depois de escolher ou volta para a tela inicial se cancelar.

Seu carro inicial começa level um e com uma arma, a mais fraca do jogo.
Ganha um bonus de atributo dependendo do carro que escolher.
⦁	Tela Inicial




Nesta tela é mostrado o título do jogo, alguma imagem de fundo, e alguns botões:
1.	Botão Jogar: Detecta se exite algum jogo salvo, caso exista, carrega o jogo salvo, senão, vai para a tela de novo jogo.

2.	Botão Opções: Este botão vai para a tela de opções.
Na tela de opções vai ser possivel alterar o "volume" da musica e efeitos sonoros.

3.	Botão Ajuda: Mostra slides de ajuda.

4.	Botão Créditos: Mostra a tela de Créditos.

5.	Botão Sair: Sai do jogo. Este botão é necessário? O jogador pode fechar o jogo apertando a tecla voltar no celular enquanto estiver na tela inicial.
⦁	Hangar:

Depois de carregado ou iniciado um novo jogo, o jogador vai para a tela principal da garagem.



Acho que seria interessante que a pontuação do jogador estivesse sempre visível em algum canto da tela depois que o jogo for carregado, porque ele usa pontos pra tudo (upar o level do carro e comprar itens e armas).

E como praticamente tudo tem peso, nessas telas tambem deve ser exibido uma relação de quanto de peso o jogador tem disponivel, quanto é o peso atual e quanto é o peso máximo.

Nesta tela o jogador tem as seguintes opções:
1.	Jogar Corrida:

Ao escolher jogar corrida, primeiro o jogador vai para uma tela onde escolhe um entre os dez setores e depois escolhe uma entre as dez pistas do setor que foi escolhido. ( Estes números podem varias futuramente )

No inicio do jogo só a primeira pista e o primeiro setor vão estar desbloqueados. A proxima pista só é desbloqueada após o jogador vencer na ultima pista desbloqueada. O proximo setor só é desbloqueado após o jogador vencer nas dez pistas do último setor desbloqueado.

2.	Supply Room:



Aqui o jogador vai para uma tela onde pode ajustar:

-Armas:
Equipar "X" armas. As armas são compradas com os pontos que o jogador conseguiu nas corridas.

Depois de comprar, também é possivel fazer upgrade(ou downgrade) na arma, cada upgrade aumenta o peso, dano e o tamanho do carregador da arma.

-Munição:
Aumentar ou diminuir a quantidade de munição de cada arma equipada.

-Itens:
.Células de energia:
 As Células de energia são equipadas automaticamente depois de compradas, não podem ser vendidas, só desequipadas.

.Cristais de energia:
 Depois de coletados são visíveis nesta tela

Os itens não ocupam slots de armas, depois de comprados são equipados automaticamente.

3.	Level
Visualizar o progresso do level. Level atual e pontos restantes para o proximo level.

4.	Salvar Jogo Ou Sistema autosave:

Exibe uma mensagem perguntando se o jogador desejar sobreescrever o save, se clicar em "sim", sobreescreve o save, se clicar em não, volta e não faz nada.

5.	Configurar os atributos do carro:

Aumentar o diminuir: Velocidade, Aceleração, Agilidade, Blindagem, Poder do Escudo, Recarga do Escudo, Suspensão, Pneus e Aerodinâmica.
O aumento destes atributos é limitado pelo peso disponível.

6.	Sair para o Menu Principal:

Avisa o jogador que o progresso não salvo será perdido e pede confirmação, ok ou cancelar.


Jogabilidade




⦁	Controles:

Acelerador e freio...
O carro atira automaticamente quando detecta um inimigo na mira.

⦁	Movimentação

Usa a engine física Farseer
HUD:

Aqui aparecem as informações sobre armas equipadas, munição carregada, munição total disponível para cada arma, energia e pontuação atual.

⦁	Pontos de vida e de escudo:

Os pontos de vida e escudo são exibidos acima de todo carro.

A vida é representada por uma barra que vai de verde, passando pelo amarelo, até o vermelho, que indica a porcentagem de pontos de vida de um carro.

O escudo de energia é representado por uma barra azul acima da barra de vida.

⦁	Inimigos



O jogo vai ter "X" tipos de inimigos predefinidos, com armas e stributos diferentes,  a cada missão um novo inimigo é desbloqueado e pode aparecer para lutar contra o jogador.
