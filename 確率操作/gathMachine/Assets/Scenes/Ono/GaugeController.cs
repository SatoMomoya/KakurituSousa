

using Momoya;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GaugeController : MonoBehaviour
{
	// <メンバー変数>
    public Monster m_player;
    private Slider m_slider;

    float time = 0.0f;
    float span = 0.3f;

  
    private void Start()
    {
        m_slider = GetComponent<Slider>();

        m_player.HP = m_player.StartHP;
        Debug.Log("入れる" + m_player.StartHP);
        m_slider.maxValue = m_player.HP;



    }




	private void Update ()
	{
        if (m_player.tag == "Player")
        {
            m_slider.maxValue = m_player.GetComponent<Player>().MaxHP();
            Debug.Log("新たに入れる" + m_player.GetComponent<Player>().MaxHP());
        }
        //time += Time.deltaTime;
        //if(time > span)
        //{
        //    m_player.HP = m_player.HP - 1;
        //    time = 0.0f;
            
        //}
        m_slider.value = m_player.HP;
        Debug.Log("今のHP" + m_player.HP);
    }
}
