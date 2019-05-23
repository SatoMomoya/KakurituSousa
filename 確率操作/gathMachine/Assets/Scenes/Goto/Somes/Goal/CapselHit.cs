using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapselHit : MonoBehaviour
{
    private bool hitFlag;

    // Start is called before the first frame update
    void Start()
    {
        hitFlag = false;
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
            Debug.Log("ddddsdssdds");
        }
    }

    public bool HitFlag()
    {
        return hitFlag;
    }

}
