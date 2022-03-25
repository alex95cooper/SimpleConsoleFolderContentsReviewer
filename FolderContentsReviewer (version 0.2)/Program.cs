using System;
using System.IO;

namespace FolderContentsReviewer__version_0._2_
{
    class Program
    {
        private const string NumberErrorMessage = "You must enter a number!";

        private const string AddressErrorMessage = "Enter correct address!";

        public static void ShowFirstGreeting()
        {
            Console.WriteLine("Please, select your action with the key :\n" +
            "1 - Show all folders in a directory;\n" +
            "2 - Show all files in a directory;\n" +
            "3 - Exit the program.");
        }

        public static void ShowSecondGreeting()
        {
            Console.WriteLine("Do you want to continue?\n" +
            "1 - Show all folders in a selected directory;\n" +
            "2 - Show all files in a selected directory;\n" +
            "3 - Change memorized address;\n" +
            "4 - Exit the program.");
        }

        public static string GetAddress(int selectedMenuItem)
        {
            while (true)
            {
                Console.WriteLine("Enter folder address : ");
                string dirAddress = Console.ReadLine();

                if (Directory.Exists(dirAddress))
                {
                    if (selectedMenuItem == 1 || selectedMenuItem == 2)
                    {
                        return dirAddress;
                    }
                    else if (selectedMenuItem == 3)
                    {
                        ShowErrorOrNotification("Your address has been successfully changed!", ConsoleColor.DarkGreen);
                        return dirAddress;
                    }
                    return dirAddress;
                }
                else
                {
                    ShowErrorOrNotification(AddressErrorMessage, ConsoleColor.Red);
                }
            }
        }

        public static void ShowFoldersAtTheAddress(string dirAddress)
        {
            string[] dirs = Directory.GetDirectories(dirAddress);

            ShowFolderContents("Your folders :", dirs);
        }

        public static void ShowFilesAtTheAddress(string dirAddress)
        {
            string[] files = Directory.GetFiles(dirAddress);

            ShowFolderContents("Your files :", files);
        }

        public static void ShowFolderContents(string category, string[] objects)
        {
            if (objects.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(category);

                foreach (string el in objects)
                {
                    Console.WriteLine(el);
                }

                Console.ResetColor();
                Console.WriteLine();
            }
            else if (objects.Length < 1)
            {
                ShowErrorOrNotification("Files not found.", ConsoleColor.Yellow);
            }
        }

        public static void ShowErrorOrNotification(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message + "\n");
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            string dirAddress = null;

            bool addressReceived = false;
            while (!addressReceived)
            {
                ShowFirstGreeting();

                bool enterNumberFirstTime = int.TryParse(Console.ReadLine(), out int selectedMenuItem);
                if (!enterNumberFirstTime)
                {
                    ShowErrorOrNotification(NumberErrorMessage, ConsoleColor.Red);
                }
                else
                {
                    switch (selectedMenuItem)
                    {
                        case 1:
                            dirAddress = GetAddress(selectedMenuItem);
                            ShowFoldersAtTheAddress(dirAddress);
                            addressReceived = true;
                            break;
                        case 2:
                            dirAddress = GetAddress(selectedMenuItem);
                            ShowFilesAtTheAddress(dirAddress);
                            addressReceived = true;
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        default:
                            ShowErrorOrNotification("You must enter number: 1, 2 or 3, to make your choice!", ConsoleColor.Red);
                            break;
                    }
                }
            }

            while (true)
            {
                ShowSecondGreeting();

                bool enterNumberSecondTime = int.TryParse(Console.ReadLine(), out int selectedMenuItem);
                if (!enterNumberSecondTime)
                {
                    ShowErrorOrNotification(NumberErrorMessage, ConsoleColor.Red);
                }
                else
                {
                    switch (selectedMenuItem)
                    {
                        case 1:
                            ShowFoldersAtTheAddress(dirAddress);
                            break;
                        case 2:
                            ShowFilesAtTheAddress(dirAddress);
                            break;
                        case 3:
                            dirAddress = GetAddress(selectedMenuItem);
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            ShowErrorOrNotification("You must enter number: 1, 2, 3 or 4, to make your choice!", ConsoleColor.Red);
                            break;
                    }
                }
            }
        }
    }
}
