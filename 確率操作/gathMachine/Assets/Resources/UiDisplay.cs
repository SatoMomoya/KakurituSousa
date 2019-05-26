using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiDisplay : MonoBehaviour
{
    public Sprite m_sprite;
    public Image m_preImage;
    private Image m_img;
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        SetImage();
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == false)
        {
            m_img.transform.localScale = new Vector3(0, 0, 0);

        }
        else if (flag == true)
        {
            Debug.Log("当たってる");
            m_img.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
    }

    void SetImage()
    {
        m_img = Instantiate<Image>(m_preImage);
        m_img.sprite = m_sprite;
        m_img.transform.parent = this.transform;
        m_img.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        m_img.transform.localPosition = new Vector3(0, 2, 0);
    }
    private void OnTriggerEnter(Collider collision)
    {
        // 物体がトリガーに接触しとき、１度だけ呼ばれる
        if (collision.transform.tag == "Player")
        {
            flag = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        // 物体がトリガーと離れたとき、１度だけ呼ばれる
        if (collision.transform.tag == "Player")
        {
            flag = false;
        }
    }
}