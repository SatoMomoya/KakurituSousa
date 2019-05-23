using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Sample : MonoBehaviour
{

    [SerializeField]
    private VisualEffect m_VFX = null;

    float time = 0.0f;
    [SerializeField]
    float span = 5.0f;

    bool _startFlag = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > span && !_startFlag)
        {
            m_VFX.SendEvent("OnStart");
            _startFlag = true;
        }
    }
}
