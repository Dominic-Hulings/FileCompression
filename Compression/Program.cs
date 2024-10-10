using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Buffers.Text;
using System.Text.Unicode;
using Microsoft.VisualBasic;

class Init // Class for initialization of the program
{
    static void Main() //* As of right now there is nothing to initialize, but I want to leave it here in case there is something that does in the future
    {
        Run.App(); // Line 16
    }
}

class Run // Class to ensure that terminal always runs when commands are completed
{
    public static void App()
    {
        Console.WriteLine("Welcome to [name of program], type 'Help' for documentation.");
        while(true) //* Lines 21 and 22 need to be in the while true loop so once user input is read and finished executing it asks for more input
            {
                Console.Write("~: "); string input = Console.ReadLine(); // First half of line just makes the prompt look prettier, last part reads input 
                Commands.ReadCommand(input); // TODO: Fix Null reference errors
            }
    }
}

class Commands // Class to interpret user input to commands
{
    public static void ReadCommand(string InCmd)
    {
        string[] InCmdRead = InCmd.Split(); // Separates words in a command to enable subcommands

        if(InCmdRead[0].ToLower() == "comp") // Compresses a file with path specified in next word of command
        {
            Compression.Init(InCmdRead[1].ToLower());
        }

        else if(InCmdRead[0].ToLower() == "rm")
        {
            File.Delete("C:/Users/22008643CTC/Projects/CompressionThing/Compression/output/output.txt");
        }

        else if(InCmdRead[0].ToLower() == "help") // Help doc... duh
        {
            string ReadText = File.ReadAllText("C:/Users/22008643CTC/Projects/CompressionThing/Compression/docs/Help.txt");
            Console.WriteLine(ReadText);
        }

        else if(InCmdRead[0].ToLower() == "exit") // Exits the program
        {
            Environment.Exit(1);
        }

        else if(InCmdRead[0].ToLower() == "clear") // Clears terminal
        {
            Console.Clear();
        }

        else // Runs if 1st command isn't recognized
        {
            Console.WriteLine("The command: '" + InCmdRead[0] + "' was not recognized, please run help for documentation on commands."); // TODO: Make a not recognized command for later commands
        }
    }
}

class Compression // Compresses the file at the path specified
{
    public static void Init(string File) //* Initializes anything that needs to be initialized before actual compression begins
    {
        if(File.ToLower() == "test") //* Just for testing so I don't have to write out the path every time. Remove later
        {
            File = "C:/Users/22008643CTC/Projects/CompressionThing/Compression/testFiles/TestFile.cs";
        }
        Comp(File);
    }

    public static void Comp(string FilePath) // Compression algorithm
    {
        int HexCode = 164; // Starts using unicode characters after normal ascii characters
        string ParsedWord; // Initializes variable to be assigned to each word in FullFile list in foreach loop
        Dictionary<string, int> WordList = new Dictionary<string, int>(); // Creates a dictionary to hold an array of KeyValuePairs of a word that appears in the targeted file and how much it appears in the file
        Dictionary<string, int> RepList = new Dictionary<string, int>(); // Creates another dictionary to store a key and the hex code of the unicode character it should be replaced with
        string[] FullFile = File.ReadAllText(FilePath).Split(' '); // Array of every word in targeted file
        //* ¡ = tab     Console.WriteLine(Strings.ChrW(161).ToString());
        //* ¢ = newLine     Console.WriteLine(Strings.ChrW(162).ToString());
        foreach(string word in FullFile)
        {
            ParsedWord = word.Replace(" ", Strings.ChrW(161).ToString()).Replace("\n", Strings.ChrW(162).ToString()).Replace("\r", "");
            try // Try to find ParsedWord in WordList and increment the key's corresponding value by 1
            {
                ++WordList[ParsedWord];
            }

            catch // If try fails, add the ParsedWord to the end of the dictionary with the value of 1 to represent that it's the first time it appeared in the file
            {
                WordList.Add(ParsedWord, 1);
            }        
        }
        
        var SortedList = WordList.OrderBy(item => item.Value).ToDictionary(item => item.Key, item => item.Value); // Sorts the dictionary by Value (Smallest to Largest)
        int Pairs = SortedList.Count() - 1; // Finds total number of pairs to easily use as an indexer for the last (and correspondingly the one with the largest value) pair

        for(int nums = 0; nums != SortedList.Count(); ++nums) //! UNFINISHED -- Loop through pairs and assign each repeat word to a Unicode character for reference
        {
            string Key = SortedList.ElementAt(Pairs).Key;
            int Value = SortedList.ElementAt(Pairs).Value;

            if(Key == "" | Value <= 1 | Key.Length == 1)
            {
                --Pairs;
                continue;
            }
            else
            {
                RepList.Add(Key, HexCode);
                ++HexCode;
                --Pairs;
            }
        }
        
        RepList = RepList.ToDictionary(item => item.Key, item => item.Value);
        string OutputFilePath = "C:/Users/22008643CTC/Projects/CompressionThing/Compression/output/output.txt";
        var OutputFile = File.Create(OutputFilePath);
        OutputFile.Close();
        var AlreadyReplaced = new List<string>();

        using(StreamWriter Writer = new StreamWriter(OutputFilePath))
        {
            bool EndLine = false;
            foreach(string word in FullFile)
            {
                ParsedWord = word.Replace(" ", Strings.ChrW(161).ToString()).Replace("\n", Strings.ChrW(162).ToString()).Replace("\r", "");

                if(!RepList.ContainsKey(ParsedWord))
                {
                    Writer.Write(ParsedWord);
                }
                
                else if(!AlreadyReplaced.Contains(ParsedWord))
                {
                    Writer.Write(ParsedWord + Strings.ChrW(RepList[ParsedWord]));
                    AlreadyReplaced.Add(ParsedWord);
                }

                else
                {
                    Writer.Write(Strings.ChrW(RepList[ParsedWord]));
                }


                if(ParsedWord.Contains(Strings.ChrW(162)))
                {
                    try
                    {
                        char[] charArray = ParsedWord.ToCharArray();
                        int Counter;
                        for(Counter = 0; charArray[Counter] == Strings.ChrW(161); ++Counter);
                        ParsedWord = ParsedWord.Remove(0, Counter);
                        ParsedWord.Insert(0, Strings.ChrW(161).ToString() + Counter.ToString());
                    }
                    catch
                    {
                        Console.WriteLine("No spaces");
                    }
                }

                else
                {
                    Writer.Write(Strings.ChrW(161).ToString());
                }
            }
        }
        Console.WriteLine("File successfully compressed");
    }
}


/*
using(StreamWriter writer = new StreamWriter("Hello", false, Encoding.UTF8))
{
    writer.WriteLine(writer);
}

Strings.ChrW(HexCode)

foreach(KeyValuePair<string, int> a in WordList)
            {
                Console.WriteLine(a);
            }
*/