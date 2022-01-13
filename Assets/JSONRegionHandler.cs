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

    [SerializeField]
    TextMeshPro CasiMin;

    [SerializeField]
    TextMeshPro CasiMax;

    Canvas RegionCanvas;

    TextMeshProUGUI NomeReg;

    TextMeshProUGUI InfoReg;

    DataGetter dataGetter;

    private string[] region_names = {"Abruzzo", "Basilicata", "Calabria", "Campania", "Emilia Romagna",
                                    "Friuli Venezia Giulia", "Lazio", "Liguria", "Lombardia",
                                    "Marche", "Molise", "P.A. Bolzano", "P.A. Trento", "Piemonte",
                                    "Puglia", "Sardegna", "Sicilia", "Toscana", "Umbria",
                                    "Valle d'Aosta", "Veneto"};

    public AndamentoRegionale[] andamentoRegionale = new AndamentoRegionale[42];

    int[] totale_positivi_scale;

    // Start is called before the first frame update
    void Start()
    {
        dataGetter = FindObjectOfType<DataGetter>();
        if (dataGetter != null && dataGetter.andamentoRegionale != null)
        {
            andamentoRegionale = dataGetter.andamentoRegionale;
            setScale();
            StartCoroutine(FillRegionInfo(andamentoRegionale));

        }
        
    }

    void Update()
    {
        StartCoroutine(FillRegionCanvasInfo(andamentoRegionale));
    }

    IEnumerator FillRegionCanvasInfo(AndamentoRegionale[] regioni)
    {
        Gaze gaze = FindObjectOfType<Gaze>();
        var CanvasParent = GameObject.FindGameObjectWithTag("canvas");
        RegionCanvas = CanvasParent.GetComponentInChildren<Canvas>(true);
        foreach (TextMeshProUGUI text in RegionCanvas.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (text.name.Equals("NomeReg"))
            {
                NomeReg = text;
            }
            if (text.name.Equals("Info"))
            {
                InfoReg = text;
            }
        }
        int j = 0;
        int totale_positivi = 0;
        for (int i = andamentoRegionale.Length - 21; i < andamentoRegionale.Length; i++)
        {
            if (gaze.regionOpened.Equals(andamentoRegionale[i].denominazione_regione))
            {
                int nuovi_tamponi = andamentoRegionale[i].tamponi - andamentoRegionale[i - 21].tamponi;
                int nuovi_deceduti = andamentoRegionale[i].deceduti - andamentoRegionale[i - 21].deceduti;
                int nuovi_guariti = andamentoRegionale[i].dimessi_guariti - andamentoRegionale[i - 21].dimessi_guariti;
                float rapporto_positivi_tamponi = ((float)andamentoRegionale[i].nuovi_positivi / (float)nuovi_tamponi) * 100;
                totale_positivi = andamentoRegionale[i].totale_positivi;

                NomeReg.text = gaze.regionOpened;
                InfoReg.text = andamentoRegionale[i].totale_casi.ToString("#,#.#############################") +
                               "\n+" + andamentoRegionale[i].nuovi_positivi.ToString("#,#.#############################") +
                               "\n" + andamentoRegionale[i].totale_positivi.ToString("#,#.#############################") +
                               "\n+" + nuovi_deceduti.ToString("#,#.#############################") +
                               "\n" + andamentoRegionale[i].terapia_intensiva.ToString("#,#.#############################") +
                               "\n" + andamentoRegionale[i].ricoverati_con_sintomi.ToString("#,#.#############################") +
                               "\n" + andamentoRegionale[i].isolamento_domiciliare.ToString("#,#.#############################") +
                               "\n+" + nuovi_guariti.ToString("#,#.#############################") +
                               "\n+" + nuovi_tamponi.ToString("#,#.#############################") +
                               "\n\n" + Math.Round(rapporto_positivi_tamponi, 2) + "%";

            }
            j++;
        }
        foreach (Transform region in RegionCanvas.GetComponentsInChildren<Transform>(true))
        {
            if (Array.IndexOf(region_names, region.name) > -1)
            {
                if (gaze.regionOpened.Equals(region.name))
                {
                    Renderer RegionRendered = region.GetComponentInChildren<Renderer>();
                    RegionRendered.material.color = getRegionColor(totale_positivi);
                    region.gameObject.SetActive(true);
                }
                else
                {
                    region.gameObject.SetActive(false);
                }
            }

        }
        yield return null;


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
            j++;
        }
        j = 0;
        for (int i = regioni.Length - 21; i < regioni.Length; i++)
        {
            Transform[] comps = InfoRegioni[j].GetComponentsInParent<Transform>();
            foreach (Transform trans in comps)
            {
                if (trans.name.Equals(regioni[i].denominazione_regione))
                {
                    Renderer RegionRendered = trans.GetComponent<Renderer>();

                    UnityEngine.Random.InitState(regioni[i].totale_casi);
                    int index = UnityEngine.Random.Range(0, 6);
                    float duration = UnityEngine.Random.Range(0f, 0.05f);
                    yield return new WaitForSeconds(duration);
                    RegionRendered.material.DOColor(getRegionColor(regioni[i].totale_positivi), duration);
                }

            }

            if (regioni[i].denominazione_regione.Equals("Friuli Venezia Giulia"))
            {
                InfoRegioni[j].text =
                                           "\n\n\n" + regioni[i].totale_positivi.ToString("#,#.#############################") +
                                           "\n" + regioni[i].nuovi_positivi.ToString("#,#.#############################") +
                                           "\n+" + nuovi_deceduti[j];
            }
            else
            {
                InfoRegioni[j].text =
                           "\n\n" + regioni[i].totale_positivi.ToString("#,#.#############################") +
                           "\n+" + regioni[i].nuovi_positivi.ToString("#,#.#############################") +
                           "\n+" + nuovi_deceduti[j];
            }
            j++;
            CasiMin.text = totale_positivi_scale[0].ToString("#,#.#############################");
            CasiMax.text = totale_positivi_scale[totale_positivi_scale.Length - 1].ToString("#,#.#############################");

        }

    }

    private Color32 getRegionColor(int casi)
    {
        Color32[] colors = new Color32[6];
        colors[0] = new Color32(105, 179, 76, 1);
        colors[1] = new Color32(172, 179, 52, 1);
        colors[2] = new Color32(250, 183, 51, 1);
        colors[3] = new Color32(255, 142, 21, 1);
        colors[4] = new Color32(255, 78, 17, 1);
        colors[5] = new Color32(255, 13, 13, 1);
        Color32 colorToReturn = new Color32(0, 0, 0, 255);
        if (Array.IndexOf(totale_positivi_scale, casi) <= 3)
        {
            colorToReturn = colors[0];
        }
        else if (Array.IndexOf(totale_positivi_scale, casi) > 3 && Array.IndexOf(totale_positivi_scale, casi) <= 6)
        {
            colorToReturn = colors[1];
        }
        else if (Array.IndexOf(totale_positivi_scale, casi) > 6 && Array.IndexOf(totale_positivi_scale, casi) <= 9)
        {
            colorToReturn = colors[2];
        }
        else if (Array.IndexOf(totale_positivi_scale, casi) > 9 && Array.IndexOf(totale_positivi_scale, casi) <= 13)
        {
            colorToReturn = colors[3];
        }
        else if (Array.IndexOf(totale_positivi_scale, casi) > 13 && Array.IndexOf(totale_positivi_scale, casi) <= 17)
        {
            colorToReturn = colors[4];
        }
        else if (Array.IndexOf(totale_positivi_scale, casi) > 17)
        {
            colorToReturn = colors[5];
        }
        else
        { //fallback
            colorToReturn = colors[5];
        }
        return colorToReturn;
    }

    private void setScale()
    {
        totale_positivi_scale = new int[21];
        int j = 0;
        for (int i = andamentoRegionale.Length - 21; i < andamentoRegionale.Length; i++)
        {
            totale_positivi_scale[j] = andamentoRegionale[i].totale_positivi;
            j++;
        }
        Array.Sort(totale_positivi_scale);
    }
}

[Serializable]
public class AndamentoRegionale
{
    public string data;
    public string denominazione_regione;

    public int ricoverati_con_sintomi;
    public int terapia_intensiva;
    public int isolamento_domiciliare;
    public int totale_positivi;
    public int nuovi_positivi;
    public int dimessi_guariti;
    public int deceduti;
    public int totale_casi;
    public int tamponi;
    public int casi_testati;

}

[Serializable]
public class RootAndamentoRegionale
{
    public AndamentoRegionale[] regioni;
}
