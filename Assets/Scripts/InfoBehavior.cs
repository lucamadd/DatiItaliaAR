using UnityEngine;
using DG.Tweening;
using TMPro;

public class InfoBehavior : MonoBehaviour
{

    const float SPEED = 6f;

    [SerializeField]
    Transform SectionInfo;

    Vector3 desiredScale = Vector3.zero;
    Vector3 originalScale = new Vector3(0.0003f, 0.0003f, 0.0003f);

    float desiredZ = 5.7e-05f; //chiuso
    float originalZ = 0.000281f; //aperto

    Vector3 desiredRegionPosition = new Vector3(0f,0.003f,0f);//chiuso
    Vector3 originalRegionPosition = new Vector3(0f,0.003f,0.006f); //aperto

    //size normalmente
    Vector3 desiredRegionScale = new Vector3(100,100,100);

    //size quando apri le info
    Vector3 originalRegionScale = new Vector3(110,110,110);
    
    Transform Region;

    Renderer RegionRendered;
    Color regionColor = Color.black;
    Color32[] colors = new Color32[6]; 

    Color32 desiredColor = new Color32(0,0,0,1);

    private int index;
    private int duration;
    
    TextMeshPro InfoText;

    void Start(){
        Region = GetComponentInParent<Transform>();
        RegionRendered = Region.GetComponent<Renderer>();
        
    }


    // Update is called once per frame
    void Update()
    {
        SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desiredScale, Time.deltaTime * SPEED);
        //RegionRendered.material.DOColor(regionColor, 0.5f);
        Region.localPosition = Vector3.Lerp(Region.localPosition, desiredRegionPosition, Time.deltaTime * SPEED);
        SectionInfo.localPosition = Vector3.Lerp(SectionInfo.localPosition, new Vector3(SectionInfo.localPosition.x,SectionInfo.localPosition.y,desiredZ), Time.deltaTime * SPEED);
        foreach(TextMeshPro infoText in SectionInfo.GetComponentsInChildren<TextMeshPro>()){
            infoText.color = desiredColor;
        }
    }

    public void OpenInfo()
    {
        Region.localPosition = Vector3.Lerp(Region.localPosition, new Vector3(0f,0.003f,0.006f), Time.deltaTime * SPEED);
        desiredZ = originalZ;
        desiredRegionPosition = originalRegionPosition;
        desiredScale = originalScale;
        desiredColor = new Color32(0,0,0,255);
        
    }

    public void CloseInfo()
    {
        //regionColor = Color.black;
        Region.localPosition = Vector3.Lerp(Region.localPosition, new Vector3(0f,0.003f,0f), Time.deltaTime * SPEED);
        //desiredScale = Vector3.zero;
        desiredZ = 5.7e-05f;
        desiredRegionPosition = new Vector3(0f,0.003f,0f);
        desiredScale = new Vector3(5e-05f, 5e-05f,0.0003f);
        desiredColor = new Color32(0,0,0,0);
    }
}