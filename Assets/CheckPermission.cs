using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CheckPermission : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckCameraPermission());
    }

    IEnumerator CheckCameraPermission(){
        yield return new WaitForSeconds(5);
        ARCameraManager cameraManager = FindObjectOfType<ARCameraManager>();
        if (cameraManager != null){
            if (!cameraManager.permissionGranted){
                TextAlert.Show("Questa applicazione ha bisogno dei permessi della fotocamera per funzionare correttamente. Attiva i permessi dalle impostazioni.");
                
            }
        }
    }

    
}
