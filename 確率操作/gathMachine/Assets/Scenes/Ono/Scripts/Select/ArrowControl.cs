using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{

    private float m_a;
    // public GameDirector.Direction m_dir;
    // Use this for initialization
    void Start()
    {
        m_a = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_a += 0.2f;

        //    if (m_dir == GameDirector.Direction.RIGHT)
        //        transform.position = new Vector3(transform.position.x + Mathf.Sin(m_a) / 2, transform.position.y, transform.position.z);

        //    if(m_dir == GameDirector.Direction.LEFT)
        //        transform.position = new Vector3(transform.position.x - Mathf.Sin(m_a) / 2, transform.position.y, transform.position.z);
        //}
    }
}
