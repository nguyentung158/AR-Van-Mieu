using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class MediaController : MonoBehaviour
{
    [SerializeField]
    GameObject imageScreen;

    [SerializeField]
    GameObject videoScreen;

    [SerializeField]
    GameObject audioScreen;

    [SerializeField]
    GameObject btnsPanel;

    [SerializeField]
    VideoPlayer videoPlayer;

    [SerializeField]
    MediaSource mediaSource;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    GameObject closeBtn;

    [SerializeField]
    GameObject playBtn;

    [SerializeField]
    GameObject pauseBtn;

    [SerializeField]
    Button[] mediaBtns;

    [SerializeField]
    GameObject infoBtn;

    [SerializeField]
    Slider timeSlider;

    [SerializeField]
    Text currentTimeText;

    [SerializeField]
    Text totalTimeText;

    [SerializeField]
    AudioClip[] audioClips;

    [SerializeField]
    VideoClip[] videoClips;

    AudioClip myClip;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateSlider());
        StartCoroutine(LoadImageFromUrl(mediaSource.PhotoUrl));
        StartCoroutine(prepareVideo());
        audioSource.clip = null;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void showVideoScreen()
    {
        imageScreen.SetActive(false);
        audioScreen.SetActive(false);
        Color color = mediaBtns[2].GetComponent<Image>().color;
        color.a = 0.5f;
        mediaBtns[2].GetComponent<Image>().color = color;
        stopAudio();
        if (videoScreen.activeSelf)
        {
            //StopVideo();
            //videoScreen.SetActive(false);
            //closeBtn.SetActive(false);
        }
        else
        {
            videoScreen.SetActive(true);
            closeBtn.SetActive(true);

            PlayVideo();
        }
    }

    public void showImageScreen()
    {

        StopVideo();
        stopAudio();
        audioScreen.SetActive(false);
        videoScreen.SetActive(false);
        if (imageScreen.activeSelf)
        {
            //closeBtn.SetActive(false);
            //imageScreen.SetActive(false);
            //Color btnColor = mediaBtns[2].GetComponent<Image>().color;
            //btnColor.a = 0.5f;
            //mediaBtns[2].GetComponent<Image>().color = btnColor;
        }
        else
        {
            closeBtn.SetActive(true);
            imageScreen.SetActive(true);
            Color btnColor = mediaBtns[2].GetComponent<Image>().color;
            btnColor.a = 1f;
            mediaBtns[2].GetComponent<Image>().color = btnColor;
        }
    }

    public void showAudioScreen()
    {
        videoScreen.SetActive(false);
        imageScreen.SetActive(false);
        StopVideo();
        Color color = mediaBtns[2].GetComponent<Image>().color;
        color.a = 0.5f;
        mediaBtns[2].GetComponent<Image>().color = color;
        if (audioScreen.activeSelf)
        {
            //closeBtn.SetActive(false);
            //audioScreen.SetActive(false);
            //stopAudio();
        }
        else
        {
            closeBtn.SetActive(true);
            audioScreen.SetActive(true);
            playAudio();
        }
    }

    public void showInfoScreen()
    {
        if (infoBtn.GetComponent<BlinkingButton>().isSelected)
        {

        } else
        {
            btnsPanel.SetActive(true);
            showImageScreen();
            infoBtn.GetComponent<BlinkingButton>().isSelected = true;
        }

    }

    public void closeScreen()
    {
        StopVideo();
        stopAudio();
        infoBtn.GetComponent<BlinkingButton>().isSelected = false;
        Color color = mediaBtns[2].GetComponent<Image>().color;
        color.a = 0.5f;
        mediaBtns[2].GetComponent<Image>().color = color;
        closeBtn.SetActive(false);
        btnsPanel.SetActive(false);
        videoScreen.SetActive(false);
        imageScreen.SetActive(false);
        audioScreen.SetActive(false);
    }

    public void setMediaSource(MediaSource mediaSource)
    {
        this.mediaSource = mediaSource;
    }

    IEnumerator GetAudioClip()
    {
        Debug.Log(" loading audio clip");
        Debug.Log(" loading audio clip" + mediaSource.AudioUrl);

        //UnityWebRequest request = UnityWebRequest.Get(mediaSource.AudioUrl);
        //string path = Path.Combine(Application.persistentDataPath, mediaSource.Name + ".mp3");

        //yield return request.SendWebRequest();

        //if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        //{
        //    Debug.LogError("Error loading audio: " + request.error);
        //}
        //else
        //{
        //    File.WriteAllBytes(path, request.downloadHandler.data); // Save the video data to a file
        //    Debug.Log("audio saved to: " + path);
        //}


        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(mediaSource.AudioUrl, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = myClip;
                Debug.Log("Audio is playing.");
            }
        }

    }

    private IEnumerator LoadImageFromUrl(string url)
    {
        Debug.Log("image url" + url);
        //url = "https://cdn.pixabay.com/photo/2023/08/02/18/21/yoga-8165759_640.jpg";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error loading image: " + request.error);
        }
        else
        {
            Debug.Log(" loading image:");


            // Get the downloaded texture
            //Texture2D sourceTexture = DownloadHandlerTexture.GetContent(request);
            Texture2D sourceTexture = new Texture2D(2, 2);
            sourceTexture.LoadImage(request.downloadHandler.data);

            // Create a new Texture2D with the desired size
            Texture2D resizedTexture = new Texture2D(512, 512, sourceTexture.format, false);

            // Copy the pixels from the source texture to the resized texture
            for (int y = 0; y < resizedTexture.height; y++)
            {
                for (int x = 0; x < resizedTexture.width; x++)
                {
                    Color color = sourceTexture.GetPixelBilinear((float)x / resizedTexture.width, (float)y / resizedTexture.height);
                    resizedTexture.SetPixel(x, y, color);
                }
            }
            resizedTexture.Apply();
            // Compress the resized texture to reduce its size
            resizedTexture.Compress(true); // 'true' for high-quality compression

            // Apply the compressed texture to the RawImage component
            imageScreen.GetComponent<RawImage>().texture = resizedTexture;
            Destroy(sourceTexture);
        }
        request.Dispose();
    }

    IEnumerator prepareVideo()
    {
        
        UnityWebRequest request = UnityWebRequest.Get(mediaSource.VideoUrl);
        string path = Path.Combine(Application.persistentDataPath, mediaSource.Name + ".mp4");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error loading video: " + request.error);
        }
        else
        {
            File.WriteAllBytes(path, request.downloadHandler.data); // Save the video data to a file
            Debug.Log("Video saved to: " + path);
     
        }
        videoPlayer.playOnAwake = false;
        videoPlayer.url = path;
        videoPlayer.targetTexture = new RenderTexture(1280, 720, 24);
        videoScreen.GetComponent<RawImage>().texture = videoPlayer.targetTexture;
        videoPlayer.Prepare();
        videoPlayer.Stop();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Xử lý khi video kết thúc (nếu cần)
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        Color btnColor = mediaBtns[1].GetComponent<Image>().color;
        btnColor.a = 1f;
        mediaBtns[1].GetComponent<Image>().color = btnColor;
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
        Color btnColor = mediaBtns[1].GetComponent<Image>().color;
        btnColor.a = 0.5f;
        mediaBtns[1].GetComponent<Image>().color = btnColor;
    }

    public void pauseAudio()
    {
        audioSource.Pause();
        playBtn.SetActive(true);
        pauseBtn.SetActive(false);
    }

    public void playAudio()
    {
        if (audioSource.clip.length == audioSource.time)
        {
            audioSource.time = 0;
        }
        audioSource.Play();
        playBtn.SetActive(false);
        pauseBtn.SetActive(true);
        Color btnColor = mediaBtns[0].GetComponent<Image>().color;
        btnColor.a = 1f;
        mediaBtns[0].GetComponent<Image>().color = btnColor;
    }

    public void stopAudio()
    {
        audioSource.Stop();
        playBtn.SetActive(true);
        pauseBtn.SetActive(false);
        Color btnColor = mediaBtns[0].GetComponent<Image>().color;
        btnColor.a = 0.5f;
        mediaBtns[0].GetComponent<Image>().color = btnColor;
    }

    public void nextAudio5s()
    {
        if (audioSource.isPlaying)
        {
            // Set the playback time to the current time plus 5 seconds
            audioSource.time += 5;

            // If the new time exceeds the clip length, wrap it around or stop the playback
            if (audioSource.time >= audioSource.clip.length)
            {
                audioSource.time = audioSource.clip.length; // Restart from the beginning or you can stop the playback
                stopAudio();
            }
        }
        else
        {
            // If the audio isn't playing, start it at 5 seconds
            audioSource.time = 5;
        }
    }

    public void preAudio5s()
    {
         // Set the playback time to the current time plus 5 seconds
            audioSource.time -= 5;

            // If the new time exceeds the clip length, wrap it around or stop the playback
            if (audioSource.time <= 0)
            {
                audioSource.time = 0; // Restart from the beginning or you can stop the playback
            }
        
    }

    void OnSliderValueChanged()
    {
        audioSource.time = timeSlider.value;
        //// Set the audio time to the slider's current value
        //if (!audioSource.isPlaying)
        //{
        //    audioSource.time = timeSlider.value;
        //}
    }

    IEnumerator UpdateSlider()
    {
        yield return StartCoroutine(GetAudioClip());
        //audioSource.clip = audioClips[0];
        //switch (mediaSource.AudioUrl)
        //{
        //    case "Thí sinh":
        //        audioSource.clip = audioClips[0];
        //        Debug.Log("Audio is playing.");
        //        break;
        //    case "Lính canh":
        //        audioSource.clip = audioClips[1];
        //        Debug.Log("Audio is playing.");
        //        break;
        //    case "Quan coi thi":
        //        audioSource.clip = audioClips[2];
        //        Debug.Log("Audio is playing.");
        //        break;
        //    default:
        //        break;
        //}
        Debug.Log(" start update slider:");

        

        timeSlider.minValue = 0;
        timeSlider.maxValue = audioSource.clip.length;
        totalTimeText.text = FormatTime(audioSource.clip.length);
        timeSlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
        while (true)
        {
            if (audioSource.isPlaying)
            {
                // Update the slider to the current audio time
                currentTimeText.text = FormatTime(audioSource.time);
                timeSlider.value = audioSource.time;
            } else
            {
                playBtn.SetActive(true);
                pauseBtn.SetActive(false);
            }
            yield return null;
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(timeInSeconds);
        return time.ToString(@"mm\:ss");
    }
}
