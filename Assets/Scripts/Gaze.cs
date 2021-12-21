using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Gaze : MonoBehaviour
{

    List<InfoBehavior> infos = new List<InfoBehavior>();

    private Vector3 vectorX = new Vector3(0.02f,0f,0f);
    private Vector3 vectorY = new Vector3(0f,0.02f,0f);
    private Vector3 vectorZ = new Vector3(0f,0f,0.02f);

    // Start is called before the first frame update
    void Start()
    {
        infos = FindObjectsOfType<InfoBehavior>().ToList();

    }

    // Update is called once per frame
    void Update()
    {
        infos = FindObjectsOfType<InfoBehavior>().ToList();
        if ((Physics.Raycast(transform.position, transform.forward, out RaycastHit hit)) ||
            (Physics.Raycast(transform.position, transform.forward + vectorX, out RaycastHit hitPlusX)) ||
            (Physics.Raycast(transform.position, transform.forward + vectorY, out RaycastHit hitPlusY)) ||
            (Physics.Raycast(transform.position, transform.forward + vectorZ, out RaycastHit hitPlusZ)) ||
            (Physics.Raycast(transform.position, transform.forward - vectorX, out RaycastHit hitMinusX)) ||
            (Physics.Raycast(transform.position, transform.forward - vectorY, out RaycastHit hitMinusY)) ||
            (Physics.Raycast(transform.position, transform.forward - vectorZ, out RaycastHit hitMinusZ)))
        {
            Debug.DrawRay(transform.position, transform.forward + vectorX, Color.green); 
            Debug.DrawRay(transform.position, transform.forward + vectorY, Color.green); 
            Debug.DrawRay(transform.position, transform.forward + vectorZ, Color.green); 
            Debug.DrawRay(transform.position, transform.forward - vectorX, Color.green); 
            Debug.DrawRay(transform.position, transform.forward - vectorY, Color.green); 
            Debug.DrawRay(transform.position, transform.forward - vectorZ, Color.green); 


            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("hasInfo"))
            {   
                OpenInfo(go.GetComponent<InfoBehavior>());
                if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit raycastHit;
                    if (Physics.Raycast(raycast, out raycastHit))
                    {
                        if (raycastHit.collider.gameObject.name.Equals(go.name)){
                            TextAlert.Show("Cliccato su " + raycastHit.collider.gameObject.name);
                        }
                        
                    }
                }
            }
        }
        else
        {
            CloseAll();
        }
    }

    void OpenInfo(InfoBehavior desiredInfo)
    {
        foreach (InfoBehavior info in infos)
        {
            if (info == desiredInfo)
            {
                info.OpenInfo();
            }
            else
            {
                info.CloseInfo();
            }
        }
    }


    void CloseAll()
    {
        foreach (InfoBehavior info in infos)
        {
            info.CloseInfo();
        }
    }
}