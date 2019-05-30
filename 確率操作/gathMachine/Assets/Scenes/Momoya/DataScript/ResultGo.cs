using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultGo : MonoBehaviour
{
    [SerializeField]
    float resultTime;

    int PlayerRaity;
    int conditions;
    public GameDirector changeScene;
    public Momoya.Player player;
    bool a;
    private int time;
    // Start is called before the first frame update
    void Start()
    {

        PlayerRaity = player.Rarity;
        time = 0;
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
        Debug.Log("プレイヤーのレアリティ"+player.Rarity);
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

            time += 1;
            
        }
        else if (a == false)
        {
            SceneManager.LoadScene("TitleCopyScene");
        }

        if (time > 60)
        {
            if(player.HP <= 0)
            {
                Debug.Log("死んで通った");
                //player.Rarity = player.Rarity -1;
                //Debug.Log("プレイヤーのレアリティ==" + player.Rarity);
                //player.Rarity(player.Rarity);
                SceneManager.LoadScene("TitleCopyScene");

            }
            SceneManager.LoadScene("Result");
        }
    }
}
