//Variaveis
int ContadorSorteados = 0;      // Controla nros distintos sorteados (de 1 a 99)
int Colunas = 6;                // Estrutura 5x5 com + 1 coluna pra salvar dados
int Jogadores = 0;              // Controla o nro de jogadores selecionados
int LinhaCartelaIncremento = 0; // Controla a linha de cada cartela
int LinhaCartelaPessoa = 0;     // Controla o codigo da cartela da pessoa
int LinhaTotal = 0;             // Controla o total de linhas da matriz geral de cartelas
int Id_Jogador = 0, NroSorteado = 0, LinhaTotalControl = 0;
int ContadorResetNumeros = 0;   // Controla o reset do sorteio para preencher uma cartela
bool Bingo = false;
//Vetores
int[] Roleta = new int[99];
int[] RoletaSorteados = new int[99];
int[] RoletaCartela = new int[99];
//Matrizes
int[,] MatrizCartelas = new int[5, Colunas];
int[,] JogadoresRegistrados = new int[100, 3];
int[,] MatrizNumeros = new int[0, 100];
int[,] MatrizAcertos = new int[2, 5];
string[,] MatrizMensagens = new string[3, 5];

void EnumerarRoleta()
{
    // Popula numeros de 1 a 99 no vetor Roleta
    for (int i = 0; i < Roleta.Length; i++)
    {
        Roleta[i] = i + 1;
    }
}
void EnumerarCartelas()
{
    // Popula numeros de 1 a 99 no vetor Roleta
    for (int i = 0; i < 99; i++)
    {
        RoletaCartela[i] = i + 1;
    }
}
void GeradorCartelas()
{
    int CartelaJogador = 1, CartelaNumerosLinhas = 0;
    for (int linha = 0; linha < Jogadores; linha++)
    {
        CartelaJogador = JogadoresRegistrados[linha, 0];
        CartelaNumerosLinhas = (JogadoresRegistrados[linha, 1]) * 5; // cada cartela tem 5 linhas
        CriarCartelas(CartelaJogador, CartelaNumerosLinhas);
    }
}
void CriarCartelas(int CartelaJogador, int CartelaNumerosLinhas)
{
    int CodigoJogadorCartela = 0;
    for (int linha = 0; linha < CartelaNumerosLinhas; linha++)
    {
        for (int coluna = 0; coluna < Colunas; coluna++)
        {
            switch (coluna)
            {
                case < 5:   // Se coluna < 5, então preenche os numeros da cartela
                    MatrizCartelas[LinhaCartelaIncremento, coluna] = PreencherCartelaJogador();  // Executa função para preencher a cartela do jogador
                    break;
                case 5:     // Senão, preenche a coluna de dados para controle
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
        LinhaCartelaIncremento++;   // Essa variavel inicia com 0, e a cada linha de cartela criada é incrementada em 1
    }
}

int PreencherCartelaJogador()
{
    int indiceAleatorio = 0, indiceNumero = 0;
    if (ContadorResetNumeros == 25) // Se 25 números foram sorteados, reseta para a próxima cartela
    {
        ContadorResetNumeros = 0;
        EnumerarCartelas();
    }
    while (indiceNumero == 0) // Enquanto numero sorteado for zero
    {
        indiceAleatorio = new Random().Next(1, 98);     // traz um indice aleatorio entre 1 e 98
        indiceNumero = RoletaCartela[indiceAleatorio];  // pega o numero referente a esse indice no vetor RoletaCartela
        switch (indiceNumero)
        {
            case > 0:                                   // se numero maior que zero, ele é sorteado
                RoletaCartela[indiceAleatorio] = 0;     // esse numero é zerado do vetor de origem
                break;
            default:
                indiceNumero = 0;                       // se vier numero zero, executa o laço novamente até encontrar um maior que zero
                break;
        }
    }
    ContadorResetNumeros++; // Soma no contador de números sorteados
    return indiceNumero;
}
void imprimirCartelas()
{
    int JCartelas = 0, linhaAtual = -1, linhaParada = 0, JColunas = 0, colaux = 0, linaux = 0;
    // Jogadores
    for (int JogadorUnico = 0; JogadorUnico < Jogadores; JogadorUnico++)
    {
        JCartelas = JogadoresRegistrados[JogadorUnico, 1];
        JColunas = (JCartelas * 5);
        Console.Write($"\n\nCartelas do Jogador {JogadorUnico + 1}\n");
        for (int lin = 0; lin < 5; lin++)
        {
            linaux = linhaParada;
            Console.Write("\n");
            for (int col = 0; col < JColunas; col++)
            {
                if (colaux == 5)
                {
                    linaux += 5;
                    colaux = 0;
                    Console.Write("|   ");
                }
                Console.Write("|" + MatrizCartelas[linaux, colaux].ToString().PadLeft(2, '0'));
                colaux++;
            }
            Console.Write("|   ");
            linhaParada++;
            linhaAtual += 2;
            colaux = 0;
        }
        linhaParada = linhaAtual + 1;
    }
} // ver aqui
void VerificarSorteado()
{
    int ControlePosicaoLinha = 0;
    int LinhaIdJogador = 0;
    int LinhaIdCartela = 0;
    int LinhaAcertosCartela = 0;
    int NrosAcertados = 0;
    int indexLinha = 0, indexColuna = 0;
    while (ContadorSorteados < 99 && !Bingo)
    {
        if (LinhaTotalControl > 0)
        {
            LinhaTotalControl = 0;
        }
        Sortear(); // sorteia assim que acabam as linhas e contador ainda for menor que 100
        for (int linha = 0; linha < LinhaTotal; linha++) // varre todas as linhas
        {
            switch (indexLinha)
            {
                case 0:
                    LinhaIdJogador = linha;
                    LinhaIdCartela = linha + 1;
                    LinhaAcertosCartela = linha + 2;
                    break;
                case 5:
                    LinhaIdJogador = linha;
                    LinhaIdCartela = linha + 1;
                    LinhaAcertosCartela = linha + 2;
                    indexLinha = 0;
                    break;
            }
            for (int coluna = 0; coluna < Colunas; coluna++) // varre as colunas de 0 a 4
            {
                if (coluna < 5) // de 0 a 4 = Numeros da cartela
                {
                    indexColuna = coluna;
                    if (MatrizCartelas[linha, coluna] == NroSorteado)
                    {
                        MatrizCartelas[linha, coluna] = (MatrizCartelas[linha, coluna] * -1); // Ao acertar o sorteio de um numero, muda para negativo
                        NrosAcertados = MatrizCartelas[LinhaAcertosCartela, 5];
                        NrosAcertados++;
                        MatrizCartelas[LinhaAcertosCartela, 5] = NrosAcertados; // se localizar o nro sorteado, altera para 0 no vetor clonado
                    }
                    VerificarResultado(linha, coluna, LinhaIdJogador, LinhaIdCartela, LinhaAcertosCartela, indexLinha, indexColuna);
                }
            }
            ControlePosicaoLinha++;
            if (ControlePosicaoLinha == 4)
            {
                ControlePosicaoLinha = 0;
            }
            LinhaTotalControl++;// nao mexer
            indexLinha++;
        }
    }
}
void EfetuarPontuacao(int TipoPontuacao, int indexLinha, int indexColuna, int LinhaIdJogador, int LinhaIdCartela)
{
    int Id_Jogador = 0, Id_Cartela = 0, pontos = 1;
    Id_Jogador = MatrizCartelas[LinhaIdJogador, 5];
    Id_Cartela = MatrizCartelas[LinhaIdCartela, 5];

    switch (TipoPontuacao) // TipoPontuacao -  0 = LINHA, 1 = COLUNA
    {
        case 0:
            MatrizAcertos[TipoPontuacao, indexLinha] = 1;   // salva essa LINHA como pontuada
            //MatrizMensagens[TipoPontuacao, indexLinha] = $"DEU LINHA  ({indexLinha + 1}) PARA O JOGADOR {Id_Jogador + 1}! NA CARTELA: {Id_Cartela}";
            Console.WriteLine($"\nDEU LINHA  ({indexLinha + 1}) PARA O JOGADOR {Id_Jogador + 1}! NA CARTELA: {Id_Cartela}");
            break;
        case 1:
            MatrizAcertos[TipoPontuacao, indexColuna] = 1;   // salva essa COLUNA como pontuada
            //MatrizMensagens[TipoPontuacao, indexColuna] = $"DEU COLUNA ({indexColuna + 1}) PARA O JOGADOR {Id_Jogador + 1}! NA CARTELA: {Id_Cartela}";
            Console.WriteLine($"\nDEU COLUNA ({indexColuna + 1}) PARA O JOGADOR {Id_Jogador + 1}! NA CARTELA: {Id_Cartela}");
            break;
        case 2:
            //MatrizMensagens[TipoPontuacao, 0] = $"******************* BINGO ****************\nPARA O JOGADOR {Id_Jogador + 1}! NA CARTELA: {Id_Cartela}";
            Console.WriteLine($"\n******************* BINGO ****************\nPARA O JOGADOR {Id_Jogador + 1}! NA CARTELA: {Id_Cartela}");
            Bingo = true;
            pontos = 5;
            break;
    }
    JogadoresRegistrados[Id_Jogador, 2] = JogadoresRegistrados[Id_Jogador, 2] + pontos; // Add pontuação para o jogador (id jogador, qtde cartelas, pontuacao)
}
void VerificarResultado(int linha, int coluna, int LinhaIdJogador, int LinhaIdCartela, int LinhaAcertosCartela, int indexLinha, int indexColuna)
{
    int AcertosCartela, Id_Jogador = 0, Id_Cartela = 0;
    int contador;
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
        if (contador == 4)
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
        if (contador == 4)
        {
            EfetuarPontuacao(1, indexLinha, indexColuna, LinhaIdJogador, LinhaIdCartela);
        }
    }
    // VALIDAR BINGO
    AcertosCartela = 0;
    if (!Bingo)
    {
        AcertosCartela = MatrizCartelas[LinhaAcertosCartela, 5];
        if (AcertosCartela == 25)
        {
            EfetuarPontuacao(2, indexLinha, indexColuna, LinhaIdJogador, LinhaIdCartela);
        }
    }
}
void RealizarSorteio()
{
    int Jogador = 1, Pontuacao = 0, Qtde = 0;
    imprimirCartelas();
    Console.WriteLine("\n\nAperte qualquer tecla para realizar os sorteios.");
    Console.ReadKey();
    //Console.Clear();

    for (int i = 0; i < 99; i++)
    {
        VerificarSorteado();
    }
    Console.WriteLine("\n");
    for (int linha = 0; linha < 3; linha++)
    {
        for (int col = 0; col < 5; col++)
        {
            if (MatrizMensagens[linha, col] != null)
            {
                Console.WriteLine(MatrizMensagens[linha, col]);
            }
        }
    }
    Console.Write("");
    for (int linha = 0; linha < Jogadores; linha++)
    {
        Jogador = JogadoresRegistrados[linha, 0];
        Qtde = JogadoresRegistrados[linha, 1];
        Pontuacao = JogadoresRegistrados[linha, 2];
        Console.Write($"\nO Jogador {Jogador + 1} possui {Qtde} Cartelas e fez {Pontuacao} pontos!");
    }
    //imprimirCartelas();
    ImprimeVetor(RoletaSorteados, "\n\nOrdem dos números que foram sorteados:", 0, RoletaSorteados.Length, 1);
    ImprimeVetor(Roleta, "\n\nNúmeros que não foram sorteados:", 0, Roleta.Length, 1);
    Console.ReadKey();
}
void Sortear()
{
    int i = 0, Sorteio = 0;
    bool Sorteou = false;
    for (int tentativas = 0; (tentativas < 99 && !Sorteou); tentativas++)
    {
        while (Sorteio == 0)     // Enquanto numero sorteado for zero e sorteio ate 99
        {
            i = new Random().Next(0, 99);                   // Pega um indice aleatorio entre 0 e 99
            Sorteio = Roleta[i];                           // Sorteado recebe o numero da roleta nesse indice
        }                             // Aumenta contador de sorteado (controlador do indice do vetor)
        RoletaSorteados[ContadorSorteados] = Sorteio;  // Salva esse numero no vetor de sorteados
        NroSorteado = Sorteio;
        ContadorSorteados++;
        Roleta[i] = 0;                                  // Muda o valor do vetor no indice selecionado, para zero
        Sorteou = true;
    }
}
void MenuJogadores()
{
    int QtdeCartela = 0;
    int qtdetotal_temp = 0;
    //int JogadorRegistradoIndice = 0;
    while (Jogadores <= 0)
    {
        Console.WriteLine("Quantos jogadores?");
        Jogadores = int.Parse(Console.ReadLine());
    }

    while (QtdeCartela <= 0)
    {
        Console.WriteLine($"Quantas cartelas para cada jogador?");
        QtdeCartela = int.Parse(Console.ReadLine());
    }

    EnumerarCartelas();
    while (Id_Jogador < Jogadores)
    {
        //while (QtdeCartela <= 0)
        //{
        //    Console.WriteLine($"Quantas cartelas para o {Id_Jogador + 1}o jogador?");
        //    QtdeCartela = int.Parse(Console.ReadLine());
        //}

        // Vetor JogadoresRegistrados tem 100 casas (aqui salvo ID JOGADOR e QTDE CARTELAS)
        JogadoresRegistrados[Id_Jogador, 0] = Id_Jogador;
        JogadoresRegistrados[Id_Jogador, 1] = QtdeCartela;
        qtdetotal_temp += QtdeCartela;
        Id_Jogador++;
        //QtdeCartela = 0;
    }
    LinhaTotal = (qtdetotal_temp * 5);
    MatrizCartelas = new int[LinhaTotal, 6];
    GeradorCartelas();
    EnumerarRoleta();
    RealizarSorteio();
}


void ImprimeVetor(int[] VetorGenerico, string titulo, int param_linhas, int param_colunas, int Customizado)
{
    int linha = 0;
    Console.WriteLine($"{titulo}");
    switch (Customizado)
    {
        case 1:
            for (int i = 0; i < param_colunas; i++)
            {
                linha++;
                if (VetorGenerico[i] > 0)
                {
                    Console.Write(VetorGenerico[i].ToString().PadLeft(2, '0') + " ");
                    if (linha == 15)
                    {
                        Console.WriteLine();
                        linha = 0;
                    }
                }
            }
            break;
        default:
            for (int i = 0; i < param_colunas; i++)
            {
                linha++;
                Console.Write(VetorGenerico[i].ToString().PadLeft(2, '0') + " ");
                if (linha == 15)
                {
                    Console.WriteLine();
                    linha = 0;
                }
            }
            break;
    }

}
MenuJogadores();
