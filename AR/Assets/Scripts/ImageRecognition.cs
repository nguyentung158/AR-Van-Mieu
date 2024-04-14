using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    GameObject[] btnModels = new GameObject[3];
    bool t = false;
    Quaternion originRotation = Quaternion.Euler(x: 0,y: 0,z: 0);
    Vector3 originPosition = new Vector3(0, 0, 0 );

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void Start()
    {
        _models = LoadingData._models;
        foreach(Model mo in _models)
        {
            GameObject game = new GameObject();
            game.transform.position = mo.Position * 0.4f;
            game.transform.rotation = mo.Rotation;
            game.transform.localScale = mo.Scale * 0.4f;
            game.SetActive(false);
            _worldModels.Add(game);
        }
        arModels = LoadingData.arModels;
        print(_models.Count);
        print(arModels.Count);
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
                //_models[i].ModelObject.transform.position = _models[i].Position;
                //_models[i].ModelObject.transform.rotation = _models[i].Rotation;
                //_models[i].ModelObject.transform.localScale = _models[i].Scale;
                _worldModels[i].SetActive(true);
                _worldModels[i].transform.position = getRelativePosition(originPosition, _models[i].Position * 0.4f, getRotationDiff(initialRotation), initialPosition);
                _worldModels[i].transform.rotation = getRotation(imageRotation: initialRotation, modelRotation: _models[i].Rotation);
                _worldModels[i].AddComponent<ARAnchor>();
                //for (int t = 0; t < _arModels.Length; t++)
                //{
                //    if(_arModels[t].transform.name == _models[i].Name)
                //    {
                //        _worldModels[i] = Instantiate(_arModels[t], _models[i].Position, _models[i].Rotation);
                //        _worldModels[i].AddComponent<ARAnchor>();
                //    }
                //}
                for (int j = 0; j < _prefabToPlace.Length; j++)
                {
                    if (_models[i].Name == "quan_coi_thi" ||  _models[i].Name == "thi_sinh")
                    {
                        //print(_prefabToPlace[j].transform.tag);
                        _models[i].btnModels[j] = Instantiate(_prefabToPlace[j], getPosition((_worldModels[i].transform.position) + new Vector3(0,(0.4f * (j + 1)), 0), initialRotation), getRotation(imageRotation: initialRotation, modelRotation: _prefabToPlace[j].transform.rotation));
                        _models[i].btnModels[j].AddComponent<ARAnchor>();
                        _models[i].btnModels[j].GetComponent<MediaSource>().photoUrl = _models[i].ImageUrl;
                        _models[i].btnModels[j].GetComponent<MediaSource>().infoText = _models[i].Text;
                        _models[i].btnModels[j].GetComponent<MediaSource>().videoUrl = _models[i].VideoUrl;
                    }
                    //_models[i].btnModels[j].GetComponent<ImageSource>().is = "";
                }
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
                    _worldModels[i].transform.position = getRelativePosition(originPosition, _models[i].Position * 0.4f, getRotationDiff(imgRotation), imgPos);
                    _worldModels[i].transform.rotation = getRotation(imageRotation: imgRotation, modelRotation: _models[i].Rotation);
                    //_worldModels[i].AddComponent<ARAnchor>();

                    for (int j = 0; j < _prefabToPlace.Length; j++)
                    {
                        if (_models[i].Name == "thi_sinh")
                        {
                            _models[i].btnModels[j].transform.position = getPosition((_worldModels[i].transform.position) + new Vector3(0,(0.4f * (j + 1)), 0), updatedImage.transform.rotation);
                            _models[i].btnModels[j].transform.rotation = getRotation(imageRotation: imgRotation, modelRotation: _prefabToPlace[j].transform.rotation);
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
        Quaternion rotationDifference = getRotationDiff(imageRotation);

        // Combine the rotations
        Quaternion finalRotation = rotationDifference * modelRotation;
        
        // Return the combined rotation
        return finalRotation;
    }

    Quaternion getRotationDiff(Quaternion imgRotation)
    {
        return imgRotation * Quaternion.Inverse(originRotation);
    }

    Vector3 getPosition(Vector3 pos, Quaternion rotation)
    {

        // Define the right vector (positive X-axis)
        Vector3 rightVector = Vector3.back;

        // Rotate the right vector by the origin rotation
        Vector3 rotatedRightVector = rotation * rightVector;

        // Define the distance to the right
        float distanceToRight = 1.5f; // Adjust as needed

        // Calculate the position to the right
        Vector3 positionToRight = pos + (rotatedRightVector * distanceToRight);

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

        for (int i = 0; i < _worldModels.Count; i++)
        {
            //model.SetModelTransform();
            var gltf = arModels[_models[i].Name];
            print(gltf);
            var instantiator = new GameObjectInstantiator(gltf, _worldModels[i].transform);
            await gltf.InstantiateMainSceneAsync(instantiator);
            var legacyAnimation = instantiator.SceneInstance.LegacyAnimation;
            if (legacyAnimation != null)
            {
                legacyAnimation.Play();
            }
        }
        print("load cdone");
    }
}
