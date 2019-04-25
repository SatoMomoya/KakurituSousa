using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public List<GameObject> objectBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    //アイテム指定ドロップ
    public GameObject GiveObjectBox(int objNumber)
    {
        if(objectBox.Count - 1 < objNumber)
        {
            objNumber = objectBox.Count;
        }

        if(objNumber  < 0)
        {
            objNumber = 0;
        }

        return objectBox[objNumber];
    }

    //アイテムランダムドロップ
    public GameObject GiveRandomObjectBox()
    {
        int randNum;
        randNum = Random.Range(0, objectBox.Count - 1);

        return objectBox[randNum];
    }


}
