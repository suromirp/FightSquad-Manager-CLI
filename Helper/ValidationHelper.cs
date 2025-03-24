using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FightSquad___CLI.Helper
{
    public static class ValidationHelper
    {
        private static readonly Regex nameRegex = new Regex(
             @"^(?!-+$)(?!.* {2})[A-Za-zÀ-ÖØ-öø-ÿ-]+(?: [A-Za-zÀ-ÖØ-öø-ÿ-]+)*$",
            RegexOptions.Compiled
        );

        public static bool IsValidName(string name)
        {
            return nameRegex.IsMatch(name);
        }

        public static bool ValidateNameAndPrompt(string prompt, out string ValidName)
        {
            string? input;
            do
            {
                Console.Write(prompt);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Reads key without displaying it

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    ValidName = string.Empty;
                    return false; // Back to previous menu
                }

                input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Name cannot be empty, please try again.");
                }
                else if (!IsValidName(input!))
                {
                    Console.WriteLine("Enter a name (1-50 letters, '-' allowed, no numbers or special characters)\n");

                }
            } while (!IsValidName(input!));

            ValidName = input!;
            return true; // Next menu or back to main menu
        }
    }
}
