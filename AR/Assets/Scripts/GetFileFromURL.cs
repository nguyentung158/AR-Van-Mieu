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


public class GetFileFromURL : MonoBehaviour
{
    public Text Percentage;
    public Text processText;
    private List<Model> _models = LoadingData._models;
    const string DOWNLOADTEXT = "Đang tải dữ liệu";
    const string IMPORTTEXT = "Đang tải cảnh";

    Dictionary<String, String> downloadUrl = new Dictionary<string, string>();
    //Dictionary<String, GltfImport> arModels = new Dictionary<string, GltfImport>();
    private double myIndex = 1;
    string url = "http://127.0.0.1/api/getAllData";

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene(0);
        //_models.Add(new Model("thi_sinh", new Vector3(0.5f, 0, 0.5f), Quaternion.Euler(0, 180, 0), new Vector3(0.1f, 0.1f, 0.1f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/thi_sinh.glb?alt=media&token=dfbd0298-733a-41f9-8d6b-fbccf742f646", "", "", ""));
        //_models.Add(new Model("thi_sinh", new Vector3(-0.5f, 0, 0.5f), Quaternion.Euler(0, 180, 0), new Vector3(0.1f, 0.1f, 0.1f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/thi_sinh.glb?alt=media&token=dfbd0298-733a-41f9-8d6b-fbccf742f646", "", "", ""));
        //_models.Add(new Model("thi_sinh", new Vector3(-0.5f, 0, -0.5f), Quaternion.Euler(0, 180, 0), new Vector3(0.1f, 0.1f, 0.1f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/thi_sinh.glb?alt=media&token=dfbd0298-733a-41f9-8d6b-fbccf742f646", "", "", ""));
        //_models.Add(new Model("thi_sinh", new Vector3(0.5f, 0, -0.5f), Quaternion.Euler(0, 180, 0), new Vector3(0.1f, 0.1f, 0.1f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/thi_sinh.glb?alt=media&token=dfbd0298-733a-41f9-8d6b-fbccf742f646", "", "", ""));
        //_models.Add(new Model("thi_sinh", new Vector3(-0.9f, 0, 0.5f), Quaternion.Euler(0, 180, 0), new Vector3(0.1f, 0.1f, 0.1f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/thi_sinh.glb?alt=media&token=dfbd0298-733a-41f9-8d6b-fbccf742f646", "", "", ""));
        //_models.Add(new Model("thi_sinh", new Vector3(-0.9f, 0, -0.5f), Quaternion.Euler(0, 180, 0), new Vector3(0.1f, 0.1f, 0.1f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/thi_sinh.glb?alt=media&token=dfbd0298-733a-41f9-8d6b-fbccf742f646", "", "", ""));
        //_models.Add(new Model("thi_sinh", new Vector3(0.9f, 0, -0.5f), Quaternion.Euler(0, 180, 0), new Vector3(0.1f, 0.1f, 0.1f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/thi_sinh.glb?alt=media&token=dfbd0298-733a-41f9-8d6b-fbccf742f646", "", "", ""));
        //_models.Add(new Model("thi_sinh", new Vector3(0.9f, 0, 0.5f), Quaternion.Euler(0, 180, 0), new Vector3(0.1f, 0.1f, 0.1f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/thi_sinh.glb?alt=media&token=dfbd0298-733a-41f9-8d6b-fbccf742f646", "", "", ""));

        //_models.Add(new Model("hang_rao", new Vector3(0.375f, 0, -1.282f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(0.838f, 0, -1.282f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(1.325f, 0, -1.282f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(-0.895f, 0, -1.282f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(-0.408f, 0, -1.282f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(-1.358f, 0, -1.282f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));

        //_models.Add(new Model("hang_rao", new Vector3(1.571f, 0, -1.029f), Quaternion.Euler(0, 270, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(1.571f, 0, -0.04900002f), Quaternion.Euler(0, 270, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(1.571f, 0, -0.538f), Quaternion.Euler(0, 270, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(1.571f, 0, 0.4449999f), Quaternion.Euler(0, 270, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(1.571f, 0, 0.936f), Quaternion.Euler(0, 270, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(1.571f, 0, 1.425f), Quaternion.Euler(0, 270, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));

        //_models.Add(new Model("hang_rao", new Vector3(-1.338f, 0, 1.658f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(-0.875f, 0, 1.658f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(-0.3880001f, 0, 1.658f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(-0.055f, 0, 1.658f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(0.368f, 0, 1.658f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(0.831f, 0, 1.658f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(1.318f, 0, 1.658f), Quaternion.Euler(0, 180, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));

        //_models.Add(new Model("hang_rao", new Vector3(-1.59f, 0, -1.029f), Quaternion.Euler(0, 270, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "https://images.kienthuc.net.vn/zoom/800/uploaded/quocquan/2020_07_16/so-tan-tay-leu-chong-cua-si-tu-viet-hang-tram-nam-truoc.jpg", ""));
        //_models.Add(new Model("hang_rao", new Vector3(-1.59f, 0, -0.04900002f), Quaternion.Euler(0, 270, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(-1.59f, 0, -0.538f), Quaternion.Euler(0, 270, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(-1.59f, 0, 0.4449999f), Quaternion.Euler(0, 270, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(-1.59f, 0, 0.936f), Quaternion.Euler(0, 270, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(-1.59f, 0, 1.425f), Quaternion.Euler(0, 270, 0), new Vector3(0.25f, 0.15f, 0.15f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));

        //_models.Add(new Model("cong", new Vector3(0.018f, -0.06f, -1.314f), Quaternion.Euler(0, 180, 0), new Vector3(0.1f, 0.1f, 0.1f), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/cong.glb?alt=media&token=a26b75cf-f44c-4259-be06-b74ac9d3b033", "", "", ""));


        _models.Add(new Model("thi_sinh", new Vector3(11, -1, -2), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/thi_sinh.glb?alt=media&token=a8dc8924-fee9-4646-ba98-193c701b1ec3", "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/VideoTruongThi.mp4?alt=media&token=2cc0b0aa-8122-4e3b-8c27-780f129e5f9d", "https://images.kienthuc.net.vn/zoom/800/uploaded/quocquan/2020_07_16/so-tan-tay-leu-chong-cua-si-tu-viet-hang-tram-nam-truoc.jpg", "Người dự thi phải chuẩn bị trước cho mình một chõng và một lều nhỏ được đan bằng tre để mang vào rồi chọn cho mình một chỗ để dựng lều đặt chõng, lấy đó làm nơi che chắn nắng mưa, đây cũng là nơi sĩ tử làm bài thi."));
        _models.Add(new Model("thi_sinh", new Vector3(23, -1, -2), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/thi_sinh.glb?alt=media&token=a8dc8924-fee9-4646-ba98-193c701b1ec3", "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/VideoTruongThi.mp4?alt=media&token=2cc0b0aa-8122-4e3b-8c27-780f129e5f9d", "https://images.kienthuc.net.vn/zoom/800/uploaded/quocquan/2020_07_16/so-tan-tay-leu-chong-cua-si-tu-viet-hang-tram-nam-truoc.jpg", "Người dự thi phải chuẩn bị trước cho mình một chõng và một lều nhỏ được đan bằng tre để mang vào rồi chọn cho mình một chỗ để dựng lều đặt chõng, lấy đó làm nơi che chắn nắng mưa, đây cũng là nơi sĩ tử làm bài thi."));
        _models.Add(new Model("thi_sinh", new Vector3(-10, -1, -2), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/thi_sinh.glb?alt=media&token=a8dc8924-fee9-4646-ba98-193c701b1ec3", "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/VideoTruongThi.mp4?alt=media&token=2cc0b0aa-8122-4e3b-8c27-780f129e5f9d", "https://images.kienthuc.net.vn/zoom/800/uploaded/quocquan/2020_07_16/so-tan-tay-leu-chong-cua-si-tu-viet-hang-tram-nam-truoc.jpg", "Người dự thi phải chuẩn bị trước cho mình một chõng và một lều nhỏ được đan bằng tre để mang vào rồi chọn cho mình một chỗ để dựng lều đặt chõng, lấy đó làm nơi che chắn nắng mưa, đây cũng là nơi sĩ tử làm bài thi."));
        _models.Add(new Model("thi_sinh", new Vector3(23, -1, 15), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/thi_sinh.glb?alt=media&token=a8dc8924-fee9-4646-ba98-193c701b1ec3", "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/WeAreGoingOnBullrun.mp4", "https://images.kienthuc.net.vn/zoom/800/uploaded/quocquan/2020_07_16/so-tan-tay-leu-chong-cua-si-tu-viet-hang-tram-nam-truoc.jpg", "Người dự thi phải chuẩn bị trước cho mình một chõng và một lều nhỏ được đan bằng tre để mang vào rồi chọn cho mình một chỗ để dựng lều đặt chõng, lấy đó làm nơi che chắn nắng mưa, đây cũng là nơi sĩ tử làm bài thi."));
        _models.Add(new Model("thi_sinh", new Vector3(11, -1, 15), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/thi_sinh.glb?alt=media&token=a8dc8924-fee9-4646-ba98-193c701b1ec3", "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/VideoTruongThi.mp4?alt=media&token=2cc0b0aa-8122-4e3b-8c27-780f129e5f9d", "https://images.kienthuc.net.vn/zoom/800/uploaded/quocquan/2020_07_16/so-tan-tay-leu-chong-cua-si-tu-viet-hang-tram-nam-truoc.jpg", "Người dự thi phải chuẩn bị trước cho mình một chõng và một lều nhỏ được đan bằng tre để mang vào rồi chọn cho mình một chỗ để dựng lều đặt chõng, lấy đó làm nơi che chắn nắng mưa, đây cũng là nơi sĩ tử làm bài thi."));
        _models.Add(new Model("thi_sinh", new Vector3(-23, -1, -2), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/thi_sinh.glb?alt=media&token=a8dc8924-fee9-4646-ba98-193c701b1ec3", "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/VideoTruongThi.mp4?alt=media&token=2cc0b0aa-8122-4e3b-8c27-780f129e5f9d", "https://images.kienthuc.net.vn/zoom/800/uploaded/quocquan/2020_07_16/so-tan-tay-leu-chong-cua-si-tu-viet-hang-tram-nam-truoc.jpg", "Người dự thi phải chuẩn bị trước cho mình một chõng và một lều nhỏ được đan bằng tre để mang vào rồi chọn cho mình một chỗ để dựng lều đặt chõng, lấy đó làm nơi che chắn nắng mưa, đây cũng là nơi sĩ tử làm bài thi."));
        _models.Add(new Model("thi_sinh", new Vector3(-10, -1, 15), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/thi_sinh.glb?alt=media&token=a8dc8924-fee9-4646-ba98-193c701b1ec3", "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/VideoTruongThi.mp4?alt=media&token=2cc0b0aa-8122-4e3b-8c27-780f129e5f9d", "https://images.kienthuc.net.vn/zoom/800/uploaded/quocquan/2020_07_16/so-tan-tay-leu-chong-cua-si-tu-viet-hang-tram-nam-truoc.jpg", "Người dự thi phải chuẩn bị trước cho mình một chõng và một lều nhỏ được đan bằng tre để mang vào rồi chọn cho mình một chỗ để dựng lều đặt chõng, lấy đó làm nơi che chắn nắng mưa, đây cũng là nơi sĩ tử làm bài thi."));
        _models.Add(new Model("thi_sinh", new Vector3(-23, -1, 15), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/thi_sinh.glb?alt=media&token=a8dc8924-fee9-4646-ba98-193c701b1ec3", "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/WeAreGoingOnBullrun.mp4", "https://images.kienthuc.net.vn/zoom/800/uploaded/quocquan/2020_07_16/so-tan-tay-leu-chong-cua-si-tu-viet-hang-tram-nam-truoc.jpg", "Người dự thi phải chuẩn bị trước cho mình một chõng và một lều nhỏ được đan bằng tre để mang vào rồi chọn cho mình một chỗ để dựng lều đặt chõng, lấy đó làm nơi che chắn nắng mưa, đây cũng là nơi sĩ tử làm bài thi."));

        _models.Add(new Model("hang_rao", new Vector3(-3, -1, 45), Quaternion.Euler(0, 180, 0), new Vector3(4, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hang_rao.glb?alt=media&token=d26f6299-1b02-4656-a0c5-648e9e39e2b0", "", "", ""));
        _models.Add(new Model("hang_rao", new Vector3(-44, -1, 1), Quaternion.Euler(0, 90, 0), new Vector3(4, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hang_rao.glb?alt=media&token=d26f6299-1b02-4656-a0c5-648e9e39e2b0", "", "", ""));
        _models.Add(new Model("hang_rao", new Vector3(-30, -1, 45), Quaternion.Euler(0, 180, 0), new Vector3(4, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hang_rao.glb?alt=media&token=d26f6299-1b02-4656-a0c5-648e9e39e2b0", "", "", ""));
        _models.Add(new Model("hang_rao", new Vector3(15, -1, -13), Quaternion.Euler(0, 180, 0), new Vector3(3, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hang_rao.glb?alt=media&token=d26f6299-1b02-4656-a0c5-648e9e39e2b0", "", "", ""));
        _models.Add(new Model("hang_rao", new Vector3(-15, -1, -13), Quaternion.Euler(0, 180, 0), new Vector3(3, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hang_rao.glb?alt=media&token=d26f6299-1b02-4656-a0c5-648e9e39e2b0", "", "", ""));
        _models.Add(new Model("hang_rao", new Vector3(-44, -1, 31), Quaternion.Euler(0, 90, 0), new Vector3(4, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hang_rao.glb?alt=media&token=d26f6299-1b02-4656-a0c5-648e9e39e2b0", "", "", ""));

        _models.Add(new Model("hang_rao", new Vector3(-33, -1, -13), Quaternion.Euler(0, 180, 0), new Vector3(3, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hang_rao.glb?alt=media&token=d26f6299-1b02-4656-a0c5-648e9e39e2b0", "", "", ""));
        _models.Add(new Model("hang_rao", new Vector3(31, -1, -13), Quaternion.Euler(0, 180, 0), new Vector3(3, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hang_rao.glb?alt=media&token=d26f6299-1b02-4656-a0c5-648e9e39e2b0", "", "", ""));
        _models.Add(new Model("hang_rao", new Vector3(26, -1, 45), Quaternion.Euler(0, 180, 0), new Vector3(4, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hang_rao.glb?alt=media&token=d26f6299-1b02-4656-a0c5-648e9e39e2b0", "", "", ""));
        _models.Add(new Model("hang_rao", new Vector3(41, -1, 2), Quaternion.Euler(0, 90, 0), new Vector3(4, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hang_rao.glb?alt=media&token=d26f6299-1b02-4656-a0c5-648e9e39e2b0", "", "", ""));
        _models.Add(new Model("hang_rao", new Vector3(41, -1, 31), Quaternion.Euler(0, 90, 0), new Vector3(4, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hang_rao.glb?alt=media&token=d26f6299-1b02-4656-a0c5-648e9e39e2b0", "", "", ""));

        _models.Add(new Model("co", new Vector3(41, -1, -13), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/co.glb?alt=media&token=709eaa27-e082-45b1-a58b-457ce69c4e7f", "", "", ""));
        _models.Add(new Model("co", new Vector3(-44, -1, -13), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/co.glb?alt=media&token=709eaa27-e082-45b1-a58b-457ce69c4e7f", "", "", ""));
        _models.Add(new Model("co", new Vector3(-44, -1, 45), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/co.glb?alt=media&token=709eaa27-e082-45b1-a58b-457ce69c4e7f", "", "", ""));
        _models.Add(new Model("co", new Vector3(41, -1, 44), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/co.glb?alt=media&token=709eaa27-e082-45b1-a58b-457ce69c4e7f", "", "", ""));

        _models.Add(new Model("quan_coi_thi", new Vector3(-34, -1, 3), Quaternion.Euler(0, 90, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/quan_coi_thi.glb?alt=media&token=9e966448-2818-461c-bcbb-a0625f2b3e36", "", "", ""));
        _models.Add(new Model("quan_coi_thi", new Vector3(36, -1, 12), Quaternion.Euler(0, -90, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/quan_coi_thi.glb?alt=media&token=9e966448-2818-461c-bcbb-a0625f2b3e36", "", "", ""));
        _models.Add(new Model("quan_coi_thi", new Vector3(-4, -1, 27), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/quan_coi_thi.glb?alt=media&token=9e966448-2818-461c-bcbb-a0625f2b3e36", "", "", ""));

        _models.Add(new Model("oche", new Vector3(-33, -1, 9), Quaternion.Euler(0, 180, 0), new Vector3(4, 6, 3), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/oche.glb?alt=media&token=9b6cd741-a205-4b92-bde4-a612d9dcad2c", "", "", ""));
        _models.Add(new Model("oche", new Vector3(3, -1, 28), Quaternion.Euler(0, 180, 0), new Vector3(5, 6, 5), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/oche.glb?alt=media&token=9b6cd741-a205-4b92-bde4-a612d9dcad2c", "", "", ""));
        _models.Add(new Model("oche", new Vector3(36, -1, 9), Quaternion.Euler(0, 180, 0), new Vector3(4, 6, 3), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/oche.glb?alt=media&token=9b6cd741-a205-4b92-bde4-a612d9dcad2c", "", "", ""));

        _models.Add(new Model("maple_tree", new Vector3(-37, -1, -24), Quaternion.Euler(0, 180, 0), new Vector3(0.05f, 0.05f, 0.05f), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/maple_tree.glb?alt=media&token=0724182e-ccac-41e1-827d-cd80cb844e82", "", "", ""));

        _models.Add(new Model("khu_ngoai_liem", new Vector3(-31, -1, 35), Quaternion.Euler(0, 270, 0), new Vector3(3, 3, 3), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/khu_ngoai_liem.glb?alt=media&token=1f5af8df-c101-439a-a09d-264e0ba3f7cc", "", "", ""));
        _models.Add(new Model("nha_thap_dao", new Vector3(0, -1, 10), Quaternion.Euler(0, 270, 0), new Vector3(1, 1, 1), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/nha_thap_dao.glb?alt=media&token=ec60f9ca-d4aa-4cd5-800f-dfa993f66344", "", "", ""));
        //_models.Add(new Model("hang_rao", new Vector3(0,0,0), Quaternion.Euler(0, 180, 0), new Vector3(4,2,2), "https://firebasestorage.googleapis.com/v0/b/my-tiktok-b56cd.appspot.com/o/hang_rao.glb?alt=media&token=4428cd55-3f82-4769-8fe7-77f5739d4096", "", "", ""));

        _models.Add(new Model("tree_1", new Vector3(45, -1, 19), Quaternion.Euler(0, 180, 0), new Vector3(4, 5, 4), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/tree_1.glb?alt=media&token=ea29bb38-b369-4be3-b3ad-0d828a60272a", "", "", ""));
        _models.Add(new Model("tree_1", new Vector3(44, -1, -1), Quaternion.Euler(0, 180, 0), new Vector3(4, 5, 5), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/tree_1.glb?alt=media&token=ea29bb38-b369-4be3-b3ad-0d828a60272a", "", "", ""));
        _models.Add(new Model("tree_1", new Vector3(44, -1, 34), Quaternion.Euler(0, 180, 0), new Vector3(4, 5, 4), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/tree_1.glb?alt=media&token=ea29bb38-b369-4be3-b3ad-0d828a60272a", "", "", ""));
        _models.Add(new Model("tree_1", new Vector3(-47, -1, -2), Quaternion.Euler(0, 180, 0), new Vector3(4, 5, 5), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/tree_1.glb?alt=media&token=ea29bb38-b369-4be3-b3ad-0d828a60272a", "", "", ""));
        _models.Add(new Model("tree_1", new Vector3(-48, -1, 42), Quaternion.Euler(0, 180, 0), new Vector3(5, 4, 4), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/tree_1.glb?alt=media&token=ea29bb38-b369-4be3-b3ad-0d828a60272a", "", "", ""));
        _models.Add(new Model("tree_1", new Vector3(-46, -1, 15), Quaternion.Euler(0, 180, 0), new Vector3(5, 5, 4), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/tree_1.glb?alt=media&token=ea29bb38-b369-4be3-b3ad-0d828a60272a", "", "", ""));

        _models.Add(new Model("da_2", new Vector3(-33, -1, -28), Quaternion.Euler(0, 180, 0), new Vector3(0.04f, 0.04f, 0.04f), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/da_2.glb?alt=media&token=d5aec23d-23ab-42b2-9c61-f399e624b213", "", "", ""));

        _models.Add(new Model("hoa", new Vector3(34, -1, -25), Quaternion.Euler(0, 180, 0), new Vector3(0.1f, 0.1f, 0.1f), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/hoa.glb?alt=media&token=9891f9ea-51fe-49ab-a323-d0b58b7517b5", "", "", ""));

        _models.Add(new Model("stone", new Vector3(47, -1, -24), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/stone.glb?alt=media&token=f73cdda1-0e8d-4554-bde9-6de5bdf81c31", "", "", ""));

        _models.Add(new Model("cong", new Vector3(0f, -1, -15), Quaternion.Euler(0, 180, 0), new Vector3(2, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/cong.glb?alt=media&token=784b6175-bc03-464b-a41b-f170a68730dd", "", "", ""));

        _models.Add(new Model("linh", new Vector3(6, -1, -20), Quaternion.Euler(0, 180, 0), new Vector3(3, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/linh.glb?alt=media&token=f9c1ec3d-aae5-4b58-ab9f-ebbec226207c", "", "", ""));
        _models.Add(new Model("linh", new Vector3(-6, -1, -20), Quaternion.Euler(0, 180, 0), new Vector3(3, 2, 2), "https://firebasestorage.googleapis.com/v0/b/mental-health-674b8.appspot.com/o/linh.glb?alt=media&token=f9c1ec3d-aae5-4b58-ab9f-ebbec226207c", "", "", ""));

        for (int i = 0; i < _models.Count; i++)
        {

            if (!downloadUrl.ContainsKey(_models[i].Name))
            {
                downloadUrl.Add(_models[i].Name, _models[i].Url);
            }
        }
        //print(downloadUrl.Count);
        //StartCoroutine(DownloadFile());
        LoadGltf();
        //StartCoroutine(GetRequest(url: url));
    }

    IEnumerator DownloadFile()
    {
        myIndex = 1;
        foreach (var (name, url) in downloadUrl)
        {
            var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
            string path = Path.Combine(Application.persistentDataPath, name + ".glb");
            uwr.downloadHandler = new DownloadHandlerFile(path);
            yield return uwr.SendWebRequest();
            processText.text = DOWNLOADTEXT;
            if (uwr.result != UnityWebRequest.Result.Success)
                Debug.LogError(uwr.error);
            else
            {
                Debug.Log("File successfully downloaded and saved to " + path);
                if (Percentage != null)
                    Percentage.text = "%" + (100 * (myIndex / downloadUrl.Count)).ToString("####");
            }
            myIndex++;
        }
        LoadGltf();

        print("here");
    }

      async void LoadGltf()
    {
        processText.text = IMPORTTEXT;
        myIndex = 1;
        foreach (var (name, url) in downloadUrl)
        {
            var filePath = Path.Combine(Application.persistentDataPath, name + ".glb");
            var gltf = new GltfImport();
            //print(gltf.Load(filePath).Result);

            var success = await gltf.Load(filePath);
            if (success)
            {
                if (!LoadingData.arModels.ContainsKey(name))
                {
                    LoadingData.arModels.Add(name, gltf);
                }
            }
            else
            {
                print("errerre");
            }
            print(name);
            if (Percentage != null)
                Percentage.text = "%" + (100 * (myIndex / downloadUrl.Count)).ToString("####");
            myIndex++;

        }
        //print(arModels.Count);
        //LoadingData._models = _models;
        //LoadingData.arModels = arModels;
        SceneManager.LoadScene(1);
    }

    IEnumerator GetRequest(string url)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.Success)
        {
            string responseText = unityWebRequest.downloadHandler.text;
            ModelData.Root myDeserializedClass = JsonConvert.DeserializeObject<ModelData.Root>(responseText);
            Debug.Log(myDeserializedClass.data[0].downloadUrl);
        } else
        {
            Debug.Log("Error: " + unityWebRequest.error);
        }
    }
}
