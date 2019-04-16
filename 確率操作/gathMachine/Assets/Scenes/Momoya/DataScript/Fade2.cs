using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Fade2 : MonoBehaviour
{
    Color color;

    [SerializeField]
    float flashTime; //白くなる時間
    [SerializeField]
    float blackTime; //黒くなる時間
    [SerializeField]
    float finaltime;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        //透明に設定
        transform.GetComponent<SpriteRenderer>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > flashTime && time < blackTime)
        {
            color.a += Time.deltaTime;
            color.r += Time.deltaTime;
            color.g += Time.deltaTime;
            color.b += Time.deltaTime;
        }

        if(time > blackTime)
        {
            color.a -= Time.deltaTime;
            color.r -= Time.deltaTime;
            color.g -= Time.deltaTime;
            color.b -= Time.deltaTime;
        }

        if(time > finaltime)
        {
            SceneManager.LoadScene("teramotoTeast");
        }


        transform.GetComponent<SpriteRenderer>().color = color;
    }

}
