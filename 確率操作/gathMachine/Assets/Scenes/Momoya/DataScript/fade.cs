using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    Color color;
    public Momoya.Player player;
    // Start is called before the first frame update
    void Start()
    {
        color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        this.GetComponent<SpriteRenderer>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GoalFlag())
        {
            color.r += Time.deltaTime;
            color.g += Time.deltaTime;
            color.b += Time.deltaTime;
            color.a += Time.deltaTime;
            this.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
