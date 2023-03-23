using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Diagnostics;
using VideoLibrary;




Console.WriteLine("Welcome to Scrella-MP3. Paste the Youtube link in the conosle to get started.");

string link = Console.ReadLine();

Console.WriteLine("Downloading YouTube link to " + System.IO.Directory.GetCurrentDirectory());


var video = SaveVideoToDisk(link);

Console.WriteLine("Downloading" + video.FullName + " Audio to " + System.IO.Directory.GetCurrentDirectory());


CreateMp3File(video);

RemoveVideoFile(video);



YouTubeVideo SaveVideoToDisk(string link)
{
    var youTube = YouTube.Default; // starting point for YouTube actions
    var video = youTube.GetVideo(link); // gets a Video object with info about the video

    File.WriteAllBytes(System.IO.Directory.GetCurrentDirectory() + video.FullName, video.GetBytes());

    return video;
}
void CreateMp3File(YouTubeVideo video)
{

    var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    var subFolderPath = Path.Combine(path, "mp3\\");

    var user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

    var inputFile = new MediaFile { Filename = System.IO.Directory.GetCurrentDirectory() + video.FullName };
    var outputFile = new MediaFile { Filename = subFolderPath + video.FullName.Remove(video.FullName.Length - 4) + ".mp3" };

    using (var engine = new MediaToolkit.Engine())
    {
        engine.GetMetadata(inputFile);

        engine.Convert(inputFile, outputFile);
    }

}

void RemoveVideoFile(YouTubeVideo video)
{

    File.Delete(System.IO.Directory.GetCurrentDirectory() + video.FullName);

}