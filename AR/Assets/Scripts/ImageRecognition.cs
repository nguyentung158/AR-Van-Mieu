using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using GLTFast;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageRecognition : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;

    [SerializeField]
    private GameObject[] _prefabToPlace;

    Dictionary<String, GltfImport> arModels = new Dictionary<string, GltfImport>();
    private List<Model> _models = new List<Model>();
    private List<GameObject> _worldModels = new List<GameObject>();
    //GameObject[] btnModels = new GameObject[3];
    bool t = false;
    Quaternion originRotation = Quaternion.Euler(x: 0,y: 0,z: 0);
    Vector3 originPosition = new(0, 0, -2);

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void Start()
    {
        _models = LoadingData._models;
        foreach(Model mo in _models)
        {
            GameObject game = new GameObject();
            game.transform.position = mo.Position;
            game.transform.rotation = mo.Rotation;
            game.transform.localScale = mo.Scale;
            game.SetActive(false);
            _worldModels.Add(game);
            if (mo.Text != "" && mo.Text != null)
            {
                //mo.btnModels[1] = Instantiate(_prefabToPlace[1], getPosition((game.transform.position) + new Vector3(0, (0.2f), 0), game.transform.rotation, 2.0f), game.transform.rotation);

                //mo.btnModels[1].GetComponentInChildren<InformationLoader>().loadText(mo.Text);

                mo.btnModels[0] = Instantiate(_prefabToPlace[0], getPosition((game.transform.position) + new Vector3(0, (0.85f), 0), game.transform.rotation, 2.0f, 2.0f), getRotation(imageRotation: originRotation, modelRotation: _prefabToPlace[0].transform.rotation));
                mo.btnModels[0].AddComponent<ARAnchor>();
                MediaSource mediaSource = new MediaSource(mo.ImageUrl, mo.VideoUrl, mo.AudioUrl, mo.Name);
                mo.btnModels[0].GetComponent<MediaController>().setMediaSource(mediaSource);
            }
        }
        arModels = LoadingData.arModels;
        print(_models.Count);
        print(arModels.Count);
        foreach (var (name, url) in arModels)
        {
            print(name + "    " + url);


        }
        LoadGltfBinaryFromMemoryAsync();
        //_models.ForEach(model => _worldModels.Add(_arModels[0]));
    }

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            Vector3 initialPosition = newImage.transform.position;
            Quaternion initialRotation = newImage.transform.rotation;
            for (int i = 0; i < _models.Count; i++)
            {
                _worldModels[i].SetActive(true);
                _worldModels[i].transform.position = getRelativePosition(originPosition, _models[i].Position, getRotationDiff(initialRotation, originRotation), initialPosition);
                _worldModels[i].transform.rotation = getRotation(imageRotation: initialRotation, modelRotation: _models[i].Rotation);
                _worldModels[i].AddComponent<ARAnchor>();
            }
            
        }
        

        foreach (var updatedImage in eventArgs.updated)
        {
            if (!t)
            {
                //Debug.Log("update Position: " + updatedImage.transform.position);
                //Debug.Log("update rotation: " + updatedImage.transform.rotation.eulerAngles);
                //Debug.Log("model rotation: " + _thiSinh.transform.rotation.eulerAngles);
                //Debug.Log("model pos: " + _thiSinh.transform.position);
                Quaternion imgRotation = updatedImage.transform.rotation;
                Vector3 imgPos = updatedImage.transform.position;
                for (int i = 0; i < _worldModels.Count; i++)
                {
                    _worldModels[i].transform.position = getRelativePosition(originPosition, _models[i].Position, getRotationDiff(imgRotation, originRotation), imgPos);
                    _worldModels[i].transform.rotation = getRotation(imageRotation: imgRotation, modelRotation: _models[i].Rotation);
                    
                    for (int j = 0; j < _prefabToPlace.Length - 1; j++)
                    {
                        if (_models[i].Text != "" && _models[i].Text != null)
                        {
                            Debug.Log(_models[i].Text);
                            _models[i].btnModels[j].transform.position = getPosition((_worldModels[i].transform.position) + new Vector3(0, (0.85f), 0), _worldModels[i].transform.rotation, 1.8f, -1.6f);
                            _models[i].btnModels[j].transform.rotation = _worldModels[i].transform.rotation;
                            if (_models[i].Name.Contains("thi_sinh"))
                            {
                                _models[i].btnModels[j].transform.position = getPosition((_worldModels[i].transform.position) + new Vector3(0, (0.85f), 0), _worldModels[i].transform.rotation, 1.8f, -1.6f);
                                _models[i].btnModels[j].transform.rotation = _worldModels[i].transform.rotation;
                            }
                            else if(_models[i].Name.Contains("quan_coi_thi"))
                            {
                                _models[i].btnModels[j].transform.position = getPosition((_worldModels[i].transform.position) + new Vector3(0, (1.85f), 0), _worldModels[i].transform.rotation, 2.0f, -4.0f);
                                _models[i].btnModels[j].transform.rotation = _worldModels[i].transform.rotation;
                            } else if (_models[i].Name.Contains("linh"))
                            {
                                _models[i].btnModels[j].transform.position = getPosition((_worldModels[i].transform.position) + new Vector3(0, (1f), 0), _worldModels[i].transform.rotation, 1.0f, -1.5f);
                                _models[i].btnModels[j].transform.rotation = _worldModels[i].transform.rotation;
                            }
                        }
                    }
                }
                t = false;
            }
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
        }
    }

    Quaternion getRotation(Quaternion imageRotation, Quaternion modelRotation)
    {
        Quaternion rotationDifference = getRotationDiff(imageRotation, originRotation);

        // Combine the rotations
        Quaternion finalRotation = rotationDifference * modelRotation;
        
        // Return the combined rotation
        return finalRotation;
    }

    Quaternion getRotationDiff(Quaternion afterRotation, Quaternion beforeRotation)
    {
        return afterRotation * Quaternion.Inverse(beforeRotation);
    }

    Vector3 getPosition(Vector3 pos, Quaternion rotation, float fowardVal, float rightVal)
    {

        // Define the right vector (positive X-axis)
        Vector3 fowardVector = Vector3.forward;

        // Rotate the right vector by the origin rotation
        Vector3 rotatedFowardVector = rotation * fowardVector;

        Vector3 rotatedRightVector = rotation * Vector3.right;

        // Calculate the position to the right
        Vector3 positionToRight = pos + (rotatedFowardVector * fowardVal) + (rotatedRightVector * rightVal);

        return positionToRight;
    }

    Vector3 getRelativePosition(Vector3 imgOriginPos, Vector3 modelPos, Quaternion diff, Vector3 imgCurrentPos)
    {
        Vector3 vt = modelPos - imgOriginPos;
        Vector3 rotationVector = diff * vt;
        if (!t)
        {
        }
        return imgCurrentPos + rotationVector;
    }

      async void LoadGltfBinaryFromMemoryAsync()
    {

        for (int i =  6; i < _worldModels.Count; i++)
        {
            //model.SetModelTransform();
            string input = _models[i].Name;
            string[] parts = input.Split('-');
            string filename = parts[1];
            var gltf = arModels[input];
            var instantiator = new GameObjectInstantiator(gltf, _worldModels[i].transform);
            print("load " + _models[i].Name);
            print("name " + gltf == null);
            print(instantiator);
            print(instantiator == null);
            try
            {
                await gltf.InstantiateMainSceneAsync(instantiator);
                print("success infissdkfdsmn");
            } catch (Exception e)
            {
                print("error in snfidsnfsdfs");
                print(e);
                print("error in snfidsnfsdfs");

            }
            var legacyAnimation = instantiator.SceneInstance.LegacyAnimation;
            if (legacyAnimation != null)
            {
                legacyAnimation.Play();
            }
        }

        print("load cdone");

    }

    //async Task CustomDeferAgentPerGltfImport()
    //{
    //    // Recommended: Use a common defer agent across multiple GltfImport instances!
    //    // For a stable frame rate:
    //    IDeferAgent deferAgent = gameObject.AddComponent<TimeBudgetPerFrameDeferAgent>();
    //    // Or for faster loading:
    //    deferAgent = new UninterruptedDeferAgent();

    //    var tasks = new List<Task>();

    //    foreach (var url in manyUrls)
    //    {
    //        var gltf = new GLTFast.GltfImport(null, deferAgent);
    //        var task = gltf.Load(url).ContinueWith(
    //            async t => {
    //                if (t.Result)
    //                {
    //                    await gltf.InstantiateMainSceneAsync(transform);
    //                }
    //            },
    //            TaskScheduler.FromCurrentSynchronizationContext()
    //            );
    //        tasks.Add(task);
    //    }

    //    await Task.WhenAll(tasks);
    //}
}
