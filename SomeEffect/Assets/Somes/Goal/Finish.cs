using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Finish : MonoBehaviour
{
    [SerializeField]
    private GameObject _myObj;
    [SerializeField]
    private GameObject _goalObj;
    [SerializeField, Range(0.0f, 10.0f)]
    private float _time;
    [SerializeField]
    private Color m_startColor = Color.white;
    [SerializeField]
    private Color m_targetColor = Color.white;

    private Vector3 _startPos;
    private Vector3 _endPos;
    private float _startTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
        _startPos = _myObj.transform.position;
        _endPos = _goalObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float timeStep = _time > 0.0f ? (Time.time - _startTime) / _time : 1.0f;

        //transform.position = Vector3.Lerp(m_startPosition, m_targetPosition, timeStep);
        _myObj.transform.position = Lerp(_startPos, _endPos, timeStep, CosLerp);
        CurrentColor = Color.Lerp(m_startColor, m_targetColor, timeStep);
    }
    
    private Color CurrentColor
    {
        set
        {
            var render = GetComponent<Renderer>();
            if (render == null)
            {
                return;
            }

            if (render.material == null)
            {
                return;
            }

            render.material.color = value;

        }
    }

    static Vector3 Lerp(Vector3 startPosition, Vector3 targetPosition, float t, Func<float, float> v)
    {
        Vector3 lerpPosition = Vector3.zero;

        lerpPosition = (1 - v(t)) * startPosition + v(t) * targetPosition;

        return lerpPosition;
    }

    static float Iinearity(float time)
    {
        float vt = 0.0f;

        vt = time;

        return vt;
    }

    static float EaseIn(float time)
    {
        float vt = 0.0f;
        vt = time * time;

        return vt;
    }

    static float EaseOut(float time)
    {
        float y = 0.0f;

        y = time * (2 - time);

        return y;

    }

    static float Cube(float time)
    {
        float y = 0.0f;

        y = time * time * (3 - 2 * time);

        return y;
    }

    static float CosLerp(float time)
    {
        float y = 0.0f;

        y = (1 - Mathf.Cos(time * Mathf.PI)) / 2.0f;

        return y;
    }
}
