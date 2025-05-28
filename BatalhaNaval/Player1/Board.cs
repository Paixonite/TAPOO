using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Board
{
    const int HEIGHT = 10;
    const int WIDTH = 10;

    const char EMPTY = '~';
    const char SHIP = 'N';
    const char HIT = 'X';
    const char MISS = 'O';
  
    public char[,] board = new char[HEIGHT, WIDTH];

    public Board()
    {
        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                board[i, j] = EMPTY;
            }
        }
    }

    public void PrintBoard()
    {
        Console.WriteLine("  0 1 2 3 4 5 6 7 8 9");
        for (int i = 0; i < HEIGHT; i++)
        {
            Console.Write((char)('A' + i) + " ");
            for (int j = 0; j < WIDTH; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void PlaceShipsRandomly(int numShips)
    {
        Random rand = new Random();
        for (int i = 0; i < numShips; i++)
        {
            int x, y;
            do
            {
                x = rand.Next(0, HEIGHT);
                y = rand.Next(0, WIDTH);
            } while (board[x, y] != EMPTY);
            board[x, y] = SHIP;
        }
    }

    public void PlaceShipManually(int x, int y)
    {
        if (board[x, y] != EMPTY)
        {
            throw new InvalidOperationException("Position already occupied.");
        }
        if (x >= 0 && x < HEIGHT && y >= 0 && y < WIDTH)
        {
            board[x, y] = SHIP; // Place ship
        }
        else
        {
            throw new ArgumentOutOfRangeException("Coordinates out of range.");
        }
    }

    public bool Hit(int x, int y)
    {
        if (x >= 0 && x < HEIGHT && y >= 0 && y < WIDTH)
        {
            if (board[x, y] == SHIP)
            {
                board[x, y] = HIT; // Hit
                return true;
            }
            else
            {
                board[x, y] = MISS; // Miss
                return false;
            }
        }
        else
        {
            Console.WriteLine("Invalid position!");
            throw new ArgumentOutOfRangeException("Coordinates out of range.");
        }
    }

    public (int, int) ParseInput(string input)
    {
        if (input.Length != 2)
        {
            throw new FormatException("Input must be two characters.");
        }

        int x = char.ToUpper(input[0]) - 'A'; // Convert letter to number (A=0, B=1, ..., J=9)
        int y = input[1] - '0';

        if (x < 0 || x >= HEIGHT || y < 0 || y >= WIDTH)
        {
            throw new ArgumentOutOfRangeException("Coordinates out of range.");
        }

        return (x, y);
    }

    public bool IsGameOver()
    {
        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                if (board[i, j] == SHIP)
                {
                    return false; // Game is not over
                }
            }
        }
        return true; // All ships are sunk
    }
}
