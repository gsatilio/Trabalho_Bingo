//Variaveis
int ContadorSorteados = 0, Colunas = 6, Jogadores = 0, LinhaCartelaIncremento = 0, LinhaCartelaPessoa = 0;
int LinhaTotal = 0, Id_Jogador = 0, NroSorteado = 0, ContadorResetNumeros = 0, QtdeCartela = 0, pulaBingo = -1;
string MensagemPontuacao = "";
bool Bingo = false;
//Vetores
int[] Roleta = new int[99], RoletaSorteados = new int[99], RoletaCartela = new int[99];
//Matrizes
int[,] MatrizCartelas = new int[5, Colunas], JogadoresRegistrados = new int[20, 3], MatrizNumeros = new int[0, 100], MatrizAcertos = new int[2, 5];
string[,] JogadoresRegistradosNome = new string[20, 2];
void ImprimeVetor(int[] VetorGenerico, string titulo, int param_linhas, int param_colunas)
{
    int linha = 0;
    Console.WriteLine($"{titulo}");

    for (int i = 0; i < param_colunas; i++)
    {
        linha++;
        if (VetorGenerico[i] > 0)
        {
            Console.Write(VetorGenerico[i].ToString().PadLeft(2, '0') + " ");
            if (param_linhas > 0 && linha == param_linhas)
            {
                Console.WriteLine();
                linha = 0;
            }
        }
    }
}
void EnumerarRoleta()
{
    // Popula numeros de 1 a 99 no vetor Roleta (essa não repopula)
    for (int i = 0; i < Roleta.Length; i++)
    {
        Roleta[i] = i + 1;
    }
}
void EnumerarCartelas()
{
    for (int i = 0; i < 99; i++)  // Popula numeros de 1 a 99 no vetor Roleta (essa repopula, para gerar as cartelas dos jogadores)
    {
        RoletaCartela[i] = i + 1;
    }
}
void CriarCartelas()
{
    int CartelaJogador = 0, CartelaNumerosLinhas = 0, CodigoJogadorCartela = 0;
    for (int jogadorUnico = 0; jogadorUnico < Jogadores; jogadorUnico++)
    {
        CartelaJogador = JogadoresRegistrados[jogadorUnico, 0];             // código do jogador
        CartelaNumerosLinhas = (JogadoresRegistrados[jogadorUnico, 1]) * 5; // nro de cartelas escolhida * 5 linhas
        CodigoJogadorCartela = 0;                                           // código da cartela
        for (int linha = 0; linha < CartelaNumerosLinhas; linha++)
        {
            for (int coluna = 0; coluna < Colunas; coluna++)
            {
                switch (coluna)
                {
                    case < 5:   // Se coluna < 5, então preenche os numeros da cartela (de 0 a 4 sao numeros, a coluna 5 exclusiva para controle)
                        MatrizCartelas[LinhaCartelaIncremento, coluna] = PreencherCartelaJogador();  // Executa função para preencher a cartela do jogador
                        break;
                    case 5:
                        if (LinhaCartelaIncremento == LinhaCartelaPessoa)   // Se linha incremento = linha cartela pessoa então é a linha pai de cada cartela
                        {
                            MatrizCartelas[LinhaCartelaIncremento, coluna] = CartelaJogador;       // SALVA O ID JOGADOR
                        }
                        else if (LinhaCartelaIncremento == LinhaCartelaPessoa + 1)  // Linha pai da cartela + 1
                        {
                            CodigoJogadorCartela++;     // Incrementa ID da Cartela do jogador
                            MatrizCartelas[LinhaCartelaIncremento, coluna] = CodigoJogadorCartela;    // SALVA O ID CARTELA
                            LinhaCartelaPessoa += 5;    // Essa variável controle a linha fim de cada cartela (ou seja, a cada 5 linhas é uma cartela diferente)
                        }
                        break;
                }
            }
            LinhaCartelaIncremento++;   // Essa variavel inicia com 0, e a cada linha de cartela criada é incrementada em 1 (salvo todas as cartelas em uma unica matriz)
        }
    }
}
int PreencherCartelaJogador()
{
    int indiceAleatorio = 0, NumeroNoIndice = 0;
    if (ContadorResetNumeros == 25) // Se 25 números foram sorteados em uma cartela, repopula o vetor para voltar os 99 numeros para a proxima cartela ser gerada
    {
        ContadorResetNumeros = 0;
        EnumerarCartelas();
    }
    while (NumeroNoIndice == 0) // Enquanto numero sorteado for zero
    {
        indiceAleatorio = new Random().Next(1, 98);     // traz um indice aleatorio entre 1 e 98
        NumeroNoIndice = RoletaCartela[indiceAleatorio];  // pega o numero referente a esse indice no vetor RoletaCartela
        switch (NumeroNoIndice)
        {
            case > 0:                                   // se numero maior que zero, ele é sorteado
                RoletaCartela[indiceAleatorio] = 0;     // esse numero é zerado do vetor de origem
                break;
            default:
                NumeroNoIndice = 0;                       // se vier numero zero, executa o laço novamente até encontrar um numero maior que zero
                break;
        }
    }
    ContadorResetNumeros++; // Soma no contador de números sorteados
    return NumeroNoIndice;  // retorna numero sorteado
}
void Sortear()
{
    int i = 0, Sorteio = 0, NroSorteadoAnterior = NroSorteado;
    bool Sorteou = false;
    for (int tentativas = 0; (tentativas < 99 && !Sorteou); tentativas++)
    {
        while (Sorteio == 0)     // Enquanto numero sorteado for zero e sorteio ate 99
        {
            i = new Random().Next(0, 99);                   // Pega um indice aleatorio entre 0 e 99
            Sorteio = Roleta[i];                           // Sorteado recebe o numero da roleta nesse indice
        }
        RoletaSorteados[ContadorSorteados] = Sorteio;  // Salva o numero sorteado dentro do vetor de sorteados
        NroSorteado = Sorteio;
        ContadorSorteados++;                            // Aumenta contador de sorteado (controlador do indice do vetor)
        Roleta[i] = 0;                                  // Zera o numero no vetor Roleta de origem para que ele nao seja sorteado novamente
        Sorteou = true;
        if (NroSorteadoAnterior > 0)    // Executa funções para impressão na tela após o sorteio
        {
            if (pulaBingo != 1)
            {
                Console.Clear();
                imprimirCartelas();
                if (MensagemPontuacao != "")
                {
                    Console.Write($"\n\n[   {MensagemPontuacao}   ]");
                    Console.ReadKey();
                }
                Console.Write($"\n\nNúmero sorteado nessa rodada:");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($" [{NroSorteadoAnterior.ToString().PadLeft(2, '0')}]");
                Console.ResetColor();
                Console.WriteLine("\nAperte qualquer tecla para continuar o sorteio.");
                Console.ReadKey();
            }
        }
    }
}
void VerificarSorteado()
{
    int LinhaIdJogador = 0, LinhaIdCartela = 0, LinhaAcertosCartela = 0;
    int NrosAcertados = 0, indexLinha = 0, indexColuna = 0;
    bool NovaCartela = true;
    while (ContadorSorteados < 99 && !Bingo)
    {
        Sortear(); // Enquanto não for bingo e existirem numeros, efetua o sorteio
        MensagemPontuacao = "";
        for (int linha = 0; linha < LinhaTotal && !Bingo; linha++) // Varre todas as linhas de todas as cartelas
        {
            if (NovaCartela)    // controle para saber quando uma linha inicia uma cartela nova
            {
                LinhaIdJogador = linha;
                LinhaIdCartela = linha + 1;
                LinhaAcertosCartela = linha + 2;
                indexLinha = 0;
                NovaCartela = false;
            }
            for (int coluna = 0; coluna < Colunas && !Bingo; coluna++) // varre as colunas de 0 a 4
            {
                if (coluna < 5) // de 0 a 4 = Numeros da cartela
                {
                    indexColuna = coluna;
                    if (MatrizCartelas[linha, coluna] == NroSorteado)
                    {
                        MatrizCartelas[linha, coluna] = (MatrizCartelas[linha, coluna] * -1); // Ao acertar o sorteio de um numero, muda para negativo na cartela
                        NrosAcertados = MatrizCartelas[LinhaAcertosCartela, 5];
                        NrosAcertados++;
                        MatrizCartelas[LinhaAcertosCartela, 5] = NrosAcertados; // se localizar o nro sorteado, altera para 0 no vetor clonado
                    }
                    VerificarResultado(linha, coluna, LinhaIdJogador, LinhaIdCartela, LinhaAcertosCartela, indexLinha, indexColuna); // executa validações apos o sorteio do numero
                }
            }
            indexLinha++;
            if (indexLinha == 5)
            {
                NovaCartela = true;
            }
        }
    }
}
void RealizarSorteio()
{
    imprimirCartelas(); // Imprime as cartelas inicialmente
    Console.WriteLine("\n\nAperte qualquer tecla para realizar os sorteios.");
    Console.ReadKey();
    while (!Bingo) // Executa o sorteio
    {
        VerificarSorteado();
    }
    if (Bingo)
    {
        Console.Clear();
        imprimirCartelas();
        if (MensagemPontuacao != "")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(MensagemPontuacao); Console.ResetColor();
        }
    }
    ImprimeVetor(RoletaSorteados, "\n\nOrdem dos números que foram sorteados:", 15, RoletaSorteados.Length);
    ImprimeVetor(Roleta, "\n\nNúmeros que não foram sorteados:", 15, Roleta.Length);
    Console.ReadKey();
}
void EfetuarPontuacao(int TipoPontuacao, int indexLinha, int indexColuna, int LinhaIdJogador, int LinhaIdCartela)
{
    int Id_Jogador = 0, Id_Cartela = 0;
    Id_Jogador = MatrizCartelas[LinhaIdJogador, 5];
    Id_Cartela = MatrizCartelas[LinhaIdCartela, 5];

    switch (TipoPontuacao) // TipoPontuacao -  0 = LINHA, 1 = COLUNA
    {
        case 0:
            JogadoresRegistrados[Id_Jogador, 2] = JogadoresRegistrados[Id_Jogador, 2] + 1; // Add pontuação para o jogador (id jogador, qtde cartelas, pontuacao)
            MatrizAcertos[TipoPontuacao, indexLinha] = 1;   // salva essa LINHA como pontuada
            MensagemPontuacao = ($"O JOGADOR {Id_Jogador + 1}, NA CARTELA {Id_Cartela}, PREENCHEU A LINHA {indexLinha + 1} !");
            break;
        case 1:
            JogadoresRegistrados[Id_Jogador, 2] = JogadoresRegistrados[Id_Jogador, 2] + 1; // Add pontuação para o jogador (id jogador, qtde cartelas, pontuacao)
            MatrizAcertos[TipoPontuacao, indexColuna] = 1;   // salva essa COLUNA como pontuada
            MensagemPontuacao = ($"O JOGADOR {Id_Jogador + 1}, NA CARTELA {Id_Cartela}, PREENCHEU A COLUNA {indexColuna + 1} !");
            break;
        case 2:
            int CodigoJogador, PontoJogador, JogadorVencedor = 0, JogadorVencedorPontos = 0;
            string JogadorNome = "";
            // Informa quem fez o Bingo
            JogadoresRegistrados[Id_Jogador, 2] = JogadoresRegistrados[Id_Jogador, 2] + 5; // Add pontuação para o jogador (id jogador, qtde cartelas, pontuacao)
            JogadorNome = JogadoresRegistradosNome[Id_Jogador, 1]; ;
            Bingo = true;
            MensagemPontuacao += "\n\n\n__________________________________________________\n";
            MensagemPontuacao += "|$$$$$$$$$$$$$$$ !|B I N G O |! |$$$$$$$$$$$$$$$ |\n";
            MensagemPontuacao += "|¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯|\n";
            MensagemPontuacao += $"|       PARA O JOGADOR {Id_Jogador + 1} - {JogadorNome}! NA CARTELA: {Id_Cartela}          \n";
            MensagemPontuacao += "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯\n";
            // Informa quem pontuou mais no jogo
            for (int linha = 0; linha < Jogadores; linha++)
            {
                CodigoJogador = JogadoresRegistrados[linha, 0];
                PontoJogador = (JogadoresRegistrados[linha, 2]);
                if (PontoJogador > JogadorVencedorPontos)
                {
                    JogadorVencedor = JogadoresRegistrados[CodigoJogador, 0];
                    JogadorVencedorPontos = (JogadoresRegistrados[CodigoJogador, 2]);
                }
            }
            JogadorNome = JogadoresRegistradosNome[JogadorVencedor, 1];
            MensagemPontuacao += $"\n¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯";
            MensagemPontuacao += $"\nO VENCEDOR DO BINGO É O JOGADOR: {JogadorVencedor + 1} - {JogadorNome} COM INCRÍVEIS: {JogadorVencedorPontos} PONTOS! PARABÉNS!\n";
            MensagemPontuacao += "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯\n";
            MensagemPontuacao += "[   Obrigado por jogar! Volte quando quiser =)   ]\n";
            break;
    }
}
void VerificarResultado(int linha, int coluna, int LinhaIdJogador, int LinhaIdCartela, int LinhaAcertosCartela, int indexLinha, int indexColuna)
{
    int AcertosCartela;
    int contador;
    AcertosCartela = 0;
    // VALIDAR LINHA
    contador = 0;
    AcertosCartela = 0;
    if (MatrizAcertos[0, indexLinha] == 0) // Verifica no vetor de acertos se alguem ja fez essa LINHA
    {
        for (int col = 0; (col < 5); col++)
        {
            AcertosCartela = MatrizCartelas[linha, col];
            if (AcertosCartela < 0)
            {
                contador++;
            }
        }
        if (contador == 5)
        {
            EfetuarPontuacao(0, indexLinha, indexColuna, LinhaIdJogador, LinhaIdCartela);
        }
    }
    // VALIDAR COLUNA
    contador = 0;
    AcertosCartela = 0;
    if (MatrizAcertos[1, indexColuna] == 0) // Verifica no vetor de acertos se alguem ja fez essa COLUNA
    {
        for (int lin = LinhaIdJogador; (lin < LinhaIdJogador + 5); lin++)
        {
            AcertosCartela = MatrizCartelas[lin, coluna];
            if (AcertosCartela < 0)
            {
                contador++;
            }
        }
        if (contador == 5)
        {
            EfetuarPontuacao(1, indexLinha, indexColuna, LinhaIdJogador, LinhaIdCartela);
        }
    }
    if (!Bingo)
    {
        AcertosCartela = MatrizCartelas[LinhaAcertosCartela, 5];
        if (AcertosCartela == 25)
        {
            EfetuarPontuacao(2, indexLinha, indexColuna, LinhaIdJogador, LinhaIdCartela);
        }
    }
}
void imprimirCartelas()
{
    int JCartelas = 0, linhaParada = 0, JColunas = 0, JPontos = 0, colaux = 0, linaux = 0, linhaParadaAux = 0, ContaCartela = 1;
    string JNome = "";
    for (int JogadorUnico = 0; JogadorUnico < Jogadores; JogadorUnico++)
    {
        JCartelas = JogadoresRegistrados[JogadorUnico, 1];
        JPontos = JogadoresRegistrados[JogadorUnico, 2];
        JNome = JogadoresRegistradosNome[JogadorUnico, 1];
        JColunas = (JCartelas * 5);
        Console.ResetColor();
        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
        Console.Write($"\n\n[     Cartelas do Jogador {JogadorUnico + 1} - ");
        Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"  {JNome}  ");
        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
        Console.Write($" - Pontuação Atual: ");
        Console.BackgroundColor = ConsoleColor.DarkMagenta; Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"   {JPontos}   "); Console.ResetColor();
        Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
        Console.Write($"      ]\n\n"); Console.ResetColor();
        for (int lin = 0; lin < 6; lin++)
        {
            if (lin > 0)
            {
                linaux = linhaParada;
                Console.Write("\n");
                for (int col = 0; col < JColunas; col++)
                {
                    if (colaux == 5)
                    {
                        linaux += 5;
                        linhaParadaAux++;
                        colaux = 0;
                        Console.Write("|     ");
                    }
                    switch (MatrizCartelas[linaux, colaux])
                    {
                        case < 0:
                            Console.Write("|");
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.Write(" " + (MatrizCartelas[linaux, colaux] * -1).ToString().PadLeft(2, '0') + " "); Console.ResetColor();
                            break;
                        default:
                            Console.Write("| " + MatrizCartelas[linaux, colaux].ToString().PadLeft(2, '0') + " ");
                            break;
                    }
                    colaux++;
                }
                Console.Write("|  ");
                linhaParada++;
                colaux = 0;
            }
            else
            {
                for (int col = 0; col < JCartelas; col++)
                {
                    Console.Write("         ");
                    Console.BackgroundColor = ConsoleColor.Gray; Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($"Cartela {ContaCartela}"); Console.ResetColor();
                    ContaCartela++;
                    Console.Write("             ");
                }
            }
        }
        linhaParada += linhaParadaAux;
        linhaParadaAux = 0;
        ContaCartela = 1;
    }
}
void MenuJogadores()
{
    int TotalLinhasTemp = 0;
    string Nome = "";
    while (Jogadores <= 0)
    {
        Console.WriteLine("Bem vindo ao Bingo!\nPor favor, informe a quantidade de jogadores que irão participar:");
        Jogadores = int.Parse(Console.ReadLine());
    }
    while (QtdeCartela <= 0)
    {
        Console.WriteLine($"Quantas cartelas serão distribuídas para cada jogador?");
        QtdeCartela = int.Parse(Console.ReadLine());
    }
    EnumerarCartelas();
    while (Id_Jogador < Jogadores)
    {
        JogadoresRegistrados[Id_Jogador, 0] = Id_Jogador;
        JogadoresRegistrados[Id_Jogador, 1] = QtdeCartela;
        while (Nome == "")
        {
            Console.WriteLine($"Qual o nome do jogador {Id_Jogador + 1}?");
            Nome = Console.ReadLine();
        }
        JogadoresRegistradosNome[Id_Jogador, 1] = Nome;
        TotalLinhasTemp += QtdeCartela;
        Id_Jogador++;
        Nome = "";
    }
    while (pulaBingo < 0 || pulaBingo > 1)
    {
        Console.WriteLine("Deseja pular o sorteio e ir até o final do bingo?\n0 - Não, quero ver o bingo sendo feito\n1 - Sim, estou com pressa!");
        pulaBingo = int.Parse(Console.ReadLine());
    }
    LinhaTotal = (TotalLinhasTemp * 5);
    MatrizCartelas = new int[LinhaTotal, 6];
    CriarCartelas();
    EnumerarRoleta();
    RealizarSorteio();
}

MenuJogadores();