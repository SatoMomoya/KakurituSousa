﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateStar : MonoBehaviour
{
    [SerializeField]
    int maxRarity = 5;

    private Momoya.Player player;
    public Momoya.SavePlayer load;
    public GameObject star001;
    public GameObject star002;
    private GameObject playerObj;
    // public Text text;

    [SerializeField]
    float width;
    bool geneFlag;

    float createtime = 3.0f;
    float time;
    int a;

    private GameObject doDesObj;
    private WeponData weponData;

    // Start is called before the first frame update
    void Start()
    {
        geneFlag = false;
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Momoya.Player>();

        doDesObj = GameObject.Find("DoDesObj");
        weponData = doDesObj.GetComponent<WeponData>();
    }

    // Update is called once per frame
    void Update()
    {
        //ロードされたら星の生成を始める
        //if(load.LoadFlag())
        //{
            time += Time.deltaTime;
            if (geneFlag)
            {
                return;
            }
            if (time > createtime)
            {

                for (int i = 0; i < maxRarity; i++)
                {
                    if (i < player.Rarity)
                    {
                        GameObject go = Instantiate(star001) as GameObject;
                        go.transform.position = new Vector3(transform.position.x + (i * width), transform.position.y, transform.position.z);

                    }
                    else
                    {
                        GameObject go = Instantiate(star002) as GameObject;
                        go.transform.position = new Vector3(transform.position.x + (i * width), transform.position.y, transform.position.z);

                    }
                    Vector2 a = new Vector2(0f, 0f);
                 
                }
                geneFlag = true;
           
            }
        //   text.text = player.Rarity.ToString();
        // }

        a = weponData.WeponNumber;
        //Debug.Log(a);
    }
}
