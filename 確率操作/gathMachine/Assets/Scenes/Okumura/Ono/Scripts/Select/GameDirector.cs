using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameDirector : MonoBehaviour {
    //クリア条件
    public enum SelectGtyat
    {

        tutorial,
        nomal,
        pickUp,
        raity5 = 5,
        none,
    }
  
   public static SelectGtyat rarity;
    // Use this for initialization
    void Start () {
        rarity = SelectGtyat.none;
      
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update () {



      
    }

 public   SelectGtyat selectGyat
    {
        get { return rarity; }
        set { rarity = value; }
    }
}
