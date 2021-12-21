using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlacement : MonoBehaviour
{
    public GameObject arObjectToSpawn;
    public GameObject placementIndicator;

    private GameObject spawnedObject;
    private Pose PlacementPose;
    private ARRaycastManager RaycastManager;
    private bool placementPoseIsValid = false;
    private GameObject Italy = null;

    [SerializeField]
    ARPlaneManager m_ARPlaneManager;


    void Start()
    {
        RaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Italy = ARPlaceObject();
            Italy.transform.localScale = new Vector3(0.0003f,0.0003f,0.0003f);
            
        }
        if (Italy != null){
            //controllo una delle componenti, se è meno di 0.3 continuo a aumentare la dimensione
            if (Italy.transform.localScale.x < 0.3f)
                Italy.transform.localScale = Vector3.Lerp(Italy.transform.localScale, new Vector3(0.3f,0.3f,0.3f), Time.deltaTime * 6);
            //else //altrimenti disabilito la detection di altri planes
                //DisablePlaneDetection(); //TODO CONTROLLARE SE FUNZIONA
                DisablePlaneDetection();

        }


        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }


    void UpdatePlacementIndicator()
    {
        if (spawnedObject == null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        if (Camera.current != null){
            var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            RaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            placementPoseIsValid = hits.Count > 0;
            if (placementPoseIsValid)
            {
                PlacementPose = hits[0].pose;
            }
        }
        
    }

    GameObject ARPlaceObject()
    {
        
        //aggiungo 0.075 in altezza per far vedere l'ombra
        spawnedObject = Instantiate(arObjectToSpawn, PlacementPose.position + new Vector3(0f,0.075f,0f), PlacementPose.rotation);
        return spawnedObject;
        
    }

    void DisablePlaneDetection(){
        ARPlaneManager m_ARPlaneManager;
        m_ARPlaneManager = FindObjectOfType<ARPlaneManager>();
        //TextAlert.Show("DEBUG");
        //TextAlert.Show("CANCELLATI " + m_ARPlaneManager.trackables.count + " PIANI");
        foreach (var plane in m_ARPlaneManager.trackables){
            //TextAlert.Show("CANCELLATO PIANO " + plane.name);
            plane.gameObject.SetActive(false);
        }
            
    }

}
