using System;
using System.Collections.Generic;

namespace ConnectFour
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const int ROWS = 6;
            const int COLS = 7;
            const int WINC = 4;
            const int AI_RAND_CHANCE = 20;

            // Welcome everyone.
            Console.WriteLine("Welcome to Connect Four!");

            // Create the game board.
            Board board = new Board(ROWS, COLS, WINC);

            // Determine the number of humans and ais.
            int humans, ais;
            do
            {
                try
                {
                    string? input;
                    Console.WriteLine("Please enter the number of human players.");
                    input = Console.ReadLine();
                    if (input == null)
                    {
                        Console.WriteLine("Invalid value.");
                        continue;
                    }
                    humans = int.Parse(input);
                    Console.WriteLine("Selected value: {0}", humans);
                    if (humans < 0)
                    {
                        Console.WriteLine("Invalid value.");
                        continue;
                    }
                    Console.WriteLine("Please enter the number of AI players.");
                    input = Console.ReadLine();
                    if (input == null)
                    {
                        Console.WriteLine("Invalid value.");
                        continue;
                    }
                    ais = int.Parse(input);
                    Console.WriteLine("Selected value: {0}", ais);
                    if (ais < 0)
                    {
                        Console.WriteLine("Invalid value.");
                        continue;
                    }
                    if (humans == 0 && ais == 0)
                    {
                        Console.WriteLine("Can't have 0 humans and 0 ais playing.");
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect format!");
                }
            }
            while (true);

            // Create the players.
            List<IPlayer> players = new List<IPlayer>();
            Dictionary<IPlayer, char> player2symbol_map = new Dictionary<IPlayer, char>();
            Console.WriteLine("Creating the human players...");
            for (int i = 0; i < humans; i++)
            {
                do
                {
                    try
                    {
                        Console.WriteLine("Player, please submit your name!");
                        string? name = Console.ReadLine();
                        if (name == null)
                        {
                            Console.WriteLine("Invalid column.");
                            continue;
                        }
                        Human human = new Human(name);
                        Console.WriteLine($"Player {human.Name} is added!");
                        players.Add(human);
                        player2symbol_map.Add(human, (char)('A' + i));
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Incorrect format!");
                    }
                }
                while (true);
            }

            // Create the AI.
            Console.WriteLine("Creating the AI players...");
            for (int i = 0; i < ais; i++)
            {
                AI ai = new AI(i, AI_RAND_CHANCE);
                Console.WriteLine($"Player {ai.Name} is added!");
                players.Add(ai);
                player2symbol_map.Add(ai, (char)('0' + i));
            }

            // Randomize the turn order.
            Math.Shuffle(players);

            // Run main loop.
            while (true)
            {
                foreach (IPlayer player in players)
                {
                    Console.WriteLine("Displaying the board!");
                    Displayer.Display(board, player2symbol_map);
                    Console.WriteLine($"It's {player.Name}'s turn!");
                    player.MakeMove(board, players.Where(opponent => opponent != player).ToList());
                    if (board.IsFull())
                    {
                        Console.WriteLine("It's a tie!");
                        Displayer.Display(board, player2symbol_map);
                        return;
                    }
                    IPlayer? possible_winner = board.GetWinner();
                    if (possible_winner != null)
                    {
                        Console.WriteLine($"The winner is {possible_winner.Name}!");
                        Displayer.Display(board, player2symbol_map);
                        return;
                    }       
                }
            }
        }
    }
}