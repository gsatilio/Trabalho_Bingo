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
int Jogadores = 0, Cartelas = 0;
int LinhaCartelaPessoa = 0, LinhaTotal = 0;
int Id_Jogador = 0, Id_Cartela = 0, NroSorteado = 0;

bool Bingo = false, LinhaOK = false, ColunaOK = false;

//Vetores
int[] Roleta = new int[99];
int[] RoletaGeral = new int[99];
int[] RoletaSorteados = new int[99];

//Matrizes
int[,] MatrizCartelas = new int[Linhas, Colunas];
int[,] MatrizCartelasClone = new int[0, 0];
int[,] JogadoresRegistrados = new int[50, 3];
int[,] MatrizNumeros = new int[0, 100];

void EnumerarCartelas()
{
    // Popula numeros de 1 a 99 no vetor Roleta
    for (int i = 0; i < RoletaGeral.Length; i++)
    {
        RoletaGeral[i] = i + 1;
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
void MenuJogadores()
{
    int QtdeCartela = 0;
    int JogadorRegistradoIndice = 0;
    while (Jogadores <= 0)
    {
        Console.WriteLine("Quantos jogadores?");
        Jogadores = int.Parse(Console.ReadLine());
    }

    while (Id_Jogador < Jogadores)
    {
        while (QtdeCartela <= 0)
        {
            Console.WriteLine($"Quantas cartelas para o {Id_Jogador + 1}o jogador?");
            QtdeCartela = int.Parse(Console.ReadLine());
        }

        EnumerarCartelas();
        // Vetor JogadoresRegistrados tem 100 casas (aqui salvo ID JOGADOR e QTDE CARTELAS)
        JogadoresRegistrados[JogadorRegistradoIndice, 0] = Id_Jogador;
        JogadoresRegistrados[JogadorRegistradoIndice, 1] = QtdeCartela;
        JogadoresRegistrados[JogadorRegistradoIndice, 2] = 1;
        JogadorRegistradoIndice++;

        Cartelas += QtdeCartela;
        CriarCartelas(Id_Jogador, QtdeCartela);
        Id_Jogador++;
        QtdeCartela = 0;
    }
}
void CriarCartelas(int jogador, int quantidade)
{
    bool Resetar = false;
    int contador = 0;
    int LinhasCartela = (quantidade * 5);

    for (int linha = 0; linha < LinhasCartela; linha++)
    {
        for (int coluna = 0; coluna < Colunas; coluna++)
        {
            switch (coluna)
            {
                case < 5:
                    if (contador == 4)
                    {
                        Resetar = true;
                    }
                    MatrizCartelas[LinhaTotal, coluna] = PreencherCartela(Resetar);
                    break;
                case 5:
                    if (LinhaTotal == LinhaCartelaPessoa)
                    {
                        MatrizCartelas[LinhaTotal, coluna] = jogador;       // ID JOGADOR
                    }
                    else if (LinhaTotal == LinhaCartelaPessoa + 1)
                    {
                        MatrizCartelas[LinhaTotal, coluna] = Id_Cartela;    // ID CARTELA
                        LinhaCartelaPessoa += 5;
                        Id_Cartela++;
                    }
                    break;
            }
            contador++;
        }
        LinhaTotal++;
    }
}
int PreencherCartela(bool Resetar)
{
    int i = 0, sorteado = 0;
    if (Resetar)
    {
        EnumerarCartelas();
    }
    while (sorteado == 0) // Enquanto numero sorteado for zero
    {
        i = new Random().Next(1, 98);                   // Pega um indice aleatorio entre 0 e 99
        sorteado = RoletaGeral[i];                           // Sorteado recebe o numero da roleta nesse indice
    }
    return sorteado;
}
void imprimirCartelas()
{
    int pularlinha = 0;
    Console.WriteLine("\nCartela Normal:");
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

    Console.WriteLine("\nCartela Clonada:");
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
                Console.Write("[" + MatrizCartelasClone[linha, coluna].ToString().PadLeft(2, '0') + "]");
            }
            else
            {
                Console.Write(MatrizCartelasClone[linha, coluna].ToString().PadLeft(2, '0') + " ");
            }
        }
        pularlinha++;
    }
}
void ClonarMatrizCartelas()
{
    MatrizCartelasClone = new int[LinhaTotal, 6];
    for (int linha = 0; linha < LinhaTotal; linha++)
    {
        for (int coluna = 0; coluna < Colunas; coluna++)
        {
            MatrizCartelasClone[linha, coluna] = MatrizCartelas[linha, coluna];
        }
    }
}


void Sortear()
{
    int i = 0, Sorteio = 0;
    bool Sorteou = false;
    for(int tentativas = 0; (tentativas < 99 && !Sorteou); tentativas++)
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
    int achou = 0;
    while (ContadorSorteados < 99)
    {
        Sortear(); // sorteia assim que acabam as linhas e contador ainda for menor que 100
        if (ContadorSorteados == 99)
        {
            Console.WriteLine();
        }
        for (int linha = 0; linha < LinhaTotal; linha++) // varre todas as linhas
        {
            for (int coluna = 0; coluna < Colunas; coluna++) // varre as colunas de 0 a 4
            {
                if (coluna < 5)
                {
                    if (MatrizCartelasClone[linha, coluna] == NroSorteado)
                    {
                        achou++;
                        MatrizCartelasClone[linha, coluna] = 0; // se localizar o nro sorteado, altera para 0 no vetor clonado
                    }
                }
                if (linha == 5 && coluna == 5)
                {
                    Console.WriteLine();
                }
            }
            //VerificarLinha();
        }
    }
}

void VerificarLinha()
{
    int ValorAnterior = 0, ValorAtual = 0;
    for (int linha = 0; linha < LinhaTotal; linha++)
    {
        for (int coluna = 0; coluna < Colunas; coluna++)
        {
            if (coluna < 5)
            {
                ValorAtual = MatrizCartelasClone[linha, coluna];
                if (ValorAtual == ValorAnterior)
                {
                    LinhaOK = true;
                }
                else
                {
                    LinhaOK = false;
                }
                ValorAnterior = ValorAtual;
            }
        }
        ValorAtual = 0;
    }
    // return DeuLinha;

    if (LinhaOK && !Bingo)
    {
        Bingo = true;
        Console.WriteLine($"\nbinnnnnnnnnnngo:");
    }
}



MenuJogadores();
ClonarMatrizCartelas();
EnumerarRoleta();


// Sorteia
for (int i = 0; i < 99; i++)
{
    VerificarSorteado();
}



Console.WriteLine("\nSorteados");
for (int i = 0; i < RoletaSorteados.Length; i++)
{
    Console.Write(RoletaSorteados[i] + " ");
}

imprimirCartelas();

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
