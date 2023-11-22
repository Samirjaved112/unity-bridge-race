using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMovementController : MonoBehaviour
{
    #region Variables
    private Camera cam;
    private Animator animator;
    private GameObject CameraFollow;

    
    [SerializeField] private float turnSpeed, speed, lerpValue, raycastDistance;
    [SerializeField] private LayerMask groundLayer;

    private Vector2 touchStartPos;
    private bool isMoving = false;
    private bool isGrounded;
    //private float prevYPosition;
    //private float currentYPosition;
    //private bool isMovingDown;
    private MeshRenderer stairMesh;
    private int stairCount;
    public int prevStairCount;
    public SkinnedMeshRenderer playerMesh;
    public StackController stackController;

    #endregion

    #region UnityCallBacks
    void Start()
    {

        cam = Camera.main;
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        // checking for touch 
        if (!UIManager.isGameOver)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPos = touch.position;
                    isMoving = true;
                }
                else if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && isMoving)
                {
                    Vector2 direction = (touch.position - touchStartPos).normalized;
                    if (direction == Vector2.zero)
                    {
                        return;
                    }
                    if (!animator.GetBool("running"))
                    {
                        animator.SetBool("running", true);
                    }

                    MovePlayer(direction);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isMoving = false;
                    if (animator.GetBool("running"))
                    {
                        animator.SetBool("running", false);
                    }
                }
            }
        }
    }
    #endregion

    #region PlayerMovement
    // function to move and rotate the player //
    void MovePlayer(Vector2 direction)
    {
        Vector3 movement = new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;
        if (!CheckBoundaryCollision(movement))
        {
            transform.Translate(movement, Space.World);
            CheckGround();

        }
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
    }
    bool CheckBoundaryCollision(Vector3 movement)
    {
        RaycastHit hit;
        Vector3 rayStart = transform.position + Vector3.up * movement.magnitude;
        if (Physics.Raycast(rayStart, movement.normalized, out hit, 0.1f))
        {
            if (hit.collider.CompareTag("Boundary"))
            {
                return true;
            }
            if (hit.collider.isTrigger)
            {
                if (hit.collider.CompareTag("StairBlue")
                     || hit.collider.CompareTag("StairRed")
                     || hit.collider.CompareTag("StairYellow")
                     || hit.collider.CompareTag("Stair"))
                {
                    stairCount = hit.collider.gameObject.GetComponent<BridgeHandler>().count;
                    //Debug.Log("count is " + stairCount + " :: " + "pre" + prevStairCount);
                }
                if (stairCount < prevStairCount)
                {
                   
                    return false;
                }
                else
                {
                    if ((hit.collider.CompareTag("Stair")
                         || hit.collider.CompareTag("StairRed")
                         || hit.collider.CompareTag("StairYellow"))
                         && stackController.count == 0)
                    {
                        
                        prevStairCount = stairCount;
                        return true;
                    }
                    else if ((hit.collider.CompareTag("Stair")
                             || hit.collider.CompareTag("StairRed")
                             || hit.collider.CompareTag("StairYellow"))
                             && stackController.count != 0)
                    {
                        prevStairCount = stairCount;

                        //hit.collider.transform.tag = "StairBlue";
                       
                        return false;
                    }
                }
            }
        }
        return false;
    }

    // function to check the ground and slope
    private void CheckGround()
    {
        RaycastHit hit;
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(rayStart, Vector3.down, out hit, raycastDistance,groundLayer))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle < 90.0f)
            {

                isGrounded = true;
                transform.position = hit.point + Vector3.up * 0.02f;

            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;

        if (other.gameObject.CompareTag("Stair") || other.gameObject.CompareTag("StairBlue"))
        {
           
            if (stairCount == 0 || stairCount == 21)
            {
                prevStairCount = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinalDestination"))
        {
            
            animator.Play("Dancing");
            UIManager.isGameOver = true;
            StartCoroutine(UIManager.instance.OnLevelComplete());
            Camera.main.GetComponent<CamControl>().winTarget = gameObject.transform;

        }
    }

    #endregion
}
