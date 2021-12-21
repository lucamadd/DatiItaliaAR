using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    DataGetter dataGetter;

    public RootAndamentoRegionale andamentoRegionale = null;

    // Start is called before the first frame update
    void Start()
    {
        dataGetter = FindObjectOfType<DataGetter>();
        if (dataGetter != null && dataGetter.andamentoRegionale != null){
            andamentoRegionale = dataGetter.andamentoRegionale;
            StartCoroutine(FillRegionInfo(andamentoRegionale.regioni));
        }
        
    }

    

    IEnumerator FillRegionInfo(AndamentoRegionale[] regioni)
    {

        int[] nuovi_deceduti = new int[21];

        TextMeshPro[] InfoRegioni = {InfoAbruzzo, InfoBasilicata, InfoCalabria, InfoCampania,
                                     InfoEmiliaRomagna, InfoFriuliVeneziaGiulia, InfoLazio,
                                     InfoLiguria, InfoLombardia, InfoMarche, InfoMolise,
                                     InfoPABolzano, InfoPATrento, InfoPiemonte, InfoPuglia,
                                     InfoSardegna, InfoSicilia, InfoToscana, InfoUmbria,
                                     InfoValleDAosta, InfoVeneto};
        int j = 0;
        for (int i = regioni.Length - 21; i < regioni.Length; i++)
        {
            nuovi_deceduti[j] = regioni[i].deceduti - regioni[i - 21].deceduti;
            //Debug.Log("222 - nuovi deceduti: " + nuovi_deceduti[j].ToString("#,#.#############################") + " - " + regioni[i].denominazione_regione);
            j++;
        }
        j = 0;
        for (int i = regioni.Length - 21; i < regioni.Length; i++)
        {
            Transform[] comps = InfoRegioni[j].GetComponentsInParent<Transform>();
            foreach (Transform trans in comps)
            {
                ///Debug.Log("444 - " + trans.name + " - " + regioni[i].denominazione_regione);
                if (trans.name.Equals(regioni[i].denominazione_regione))
                {
                    Renderer RegionRendered = trans.GetComponent<Renderer>();
                    Color32[] colors = new Color32[6];
                    colors[0] = new Color32(105, 179, 76, 1);
                    colors[1] = new Color32(172, 179, 52, 1);
                    colors[2] = new Color32(250, 183, 51, 1);
                    colors[3] = new Color32(255, 142, 21, 1);
                    colors[4] = new Color32(255, 78, 17, 1);
                    colors[5] = new Color32(255, 13, 13, 1);
                    UnityEngine.Random.InitState(regioni[i].totale_casi);
                    int index = UnityEngine.Random.Range(0, 6);
                    float duration = UnityEngine.Random.Range(0f, 0.05f);
                    ///Debug.Log("444 - " + trans.name + " - " + "index: " + index + " - " + "duration: " +duration);
                    yield return new WaitForSeconds(duration);
                    RegionRendered.material.DOColor(colors[index], duration);
                }

            }

            if (regioni[i].denominazione_regione.Equals("Friuli Venezia Giulia"))
            {
                InfoRegioni[j].text =
                                           "\n\n\n" + regioni[i].totale_casi.ToString("#,#.#############################") +
                                           "\n" + regioni[i].nuovi_positivi.ToString("#,#.#############################") +
                                           "\n+" + nuovi_deceduti[j];
            }
            else
            {
                InfoRegioni[j].text =
                           "\n\n" + regioni[i].totale_casi.ToString("#,#.#############################") +
                           "\n" + regioni[i].nuovi_positivi.ToString("#,#.#############################") +
                           "\n+" + nuovi_deceduti[j];
            }
            j++;
            //Debug.Log("222 - PASSED - i = " + i + " - j = " + j);
        }

    }


}

[Serializable]
public class AndamentoRegionale
{
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
