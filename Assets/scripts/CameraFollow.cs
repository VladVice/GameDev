using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform PlayerToFollow;

    public float smoothSpeed = 0.2f;
    public float rotationSpeed = 5.0f;
    public Vector3 offset; // Ofsets
    float inputY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate() // Runs after Upade
    {
        Vector3 lookat = PlayerToFollow.position + offset; // Uz ko mes skatiisimies
        Vector3 smoothPos = Vector3.Lerp(transform.position, lookat, smoothSpeed); // + smooth camera movement. 
        transform.position = smoothPos;

        inputY = Input.GetAxis("Horizontal");

        // Kameras rotesana ap speletaju
        if(Input.GetMouseButton(1))
        {
            //transform.RotateAround(PlayerToFollow.position, Vector3.up, Input.GetAxis("Mouse X") * 45.0f);
            Quaternion camRotate = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            transform.LookAt(PlayerToFollow);

            offset = camRotate * offset;
        }
    }


    void Update()
    {
        
    }
}
