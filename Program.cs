using System;
using System.Media;
using System.Threading;

namespace CyberSecurityChatbot
{
    class Program
    {
        static string userName = "";

        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Bot";
            Console.Clear();

            DisplayHeader();
            PlayGreeting();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nPlease enter your name: ");
            userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName))
                userName = "User";

            BotSay($"Welcome, {userName}. I am your Cybersecurity Awareness Assistant.");
            BotSay("I will guide you on how to stay safe online.");

            MainMenu();
        }

        static void DisplayHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("============================================");
            Console.WriteLine("        CYBERSECURITY AWARENESS BOT");
            Console.WriteLine("============================================");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
        [ 🔐 Stay Safe Online 🔐 ]
");
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n--------------------------------------------");
                Console.WriteLine("               MAIN MENU");
                Console.WriteLine("--------------------------------------------");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. Password Safety");
                Console.WriteLine("2. Phishing Awareness");
                Console.WriteLine("3. Safe Browsing");
                Console.WriteLine("4. Ask a Question");
                Console.WriteLine("5. Exit");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Select an option (1-5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PasswordTips();
                        break;
                    case "2":
                        PhishingTips();
                        break;
                    case "3":
                        BrowsingTips();
                        break;
                    case "4":
                        AskQuestion();
                        break;
                    case "5":
                        BotSay("Session ended. Stay safe online.");
                        return;
                    default:
                        BotSay("Invalid selection. Please choose a number between 1 and 5.");
                        break;
                }
            }
        }

        static void PasswordTips()
        {
            BotSay("Strong passwords are essential for protecting your accounts.");
            BotSay("Use a combination of uppercase, lowercase, numbers, and symbols.");
            BotSay("Avoid using personal information such as your name or date of birth.");
            BotSay("Consider using a password manager for better security.");
        }

        static void PhishingTips()
        {
            BotSay("Phishing attacks attempt to trick you into revealing personal information.");
            BotSay("Be cautious of emails or messages that create urgency.");
            BotSay("Always verify the sender before clicking any links.");
            BotSay("If something looks suspicious, it is best to ignore or report it.");
        }

        static void BrowsingTips()
        {
            BotSay("Safe browsing helps protect your personal information online.");
            BotSay("Only visit secure websites that use HTTPS.");
            BotSay("Avoid downloading files from untrusted sources.");
            BotSay("Keep your browser and antivirus software up to date.");
        }

        static void AskQuestion()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter your question: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                BotSay("Input not recognised. Please enter a valid question.");
                return;
            }

            input = input.ToLower();

            if (input.Contains("password"))
                PasswordTips();
            else if (input.Contains("phishing"))
                PhishingTips();
            else if (input.Contains("browse") || input.Contains("safe"))
                BrowsingTips();
            else if (input.Contains("purpose"))
                BotSay("My purpose is to assist users in understanding basic cybersecurity practices.");
            else if (input.Contains("how are you"))
                BotSay("I am functioning optimally and ready to assist you.");
            else
                BotSay("I am not able to answer that directly. Please select a topic from the menu.");
        }

        static void BotSay(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Bot: ");

            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }

            Console.WriteLine("\n");
        }

        static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("greeting.wav");
                player.PlaySync();
            }
            catch
            {
                Console.WriteLine("(Audio greeting not found)");
            }
        }
    }
}