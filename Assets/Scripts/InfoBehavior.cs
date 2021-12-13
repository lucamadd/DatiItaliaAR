using UnityEngine;
using DG.Tweening;

public class InfoBehavior : MonoBehaviour
{

    const float SPEED = 6f;

    [SerializeField]
    Transform SectionInfo;

    Vector3 desiredScale = Vector3.zero;
    Vector3 originalScale = new Vector3(0.0003f, 0.0003f, 0.0003f);

    //size normalmente
    Vector3 desiredRegionScale = new Vector3(100,100,100);

    //size quando apri le info
    Vector3 originalRegionScale = new Vector3(110,110,110);
    
    Transform Region;

    Renderer RegionRendered;
    Color regionColor = Color.black;

    

    void Start(){
        Region = GetComponentInParent<Transform>();
        RegionRendered = Region.GetComponent<Renderer>();
    }


    // Update is called once per frame
    void Update()
    {
        SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desiredScale, Time.deltaTime * SPEED);
        RegionRendered.material.DOColor(regionColor, 0.5f);
    }

    public void OpenInfo()
    {
        Region.localPosition = Vector3.Lerp(Region.localPosition, new Vector3(0f,0.003f,0.006f), Time.deltaTime * SPEED);
        desiredScale = originalScale;
        regionColor = new Color32(99,102,106,1);
    }

    public void CloseInfo()
    {
        regionColor = Color.black;
        Region.localPosition = Vector3.Lerp(Region.localPosition, new Vector3(0f,0.003f,0f), Time.deltaTime * SPEED);
        desiredScale = Vector3.zero;
    }
}
