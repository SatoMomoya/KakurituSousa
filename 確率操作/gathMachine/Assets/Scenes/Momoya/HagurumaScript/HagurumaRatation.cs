using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HagurumaRatation : MonoBehaviour
{
    //回転タイプ
    enum RotationType
    {
        Left, //左
        Right,//右
    }

    [SerializeField]
    RotationType rotationType; //回転タイプ
    
    private float angle;       //角度
    [SerializeField]
    private float angleSpeed = 1.0f; //角度を足すスピード
    // Start is called before the first frame update
    void Start()
    {
        angle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch(rotationType)
        {
            case RotationType.Left:angle -= angleSpeed; break;
            case RotationType.Right: angle += angleSpeed; break;
        }

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
