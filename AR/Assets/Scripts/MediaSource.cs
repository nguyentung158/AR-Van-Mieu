using UnityEngine;

public class MediaSource
{
    string photoUrl = "";
    string videoUrl = "";
    string audioUrl = "";
    string name = "";


    public MediaSource(string photoUrl, string videoUrl, string audioUrl, string name)
    {
        this.PhotoUrl = photoUrl;
        this.VideoUrl = videoUrl;
        this.AudioUrl = audioUrl;
        this.Name = name;
    }

    public string PhotoUrl { get => photoUrl; set => photoUrl = value; }
    public string VideoUrl { get => videoUrl; set => videoUrl = value; }
    public string AudioUrl { get => audioUrl; set => audioUrl = value; }
    public string Name { get => name; set => name = value; }

}


