using UnityEngine;

[ExecuteInEditMode]
public class FaceCamera : MonoBehaviour
{

    Transform cam;
    Vector3 targetAngle = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam);
        targetAngle = transform.eulerAngles;
        targetAngle.y = targetAngle.y + 180;
        targetAngle.x = 0;
        targetAngle.z = 0;
        transform.eulerAngles = targetAngle;
    }
}
