//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CollectAICubes : MonoBehaviour
//{

//    #region Variables
//    private Transform lastCubeAdded;
//    [SerializeField] private float speed;
//    public bool isCollected;
//    [SerializeField] private Vector3 pointA;
//    [SerializeField] private Vector3 pointB;
//    [SerializeField] private Vector3 pointC;
//    [SerializeField] private Vector3 pointAB;
//    [SerializeField] private Vector3 pointBC;
//    [SerializeField] private Vector3 pointAB_BC;
//    private float interpolateAmount;
//    private Transform pointb;
//    private GameObject collidedGameObject;
//    public GameObject playerObject;
//    public GameObject parent;
//    private bool Picked;
//    [SerializeField] private string aiTag;
//    [SerializeField] private string cubeTag;
//    #endregion

//    #region UnityCallBacks
//    private void Start()
//    {
//        //lastCubeAdded = GameManager.instance.initialCubeAi;
//        //pointb = GameManager.instance.pointbAi;
//    }

//    void Update()
//    {
//        if (isCollected)
//        {
//            interpolateAmount = (interpolateAmount + Time.deltaTime * speed) % 1f;
//            pointAB = Vector3.Lerp(pointA, pointB, interpolateAmount);
//            pointBC = Vector3.Lerp(pointB, pointC, interpolateAmount);
//            pointAB_BC = Vector3.Lerp(pointAB, pointBC, interpolateAmount);
//            collidedGameObject.transform.localPosition = pointAB_BC;
//            float distance = Vector3.Distance(collidedGameObject.transform.localPosition, pointC);
//            if (distance <= 0.8)
//            {
//                Picked = false;
//                ManageTrail(Picked);
//                collidedGameObject.transform.localRotation = lastCubeAdded.localRotation;
//                collidedGameObject.transform.localPosition = GameManager.instance.initialCubeAi.localPosition;
//                isCollected = false;
//            }
//        }
//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag(aiTag) && !isCollected)
//        {
//            CollectCube(other.gameObject);
//            gameObject.layer = LayerMask.NameToLayer("Collected");
//        }
//    }
//    #endregion

//    #region Cube Management
//    private void CollectCube(GameObject player)
//    {
//        collidedGameObject = gameObject;
//        playerObject = player;
//        parent = playerObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
//        collidedGameObject.transform.SetParent(parent.transform);
//        Picked = true;
//        ManageTrail(Picked);
//        AddCubes(gameObject);
//        SetPosition();
//        isCollected = true;
//    }
//    private void AddCubes(GameObject cube)
//    {
//            GameManager.instance.cubeListAiRed.Add(cube);
       
//    }

//    private void SetPosition()
//    {
//        pointA = collidedGameObject.transform.localPosition;
//        pointB = new Vector3(pointb.localPosition.x, pointb.localPosition.y, pointb.localPosition.z);
//        pointC = lastCubeAdded.localPosition;
//        GameManager.instance.UpdateAiCubePos();
//        //}

//        //else
//        //{
//        //    GameManager.instance.cubeListAiYellow.Add(cube);
//        //    GameManager.instance.UpdateAiYellowCubePos();
//        //}
       
//    }

//    private void ManageTrail(bool isPicked)
//    {
//        if (isPicked)
//        {
//            transform.GetChild(0).gameObject.SetActive(true);
//        }
//        else
//        {
//            transform.GetChild(0).gameObject.SetActive(false);
//        }
//    }
//    #endregion
//}
