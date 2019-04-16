using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private EnemyController enemyController;
    private SwordPivotController swordPivotController;
    private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = FindObjectOfType<EnemyController>();
        swordPivotController = FindObjectOfType<SwordPivotController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        bool damageFlag = enemyController.DamageFlag;
        bool attackFlag = swordPivotController.AttackFlag;
        switch (other.transform.tag)
        {
            //攻撃がモンスターに当たった
            case "Monster":
                if(attackFlag)
                {
                    enemyController.DamageFlag = true;
                }
                break;
        }
    }

    public Vector3 PlayerPos
    {
        get { return playerPos; }
        set { playerPos = value; }
    }
}
