using UnityEngine;

//INCOMPLETE SCRIPT.
/// <summary>
///  Was an idea to try and make
///the dialogue indicator show up when camera is relatively center on spirit.
///Did not finish. _P
/// </summary>
public class cameraTracking : MonoBehaviour
{
    public Transform target;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        //adjust code 
        Vector3 viewPos = cam.WorldToViewportPoint(target.position);
        if (viewPos.x > 0.5F) ;
            //print("target is on the right side!");
        
           // print("target is on the left side!");
    }
}