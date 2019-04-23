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
    public GameDirector gameDirector;
    public Momoya.Player player;
    bool a;
    // Start is called before the first frame update
    void Start()
    {

        PlayerRaity = player.StartRarity;
        conditions = gameDirector.GetRarity();
        a = false;
    }

    // Update is called once per frame
    void Update()
    {
        // conditions.gameObject.GetComponent<>;



       
    }
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
