using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    private string name;
    private Vector3 position;
    private Quaternion rotation;
    private Vector3 scale;
    private string url;
    private string videoUrl;
    private string imageUrl;
    private string text;
    public GameObject[] btnModels = new GameObject[3];
    //private GameObject modelObject =new GameObject();

    // Constructor
    public Model(string name, Vector3 position, Quaternion rotation, Vector3 scale, string url, string videoUrl, string imageUrl, string text)
    {
        this.name = name;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.url = url;
        this.videoUrl = videoUrl;
        this.imageUrl = imageUrl;
        this.text = text;
    }

    // Getters and Setters
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    public Quaternion Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public Vector3 Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    public string Url
    {
        get { return url; }
        set { url = value; }
    }

    public string VideoUrl
    {
        get { return videoUrl; }
        set { videoUrl = value; }
    }

    public string ImageUrl
    {
        get { return imageUrl; }
        set { imageUrl = value; }
    }

    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    //public GameObject ModelObject
    //{
    //    get { return modelObject; }
    //    set { modelObject = value; }
    //}

    //public GameObject[] BtnModels
    //{
    //    get { return btnModels; }
    //}
    //public void SetModelTransform()
    //{
    //    modelObject.transform.position = position;
    //    modelObject.transform.rotation = rotation;
    //    modelObject.transform.localScale = scale;
    //}
}

