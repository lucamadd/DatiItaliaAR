using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateTools : MonoBehaviour
{
    
    
    public static string GetItalianDate(string ISODate){
        string italianDate = "";
        if (ISODate.IndexOf("-") == -1){
            italianDate = "Errore di rete";
        }
        Debug.Log(ISODate);
        string anno = ISODate.Substring(0,4);
        Debug.Log("ANNO " + anno);
        string mese = ISODate.Substring(5,2);  //il secondo parametro è la lunghezza, non l'indice finale
        string giorno = ISODate.Substring(8,2); //ho perso due ore per questo problema

        italianDate = giorno + " " + GetItalianMonth(mese) + " " + anno;


        return italianDate;
    }

    private static string GetItalianMonth(string mese){
        string italianMonth = "";
        switch(mese){
            case "01":
                italianMonth = "Gennaio";
                break;
            case "02":
                italianMonth = "Febbraio";
                break;
            case "03":
                italianMonth = "Marzo";
                break;
            case "04":
                italianMonth = "Aprile";
                break;
            case "05":
                italianMonth = "Maggio";
                break;
            case "06":
                italianMonth = "Giugno";
                break;
            case "07":
                italianMonth = "Luglio";
                break;
            case "08":
                italianMonth = "Agosto";
                break;
            case "09":
                italianMonth = "Settembre";
                break;
            case "10":
                italianMonth = "Ottobre";
                break;
            case "11":
                italianMonth = "Novembre";
                break;
            case "12":
                italianMonth = "Dicembre";
                break;

        }
        return italianMonth;
    }
    
}
