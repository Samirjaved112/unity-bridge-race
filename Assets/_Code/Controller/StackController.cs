using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StackController : MonoBehaviour
{
    public Transform startingPoint;
    public Transform projectilePoint;
    public bool isCollected;
    public GameObject collidedGameObject;
    public GameObject parent;
    public int count;
    public List<GameObject> cubeList = new List<GameObject>();
    public CollectCubes collectcubes;
    public string cubeTag;
    public Transform target;
    public Vector3 offset;
    [SerializeField] private GameObject playerStackCount;
    [SerializeField] private TextMeshProUGUI stackCountText;
    public void CollectCubes(GameObject cube)
    {
        UpdatePositions();
        collidedGameObject = cube;
        collidedGameObject.GetComponent<CollectCubes>().SetPosition(collidedGameObject.transform.localPosition, projectilePoint.transform.localPosition, startingPoint.transform.localPosition);
        parent = transform.GetChild(0).transform.GetChild(1).gameObject;
        cube.transform.SetParent(parent.transform);
        AddCubes(cube);
        count++;
    }
    public void UpdatePositions()
    {
        projectilePoint.localPosition = new Vector3(0, projectilePoint.localPosition.y+0.57f, projectilePoint.localPosition.z);
        startingPoint.localPosition = new Vector3(0, startingPoint.localPosition.y+0.57f, startingPoint.localPosition.z);
    }
    public void ResetPosition()
    {
        projectilePoint.localPosition = new Vector3(0, projectilePoint.localPosition.y - 0.57f, projectilePoint.localPosition.z);
        startingPoint.localPosition = new Vector3(0, startingPoint.localPosition.y - 0.57f, startingPoint.localPosition.z);
    }
    public void AddCubes(GameObject cube)
    {
        cubeList.Add(cube);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(cubeTag))
        {
            CollectCubes(other.gameObject);
        }
    }
    private void Update()
    {
        Vector3 desPos = target.position + offset;
        playerStackCount.transform.position = desPos;
        stackCountText.text = count.ToString();

    }
   
}
