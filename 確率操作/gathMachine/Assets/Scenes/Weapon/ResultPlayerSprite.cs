using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPlayerSprite : MonoBehaviour
{
    private GameObject doDesObj;
    private WeponData weponData;

    SpriteRenderer main;

    public Sprite wand;
    public Sprite ax;

    // Start is called before the first frame update
    void Start()
    {
        doDesObj = GameObject.Find("DoDesObj");
        weponData = doDesObj.GetComponent<WeponData>();

        main = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(weponData.WeponNumber == 2)
        {
            main.sprite = wand;
        }
        else if(weponData.WeponNumber == 3)
        {
            main.sprite = ax;
        }
       
    }
}
