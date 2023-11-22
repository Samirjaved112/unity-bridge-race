using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawnHandler : MonoBehaviour
{
    private Vector3 spawnPoint;
    private int cubeLayer;
    [SerializeField] private GameManager gamemanager;
    [SerializeField] private GameObject collectAbles;
    private GameObject parentCube;
    public void SetSpawnPoint(Vector3 point)
    {
        spawnPoint = point;
    }
    public void SetInitialLayer(int layer)
    {
        cubeLayer = layer;
    }
    public void SetParent(GameObject parent)
    {
        parentCube = parent;
    }
    public void SpawnRepeat()
    {
        GameObject newCube = Instantiate(gameObject, spawnPoint,Quaternion.identity,parentCube.transform);
        newCube.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        newCube.layer = cubeLayer;
        newCube.GetComponent<CubeSpawnHandler>().SetSpawnPoint(spawnPoint);
        newCube.GetComponent<CubeSpawnHandler>().SetInitialLayer(cubeLayer);
        newCube.GetComponent<CubeSpawnHandler>().SetParent(parentCube);
    }
}
