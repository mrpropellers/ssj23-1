using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovement : MonoBehaviour{

    public float horizontalSpeed;
    public float verticalSpeed;
    public float rotationSpeed;
    public float amplitude;

    public Vector3 tempPosition;
    public Vector2 originalPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        tempPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tempPosition.x = Mathf.Cos(Time.time * horizontalSpeed) * amplitude;
        tempPosition.y = Mathf.Sin(Time.time * verticalSpeed) * amplitude;

        tempPosition += (Vector3)originalPosition;

        var direction = tempPosition - transform.position;
        var rotateDir = Quaternion.LookRotation(Vector3.forward, direction);

        var zRot = rotateDir.eulerAngles.z;
        zRot = Mathf.Clamp(zRot, -45, 45);

        if((tempPosition.x - transform.position.x) == Mathf.Abs(tempPosition.x - transform.position.x)){
            zRot = zRot * -1;
        }

        if((tempPosition.y - transform.position.y) != Mathf.Abs(tempPosition.y - transform.position.y)){
            zRot = 0;
        }

        var finalRotation = Quaternion.Euler(0, 0, zRot);

        transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, rotationSpeed);
        transform.position = tempPosition;
    }
}
