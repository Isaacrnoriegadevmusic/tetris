using System;
using System.Threading;

class Tetris
{
    static void Main()
    {
        int[,] tablero = new int[10, 20]; // Tamaño del tablero
        int[][][] figuras =
        {
            // Figura I
            new int[][] {
                new int[] { 1 },
                new int[] { 1 },
                new int[] { 1 },
                new int[] { 1 }
            },
            // Figura T
            new int[][] {
                new int[] { 1, 1, 1 },
                new int[] { 0, 1, 0 }
            },
            // Figura O
            new int[][] {
                new int[] { 1, 1 },
                new int[] { 1, 1 }
            },
            // Figura S
            new int[][] {
                new int[] { 0, 1, 1 },
                new int[] { 1, 1, 0 }
            }
        };

        int figuraIndex = 0; // Índice de la figura actual
        int[][] figura = figuras[figuraIndex]; // Figura Tetris
        int x = 4; // Posición inicial en el eje X
        int y = 0; // Posición inicial en el eje Y

        bool haLlegadoAlPiso = false;

        while (!haLlegadoAlPiso)
        {
            Console.Clear();
            MostrarTablero(tablero, x, y, figura);

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true).Key;

                // Mover la figura hacia la derecha
                if (key == ConsoleKey.D && x < tablero.GetLength(0) - figura[0].Length)
                {
                    x++;
                }
                // Mover la figura hacia la izquierda
                else if (key == ConsoleKey.A && x > 0)
                {
                    x--;
                }
                // Girar la figura en sentido horario
                else if (key == ConsoleKey.W)
                {
                    int[][] nuevaFigura = GirarFigura(figura);
                    if (EsPosibleMover(tablero, x, y, nuevaFigura))
                    {
                        figura = nuevaFigura;
                    }
                }
            }

            Thread.Sleep(100); // Retardo para simular la caída

            // Mover la figura hacia abajo
            if (y < tablero.GetLength(1) - figura.Length)
            {
                y++;
            }
            else
            {
                haLlegadoAlPiso = true;
            }
        }
    }

    static void MostrarTablero(int[,] tablero, int figuraX, int figuraY, int[][] figura)
    {
        for (int y = 0; y < tablero.GetLength(1); y++)
        {
            for (int x = 0; x < tablero.GetLength(0); x++)
            {
                if (x >= figuraX && x < figuraX + figura[0].Length && y >= figuraY && y < figuraY + figura.Length)
                {
                    if (figura[y - figuraY][x - figuraX] == 1)
                    {
                        Console.Write("█");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }

    static int[][] GirarFigura(int[][] figura)
    {
        int width = figura[0].Length;
        int height = figura.Length;
        int[][] nuevaFigura = new int[width][];

        for (int i = 0; i < width; i++)
        {
            nuevaFigura[i] = new int[height];
            for (int j = 0; j < height; j++)
            {
                // Girar la figura en sentido horario
                nuevaFigura[i][j] = figura[height - 1 - j][i];
            }
        }

        return nuevaFigura;
    }

    static bool EsPosibleMover(int[,] tablero, int x, int y, int[][] figura)
    {
        int width = figura[0].Length;
        int height = figura.Length;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (figura[j][i] == 1)
                {
                    int tableroX = x + i;
                    int tableroY = y + j;

                    if (tableroX < 0 || tableroX >= tablero.GetLength(0) || tableroY >= tablero.GetLength(1) || tablero[tableroX, tableroY] != 0)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}
