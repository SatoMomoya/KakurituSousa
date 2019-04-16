using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    private GunController gunController;
    private int count;
    private int direction;
    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        gunController = FindObjectOfType<GunController>();
        count = 0;
        flag = false;
        direction = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool shootFlag = gunController.ShootFlag;

        if(shootFlag)
        {
            if(!flag)
            {
                if(transform.lossyScale.x >= 0)
                {
                    transform.Rotate(new Vector3(0, 0, 45));
                    flag = true;
                    direction = 1;
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, -45));
                    flag = true;
                    direction = 2;
                }
                
            }
           
            
        }


        if(direction == 1)
        {
            if (transform.rotation.z >= 0)
            {
                transform.Rotate(new Vector3(0, 0, transform.rotation.z - 1));
            }
            else
            {
                gunController.ShootFlag = false;
                flag = false;
                direction = 0;
            }
        }

        if (direction == 2)
        {
            if (transform.rotation.z <= 0)
            {
                transform.Rotate(new Vector3(0, 0, transform.rotation.z + 1));
            }
            else
            {
                gunController.ShootFlag = false;
                flag = false;
                direction = 0;
            }
        }


    }
}
