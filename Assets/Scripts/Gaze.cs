using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Gaze : MonoBehaviour
{

    List<InfoBehavior> infos = new List<InfoBehavior>();

    private Vector3 vectorX = new Vector3(0.02f, 0f, 0f);
    private Vector3 vectorY = new Vector3(0f, 0.02f, 0f);
    private Vector3 vectorZ = new Vector3(0f, 0f, 0.02f);

    [SerializeField]
    Canvas RegionCanvas;

    Vector3 desiredScale = new Vector3(1f, 0f, 1f);

    string m_regionOpened;

    public string regionOpened
    {
        get => m_regionOpened;
        set => m_regionOpened = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        infos = FindObjectsOfType<InfoBehavior>().ToList();
        var CanvasParent = GameObject.FindGameObjectWithTag("canvas");
        RegionCanvas = CanvasParent.GetComponentInChildren<Canvas>(true);
    }

    // Update is called once per frame
    void Update()
    {

        var CanvasParent = GameObject.FindGameObjectWithTag("canvas");
        if (CanvasParent.transform.localScale.y > 1f)
        {
            desiredScale = Vector3.zero;
        }
        if (CanvasParent.transform.localScale.y > 0.001f)
        {
            CloseAll();
        }

        CanvasParent.transform.localScale = Vector3.Lerp(CanvasParent.transform.localScale, desiredScale, Time.deltaTime * 15f);
        infos = FindObjectsOfType<InfoBehavior>().ToList();
        GameObject[] walls = GameObject.FindGameObjectsWithTag("InfoWall");
        if ((Physics.Raycast(transform.position, walls[0].transform.position, out RaycastHit hitWall)))
        {
            
            foreach (GameObject wall in walls)
            {
                if (hitWall.distance > 0.7f)
                {

                    wall.transform.localScale = Vector3.Lerp(wall.transform.localScale, Vector3.one, Time.deltaTime * 8);
                }
                else
                {

                    wall.transform.localScale = Vector3.Lerp(wall.transform.localScale, Vector3.zero, Time.deltaTime * 8);
                }
            }
        }
        if ((Physics.Raycast(transform.position, transform.forward, out RaycastHit hit)))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.green);


            GameObject go = hit.collider.gameObject;


            if (go.CompareTag("hasInfo"))
            {


                OpenInfo(go.GetComponent<InfoBehavior>(), hit.distance);
                if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit raycastHit;
                    if (Physics.Raycast(raycast, out raycastHit))
                    {
                        if (raycastHit.collider.gameObject.name.Equals(go.name))
                        {
                            CloseAll();
                            desiredScale = Vector3.one;
                            regionOpened = go.name;
                            RegionCanvas.gameObject.SetActive(true);


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


    void OpenInfo(InfoBehavior desiredInfo, float distance)
    {
        foreach (InfoBehavior info in infos)
        {
            if (info == desiredInfo)
            {
                info.OpenInfo(distance);
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