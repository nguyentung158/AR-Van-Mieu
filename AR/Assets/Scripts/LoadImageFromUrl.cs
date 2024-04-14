﻿using System.Collections;
using GLTFast.Schema;
using UnityEngine;
using UnityEngine.Networking;

public class LoadImageFromUrl : MonoBehaviour
{

    public string TextureURL = "";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadImage(TextureURL));
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
            this.gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }

    public void loadImg(string url)
    {
        TextureURL = url;
        StartCoroutine(DownloadImage(TextureURL));
    }
}

