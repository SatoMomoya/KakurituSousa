using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameDirector : MonoBehaviour {
    //クリア条件
    public enum SelectGtyat
    {
        none,
        tutorial,
        nomal,
        pickUp,
        raity5 = 5,

    }
    int num;
    SelectGtyat rarity;
    // Use this for initialization
    void Start () {
        rarity = SelectGtyat.none;
        num = 0;                 
    }

    // Update is called once per frame
    void Update () {
       
        if (Input.GetKeyDown(KeyCode.LeftArrow)) num--;
        if (Input.GetKeyDown(KeyCode.RightArrow)) num++;
        if (num >= 5) num = 1;
        if (num <= 0) num = 4;
     
        if (Input.GetKey(KeyCode.Space))
        {
            //シーンの移行
            SceneManager.LoadScene("Stagetest");
        }
     
    }
   public int GetRarity()
    {
        switch (num)
        {

            case 1:
                rarity = SelectGtyat.tutorial;
                break;
            case 2:
                rarity = SelectGtyat.nomal;
                break;
            case 3:
                rarity = SelectGtyat.pickUp;
                break;
            case 4:
                rarity = SelectGtyat.raity5;
                break;
        }
        return (int)rarity;
    }
}
