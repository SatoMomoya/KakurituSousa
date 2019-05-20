using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Manu : MonoBehaviour
{
    public List<Image> m_imgList;
    private bool m_flag;
    public float scale;
    public int max;
    public int min;
    private float count;
    public float intval;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        m_imgList[0].transform.localScale = new Vector3(0, 0, 0);
        m_imgList[1].transform.localScale = new Vector3(0, 0, 0);
        m_flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        imag();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            changeScene();
        }
    }



    void imag()
    {
        count++;
        if (count >= intval*60)
        {

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                m_flag = true;
            }


            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                m_flag = false;
            }

            scale += speed/60;

            if (scale >= max)
            {
                scale = min;
            }
            if (m_flag == true)
            {
                m_imgList[0].transform.localScale = new Vector3(scale, scale, 0);
            }
            else
            {
                m_imgList[0].transform.localScale = new Vector3(1, 1, 1);
            }
            if (m_flag == false)
            {
                m_imgList[1].transform.localScale = new Vector3(scale, scale, 0);
            }
            else
            {
                m_imgList[1].transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void changeScene()
    {
        if (m_flag == true)
        {
            SceneManager.LoadScene("SelectScene");
        }

        if (m_flag == false)
        {
            SceneManager.LoadScene("Stagetest");
        }
    }

}
