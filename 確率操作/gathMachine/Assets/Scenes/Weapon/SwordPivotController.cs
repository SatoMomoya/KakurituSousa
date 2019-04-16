using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPivotController : MonoBehaviour
{
    private bool attackFlag;
    private bool reattackFlag;
    // Start is called before the first frame update
    void Start()
    {
        attackFlag = false;
        reattackFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(!attackFlag && !reattackFlag)
            {
                if (transform.rotation.z >= -0.1f || transform.rotation.z <= 0.1f)
                {
                    attackFlag = true;
                    reattackFlag = false;
                }
            }
            
        }


        if (!attackFlag)
        {
            if(reattackFlag)
            {
                //右
                if (transform.lossyScale.x >= 0)
                {
                    if (transform.rotation.z <= 0)
                    {
                        transform.Rotate(new Vector3(0, 0, transform.rotation.z + 9));
                    }
                    
                    else
                    {
                        reattackFlag = false;
                    }
                }
                else
                {
                    if (transform.rotation.z >= 0)
                    {
                        transform.Rotate(new Vector3(0, 0, transform.rotation.z + 9));
                    }
                    else
                    {
                        reattackFlag = false;
                    }
                }
            }
            
        }


        if (attackFlag)
        {
            if(transform.lossyScale.x >= 0)
            {
                if (transform.localRotation.z > -0.9f)
                {
                    transform.Rotate(new Vector3(0, 0, transform.rotation.z - 9));
                }
                else
                {
                    attackFlag = false;
                    reattackFlag = true;
                }
            }
            else
            {
                if (transform.localRotation.z > -0.9f)
                {
                    transform.Rotate(new Vector3(0, 0, transform.rotation.z - 9));
                }
                else
                {
                    attackFlag = false;
                    reattackFlag = true;
                }
            }
        }

    }
    public bool AttackFlag
    {
        get { return attackFlag; }
        set { attackFlag = value; }
    }
}
