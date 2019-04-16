using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momoya
{
    public class bat :  Monster
    {
       

        public override void Move()
        {
            Debug.Log("コウモリ  " + rarity);
            //vec.x = -1.0f;
        }

    }

};