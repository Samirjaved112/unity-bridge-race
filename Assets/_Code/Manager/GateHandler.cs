using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHandler : MonoBehaviour
{
    [SerializeField] private string characterTags;
    [SerializeField] private GameObject colliderObject;
    [SerializeField] private Animator gateAnimator;
    private bool openGate;
    Vector3 targetPos;
    [SerializeField] private MeshRenderer gateMesh;
    private GameObject Gate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Gate = transform.GetChild(0).gameObject;
            openGate = true;
            targetPos = new Vector3(Gate.transform.localPosition.x + 0.6f, Gate.transform.localPosition.y, Gate.transform.localPosition.z);
            foreach (Transform child in gameObject.transform)
            {
                gateMesh = child.GetComponent<MeshRenderer>();
                gateMesh.material = other.gameObject.GetComponent<PlayerMovementController>().playerMesh.material;
            }
           
        }
        else if (other.gameObject.CompareTag("AIOne") || other.gameObject.CompareTag("AiYellow")) 
        {
            Gate = transform.GetChild(0).gameObject;
            openGate = true;
            targetPos = new Vector3(Gate.transform.localPosition.x + 0.6f, Gate.transform.localPosition.y, Gate.transform.localPosition.z);
            foreach (Transform child in gameObject.transform)
            {
                gateMesh = child.GetComponent<MeshRenderer>();
                gateMesh.material = other.gameObject.GetComponent<CharacterAiBehavior>().aiMesh.material;
            }

        }
    }
    private void Update()
    {
        if (openGate)
        {
            Gate.transform.localPosition = Vector3.Lerp(Gate.transform.localPosition, targetPos, 5 * Time.deltaTime);
            if (Vector3.Distance(targetPos,Gate.transform.localPosition) < 0.1f)
            {
                openGate = false;
            }
        }
    }
    private void OpenGate()
    {
        
    }
}
