using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResultGo : MonoBehaviour
{
    [SerializeField]
    float resultTime;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > resultTime)
        {
            SceneManager.LoadScene("Result");
        }
    }
}
