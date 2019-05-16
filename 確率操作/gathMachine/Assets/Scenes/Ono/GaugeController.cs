

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

    private void Start()
    {
        m_slider = GetComponent<Slider>();

        //monster.HP = monster.StartHP;
        //Debug.Log("入れる" + monster.StartHP);
    }




	private void Update ()
	{
        Debug.Log(monster);
        m_slider.maxValue = monster.StartHP;
        //m_slider.maxValue = monster.GetComponent<Player>().MaxHP();

        //Debug.Log("新たに入れる" + m_player.GetComponent<Player>().MaxHP());

        //time += Time.deltaTime;
        //if(time > span)
        //{
        //    m_player.HP = m_player.HP - 1;
        //    time = 0.0f;

        //}


        transform.position = new Vector3(monster.transform.position.x, monster.transform.position.y + 0.5f, monster.transform.position.z);
        m_slider.value = monster.HP;

        if(monster.HP <= 0)
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
