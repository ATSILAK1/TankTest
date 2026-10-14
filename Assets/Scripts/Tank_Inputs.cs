using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Inputs : MonoBehaviour
{

    public Camera camera1;
    public float reticuleSensitivity = 0.5f;
    
    
    public LayerMask terrainLayer;
    public Vector3 reticlePosition;
    public Vector3 ReticlePosition
    {
        get { return reticlePosition; }
    }

    private Vector3 reticleNormal;
    public Vector3 ReticleNormal
    {
        get { return reticleNormal; }
    }

    private float forwardInput;
    public float ForwardInput
    {
        get { return forwardInput; }
    }

    private float rotationInput;
    public float RotationInput
    {
        get { return rotationInput; }
    }

    private void Start()
    {
        reticuleSensitivity = 0.5f; 
    }


    // Update is called once per frame
    
    
    
    void Update()
    {
        if(camera1)
        {
            HandleInputs();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(reticlePosition, 0.5f);
    }

    protected virtual void HandleInputs()
    {
        float axisToMoveX= Input.GetAxis("RightStickX");
        float axisToMoveY = Input.GetAxis("RightStickY");
        
        
        
       
        
        forwardInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");
        if (axisToMoveX != 0 || axisToMoveY != 0)
        {
            reticlePosition.x += axisToMoveX * reticuleSensitivity ;
            reticlePosition.z -= axisToMoveY  * reticuleSensitivity;
            
            reticleNormal.x = Mathf.Clamp(reticleNormal.x, -Screen.width , Screen.width);
            reticleNormal.z = Mathf.Clamp(reticleNormal.z, -Screen.height , Screen.height);
            return; 
        }

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Ray screenRay = camera1.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(screenRay, out hit,float.PositiveInfinity,terrainLayer))
            {
                reticlePosition = hit.point;
                reticleNormal = hit.normal;
            }

        }
        
    }

  
}
