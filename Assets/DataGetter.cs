using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataGetter : MonoBehaviour
{
    public RootAndamentoNazionale andamentoNazionale = null;
    public AndamentoRegionale[] andamentoRegionale = new AndamentoRegionale[42];

    void Start()
    {
        StartCoroutine(checkConnection());
        StartCoroutine(GetRequestNazionale("https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-json/dpc-covid19-ita-andamento-nazionale.json"));
        StartCoroutine(GetRequestRegionale("https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-json/dpc-covid19-ita-regioni.json"));

    }

    IEnumerator checkConnection(){
        string uri = "https://www.google.com";
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
                    TextAlert.ShowAndExit("Nessuna connessione individuata. Connettiti ad internet per continuare.");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    TextAlert.ShowAndExit("Si è verificato un errore di rete.");
                    break;
                case UnityWebRequest.Result.Success:
                    TextAlert.Show("Cerca un piano orizzontale per posizionare correttamente l'indicatore.");
                    break;
            }
        }
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
                    andamentoNazionale = JsonUtility.FromJson<RootAndamentoNazionale>("{\"info\":" + json_string + "}");

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
                    RootAndamentoRegionale temp = JsonUtility.FromJson<RootAndamentoRegionale>("{\"regioni\":" + json_string + "}");
                    int j=0;
                    for (int i=temp.regioni.Length - 42;i < temp.regioni.Length;i++){
                        andamentoRegionale[j] = temp.regioni[i];
                        j++;
                    }
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



