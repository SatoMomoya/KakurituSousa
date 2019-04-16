using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject pivot;
    bool shootFlag;                      //撃つフラグ

    private int direction;      //向き

    // Start is called before the first frame update
    void Start()
    {
        shootFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(!shootFlag)
            {
                GameObject obj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                if(pivot.transform.lossyScale.x >= 0)
                {
                    obj.transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
                    direction = 1;
                }
                else
                {
                    obj.transform.position = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
                    direction = -1;
                }

                shootFlag = true;
            }
        }
    }

    public bool ShootFlag
    {
        get { return shootFlag; }
        set { shootFlag = value; }
    }

    public int Direction
    {
        get { return direction; }
        set { direction = value; }
    }
}
