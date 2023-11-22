using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeHandlerAi : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer stairsMesh;
    public int count;
    public Material aiStairMaterial;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (gameObject.CompareTag("StairRed"))
    //    {
    //        Debug.Log("triggered");
    //        stairsMesh = GetComponent<MeshRenderer>();
    //        if (stairsMesh.enabled)
    //        {
    //            return;
    //        }
    //        else if (GameManager.instance.cubeList.Count != 0)
    //        {
    //            stairsMesh.enabled = true;
    //            stairsMesh.material = other.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material;
    //            //gameObject.GetComponent<Collider>().enabled = false;
    //            Destroy(GameManager.instance.cubeListAiRed[GameManager.instance.cubeList.Count - 1]);
    //            GameManager.instance.cubeListAiRed.RemoveAt(GameManager.instance.cubeList.Count - 1);
    //            GameManager.instance.DescreaseAiCubePos();
    //        }

    //    }
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    if (gameObject.CompareTag("StairRed"))
    //    {
            
    //        stairsMesh = GetComponent<MeshRenderer>();
    //        if (stairsMesh.enabled)
    //        {
    //            return;
    //        }
    //        else if (GameManager.instance.cubeList.Count != 0)
    //        {
    //            stairsMesh.enabled = true;
    //            stairsMesh.material = other.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material;
    //            //gameObject.GetComponent<Collider>().enabled = false;
    //            Destroy(GameManager.instance.cubeListAiRed[GameManager.instance.cubeList.Count - 1]);
    //            GameManager.instance.cubeListAiRed.RemoveAt(GameManager.instance.cubeList.Count - 1);
    //            GameManager.instance.DescreaseAiCubePos();
    //        }

    //    }
    //}
}
