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
    private string audioUrl;
    private string text;
    public GameObject[] btnModels = new GameObject[2];
    //private GameObject modelObject =new GameObject();

    // Constructor
    public Model(string name, Vector3 position, Quaternion rotation, Vector3 scale, string url, string videoUrl, string imageUrl, string text, string audioUrl)
    {
        this.name = name;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.url = url;
        this.videoUrl = videoUrl;
        this.imageUrl = imageUrl;
        this.audioUrl = audioUrl;
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

    public string AudioUrl
    {
        get { return audioUrl; }
        set { audioUrl = value; }
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

    // Method to create a new Model object from a Datum object
    public static Model CreateModelFromDatum(ModelData.Datum datum)
    {
        // Extract data from the Datum object
        string name = datum.modelId; // You can use other properties of Datum to initialize other fields of Model
        Vector3 position = ConvertXYZ.convertPos(new Vector3(datum.position.x, datum.position.y, datum.position.z));
        Vector3 rotationVt3 = ConvertXYZ.convertRot(new Vector3(datum.rotation.x, datum.rotation.y, datum.rotation.z));
        Quaternion rotation = Quaternion.Euler(rotationVt3);
        Vector3 scale = new Vector3(datum.scale.x, datum.scale.y, datum.scale.z);
        string url = datum.downloadUrl;
        string videoUrl = datum.videoUrl;
        string imageUrl = datum.imageUrl;
        string audioUrl = datum.audioUrl;
        string text = datum.textContent;

        // Create and return a new Model object
        return new Model(name, position, rotation, scale, url, videoUrl, imageUrl, text, audioUrl);
    }
}

