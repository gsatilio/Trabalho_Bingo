int Linhas = 100, Colunas = 5, ColunasReal = 6; // Estrutura 5x5 com + 1 coluna pra salvar dados
int Jogadores = 0, Cartelas = 0;

int LinhaCartelaPessoa = 0, LinhaTotal = 0;

int Id_Jogador = 0;
int Id_Cartela = 0;

//Variaveis
int ContadorSorteados = 0;

//Vetores
int[] Roleta = new int[99];
int[] RoletaGeral = new int[99];
int[] RoletaSorteados = new int[99];


//Matrizes
int[,] MatrizCartelas = new int[Linhas, ColunasReal];
int[,] JogadoresRegistrados = new int[50,3];



void PopularVetorRoleta()
{
    // Popula numeros de 1 a 99 no vetor Roleta
    for (int i = 0; i < RoletaGeral.Length; i++)
    {
        RoletaGeral[i] = i + 1;
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

    while(Id_Jogador < Jogadores)
    {
        while (QtdeCartela <= 0)
        {
            Console.WriteLine($"Quantas cartelas para o {Id_Jogador+1}o jogador?");
            QtdeCartela = int.Parse(Console.ReadLine());
        }

        PopularVetorRoleta();
        // Vetor JogadoresRegistrados tem 100 casas (aqui salvo ID JOGADOR e QTDE CARTELAS)
        JogadoresRegistrados[JogadorRegistradoIndice,0] = Id_Jogador;
        JogadoresRegistrados[JogadorRegistradoIndice,1] = QtdeCartela;
        JogadoresRegistrados[JogadorRegistradoIndice,2] = 1;
        JogadorRegistradoIndice++;

        //Cartelas += QtdeCartela;
        CriarCartelas(Id_Jogador, QtdeCartela);
        Id_Jogador++;
        QtdeCartela = 0;
    }
    //MatrizCartelas = new int[Cartelas, ColunasReal];
    
}


/*
X | X | X | X | X | ID_PESSOA  
X | X | X | X | X | ID_CARTELA
X | X | X | X | X | L (0 ou 1)
X | X | X | X | X | C (0 ou 1)
X | X | X | X | X | V (0 ou 1)
*/
void CriarCartelas(int jogador, int quantidade)
{
    bool ResetaPopular = false;
    int contador = 0;
    int LinhasCartela = (quantidade * 4);

    for (int linha = 0; linha <= LinhasCartela; linha++)
    {
        for (int coluna = 0; coluna < ColunasReal; coluna++)
        {
            switch (coluna)
            {
                case < 5:
                    if (contador == 4)
                    {
                        ResetaPopular = true;
                        break;
                    }
                    MatrizCartelas[LinhaTotal, coluna] = SorteioCartela(0, ResetaPopular);
                    break;
                case 5:
                    if (LinhaTotal == LinhaCartelaPessoa)
                    {
                        MatrizCartelas[LinhaTotal, coluna] = jogador;
                    }
                    else if (LinhaTotal == LinhaCartelaPessoa + 1)
                    {
                        MatrizCartelas[LinhaTotal, coluna] = Id_Cartela;
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

//Funções
int SorteioCartela(int tipo, bool ResetaPopular)
{
    int i = 0, sorteado = 0;

    switch (tipo)
    {
        case 0: // popular cartela
            if (ResetaPopular)
            {
                PopularVetorRoleta();
            }
            while (sorteado == 0) // Enquanto numero sorteado for zero
            {
                i = new Random().Next(0, 99);                   // Pega um indice aleatorio entre 0 e 99
                sorteado = RoletaGeral[i];                           // Sorteado recebe o numero da roleta nesse indice
            }
            break;
        case 1: // sortear bolinha
            while (sorteado == 0) // Enquanto numero sorteado for zero
            {
                i = new Random().Next(0, 99);                   // Pega um indice aleatorio entre 0 e 99
                sorteado = Roleta[i];                           // Sorteado recebe o numero da roleta nesse indice
                RoletaSorteados[ContadorSorteados] = sorteado;  // Salva esse numero no vetor de sorteados
                Roleta[i] = 0;                                  // Muda o valor do vetor no indice selecionado, para zero
            }
            ContadorSorteados++;                                // Aumenta contador de sorteado (controlador do indice do vetor)
            break;
    }
    return sorteado;                                        // Retorna o valor
}











///// debugar
MenuJogadores();



// print debugar
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
    for (int coluna = 0; coluna < ColunasReal; coluna++)
    {
        Console.Write(MatrizCartelas[linha, coluna] + " "); 
    }
}

Console.ReadKey();