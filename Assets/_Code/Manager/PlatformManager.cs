using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private GameObject blueCube;
    [SerializeField] private GameObject redCube;
    [SerializeField] private GameObject yellowCube;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int PlatformCount;
    private int randomCubeIndex;
    private int totalCubes;
    public GameObject collectables;
    int cubesPerColor;
    [SerializeField] private int columns;
    [SerializeField] private int rows;
    [SerializeField] private float spacingX;
    [SerializeField] private float spacingZ;
    private float startX;
    private float startZ;
    private float totalWidth;
    private float totalHeight;
    private int counter;
    private bool hasPlayerEntered, hasAiRedEntered, hasAiYellowEntered;
    [SerializeField] private int start, end;
    private GameObject cubePrefab;
    private GameObject desriredCube;
    private List<Vector3> cubePosition = new List<Vector3>();
    private Vector3 spawnPosition;
    public GameObject p1, p2, p3,parent;

    private void Start()
    {
        if (PlatformCount == 0)
        {

            SetPosition();
        }
    }
    private void SetPosition()
    {
        columns = 10;
        rows = 5;
        spacingX = 0.5f;
        spacingZ = 1f;
        totalWidth = (columns - 1) * spacingX;
        totalHeight = (rows - 1) * spacingZ;
        startX = -totalWidth / 2f;
        startZ = -totalWidth / 2f;
        totalCubes = rows * columns;
        cubesPerColor = totalCubes / 3;

       

        SpawnCubes();

    }
    private void SpawnCubes()
    {

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {

                float posX = startX + col * spacingX;
                float posZ = startZ + row * spacingZ;
                Vector3 spawnPosition = new Vector3(transform.localPosition.x + posX, transform.localPosition.y + 0.08f, transform.localPosition.z + posZ);
                cubePosition.Add(spawnPosition);
            }
        }
        ShuffleList(cubePosition);

    }
    void ShuffleList(List<Vector3> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            Vector3 temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        InstantiateCube();
    }

    private void InstantiateCube()
    {
        if (PlatformCount >= 1)
        {
            for (int i = 0; i < cubesPerColor; i++)
            {
                if (cubePosition[i] != spawnPosition)
                {
                    spawnPosition = cubePosition[i];
                    GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity, collectables.transform);
                    int layer = cubePrefab.layer;
                    cube.GetComponent<CubeSpawnHandler>().SetParent(parent);
                    cube.GetComponent<CubeSpawnHandler>().SetSpawnPoint(cubePosition[i]);
                    cube.GetComponent<CubeSpawnHandler>().SetInitialLayer(layer);
                    cubePosition[i] = spawnPosition;
                }


            }
        }
        else
        {
            for (int i = 0; i < cubePosition.Count; i++)
            {
                if (i < cubesPerColor)
                {
                    cubePrefab = blueCube;
                }
                else if (i >= cubesPerColor && i < cubesPerColor * 2)
                {
                    cubePrefab = redCube;
                }
                else
                {
                    cubePrefab = yellowCube;
                }
                spawnPosition = cubePosition[i];
                GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity, collectables.transform);
                int layer = cubePrefab.layer;
                cube.GetComponent<CubeSpawnHandler>().SetParent(parent);
                cube.GetComponent<CubeSpawnHandler>().SetSpawnPoint(cubePosition[i]);
                cube.GetComponent<CubeSpawnHandler>().SetInitialLayer(layer);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasPlayerEntered)
        {
            if (PlatformCount >= 1)
            {
               
                cubePrefab = blueCube;
                SetPosition();
                hasPlayerEntered = true;
            }
        }

        else if (collision.gameObject.CompareTag("AIOne") && !hasAiRedEntered)
        {
            if (PlatformCount >= 1)
            {
                
                cubePrefab = redCube;
                SetPosition();
                hasAiRedEntered = true;
            }
        }
        else if (collision.gameObject.CompareTag("AiYellow") && !hasAiYellowEntered)
        {
            if (PlatformCount >= 1)
            {
                cubePrefab = yellowCube;
                SetPosition();
                hasAiYellowEntered = true;
            }
        }
    }
}
