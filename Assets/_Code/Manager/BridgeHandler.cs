using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeHandler : MonoBehaviour
{
    public static BridgeHandler instance;
    [SerializeField]
    private MeshRenderer stairsMesh;
    public int count;
    public int prevCount;
    public Material playerMaterial;
    public Material aiRedMaterial;
    public Material aiYellowMaterial;
    private PlayerMovementController playerController;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (gameObject.CompareTag("StairBlue"))
    //    {
    //        stairsMesh = GetComponent<MeshRenderer>();
    //        if (stairsMesh.enabled)
    //        {
    //            return;
    //        }
    //        else if (GameManager.instance.cubeList.Count != 0)
    //        {
    //            stairsMesh.enabled = true;
    //            //stairsMesh.material = gameObject.GetComponent<MeshRenderer>().material;
    //            //other.gameObject.GetComponent<Collider>().enabled = false;
    //            Destroy(GameManager.instance.cubeList[GameManager.instance.cubeList.Count - 1]);
    //            GameManager.instance.cubeList.RemoveAt(GameManager.instance.cubeList.Count - 1);
    //            GameManager.instance.DescreasePosition();
    //        } 
    //    }
    //    else if (gameObject.CompareTag("StairRed"))
    //    {
    //        Debug.Log("wwwww");
    //        stairsMesh = GetComponent<MeshRenderer>();
    //        if (stairsMesh.enabled)
    //        {
    //            return;
    //        }
    //        else if (GameManager.instance.cubeListAiRed.Count != 0)
    //        {
    //            stairsMesh.enabled = true;
    //            //stairsMesh.material = gameObject.GetComponent<MeshRenderer>().material;
    //            //other.gameObject.GetComponent<Collider>().enabled = false;
    //            Destroy(GameManager.instance.cubeListAiRed[GameManager.instance.cubeListAiRed.Count - 1]);
    //            GameManager.instance.cubeListAiRed.RemoveAt(GameManager.instance.cubeListAiRed.Count - 1);
    //            GameManager.instance.DescreasePosition();
    //        }
    //    }
    //}
     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StackController stackcontroller = other.gameObject.GetComponent<StackController>();
            stairsMesh = GetComponent<MeshRenderer>();
            Color desireColor = new Color(0, 0.7134724f, 1f, 1f);
            if (stairsMesh.enabled && gameObject.CompareTag("StairBlue") && stackcontroller.count != 0)
            {
                return;
            }
            else if (stackcontroller.count != 0)
            {
               
                //stairsMesh.material = playerMaterial;
                stairsMesh.material = other.gameObject.transform.GetComponent<PlayerMovementController>().playerMesh.material;
                stairsMesh.enabled = true;
                //stairsMesh.material = gameObject.GetComponent<MeshRenderer>().material;
                //other.gameObject.GetComponent<Collider>().enabled = false;
                gameObject.transform.tag = "StairBlue";
                stackcontroller.count--;
                stackcontroller.cubeList[stackcontroller.cubeList.Count - 1].GetComponent<CubeSpawnHandler>().SpawnRepeat();
                Destroy(stackcontroller.cubeList[stackcontroller.cubeList.Count - 1]);
                stackcontroller.cubeList.RemoveAt(stackcontroller.cubeList.Count - 1);
                stackcontroller.ResetPosition();
                //GameManager.instance.DescreasePosition();
            }

        }
        else if (other.gameObject.CompareTag("AIOne"))
        {
            StackController stackcontroller = other.gameObject.GetComponent<StackController>();
            stairsMesh = GetComponent<MeshRenderer>();
            if (stairsMesh.enabled && gameObject.CompareTag("StairRed") && stackcontroller.count != 0)
            {
                return;
            }
            else if (stackcontroller.cubeList.Count != 0)
            {
               
                stairsMesh.material = other.gameObject.transform.GetComponent<CharacterAiBehavior>().aiMesh.material;
                stairsMesh.enabled = true;
                //stairsMesh.material = gameObject.GetComponent<MeshRenderer>().material;
                //other.gameObject.GetComponent<Collider>().enabled = false;
              //  gameObject.transform.tag = "StairRed";
                stackcontroller.count--;
                stackcontroller.cubeList[stackcontroller.cubeList.Count - 1].GetComponent<CubeSpawnHandler>().SpawnRepeat();
                Destroy(stackcontroller.cubeList[stackcontroller.cubeList.Count - 1]);
                stackcontroller.cubeList.RemoveAt(stackcontroller.cubeList.Count - 1);
                stackcontroller.ResetPosition();
                gameObject.tag = "StairRed";
            }
        }
        else if (other.gameObject.CompareTag("AiYellow"))
        {
            StackController stackcontroller = other.gameObject.GetComponent<StackController>();
            stairsMesh = GetComponent<MeshRenderer>();
            if (stairsMesh.enabled && gameObject.CompareTag("StairYellow") && stackcontroller.count != 0)
            {
              
                return;
            }
            else if (stackcontroller.cubeList.Count != 0)
            {
               
                stairsMesh.material = other.gameObject.transform.GetComponent<CharacterAiBehavior>().aiMesh.material;
                stairsMesh.enabled = true;
                //stairsMesh.material = gameObject.GetComponent<MeshRenderer>().material;
                //other.gameObject.GetComponent<Collider>().enabled = false;
               // gameObject.transform.tag = "StairYellow";
                stackcontroller.count--;
                stackcontroller.cubeList[stackcontroller.cubeList.Count - 1].GetComponent<CubeSpawnHandler>().SpawnRepeat();
                Destroy(stackcontroller.cubeList[stackcontroller.cubeList.Count - 1]);
                stackcontroller.cubeList.RemoveAt(stackcontroller.cubeList.Count - 1);
                stackcontroller.ResetPosition();
                gameObject.tag = "StairYellow";
            }
        }
    }

}
