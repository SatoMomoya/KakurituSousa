using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteTime : MonoBehaviour
{
    [SerializeField]
    float dethTime; //消滅までの時間
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > dethTime)
        {
            Destroy(this.gameObject);
        }
        
    }
}
