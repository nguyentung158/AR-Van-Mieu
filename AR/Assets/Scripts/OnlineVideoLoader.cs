using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class OnlineVideoLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    [SerializeField]
    public VideoClip videoClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void playVideo(string videoUrl)
    {
        //videoPlayer.clip = ;
        //videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        //videoPlayer.EnableAudioTrack(0, true);
        //videoPlayer.Prepare();
        var tex = new RenderTexture(1920, 1080, 16);
        videoPlayer.targetTexture = tex;
        rawImage.texture = tex;
    }
}
