using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GunController gunController;
    private int count;
    private int direction;
    // Start is called before the first frame update
    void Start()
    {
        gunController = FindObjectOfType<GunController>();
        count = 0;
        direction = gunController.Direction;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        pos.x += direction;
       
        transform.position = pos;

        count++;
        if (count > 180)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

}
