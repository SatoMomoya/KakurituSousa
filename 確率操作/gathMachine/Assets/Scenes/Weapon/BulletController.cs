using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameObject playerObj;
    private Momoya.Player player;
    private int count;
    private bool direction;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Momoya.Player>();
        count = 0;
        direction = player.PlayerRightLeftFlag();
        transform.position = playerObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(direction)
        {
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
        }

        count++;
        if (count > 180)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Monster")
        {
            Destroy(gameObject);
        }
    }

}
