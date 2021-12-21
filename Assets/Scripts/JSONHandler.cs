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

    DataGetter dataGetter;

    public RootAndamentoNazionale andamentoNazionale = null;

    

    void Start()
    {
        dataGetter = FindObjectOfType<DataGetter>();
        if (dataGetter != null && dataGetter.andamentoNazionale != null){
            andamentoNazionale = dataGetter.andamentoNazionale;
            StartCoroutine(FillMainInfo(andamentoNazionale.info[andamentoNazionale.info.Length-2], andamentoNazionale.info[andamentoNazionale.info.Length-1]));
        }
        
        
        // A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));
    }

    

    IEnumerator FillMainInfo(AndamentoNazionale andamentoNazionaleOld, AndamentoNazionale andamentoNazionaleNew){

        int nuovi_deceduti = andamentoNazionaleNew.deceduti - andamentoNazionaleOld.deceduti;
        int nuovi_tamponi = andamentoNazionaleNew.tamponi - andamentoNazionaleOld.tamponi;
        float rapporto_positivi_tamponi = ((float)andamentoNazionaleNew.nuovi_positivi / (float)nuovi_tamponi)*100;
        Debug.Log("222 - rapporto positivi tamponi: " + rapporto_positivi_tamponi);

        today_date.text = DateTools.GetItalianDate(andamentoNazionaleNew.data.Substring(0,10));
        today_info.text =   andamentoNazionaleNew.totale_casi.ToString("#,#.#############################") +
                            "\n+" + andamentoNazionaleNew.nuovi_positivi.ToString("#,#.#############################") +
                            "\n+" + nuovi_deceduti.ToString("#,#.#############################") +
                            "\n+" + nuovi_tamponi.ToString("#,#.#############################") +
                            "\n\n" + Math.Round(rapporto_positivi_tamponi, 2) + "%";
        yield return null;
    }

}

