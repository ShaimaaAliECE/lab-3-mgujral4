using System;
using System.Collections.Generic;

namespace Lab3App
{
    public interface Displayable
    {
        void Display();
    }

    public class CollectionBoard
    {
        public int TotalScore { get; set; }
        public int TotalValue { get; set; }
    }

    public class Collectable : Displayable
    {
        public CollectionBoard Board { get; set; }

        public virtual void AddMe(List<Collectable> collected)
        {
            collected.Add(this);
        }

        public virtual void Display()
        {
            // Implement Display method for the base class if needed
        }
    }

    public class Tool : Collectable
    {
        public virtual void DoAction() { }
    }

    public class Treasure : Collectable
    {
        public int Score { get; set; }

        public void UpdateTotalScore()
        {
            Board.TotalScore += Score;
        }

        public override void AddMe(List<Collectable> collected)
        {
            base.AddMe(collected);
            UpdateTotalScore();
        }
    }

    public class MagicWand : Tool
    {
        public override void Display()
        {
            Console.WriteLine("MagicWand is used");
        }

        public override void DoAction()
        {
            Display();
        }
    }

    public class Axe : Tool
    {
        public override void Display()
        {
            Console.WriteLine("Axe is Used");
        }

        public override void DoAction()
        {
            Display();
        }
    }

    public class Diamond : Treasure
    {
        private string Name { get; set; }

        public Diamond(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public override void Display()
        {
            Console.WriteLine($"{Name} Collected, Congrats!!!!");
            Console.WriteLine($"Total Score is updated to: {Board.TotalScore}");
        }
    }

    public class Coin : Treasure
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Coin(string name, int score, int value)
        {
            Name = name;
            Score = score;
            Value = value;
        }

        public void UpdateTotalValue()
        {
            Board.TotalValue += Value;
        }

        public override void AddMe(List<Collectable> collected)
        {
            base.AddMe(collected);
            UpdateTotalValue();
        }

        public override void Display()
        {
            Console.WriteLine($"{Name} Collected, Congrats!!!!");
            Console.WriteLine($"Total Score is updated to: {Board.TotalScore}");
            Console.WriteLine($"Total Value is updated to: {Board.TotalValue}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a collection board
            CollectionBoard board = new CollectionBoard();

            List<Collectable> possibleCollectiable = new List<Collectable>();
            // Three coins 
            possibleCollectiable.Add(new Coin("Nickel", score:20, value:5));
            possibleCollectiable.Add(new Coin("Dime", score:40, value:10));
            possibleCollectiable.Add(new Coin("Toony", score: 50, value: 100));

            // Five Diamonds with descriptions Diamond1, Diamond2, ... etc.
            for (int i = 1; i <= 5; i++)
            {
                possibleCollectiable.Add(new Diamond("Diamond" + i, score: 100));
            }

            // One Axe
            possibleCollectiable.Add(new Axe("OnlyAxe"));

            // One MagicWand
            possibleCollectiable.Add(new MagicWand("OnlyMagicWand"));

            // Associate the CollectionBoard object to all the possible Collectiables
            // using a foreach loop
            foreach (Collectable collectable in  possibleCollectiable)
            {
                collectable.Board = board;
            }

            // Create an empty list to start collecting 
            List<Collectable> collected = new List<Collectable>();

            //Collect the items one-by-one in a foreach loop
            foreach (Collectable collectable in possibleCollectiable)
            {
                collectable.AddMe(collected);
            }

            Console.WriteLine("========================================");
            Console.WriteLine("==== All the Collected items ===========");
            Console.WriteLine("========================================");
            //Display all what was collected in a for each loop
            foreach (Collectable collectable in collected)
            {
                collectable.Display();
            }
        }
    }
