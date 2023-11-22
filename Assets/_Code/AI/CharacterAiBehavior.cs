using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAiBehavior : MonoBehaviour
{
    
    [SerializeField] private LayerMask objectLayer;
    [SerializeField] private float radius;
    [SerializeField] private float turnSpeed;
    private NavMeshAgent navmeshAgent;
    public bool isMovingTowardCube;
    private GameObject closestCube;
    private Animator aiAnimator;
    [SerializeField] private Transform destinationOne;
    [SerializeField] private Transform destinationTwo;
    [SerializeField] private Transform destinationThree;
    [SerializeField] private Transform destinationFour;
    [SerializeField] private Transform destinationFive;
    [SerializeField] private Transform destinationFinal;
    private int stairCount;
    private int prevStairCount;
    [SerializeField] private string cubeTag;
    [SerializeField] private string stairTag;
    public StackController stackcontroller;
    public SkinnedMeshRenderer aiMesh;
    private int floorCount;

    private void Start()
    {
        aiAnimator = transform.GetChild(0).GetComponent<Animator>();
        navmeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    { 
        if (!isMovingTowardCube)
        {
           // Invoke(" CheckObjectPositions", 1.5f);
           CheckObjectPositions(); 
           if (!aiAnimator.GetBool("running"))
           {
              aiAnimator.SetBool("running", true);
           }   
        }
          SetRotation();
        if (UIManager.isGameOver)
        {

            navmeshAgent.velocity = Vector3.zero;
            aiAnimator.SetBool("running", false);

        }
    }
    private void CheckObjectPositions()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, Vector3.up, 0f, objectLayer);
        closestCube = null;
        float closestDistance = float.MaxValue;
        foreach (RaycastHit hit in hits)
        {
            GameObject hitObject = hit.collider.gameObject;
            float distance = Vector3.Distance(transform.position, hitObject.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCube = hitObject;
            }
        }
        if (closestCube != null)
        {
            SetMovement();
            isMovingTowardCube = true;
        }
    }
    private void SetMovement()
    {
        switch (floorCount)
        {
            case 0:
                if (gameObject.GetComponent<StackController>().count >= Random.Range(5, 8))
                {
                    navmeshAgent.destination = destinationOne.position;
                }
                else
                {
                    navmeshAgent.destination = closestCube.transform.position;
                }
                break;
            case 1:
                if (gameObject.GetComponent<StackController>().count >= Random.Range(5, 8))
                {
                    navmeshAgent.destination = destinationThree.position;
                }
                else
                {
                    navmeshAgent.destination = closestCube.transform.position;
                }
                break;
            case 2:
                if (gameObject.CompareTag("AiYellow"))
                {
                    if (gameObject.GetComponent<StackController>().count >= Random.Range(3, 5))
                    {
                        navmeshAgent.destination = destinationFive.position;
                    }
                    else
                    {
                        navmeshAgent.destination = closestCube.transform.position;
                    }
                }
                else
                {
                    if (gameObject.GetComponent<StackController>().count >= Random.Range(8, 12))
                    {
                        navmeshAgent.destination = destinationFive.position;
                    }
                    else
                    {
                        navmeshAgent.destination = closestCube.transform.position;
                    }
                }
                //if (gameObject.GetComponent<StackController>().count >= Random.Range(5, 8))
                //{
                    
                //}
                //else
                //{
                //    navmeshAgent.destination = closestCube.transform.position;
                //}
                break;
        }
        Vector3 movement = (navmeshAgent.destination - transform.position).normalized;
        

    }
    private void SetRotation()
    {
        if (closestCube != null)
        {
            Vector3 direction = navmeshAgent.destination - transform.position;
            direction.y = 0f;
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destination"))
        {
            if (stackcontroller.count != 0)
            {
                navmeshAgent.destination = destinationTwo.position;
            }
            else
            {
                isMovingTowardCube = false;
            }
        }
        if (other.gameObject.CompareTag("DestinationTwo"))
        {
            
            isMovingTowardCube = false;
            floorCount++;
        }
        if (other.gameObject.CompareTag("DestinationThree"))
        {
            if(stackcontroller.count != 0)
            {
                
            navmeshAgent.destination = destinationFour.position;
            }
            else
            {
               

                isMovingTowardCube = false;
            }
        }
        if (other.gameObject.CompareTag("DestinationFour"))
        {
            isMovingTowardCube = false;
            floorCount++;
        }
       
        if (other.gameObject.CompareTag("DestinationFive"))
        {
            if (stackcontroller.count != 0)
            {
           
                navmeshAgent.destination = destinationFinal.position;
            }
            else
            {
              

                isMovingTowardCube = false;
            }
           
        }
        if (other.gameObject.CompareTag("FinalDestination"))
        {
            aiAnimator.Play("Dancing");
            UIManager.isGameOver = true;
            StartCoroutine(UIManager.instance.OnLevelFail());
            Camera.main.GetComponent<CamControl>().winTarget = gameObject.transform;
        }

        if (other.gameObject.CompareTag(cubeTag))
        {
            isMovingTowardCube = false;
            other.gameObject.layer = LayerMask.NameToLayer("Collected");
        }
        if (other.gameObject.CompareTag(stairTag))
        {
            switch (floorCount)
            {
                case 0:
                    if (stackcontroller.count <= 0)
                    {
                       
                        navmeshAgent.destination = destinationOne.position;
                    }
                    break;
                case 1:
                    if (stackcontroller.count <= 0)
                    {

                        navmeshAgent.destination = destinationThree.position;
                    }
                    break;
                case 2:
                    if (stackcontroller.count <= 0)
                    {

                        navmeshAgent.destination = destinationFive.position;
                    }
                    break;
            }
            
        }


    }
}
