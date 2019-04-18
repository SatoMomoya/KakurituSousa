using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momoya
{
    public class bat :  Enemy
    {


        public override void Move()
        {
            //vec.x-=0.01f;
            
            // Finish();
            //Debug.Log("コウモリ  " + rarity);
            ////vec.x = -1.0f;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.tag == "Player")
            {
                Finish();
            }
        }

    }

};