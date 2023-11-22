using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCubes : MonoBehaviour
{
    #region Variables
    private Transform lastCubeAdded;
    [SerializeField] private float speed;
    public bool isCollected;
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    [SerializeField] private Vector3 pointC;
    [SerializeField] private Vector3 pointAB;
    [SerializeField] private Vector3 pointBC;
    [SerializeField] private Vector3 pointAB_BC;
    private float interpolateAmount;
    private bool Picked;
    float distance;
    #endregion

    #region UnityCallBacks

    private void Start()
    {
        isCollected = false;
    }
    void Update()
    {
        if (isCollected)
        {
            interpolateAmount = (interpolateAmount + Time.deltaTime * speed);
            pointAB = Vector3.Lerp(pointA, pointB, interpolateAmount);
            pointBC = Vector3.Lerp(pointB, pointC, interpolateAmount);
            pointAB_BC = Vector3.Lerp(pointAB, pointBC, interpolateAmount);
            transform.localPosition = pointAB_BC;
           // float distance = Vector3.Distance(transform.localPosition, pointC);
            if (Vector3.Distance(transform.localPosition, pointC)<=0.01f)
            {
                Picked = false;
                ManageTrail(Picked);
                transform.localRotation = Quaternion.identity ;
                transform.localPosition = pointC;
                isCollected = false;
            }
        }

    }
    
    #endregion

    #region Cube Management
    public void SetPosition(Vector3 pointa,Vector3 pointb, Vector3 pointc)
    {
        pointA = pointa;
        pointB = pointb;
        pointC = pointc;
        isCollected = true;
    }

    private void ManageTrail(bool isPicked)
    {
        if (isPicked)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    #endregion
}
