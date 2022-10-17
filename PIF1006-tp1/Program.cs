using System;
using System.Diagnostics;

namespace PIF1006_tp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Automate automate = new Automate();
            automate.LoadFromFile("automate.txt");
            int selection = -1;
            do
            {
                Console.Write("===================\n[1] Load automate\n[2] Print automate\n[3] Submit input\n[4] Close\n===================\n");
                try { selection = int.Parse(Console.ReadLine()); } catch (FormatException e) { Console.Write("Invalid input!\n"); continue; }
                Console.Clear();

                switch (selection)
                {
                    case 1:
                        Console.Write("Enter file path...\n");
                        automate.LoadFromFile(Console.ReadLine());
                        break;
                    case 2:
                        Console.Write(automate.ToString());
                        break;
                    case 3:
                        Console.Write("Enter something...\n");
                        Console.Write(automate.Validate(Console.ReadLine()) ? "Valid\n" : "Invalid\n");
                        break;
                }
            } while (selection != 4);
        }
    }
}
