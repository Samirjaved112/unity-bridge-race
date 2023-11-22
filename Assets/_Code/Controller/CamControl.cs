using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    
    public Transform target;
    public Vector3 offset;
    public float lerpValue;
    public float rotationSpeed;  // Adjust the rotation speed as needed
    private bool startRotating;
    public Transform winTarget;
    void LateUpdate()
    {
        
        if (UIManager.isGameOver)
        {
            if (!startRotating)
            {
                offset = new Vector3(0f, 1.90f, -2.1f);
                Vector3 despos = winTarget.position + offset;
                transform.position = Vector3.Lerp(transform.position, despos ,5*Time.deltaTime);
                if (Vector3.Distance(transform.position, despos) < 1)
                {

                startRotating = true;
                }
            }
            else
            {
                transform.LookAt(winTarget.position);
                transform.RotateAround(winTarget.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
            
        }
        else
        {
            Vector3 desPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desPos, lerpValue);
        }
    } 

    
}
