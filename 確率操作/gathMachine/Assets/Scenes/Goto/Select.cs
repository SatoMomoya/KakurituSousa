using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Select : MonoBehaviour
{
    public List<Image> image_List;
    int count;
    float scell;
    bool ibent;
    float time;
    [SerializeField]
    float speed;
    public float max_Size;
    public float min_Size;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        image_List[0].transform.localScale = new Vector3(0, 0, 1);
        image_List[1].transform.localScale = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 7)
        {
            //サイズのリセット
            image_List[0].transform.localScale = new Vector3(1, 1, 1);
            image_List[1].transform.localScale = new Vector3(1, 1, 1);
          //UIの選択

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                
                count += 1;
                if (count >= 2)
                {
                    count = 0;
                }
            }
            //選択したUIのサイズ変更
            scell += speed/120;
            if (scell >= max_Size)
            {
                scell = min_Size;
            }
            image_List[count].transform.localScale = new Vector3(scell, scell, 1);


            //シーン選択のフラグ
            if (count == 0)
            {
                ibent = true;
            }
            if (count == 1)
            {
                ibent = false;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ibent == true)
                {
                    SceneManager.LoadScene("Stagetest");
                }
                else if (ibent == false)
                {
                    SceneManager.LoadScene("SelectScene");
                }
            }
        }
    }
}