using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Init
{
    static void Main()
    {
        Run.App();
    }
}

class Run
{
    public static void App()
    {
        Console.WriteLine("Welcome to [name of program], type 'Help' for documentation.");
        while(true)
            {
                Console.Write("~: "); string input = Console.ReadLine();
                Commands.ReadCommand(input);
            }
    }
}

class Commands
{
    public static void ReadCommand(string InCmd)
    {
        string[] InCmdRead = InCmd.Split();

        if(InCmdRead[0].ToLower() == "comp")
        {
            Compression.Init(InCmdRead[1].ToLower());
        }

        else if(InCmdRead[0].ToLower() == "help")
        {
            string ReadText = File.ReadAllText("C:/Users/22008643CTC/Projects/CompressionThing/Compression/docs/Help.txt");
            Console.WriteLine(ReadText);
        }

        else if(InCmdRead[0].ToLower() == "exit")
        {
            Environment.Exit(1);
        }

        else if(InCmdRead[0].ToLower() == "clear")
        {
            Console.Clear();
        }

        else
        {
            Console.WriteLine("The command: '" + InCmdRead[0] + "' was not recognized, please run help for documentation on commands.");
        }

    }
}

class Compression
{
    public static int Init(string File)
    {
        if(File.ToLower() == "test")
        {
            File = "C:/Users/22008643CTC/Projects/CompressionThing/Compression/testFiles/TestFile.cs";
        }
        Comp(File);
        return 1;

    }

    public static void Comp(string FilePath)
    {
        string ParsedWord;
        Dictionary<string, int> WordList = new Dictionary<string, int>();
        string[] FullFile = File.ReadAllText(FilePath).Split(' ');

        foreach(string word in FullFile)
        {
            ParsedWord = word.Replace("\t", "").Replace("\n", "").Replace("\r", "");
            try
            {
                ++WordList[ParsedWord];
            }

            catch
            {
                WordList.Add(ParsedWord, 1);
            }        
        }

        var SortedList = WordList.OrderBy(item => item.Value).ToDictionary(item => item.Key, item => item.Value);
        int Pairs = SortedList.Count() - 1;
       
        for(int nums = 0; nums != SortedList.Count(); ++nums)
        {
            string Key = SortedList.ElementAt(Pairs).Key;
            int Value = SortedList.ElementAt(Pairs).Value;
        }
    }
}