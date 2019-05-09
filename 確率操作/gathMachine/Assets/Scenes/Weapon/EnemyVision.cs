using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
   
    private Vector3 playerPos;
    private bool visionFlag;
    // Start is called before the first frame update
    void Start()
    {
        
        visionFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            //視界に入っている間
            visionFlag = true;
            Debug.Log("見えた");
            
            playerPos = other.transform.position;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //視界から出たら
            visionFlag = false;
             
        }
    }

    public bool VisionFlag
    {
        get { return visionFlag; }
        set { visionFlag = value; }
    }

    public Vector3 PlayerPos
    {
        get { return playerPos; }
        set { playerPos = value; }
    }
}
