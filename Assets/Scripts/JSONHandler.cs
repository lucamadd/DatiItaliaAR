using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using TMPro;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

public class JSONHandler : MonoBehaviour
{

    [SerializeField]
    TextMeshPro today_date;

    [SerializeField]
    TextMeshPro today_info;

    public RootAndamentoNazionale andamentoNazionale = null;

    

    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequestNazionale("https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-json/dpc-covid19-ita-andamento-nazionale-latest.json"));
        
        // A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));
    }

    IEnumerator GetRequestNazionale(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string json_string = webRequest.downloadHandler.text;
                    Debug.Log(pages[page] + ":\nReceived: " + json_string);
                    andamentoNazionale = JsonUtility.FromJson<RootAndamentoNazionale>("{\"info\":" + json_string + "}");
                    Debug.Log("ANDAMENTO NAZIONALE IS " + andamentoNazionale.info[0].data);
                    FillMainInfo(andamentoNazionale.info[0]);
                    break;
            }
        }
    }

    

    private void FillMainInfo(AndamentoNazionale andamentoNazionale){
        today_date.text = DateTools.GetItalianDate(andamentoNazionale.data.Substring(0,10));
        today_info.text =   "Totale casi " + andamentoNazionale.totale_casi.ToString("#,#.#############################") +
                            "\nNuovi " + andamentoNazionale.nuovi_positivi.ToString("#,#.#############################") +
                            "\nDeceduti " + andamentoNazionale.deceduti.ToString("#,#.#############################") +
                            "\nTamponi ..." +
                            "\n\nRapporto positivi/tamponi ...%";
    }
}

[Serializable]
public class AndamentoNazionale{
    public string data;
    public int terapia_intensiva;
    public int totale_positivi;
    public int nuovi_positivi;
    public int deceduti;
    public int totale_casi;
    public int casi_testati;

}

 [Serializable]
 public class RootAndamentoNazionale
 {
     public AndamentoNazionale[] info;
 }
