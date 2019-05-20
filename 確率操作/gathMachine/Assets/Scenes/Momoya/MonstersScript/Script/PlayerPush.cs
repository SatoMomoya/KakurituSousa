using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    [SerializeField]
    float pushpow = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
         
            collision.transform.GetComponent<Rigidbody>().AddForce(transform.right * pushpow);
        }
    }
}
