int Linhas = 5, Colunas = 5, ColunasReal = 6; // Estrutura 5x5 com + 1 coluna pra salvar dados
/*
X | X | X | X | X | indice_pessoa  
X | X | X | X | X | indice_cartela
X | X | X | X | X | L (0 ou 1)
X | X | X | X | X | C (0 ou 1)
X | X | X | X | X | V (0 ou 1)
*/


//Variaveis
int ContadorSorteados = 0;

//Vetores
int[] Roleta = new int[99];
int[] RoletaSorteados = new int[99];

//Matrizes
int[,] Cartela = new int[Linhas,Colunas];

//Funções
int SorteioCartela()
{
    int i = 0, sorteado = 0;
    // escolhe um numero e muda ele pra zero e pega maior que zero
    while (sorteado == 0)
    {
        i = new Random().Next(0, 99);
        sorteado = Roleta[i];
        RoletaSorteados[ContadorSorteados] = sorteado; 
        Roleta[i] = 0;
    }
    ContadorSorteados++;
    return sorteado;
}



// Popula numeros de 1 a 99 no vetor Roleta
for (int i = 0; i < Roleta.Length; i++)
{
    Roleta[i] = i+1;
}

// Popula Cartela 1 no VetorCartela (25 numeros Aleatorios)
for (int linha = 0; linha < Linhas; linha++)
{
    for (int coluna = 0; coluna < Colunas; coluna++)
    {
        Cartela[linha, coluna] = SorteioCartela();
    }
}
















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
    for (int coluna = 0; coluna < Colunas; coluna++)
    {
        Console.Write(Cartela[linha, coluna] + " "); 
    }
}
//

Console.ReadKey();