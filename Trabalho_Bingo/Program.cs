/*
X | X | X | X | X | ID_PESSOA  
X | X | X | X | X | ID_CARTELA
X | X | X | X | X | L (0 ou 1)
X | X | X | X | X | C (0 ou 1)
X | X | X | X | X | V (0 ou 1)
*/

//Variaveis
int ContadorSorteados = 0;
int Linhas = 100, Colunas = 6; // Estrutura 5x5 com + 1 coluna pra salvar dados
int Jogadores = 0;
int LinhaCartelaPessoa = 0, LinhaTotal = 0, LinhaControleCartela = 0;
int Id_Jogador = 0, NroSorteado = 0, LinhaTotalControl = 0;
int CodigoJogadorCartela = 0;
int ContadorResetNumeros = 0;

bool Bingo = false, LinhaOK = false, ColunaOK = false;

//Vetores
int[] Roleta = new int[99];
int[] RoletaSorteados = new int[99];
int[] RoletaCartela = new int[99];

//Matrizes
int[,] MatrizCartelas = new int[Linhas, Colunas];
int[,] JogadoresRegistrados = new int[10, 3];
int[,] MatrizNumeros = new int[0, 100];

void MenuJogadores()
{
    int QtdeCartela = 0;
    int jogador_temp = 0;
    int qtdetotal_temp = 0;
    //int JogadorRegistradoIndice = 0;
    while (Jogadores <= 0)
    {
        Console.WriteLine("Quantos jogadores?");
        Jogadores = int.Parse(Console.ReadLine());
    }
    EnumerarCartelas();
    while (Id_Jogador < Jogadores)
    {
        while (QtdeCartela <= 0)
        {
            Console.WriteLine($"Quantas cartelas para o {Id_Jogador + 1}o jogador?");
            QtdeCartela = int.Parse(Console.ReadLine());
        }

        // Vetor JogadoresRegistrados tem 100 casas (aqui salvo ID JOGADOR e QTDE CARTELAS)
        JogadoresRegistrados[jogador_temp, 0] = Id_Jogador + 1;
        JogadoresRegistrados[jogador_temp, 1] = QtdeCartela;
        //JogadorRegistradoIndice++;

        //Cartelas += QtdeCartela;
        //CriarCartelas(Id_Jogador, QtdeCartela);
        qtdetotal_temp += QtdeCartela;
        Id_Jogador++;
        jogador_temp++;
        QtdeCartela = 0;
    }
    LinhaTotal = (qtdetotal_temp * 5);
    MatrizCartelas = new int[LinhaTotal, 6];
    GeradorCartelas();
}

void GeradorCartelas()
{
    int Jogador = 1, Qtde = 0;
    for (int linha = 0; (linha < 20 && Jogador > 0); linha++)
    {
        Jogador = JogadoresRegistrados[linha, 0];
        Qtde = JogadoresRegistrados[linha, 1];
        CriarCartelas(Jogador, Qtde);
    }
}

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
void CriarCartelas(int jogador, int quantidade)
{
    int LinhasCartela = (quantidade * 5);
    for (int linha = 0; linha < LinhasCartela; linha++)
    {
        for (int coluna = 0; coluna < Colunas; coluna++)
        {
            switch (coluna)
            {
                case < 5:
                    MatrizCartelas[LinhaControleCartela, coluna] = PreencherCartela();
                    break;
                case 5:
                    if (LinhaControleCartela == LinhaCartelaPessoa)
                    {
                        MatrizCartelas[LinhaControleCartela, coluna] = jogador;       // ID JOGADOR
                    }
                    else if (LinhaControleCartela == LinhaCartelaPessoa + 1)
                    {
                        CodigoJogadorCartela++;
                        MatrizCartelas[LinhaControleCartela, coluna] = CodigoJogadorCartela;    // ID CARTELA
                        LinhaCartelaPessoa += 5;
                        //Id_Cartela++;
                    }
                    break;
            }
        }
        LinhaControleCartela++;
    }
    CodigoJogadorCartela = 0;
}

int PreencherCartela()
{
    int i = 0, sorteado = 0;
    if (ContadorResetNumeros == 25)
    {
        ContadorResetNumeros = 0;
        EnumerarCartelas();
    }
    while (sorteado == 0) // Enquanto numero sorteado for zero
    {
        i = new Random().Next(1, 98);
        sorteado = RoletaCartela[i];

        if (sorteado > 0)
        {
            RoletaCartela[i] = 0;
        }
        else
        {
            sorteado = 0;
        }
    }

    ContadorResetNumeros++;
    return sorteado;
}
void imprimirCartelas()
{
    int pularlinha = 0;

    Console.WriteLine("\nCartelas dos Jogadores:");
    for (int linha = 0; linha < LinhaTotal; linha++)
    {
        if (pularlinha == 5)
        {
            Console.WriteLine();
            pularlinha = 0;
        }
        Console.WriteLine();
        for (int coluna = 0; coluna < Colunas; coluna++)
        {
            if (coluna == 5)
            {
                Console.Write("[" + MatrizCartelas[linha, coluna].ToString().PadLeft(2, '0') + "]");
            }
            else
            {
                Console.Write(MatrizCartelas[linha, coluna].ToString().PadLeft(2, '0') + " ");
            }
        }
        pularlinha++;
    }
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
void VerificarSorteado()
{
    int ControlePosicaoLinha = 0;
    int LinhaIdJogador = 0;
    int LinhaIdCartela = 0;
    int LinhaAcertosCartela = 0;
    int CartelaRodada = 0;
    int NrosAcertados = 0;
    while (ContadorSorteados < 99 && !Bingo)
    {
        if (LinhaTotalControl > 0)
        {
            LinhaTotalControl = 0;
        }
        Sortear(); // sorteia assim que acabam as linhas e contador ainda for menor que 100
        for (int linha = 0; linha < LinhaTotal; linha++) // varre todas as linhas
        {
            switch (CartelaRodada)
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
                    CartelaRodada = 0;
                    break;
            }
            for (int coluna = 0; coluna < Colunas; coluna++) // varre as colunas de 0 a 4
            {
                switch (coluna)
                {
                    case < 5: // de 0 a 4 = Numeros da cartela
                        if (MatrizCartelas[linha, coluna] == NroSorteado)
                        {
                            MatrizCartelas[linha, coluna] = (MatrizCartelas[linha, coluna] * -1); // Ao acertar o sorteio de um numero, muda para negativo
                            NrosAcertados = MatrizCartelas[LinhaAcertosCartela, 5];
                            NrosAcertados++;
                            if (NrosAcertados == 25)
                            {
                                Console.WriteLine();
                            }
                            MatrizCartelas[LinhaAcertosCartela, 5] = NrosAcertados; // se localizar o nro sorteado, altera para 0 no vetor clonado
                        }
                        if (!LinhaOK)
                        {
                            VerificarResultado(linha, coluna, LinhaIdJogador, LinhaIdCartela, LinhaAcertosCartela, 0);
                        }
                        if (!ColunaOK)
                        {
                            VerificarResultado(linha, coluna, LinhaIdJogador, LinhaIdCartela, LinhaAcertosCartela, 1);
                        }
                        if (!Bingo)
                        {
                            VerificarResultado(linha, coluna, LinhaIdJogador, LinhaIdCartela, LinhaAcertosCartela, 2);
                        }
                        break;
                }
            }
            ControlePosicaoLinha++;
            if (ControlePosicaoLinha == 4)
            {
                ControlePosicaoLinha = 0;
            }
            LinhaTotalControl++;// nao mexer
            CartelaRodada++;
        }
    }
}

void VerificarResultado(int linha, int coluna, int LinhaIdJogador, int LinhaIdCartela, int LinhaAcertosCartela, int TipoBusca)
{
    int AcertosCartela = 0, Id_Jogador = 0, Id_Cartela = 0;
    bool continuaBusca = true;

    // Pesquisa na Linha
    switch (TipoBusca)
    {
        case 0: // valida LINHA
            AcertosCartela = 0;
            for (int col = 0; (col < 5 && continuaBusca); col++)
            {
                AcertosCartela = MatrizCartelas[linha, col];
                if (AcertosCartela < 0)
                {
                    continuaBusca = true;
                }
                else
                {
                    continuaBusca = false;
                }
            }
            if (continuaBusca && !LinhaOK)
            {
                Id_Jogador = MatrizCartelas[LinhaIdJogador, 5];
                Id_Cartela = MatrizCartelas[LinhaIdCartela, 5];
                LinhaOK = true;
                JogadoresRegistrados[Id_Jogador, 2] = JogadoresRegistrados[Id_Jogador, 2] + 1; // Add pontuação na matriz (id jogador, qtde cartelas, pontuacao)
                Console.WriteLine($"DEU LINHA PARA O JOGADOR {Id_Jogador}! CARTELA: {Id_Cartela}");
            }
            break;
        case 1: // valida COLUNA
            AcertosCartela = 0;
            for (int lin = LinhaIdJogador; (lin < LinhaIdJogador + 5 && continuaBusca); lin++)
            {
                AcertosCartela = MatrizCartelas[lin, coluna];
                if (AcertosCartela < 0)
                {
                    continuaBusca = true;
                }
                else
                {
                    continuaBusca = false;
                }
            }
            if (continuaBusca && !ColunaOK)
            {
                Id_Jogador = MatrizCartelas[LinhaIdJogador, 5];
                Id_Cartela = MatrizCartelas[LinhaIdCartela, 5];
                ColunaOK = true;
                JogadoresRegistrados[Id_Jogador, 2] = JogadoresRegistrados[Id_Jogador, 2] + 5; // Add pontuação na matriz (id jogador, qtde cartelas, pontuacao)
                Console.WriteLine($"DEU COLUNA PARA O JOGADOR {Id_Jogador}! NA CARTELA: {Id_Cartela}");
            }
            break;
        case 2:
            // valida BINGO
            AcertosCartela = 0;
            AcertosCartela = MatrizCartelas[LinhaAcertosCartela, 5];
            if (AcertosCartela == 25)
            {
                Id_Jogador = MatrizCartelas[LinhaIdJogador, 5];
                Id_Cartela = MatrizCartelas[LinhaIdCartela, 5];
                Bingo = true;
                JogadoresRegistrados[Id_Jogador, 2] = JogadoresRegistrados[Id_Jogador, 2] + 1; // Add pontuação na matriz (id jogador, qtde cartelas, pontuacao)
                Console.WriteLine($"BINGO!!!!!!!! PARA O JOGADOR {Id_Jogador}! NA CARTELA: {Id_Cartela}");
            }
            break;
    }
}

MenuJogadores();
EnumerarRoleta();
imprimirCartelas();

Console.WriteLine("\nAperte qualquer tecla para realizar os sorteios.");
Console.ReadKey();
for (int i = 0; i < 99; i++)
{
    VerificarSorteado();
}
Console.WriteLine("-------------------");
imprimirCartelas();

Console.WriteLine("\nSorteados");
for (int i = 0; i < RoletaSorteados.Length; i++)
{
    Console.Write(RoletaSorteados[i] + " ");
}

Console.ReadKey();

// print debugar
/*
Console.WriteLine("\nRoleta");
for (int i = 0; i < Roleta.Length; i++)
{
    Console.Write(Roleta[i] + " ");
}

Console.WriteLine("\nSorteados");
for (int i = 0; i < RoletaSorteados.Length; i++)
{
    Console.Write(RoletaSorteados[i] + " ");
}

Console.WriteLine("\nCartela");
for (int linha = 0; linha < Linhas; linha++)
{
    Console.WriteLine();
    for (int coluna = 0; coluna < Colunas; coluna++)
    {
        Console.Write(MatrizCartelas[linha, coluna] + " "); 
    }
}
*/
