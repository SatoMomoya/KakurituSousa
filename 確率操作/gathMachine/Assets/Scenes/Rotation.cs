using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public List<GameObject> ball_List;
    public GameObject ball;
    private List<Vector3> vec;
    public GameObject m_center;
    public GameObject m_exit;
   
    public float m_rotSpeed;
    private float m_angle;
    bool flag;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        foreach (var ball in ball_List)
        {
            ball.transform.parent = m_center.transform;
        }
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        m_angle += m_rotSpeed;

      
      
       m_center.transform.localRotation = Quaternion.Slerp(m_center.transform.rotation, Quaternion.Euler(m_center.transform.rotation.x + m_angle, 0, 0), 1);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ball_List[0].transform.parent = null;
            ball_List[0].transform.parent = m_exit.transform;
            ball_List[0].transform.position = new Vector3(100, 0, 0);
            ball.transform.position = m_exit.transform.position;
            flag = true;
        }
        if(flag==true)
        {
            m_exit.transform.position = new Vector3(m_exit.transform.position.x, m_exit.transform.position.y, m_exit.transform.position.z - 0.05f);
        }
    }

 

    private GameObject Instantiate<T>(List<T> ball_List, Transform transform)
    {
        throw new NotImplementedException();
    }
}
