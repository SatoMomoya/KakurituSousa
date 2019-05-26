using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Goto
{
    public class Damage : MonoBehaviour
    {
        private int m_time;
        public int INTERVAL = 2;
        private Color m_defoColor;

        private bool m_damageFlag;
        // Start is called before the first frame update
        void Start()
        {
            m_time = 0;
            m_defoColor = gameObject.GetComponent<Renderer>().material.color;
            m_damageFlag = false;
        }

        // Update is called once per frame
        void Update()
        {

            if(m_damageFlag)
            {
                m_time++;
                gameObject.GetComponent<Renderer>().material.color = m_time % INTERVAL == 0 ? Color.red : m_defoColor;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = m_defoColor;
            }
            
        }

        public bool DamageFlag
        {
            get { return m_damageFlag; }
            set { m_damageFlag = value; }
        }

    }
}
