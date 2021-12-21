using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataGetter : MonoBehaviour
{
    public RootAndamentoNazionale andamentoNazionale = null;
    public RootAndamentoRegionale andamentoRegionale = null;
    void Start()
    {
        StartCoroutine(GetRequestNazionale("https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-json/dpc-covid19-ita-andamento-nazionale.json"));
        StartCoroutine(GetRequestRegionale("https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-json/dpc-covid19-ita-regioni.json"));
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
                    //Debug.Log("ANDAMENTO NAZIONALE IS " + andamentoNazionale.info[andamentoNazionale.info.Length-1].data);

                    break;
            }
        }
    }
    IEnumerator GetRequestRegionale(string uri)
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
                    andamentoRegionale = JsonUtility.FromJson<RootAndamentoRegionale>("{\"regioni\":" + json_string + "}");
                    break;
            }
        }
    }


}

[Serializable]
public class AndamentoNazionale
{
    public string data;
    public int terapia_intensiva;
    public int totale_positivi;
    public int nuovi_positivi;
    public int deceduti;
    public int totale_casi;
    public int casi_testati;

    public int tamponi;

}

[Serializable]
public class RootAndamentoNazionale
{
    public AndamentoNazionale[] info;
}
