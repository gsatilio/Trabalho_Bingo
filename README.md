# Jogo do Bingo
 - Opção de informar o número de jogadores
 - Opção de informar o número de cartela que todos os jogadores terão
 - Opção para ir sorteando os números a cada rodada, ou pular para o fim do sorteio até alguém atingir o Bingo
 ## Lógica utilizada
 - Todas as cartelas são inseridas em uma matriz de 6 colunas por n linhas. Por exemplo, se a partida possuir apenas 1 jogador com 1 cartela, essa matriz será de [5,6]
 ## Por que 6 colunas?
 - Da coluna 1 até a 5 (ou índice 0 até o 4), são salvos os números das cartelas. A 6 coluna é salvo, nessa ordem:
Linha 1 : Código do jogador
Linha 2 : Código da cartela
Linha 3 : Número de acertos da cartela
Linha 4 e Linha 5 não são utilizadas nessa coluna
## Detalhando a lógica
 - Para cada cartela, essa matriz aumenta em 5 linhas
 - Foram utilizadas funções para que o programa localize na matriz o momento em que o número de cartelas de um jogador começa e termina, para iniciar do próximo jogador
 - Para cada número sorteado, se localizado nas cartelas (buscando em toda a matriz), é alterado o valor desse número para negativo
 - Caso uma linha possua 5 números negativos (iniciando na posição 0 e terminando na posição 4), é salvo para quem foi a pontuação da Linha.
 - Caso uma coluna possua 5 números negativos (iniciando na linha X até a linha X+5), é salvo para quem foi a pontuação da Coluna.
 - Foi desenvolvido uma função de impressão para facilitar a visualização do sorteio sendo realizado, sendo que a cada número acertado, a cor desse número na cartela é alterada
## Regras do Projeto:
Jogo do Bingo em C#.
As regras do bingo são as seguintes:

- As cartelas possuem 25 números escritos em ordem aleatória.

- Os números sorteados vão de 1 a 99.

- Se algum jogador completar uma linha a pontuação para todos passa a valer somente a coluna de qualquer cartela e vice-versa.

- A partir daí, só vale a pontuação de cartela cheia.

- Os sorteios devem acontecer até algum jogador completar a cartela (BINGO!).

- São 3 possibilidades de pontos:

- Ao completar uma linha, o jogador recebe 1 ponto.

- Ao completar uma coluna, o jogador recebe 1 ponto.

- Ao completar a cartela, o jogador recebe 5 pontos.
