using System;
using System.IO;
using System.Collections.Generic; 

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
                Console.Write("~: "); string? input = Console.ReadLine(); 
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
            try
            {
                 Compression.Init(InCmdRead[1]);
            }
            catch
            {
                Console.WriteLine("Please specify the path to the file");
            }
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
    public static void Init(string File)
    {
        if(File == "test")
        {
            File = "C:/Users/22008643CTC/Projects/CompressionThing/Compression/testFiles/TestFile.cs";
        }
        Comp(File);

    }

    public static void Comp(string FilePath)
    {
        Dictionary<string, int> WordList = new Dictionary<string, int>();
        string[] FullFile = File.ReadAllLines(FilePath);

        foreach(string word in FullFile)
        {
            try
            {
                int Value = WordList[word];
                Console.WriteLine(Value);
            }

            catch
            {
                WordList.Add(word, 1);
            }        
        }
    }
}