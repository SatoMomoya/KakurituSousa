using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SelectIndex : MonoBehaviour
{
    // スプライトの保存
    public List<Sprite> m_spriteList;
    public GameObject m_center;
    public Image[] arrow;
    public float m_rotSpeed;
    // ImageUIの実体保存
    private List<Image> m_imgList = new List<Image>();
    // イメージのプレハブ
    public Image m_preImage;
    // 角度
    private float m_angle;

    public float m_interval;
    private float m_position;

    private int m_index;
    private float m_changeColor;
    private bool m_changeColorFlag;
    int num;
    public GameDirector rarity;
    private bool flag;
    // Use this for initialization
    void Start()
    {
        m_index = 0;
        m_interval = 60;
        m_angle = 0;
        m_position = 0;
        m_changeColor = 1;
        m_changeColorFlag = false;
        SetImage();
        num = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        m_interval++;
        Move();
        //ChangeColors();
        if (Input.GetKey(KeyCode.Space)&&m_interval>=60)
        {
            GetRarity();
            //シーンの移行
            if(rarity.selectGyat== GameDirector.SelectGtyat.tutorial)
            {
                SceneManager.LoadScene("TutorialScene");
            }
            else if (rarity.selectGyat == GameDirector.SelectGtyat.pickUp)
            {
                SceneManager.LoadScene("PickUpScene");
            }
            else
            {
                SceneManager.LoadScene("Stagetest");
            }
           
        }

        if (flag==true&& m_interval <= 60)
        {
            arrow[0].color = new Color(1, 0, 0);
        }
        else
        {
            arrow[0].color = new Color(1, 1, 1);
        }

        if (flag == false && m_interval <= 60)
        {
            arrow[1].color = new Color(1, 0, 0);
        }
        else
        {
            arrow[1].color = new Color(1, 1,1);
        }
        GetRarity();
        Debug.Log(rarity.selectGyat);
    }

    void Move()
    {
        float speed = Time.deltaTime * m_rotSpeed;
        if (Input.GetKeyDown(KeyCode.RightArrow)&& m_interval >=60)
        {
            m_index++;
            num++;
            if (m_index > m_imgList.Count)
            {
                m_index = 0;
                num--;
            }
            m_interval = 0;
            flag = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)&& m_interval>=60)
        {
            m_index--;
            num--;
            if (m_index < 0)
            {
                m_index = m_imgList.Count;
                num++;
            }
            m_interval = 0;
            flag = false;
        }
        if (num >= 5) num = 1;
        if (num <= 0) num = 4;
        m_center.transform.rotation = Quaternion.Slerp(m_center.transform.rotation, Quaternion.Euler(0, m_center.transform.rotation.y + m_angle * -m_index, 0), speed);
    }

    void SetImage()
    {

        // Imageのインスタンス
        for (int i = 0; i < m_spriteList.Count; i++)
        {
            Image tmpImage = Instantiate<Image>(m_preImage, m_center.transform) as Image;
            tmpImage.sprite = m_spriteList[i];
            tmpImage.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);
            m_imgList.Add(tmpImage);
        }

        int j = 0;
        foreach (var img in m_imgList)
        {
            float x = 0; float z = 0;

            x = 1000 * Mathf.Cos((Mathf.PI * 2) / m_imgList.Count * j - Mathf.Deg2Rad * 90);
            z = 1000 * Mathf.Sin((Mathf.PI * 2) / m_imgList.Count * j - Mathf.Deg2Rad * 90);

            m_angle = 360 / m_imgList.Count * j;

            img.transform.localPosition = new Vector3(x, 0, z);
            img.transform.Rotate(0, -m_angle, 0);

            j++;
        }
    }

    //void ChangeColors()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        m_changeColorFlag = true;
    //        SetIndex();
    //    }
    //    if (m_changeColorFlag) m_changeColor -= 0.01f;

    //    foreach (var img in m_imgList)
    //    {
    //        img.color = new Color(1, m_changeColor, m_changeColor);
    //    }

    //    //  if (m_changeColor <= 0) GameDirector.ChangeScene(GameDirector.SceneID.SCENE_GAME);
    //}

    private void SetIndex()
    {
        //GameDirector.SetStageSize((m_index + 1) + 10);
    }
    public int GetRarity()
    {
        switch (num)
        {

            case 1:
                rarity.selectGyat = GameDirector.SelectGtyat.raity5;
                break;
            case 2:
                rarity.selectGyat = GameDirector.SelectGtyat.pickUp;
                break;
            case 3:
                rarity.selectGyat = GameDirector.SelectGtyat.nomal;
                break;
            case 4:
                rarity.selectGyat = GameDirector.SelectGtyat.tutorial;
                break;
        }
        return (int)rarity.selectGyat;
    }
}