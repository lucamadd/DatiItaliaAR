using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class JSONRegionHandler : MonoBehaviour
{

    [SerializeField]
    TextMeshPro InfoAbruzzo;
    [SerializeField]
    TextMeshPro InfoBasilicata;
    [SerializeField]
    TextMeshPro InfoCalabria;
    [SerializeField]
    TextMeshPro InfoCampania;
    [SerializeField]
    TextMeshPro InfoEmiliaRomagna;
    [SerializeField]
    TextMeshPro InfoFriuliVeneziaGiulia;
    [SerializeField]
    TextMeshPro InfoLazio;
    [SerializeField]
    TextMeshPro InfoLiguria;
    [SerializeField]
    TextMeshPro InfoLombardia;
    [SerializeField]
    TextMeshPro InfoMarche;
    [SerializeField]
    TextMeshPro InfoMolise;
    [SerializeField]
    TextMeshPro InfoPABolzano;
    [SerializeField]
    TextMeshPro InfoPATrento;
    [SerializeField]
    TextMeshPro InfoPiemonte;
    [SerializeField]
    TextMeshPro InfoPuglia;
    [SerializeField]
    TextMeshPro InfoSardegna;
    [SerializeField]
    TextMeshPro InfoSicilia;
    [SerializeField]
    TextMeshPro InfoToscana;
    [SerializeField]
    TextMeshPro InfoUmbria;
    [SerializeField]
    TextMeshPro InfoValleDAosta;
    [SerializeField]
    TextMeshPro InfoVeneto;

    public RootAndamentoRegionale andamentoRegionale = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequestRegionale("https://raw.githubusercontent.com/pcm-dpc/COVID-19/master/dati-json/dpc-covid19-ita-regioni-latest.json"));
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
                    FillRegionInfo(andamentoRegionale.regioni);
                    break;
            }
        }
    }

    private void FillRegionInfo(AndamentoRegionale[] regioni){

        TextMeshPro[] InfoRegioni = {InfoAbruzzo, InfoBasilicata, InfoCalabria, InfoCampania,
                                     InfoEmiliaRomagna, InfoFriuliVeneziaGiulia, InfoLazio,
                                     InfoLiguria, InfoLombardia, InfoMarche, InfoMolise,
                                     InfoPABolzano, InfoPATrento, InfoPiemonte, InfoPuglia,
                                     InfoSardegna, InfoSicilia, InfoToscana, InfoUmbria,
                                     InfoValleDAosta, InfoVeneto};

        for (int i=0; i<21; i++){
            InfoRegioni[i].text = regioni[i].denominazione_regione + 
                           "\n\nTotale " + regioni[i].totale_casi.ToString("#,#.#############################") +
                           "\nNuovi " + regioni[i].nuovi_positivi.ToString("#,#.#############################") +
                           "\nDeceduti " + regioni[i].deceduti.ToString("#,#.#############################");
        }
        
    }
}

[Serializable]
public class AndamentoRegionale{
    public string data;
    public string denominazione_regione;
    public int terapia_intensiva;
    public int totale_positivi;
    public int nuovi_positivi;
    public int deceduti;
    public int totale_casi;
    public int casi_testati;

}

 [Serializable]
 public class RootAndamentoRegionale
 {
     public AndamentoRegionale[] regioni;
 }
