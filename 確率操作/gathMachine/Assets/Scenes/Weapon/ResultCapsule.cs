using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultCapsule : MonoBehaviour
{
    private GameObject capsule;
    private int count;
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(flag)
        {
            count++;
        }

        if(count > 60)
        {
            SceneManager.LoadScene("Result");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "GoalCapsel")
        {
            flag = true;
        }

    }

}
