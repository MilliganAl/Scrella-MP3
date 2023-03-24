using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Diagnostics;
using VideoLibrary;


CustomizeConsole();

Console.WriteLine("Downloading YouTube link to " + System.IO.Directory.GetCurrentDirectory() + "\n\n");
string link = Console.ReadLine();

var video = SaveVideoToDisk(link);

CreateMp3File(video);   

RemoveVideoFile(video);



YouTubeVideo SaveVideoToDisk(string link)
{
    var youTube = YouTube.Default;
    var video = youTube.GetVideo(link);

    File.WriteAllBytes(System.IO.Directory.GetCurrentDirectory() + video.FullName, video.GetBytes());

    return video;
}


void CreateMp3File(YouTubeVideo video)
{

    Console.WriteLine("Creating MP3 file \n\n");

    var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    var subFolderPath = Path.Combine(path, "Mp3\\");

    var user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

    var inputFile = new MediaFile { Filename = System.IO.Directory.GetCurrentDirectory() + video.FullName };
    var outputFile = new MediaFile { Filename = subFolderPath + video.FullName.Remove(video.FullName.Length - 4) + ".mp3" };


    Console.WriteLine("Saving MP3 file to " + subFolderPath + "\n\n");
    using (var engine = new MediaToolkit.Engine())
    {
        engine.GetMetadata(inputFile);

        engine.Convert(inputFile, outputFile);
    }

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