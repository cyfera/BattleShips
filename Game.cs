using System;
namespace Battleships{
public class Game
{
    private readonly Board[] boards = new Board[2];
    private int currentPlayerIndex = 0;

    public Game()
    {
        boards[0] = new Board();
        boards[1] = new Board();
    }

    public void SetupShips()
    {
        Console.WriteLine("Gracz 1, ustaw swoje statki:");
        SetupPlayerShips(0);
        Console.Clear();
        Console.WriteLine("Gracz 2, ustaw swoje statki:");
        SetupPlayerShips(1);
    }

    private void SetupPlayerShips(int playerIndex)
    {
        Ship[] ships = { new Ship(1), new Ship(1) };
        Board board = boards[playerIndex];

        foreach (Ship ship in ships)
        {
            bool placed = false;
            while (!placed)
            {
                Console.Clear();
                board.Display(showShips: true);
                Console.WriteLine($"Ustaw statek o długości {ship.Size} (np. A0):");
                string inputCoord = Console.ReadLine().ToUpper();

                if (inputCoord.Length != 2 || !char.IsLetter(inputCoord[0]) || !char.IsDigit(inputCoord[1]))
                {
                    Console.WriteLine("Nieprawidłowy format współrzędnych! Spróbuj ponownie.");
                    continue;
                }

                int x = inputCoord[0] - 'A';
                int y = inputCoord[1] - '0';

                if (x < 0 || x >= 10 || y < 0 || y >= 10)
                {
                    Console.WriteLine("Nieprawidłowe współrzędne! Spróbuj ponownie.");
                    continue;
                }

                Console.WriteLine("Podaj kierunek (H - poziomo, V - pionowo):");
                string inputDirection = Console.ReadLine().ToUpper();

                if (inputDirection != "H" && inputDirection != "V")
                {
                    Console.WriteLine("Nieprawidłowy kierunek! Spróbuj ponownie.");
                    continue;
                }

                bool vertical = inputDirection == "V";

                if (board.PlaceShip(ship, x, y, vertical))
                {
                    placed = true;
                }
                else
                {
                    Console.WriteLine("Nie można postawić statku tutaj! Spróbuj ponownie.");
                }
            }
        }
    }

public void Play()
    {
        SetupShips();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Plansza gracza:");
            boards[currentPlayerIndex].Display(showShips: true);
            Console.WriteLine("Plansza przeciwnika:");
            boards[1 - currentPlayerIndex].Display(showShips: false);

            Console.WriteLine($"Gracz {currentPlayerIndex + 1}, podaj współrzędne strzału (np. A5):");
            string input = Console.ReadLine().ToUpper();
            if (input.Length != 2 || !char.IsLetter(input[0]) || !char.IsDigit(input[1]))
            {
                Console.WriteLine("Nieprawidłowy format współrzędnych!");
                continue;
            }

            int x = input[0] - 'A';
            int y = input[1] - '0';

            if (x < 0 || x >= 10 || y < 0 || y >= 10)
            {
                Console.WriteLine("Nieprawidłowe współrzędne!");
                continue;
            }

            if (boards[1 - currentPlayerIndex].Shoot(x, y))
            {
                Console.WriteLine("Gracz trafił! Ma kolejną turę.");
                if (boards[1 - currentPlayerIndex].AllShipsSunk())
                {
                    Console.WriteLine($"Gracz {currentPlayerIndex + 1} wygrał!");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Gracz nie trafił. Kolej na przeciwnika.");
                currentPlayerIndex = 1 - currentPlayerIndex;
            }
        }
    }
}
}