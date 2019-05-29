using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField]
    private float changeSpeed = 0.0f;
    [SerializeField]
    private float blindSpeed = 0.0f;

    private float alpha_Sin;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.blindSpeed = changeSpeed;
        }
        alpha_Sin = Mathf.Abs(Mathf.Sin(Time.time * this.blindSpeed));
        ChangeTransparency(alpha_Sin); // 点滅する
    }

    void ChangeTransparency(float alpha)
    {
        Color _color = this.spriteRenderer.material.color;
        _color.a = alpha;
        this.spriteRenderer.color = _color;
    }
}
