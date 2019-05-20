using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlaySceneGo : MonoBehaviour
{
    bool flag;
    // Start is called before the first frame update
    void Start()
    {

        flag = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (flag)
        {
            SceneManager.LoadScene("SelectScene");
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ball")
        {
            flag = true;
        }

    }
}