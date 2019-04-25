using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultGo : MonoBehaviour
{
    [SerializeField]
    float resultTime;
    float time;
    int PlayerRaity;
    int conditions;
    public GameDirector changeScene;
    public Momoya.Player player;
    bool a;
    // Start is called before the first frame update
    void Start()
    {

        PlayerRaity = player.Rarity;
     
        a = false;
    }

    // Update is called once per frame
    void Update()
    {
        // conditions.gameObject.GetComponent<>;
        PlayerRaity = player.Rarity;

        conditions = (int)changeScene.selectGyat;

    }
    //条件を満たしていなければセレクト画面へ
    public void Flag()
    {

        if (PlayerRaity >= conditions)
        {
            a = true;
        }
        else
        {
            a = false;
        }
        if (a == true)
        {
            SceneManager.LoadScene("Result");
        }
        else if (a == false)
        {
            SceneManager.LoadScene("SelectScene");
        }

    }
}
