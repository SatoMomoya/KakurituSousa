using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apper : MonoBehaviour
{
    [SerializeField]
    float apperTime; //消滅までの時間
    float time;
    
    Color color;
    // Start is called before the first frame update
    void Start()
    {

        color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > apperTime)
        {
            gameObject.SetActive(true);
            color.a += Time.deltaTime;
            color.r += Time.deltaTime;
            color.g += Time.deltaTime;
            color.b += Time.deltaTime;
        }
        transform.GetComponent<SpriteRenderer>().color = color;
    }
}
