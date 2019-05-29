using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CapselHit : MonoBehaviour
{
    private bool hitFlag;
    private GameObject player;
    private Momoya.Player playerSc;
  
   
    // Start is called before the first frame update
    void Start()
    {
        
        hitFlag = false;
        player = GameObject.Find("Player");
        playerSc = player.GetComponent<Momoya.Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "GoalCapsel" || other.transform.tag == "GoalCapsel2")
        {
            hitFlag = true;
            
        }

        

        

    }

    public bool HitFlag()
    {
        return hitFlag;
    }

}
