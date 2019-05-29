

using Momoya;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GaugeController : MonoBehaviour
{
	// <メンバー変数>
    private Monster monster;
    private Slider m_slider;

    float time = 0.0f;
    float span = 0.3f;

    void Start()
    {
        m_slider = GetComponent<Slider>();

        //monster.HP = monster.StartHP;
        //Debug.Log("入れる" + monster.StartHP);
    }




	void Update ()
	{
        if(monster)
        {
            transform.position = new Vector3(monster.transform.position.x, monster.transform.position.y + 0.8f, monster.transform.position.z);

            if (monster.transform.tag == "Player")
            {
                transform.localPosition = new Vector3(-270.0f, 275.0f, 0.0f);
                transform.localScale = new Vector3(3, 2, 2);
            }

            m_slider.maxValue = monster.StartHP;

            m_slider.value = monster.HP;

        }
        else
        {
            Destroy(gameObject);
        }
        // Debug.Log("今のHP" + m_player.HP);
    }

    public void SetMonster(Momoya.Monster mon)
    {
        monster = mon;
    }
    
}
