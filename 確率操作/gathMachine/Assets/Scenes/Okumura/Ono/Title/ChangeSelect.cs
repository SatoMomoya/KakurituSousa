using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSelect : MonoBehaviour {
    //public enum SelectGtyat
    //{
    //    none,
    //    tutorial,
    //    nomal,
    //    pickUp,
    //    raity5 = 5,

    //}
    int num;
    public GameDirector rarity;
    // Use this for initialization
    void Start () {
        num = 0;
    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.LeftArrow)) num--;
        if (Input.GetKeyDown(KeyCode.RightArrow)) num++;
        if (num >= 5) num = 1;
        if (num <= 0) num = 4;

      
     
        Debug.Log(rarity.selectGyat);
    }
    public int GetRarity()
    {
        switch (num)
        {

            case 1:
                rarity.selectGyat = GameDirector.SelectGtyat.tutorial;
                break;
            case 2:
                rarity.selectGyat = GameDirector.SelectGtyat.nomal;
                break;
            case 3:
                rarity.selectGyat = GameDirector.SelectGtyat.pickUp;
                break;
            case 4:
                rarity.selectGyat = GameDirector.SelectGtyat.raity5;
                break;
        }
        return (int)rarity.selectGyat;
    }
}
