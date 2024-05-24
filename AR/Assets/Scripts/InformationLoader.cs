using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class InformationLoader : MonoBehaviour
{
    public GameObject text;
    private string vi_text;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(text.transform.name);

        //Debug.Log(text.GetComponent<TextMeshProUGUI>().text);
        vi_text = text.GetComponent<TextMeshProUGUI>().text;
        //string jsonResponse = await loadEngText();
        //TranslationResponse response = JsonConvert.DeserializeObject<TranslationResponse>(jsonResponse);
        //en_text = response.translatedText;
    }

    public void loadText(string Text)
    {
        text.GetComponent<TextMeshProUGUI>().text = Text;
    }


    //private async Task<string> loadEngText()
    //{
    //    var client = new HttpClient();
    //    var request = new HttpRequestMessage
    //    {
    //        Method = HttpMethod.Post,
    //        RequestUri = new Uri("https://swift-translate.p.rapidapi.com/translate"),
    //        Headers =
    //{
    //    { "X-RapidAPI-Key", "e98f68fa7fmsh49d69f40813da73p14b6cejsn5fbd74d50a23" },
    //    { "X-RapidAPI-Host", "swift-translate.p.rapidapi.com" },
    //},
    //        Content = new StringContent("{\n    \"text\": \"" + vi_text + "\",\n    \"sourceLang\": \"vi\",\n    \"targetLang\": \"en\"\n}")
    //        {
    //            Headers =
    //    {
    //        ContentType = new MediaTypeHeaderValue("application/json")
    //    }
    //        }
    //    };
    //    using (var response = await client.SendAsync(request))
    //    {
    //        response.EnsureSuccessStatusCode();
    //        var body = await response.Content.ReadAsStringAsync();
    //        Console.WriteLine(body);
    //        return body;
    //    }
    //}

    //public void ChangeLanguage(int i)
    //{
    //    Debug.Log(i);
    //    switch (i)
    //    {
    //        case 0:
    //            text.GetComponent<TextMeshProUGUI>().text = vi_text;
    //            break;
    //        case 1:
    //            text.GetComponent<TextMeshProUGUI>().text = en_text;
    //            break;
    //    }
    //}
}
