using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponData : MonoBehaviour
{
    [SerializeField]
    int weponNum;

    public int WeponNumber
    {
        get { return weponNum; }
        set { weponNum = value; }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {

    }
}
