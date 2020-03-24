using System;
using tabuleiro;
using tabuleiro.enums;
namespace Xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.Peca(i, j) != null)
                        Console.Write(tab.Peca(tab.Linhas, tab.Colunas) + " ");
                    else
                        Console.Write("- ");
                }
                Console.WriteLine();
            }
        }
    }
}
