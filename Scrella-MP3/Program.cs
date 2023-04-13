using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Buffers;
using System.Diagnostics;
using VideoLibrary;





CustomizeConsole();
Operations();


void Operations()
{
    try
    {
        Console.WriteLine("Paste the YouTube link below.");

        string link = Console.ReadLine();

        var video = SaveVideoToDisk(link);

        CreateMp3File(video);

        RemoveVideoFile(video);

        AskToContinueOrExit();

    }
    catch (Exception error)
    {
        Console.WriteLine("Error occured while trying to download the MP3 file. ERROR: " + error.InnerException + "\nMessage: " + error.Message + "\nHelp Link: " + error.HelpLink);
    }
}

void AskToContinueOrExit() {

    Console.WriteLine("Do you want to continue? (Y/N)");
    string userContinue = Console.ReadLine().Trim().ToUpper();

    if (userContinue != "Y" && userContinue != "N")
    {
        Console.WriteLine("\nPlease pick a valid option.");
        AskToContinueOrExit();
    }
    else
    {
        switch (userContinue) {

            case "Y":
                Operations();
                break;

            case "N":
                Goodbye();
                break;
        
        }
            
    }
}

YouTubeVideo SaveVideoToDisk(string link)
{
    Console.WriteLine("Extracting audio from video...\n");

    var youTube = YouTube.Default;
    var video = youTube.GetVideo(link);

    File.WriteAllBytes(System.IO.Directory.GetCurrentDirectory() + video.FullName, video.GetBytes());

    return video;
}


void CreateMp3File(YouTubeVideo video)
{

    Console.WriteLine("Creating MP3 file \n\n");

    string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    // Check if the Mp3 folder exists
    string subFolderPath = Path.Combine(path, "Mp3\\");

    if (!Directory.Exists(subFolderPath))
    {
        // Create the Mp3 folder if it doesn't exist
        Console.WriteLine("Scrella could not find an Mp3 folder.\nCreating Documents/Mp3 folder... \n\n");
        Directory.CreateDirectory(subFolderPath);
    }

    var inputFile = new MediaFile { Filename = System.IO.Directory.GetCurrentDirectory() + video.FullName };
    var outputFile = new MediaFile { Filename = subFolderPath + video.FullName.Remove(video.FullName.Length - 4) + ".mp3" };

    Console.WriteLine("Saving MP3 file to " + subFolderPath + "\n\n");

    using var engine = new MediaToolkit.Engine();
    engine.GetMetadata(inputFile);
    engine.Convert(inputFile, outputFile);
    

}

void RemoveVideoFile(YouTubeVideo video)
{
    Console.WriteLine("Deleting original video...");
    File.Delete(System.IO.Directory.GetCurrentDirectory() + video.FullName);

}

void CustomizeConsole()
{

    Console.WindowWidth = 150;
    Console.BackgroundColor = ConsoleColor.DarkBlue;
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.White;
    Console.Title = "Scrella MP3";
    string art = @"




███████╗ ██████╗██████╗ ███████╗██╗     ██╗      █████╗               ███╗   ███╗██████╗ ██████╗ 
██╔════╝██╔════╝██╔══██╗██╔════╝██║     ██║     ██╔══██╗              ████╗ ████║██╔══██╗╚════██╗
███████╗██║     ██████╔╝█████╗  ██║     ██║     ███████║    █████╗    ██╔████╔██║██████╔╝ █████╔╝
╚════██║██║     ██╔══██╗██╔══╝  ██║     ██║     ██╔══██║    ╚════╝    ██║╚██╔╝██║██╔═══╝  ╚═══██╗
███████║╚██████╗██║  ██║███████╗███████╗███████╗██║  ██║              ██║ ╚═╝ ██║██║     ██████╔╝
╚══════╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝╚═╝  ╚═╝              ╚═╝     ╚═╝╚═╝     ╚═════╝ 
                                                                                                 

";

    Console.WriteLine(art + "\n \n \n \n");
}

void Goodbye() {

    Console.Clear();
    Console.CursorVisible = false;

    int screenWidth = Console.WindowWidth;
    int screenHeight = Console.WindowHeight;
    int centerX = screenWidth / 2;
    int centerY = screenHeight / 2;

    Console.SetCursorPosition(centerX - 5, centerY - 1);
    Console.WriteLine("Goodbye!");
    Console.SetCursorPosition(centerX - 4, centerY + 1);
    Console.WriteLine("See you soon & enjoy the tunes!!!");
    Thread.Sleep(500);
    Console.Clear();
    Thread.Sleep(500);

}