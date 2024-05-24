using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GLTFast;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnityEditor;

public class GetFileFromURL : MonoBehaviour
{
    public Text Percentage;
    public Text processText;
    private List<Model> _models = LoadingData._models;
    const string DOWNLOADTEXT = "Đang tải dữ liệu";
    const string IMPORTTEXT = "Đang tải cảnh";

    Dictionary<String, String> downloadUrls = new Dictionary<string, string>();
    //Dictionary<String, GltfImport> arModels = new Dictionary<string, GltfImport>();
    private double myIndex = 1;
    string url = "https://threed-threejs.onrender.com/api/getAllData";

    [SerializeField]
    TextAsset jsonFile;

    // Start is called before the first frame update
    void Start()
    {

    //    _models.Add(new Model("11-thi_sinh.glb", new Vector3(-23, -1, 15), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/thi%CC%81_sinh.glb?alt=media&token=21ba072a-9a59-4f87-979c-2832588126e0", "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/WeAreGoingOnBullrun.mp4", "https://images.kienthuc.net.vn/zoom/800/uploaded/quocquan/2020_07_16/so-tan-tay-leu-chong-cua-si-tu-viet-hang-tram-nam-truoc.jpg", "Người dự thi phải chuẩn bị trước cho mình một chõng và một lều nhỏ được đan bằng tre để mang vào rồi chọn cho mình một chỗ để dựng lều đặt chõng, lấy đó làm nơi che chắn nắng mưa, đây cũng là nơi sĩ tử làm bài thi."));

    //    _models.Add(new Model("11-hang_rao.glb", new Vector3(41, -1, 31), Quaternion.Euler(0, 90, 0), new Vector3(4, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hang_rao.glb?alt=media&token=d26f6299-1b02-4656-a0c5-648e9e39e2b0", "", "", ""));

    //    _models.Add(new Model("11-co.glb", new Vector3(41, -1, 44), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/co.glb?alt=media&token=709eaa27-e082-45b1-a58b-457ce69c4e7f", "", "", ""));

    //    _models.Add(new Model("11-quan_coi_thi.glb", new Vector3(-34, -1, 3), Quaternion.Euler(0, 90, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/quan_coi_thi.glb?alt=media&token=9e966448-2818-461c-bcbb-a0625f2b3e36", "", "", ""));

    //    _models.Add(new Model("11-oche.glb", new Vector3(36, -1, 9), Quaternion.Euler(0, 180, 0), new Vector3(4, 6, 3), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/oche.glb?alt=media&token=9b6cd741-a205-4b92-bde4-a612d9dcad2c", "", "", ""));

    //    _models.Add(new Model("11-maple_tree.glb", new Vector3(-37, -1, -24), Quaternion.Euler(0, 180, 0), new Vector3(0.05f, 0.05f, 0.05f), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/maple_tree.glb?alt=media&token=0724182e-ccac-41e1-827d-cd80cb844e82", "", "", ""));


    //    _models.Add(new Model("11-tree_1.glb", new Vector3(-46, -1, 15), Quaternion.Euler(0, 180, 0), new Vector3(5, 5, 4), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/tree_1.glb?alt=media&token=ea29bb38-b369-4be3-b3ad-0d828a60272a", "", "", ""));

    //    _models.Add(new Model("11-cong.glb", new Vector3(0f, -1, -15), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/cong.glb?alt=media&token=784b6175-bc03-464b-a41b-f170a68730dd", "", "", ""));

    //    _models.Add(new Model("11-linh.glb", new Vector3(6, -1, -20), Quaternion.Euler(0, 180, 0), new Vector3(3, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/linh.glb?alt=media&token=f9c1ec3d-aae5-4b58-ab9f-ebbec226207c", "", "", ""));
    //    _models.Add(new Model("11-mui_ten_huong_dan.glb", new Vector3(-6, -1, -20), Quaternion.Euler(0, 180, 0), new Vector3(3, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/mui_ten_huong_dan.glb?alt=media&token=19e833da-1d43-4d3b-9089-0d866c475ea1", "", "", ""));
    //    _models.Add(new Model("11-trong.glb", new Vector3(-6, -1, -20), Quaternion.Euler(0, 180, 0), new Vector3(3, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/trong.glb?alt=media&token=0cc6939a-cd2e-46f4-ad17-4423a27c2f85", "", "", ""));

    //    for (int i = 0; i < _models.Count; i++)
    //    {
    //        string input = _models[i].Name;
    //        string[] parts = input.Split('-');
    //        string filename = parts[1]; // "thi_sinh.glb"

    //        //Debug.Log(filename);
    //        //Debug.Log(modelData.downloadUrl);

    //        if (!downloadUrls.ContainsKey(filename))
    //        {
    //            downloadUrls.Add(filename, _models[i].Url);
    //        }

    //        //if (!downloadUrls.ContainsKey(_models[i].Name))
    //        //{
    //        //    downloadUrls.Add(_models[i].Name, _models[i].Url);
    //        //}
    //    }
        //GetRequest(url);
        //LoadGltf();
        //StartCoroutine(GetRequest(url: url));
        StartCoroutine(DownloadFile());

    }

    IEnumerator DownloadFile()
    {
        yield return StartCoroutine(GetRequest(url: url));
        //GetRequest(url: url);
        Debug.Log("Start download file");

        myIndex = 1;
        foreach (var (name, url) in downloadUrls)
        {
            var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
            string path = Path.Combine(Application.persistentDataPath, name);
            uwr.downloadHandler = new DownloadHandlerFile(path);
            yield return uwr.SendWebRequest();
            processText.text = DOWNLOADTEXT;
            if (uwr.result != UnityWebRequest.Result.Success)
                Debug.LogError(uwr.error);
            else
            {
                Debug.Log("File successfully downloaded and saved to " + path);
                if (Percentage != null)
                    Percentage.text = "%" + (100 * (myIndex / downloadUrls.Count)).ToString("####");
            }
            myIndex++;

        }
        LoadGltf();
        //yield return CustomDeferAgentPerGltfImport();
    }

    async void LoadGltf()
    {
        //GetRequest(url: url);
        Debug.Log("Start load gltf");

        processText.text = IMPORTTEXT;
        myIndex = 1;
        //var filePath = Path.Combine(Application.persistentDataPath, "thi_sinh.glb");
        //var gltf = new GltfImport();
        foreach (var (name, url) in downloadUrls)
        {
            var filePath = Path.Combine(Application.persistentDataPath, name);
            
            var gltf = new GltfImport();
            print(filePath);
            if (System.IO.File.Exists(filePath))
            {
                // The file exists -> run event
                print("exist");
            }
            else
            {
                // The file does not exist -> run event
                print(filePath + "not exist");
            }
            var success = await gltf.Load(filePath);
            print("nice 1");
            if (success)
            {
                print("success");
                if (!LoadingData.arModels.ContainsKey(name))
                {
                    print("add to armodels");
                    LoadingData.arModels.Add(name, gltf);
                    //await gltf.InstantiateMainSceneAsync(transform);
                }
                else
                {
                    //gltf.Dispose();
                }
            }
            else
            {
                print("error loading gltf file");
            }
            if (Percentage != null)
                Percentage.text = "%" + (100 * (myIndex / downloadUrls.Count)).ToString("####");
            myIndex++;

        }
        //LoadingData._models = _models;

        SceneManager.LoadScene(1);
    }

    //async Task CustomDeferAgentPerGltfImport()
    //{
    //    Debug.Log("Start load gltf");
    //    myIndex = 1;

    //    processText.text = IMPORTTEXT;
    //    // Recommended: Use a common defer agent across multiple GltfImport instances!
    //    // For a stable frame rate:
    //    IDeferAgent deferAgent = gameObject.AddComponent<TimeBudgetPerFrameDeferAgent>();
    //    // Or for faster loading:
    //    //deferAgent = new UninterruptedDeferAgent();

    //    var tasks = new List<Task>();


    //    foreach (var (name, url) in downloadUrls)
    //    {
    //        Debug.Log(name);
    //        var filePath = Path.Combine(Application.persistentDataPath, name);
    //        var gltf = new GLTFast.GltfImport(null, deferAgent);
    //        var task = gltf.Load(filePath).ContinueWith(
    //            async t => {
    //                if (t.Result)
    //                {
    //                    //await gltf.InstantiateMainSceneAsync(transform);
    //                    if (!LoadingData.arModels.ContainsKey(name))
    //                    {
    //                        LoadingData.arModels.Add(name, gltf);
    //                        var material = gltf.GetMaterial();

    //                        Debug.LogFormat("The first material is called {0}", gltf.TextureCount);
    //                        //for (int i = 0; i < gltf.TextureCount; i++)
    //                        //{
    //                        //    Debug.LogFormat("The first name is called {0}", gltf.GetTexture(i).name);

    //                        //    try
    //                        //    {
    //                        //        MakeTextureReadable(gltf.GetTexture(i));
    //                        //        gltf.GetTexture(i).Reinitialize(125, 125);
    //                        //    } catch (Exception e)
    //                        //    {
    //                        //        Debug.Log(e);
    //                        //    }
    //                        //        Debug.Log(gltf.GetTexture(i).height);
    //                        //}
    //                    }
    //                    else
    //                    {
    //                        gltf.Dispose();
    //                    }
    //                }
    //                if (Percentage != null)
    //                    Percentage.text = "%" + (100 * (myIndex / downloadUrls.Count)).ToString("####");
    //                myIndex++;

    //            },
    //            TaskScheduler.FromCurrentSynchronizationContext()
    //            );
    //        tasks.Add(task);

    //    }

    //    await Task.WhenAll(tasks);
    //    print("here");
    //    SceneManager.LoadScene(1);

    //}

    IEnumerator GetRequest(string url)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.Success)
        {
            string responseText = unityWebRequest.downloadHandler.text;
            ModelData.Root myDeserializedClass = JsonConvert.DeserializeObject<ModelData.Root>(responseText);
        //ModelData.Root myDeserializedClass = JsonConvert.DeserializeObject<ModelData.Root>(jsonFile.text);
        for (int i = 0; i < myDeserializedClass.data.Count; i++)
            {
                var modelData = myDeserializedClass.data[i];
                if (modelData.downloadUrl != null)
                {
                    string input = modelData.modelId;
                    string[] parts = input.Split('-');
                    string filename = parts[1]; // "thi_sinh.glb"

                    //Debug.Log(filename);
                    //Debug.Log(modelData.downloadUrl);

                    if (!downloadUrls.ContainsKey(input))
                    {
                        downloadUrls.Add(input, modelData.downloadUrl);
                    }
                }
                Model model = Model.CreateModelFromDatum(modelData);
                _models.Add(model);
            }
        }

        else
        {
            Debug.Log("Error: " + unityWebRequest.error);
        }
        Debug.Log("Done request");

    }


    public static void MakeTextureReadable(Texture2D texture)
    {
#if UNITY_EDITOR
        if (texture == null) return;

        string assetPath = AssetDatabase.GetAssetPath(texture);
        var tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        if (tImporter != null)
        {
            tImporter.textureType = TextureImporterType.Default;
            tImporter.isReadable = true;
            AssetDatabase.ImportAsset(assetPath);
            AssetDatabase.Refresh();
            Debug.Log("egsggdgd");
        }
#endif
    }
}
