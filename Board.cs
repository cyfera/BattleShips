using System;

namespace Battleships{

public class Board
{
    private readonly char[,] grid = new char[10, 10];

    public Board()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                grid[i, j] = '-';
            }
        }
    }

    public bool PlaceShip(Ship ship, int x, int y, bool vertical)
    {
        if (CanPlaceShip(ship, x, y, vertical))
        {
            if (vertical)
            {
                for (int i = y; i < y + ship.Size; i++)
                {
                    grid[i, x] = 'X';
                }
            }
            else
            {
                for (int i = x; i < x + ship.Size; i++)
                {
                    grid[y, i] = 'X';
                }
            }
            return true;
        }
        return false;
    }

    private bool CanPlaceShip(Ship ship, int x, int y, bool vertical)
    {
        if (vertical)
        {
            if (y + ship.Size > 10)
                return false;
            for (int i = y; i < y + ship.Size; i++)
            {
                if (grid[i, x] != '-')
                    return false;
            }
        }
        else
        {
            if (x + ship.Size > 10)
                return false;
            for (int i = x; i < x + ship.Size; i++)
            {
                if (grid[y, i] != '-')
                    return false;
            }
        }
        // Check for collision with horizontal ships
        if (vertical)
        {
            for (int i = Math.Max(0, y - 1); i < Math.Min(10, y + ship.Size + 1); i++)
            {
                for (int j = Math.Max(0, x - 1); j <= Math.Min(9, x + 1); j++)
                {
                    if (grid[i, j] == 'X')
                        return false;
                }
            }
        }
        else // Check for collision with vertical ships
        {
            for (int i = Math.Max(0, y - 1); i <= Math.Min(9, y + 1); i++)
            {
                for (int j = Math.Max(0, x - 1); j < Math.Min(10, x + ship.Size + 1); j++)
                {
                    if (grid[i, j] == 'X')
                        return false;
                }
            }
        }
        return true;
    }

    public bool Shoot(int x, int y)
    {
        if (grid[y, x] == 'X')
        {
            grid[y, x] = '!';
            Console.WriteLine("Gracz trafił!");
            return true;
        }
        else if (grid[y, x] == '-')
        {
            grid[y, x] = '*';
            Console.WriteLine("Pudło!");
            return false;
        }
        else if (grid[y, x] == '!')
        {
            Console.WriteLine("Gracz trafił w to samo miejsce! Powtórka tury.");
            return true;
        }
        else
        {
            Console.WriteLine("Już tu strzelałeś!");
            return true;
        }
    }

    public bool AllShipsSunk()
    {
        foreach (char cell in grid)
        {
            if (cell == 'X')
                return false;
        }
        return true;
    }

    public void Display(bool showShips)
    {
        Console.WriteLine("   A B C D E F G H I J");
        for (int i = 0; i < 10; i++)
        {
            Console.Write($"{i} ");
            for (int j = 0; j < 10; j++)
            {
                if (!showShips && grid[i, j] == 'X')
                {
                    Console.Write("- ");
                }
                else
                {
                    Console.Write(grid[i, j] + " ");
                }
            }
            Console.WriteLine();
        }
    }
}
}