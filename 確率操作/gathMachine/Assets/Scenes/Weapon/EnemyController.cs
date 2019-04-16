using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Momoya.Monster
{
    private Vector3 playerPos;  //プレイヤーの座標
    public LayerMask layerMask; //レイヤーマスク
    private bool damageFlag;    //ダメージを受けたかどうかのフラグ
    private bool visionFlag;    //視界に入ったかどうかのフラグ
    private bool attackFlag;    //攻撃フラグ
    private EnemyVision enemyVision;
    private EnemyAttackZone enemyAttackZone;
    private SwordController swordController;

    private int knockBackCount;     //ノックバックするカウント
    private int rushAttackCount;    //突進攻撃するカウント
    private int buildUpPowerCount;  //突進前の溜めのカウント

    public int knockBackTime;     //ノックバックする時間
    public int rushAttackTime;    //突進攻撃する時間
    public int buildUpPowerTime;  //突進前の溜めの時間

    public float scale;
    //初期化
    public override void Initialize()
    {
        enemyVision = FindObjectOfType<EnemyVision>();
        enemyAttackZone = FindObjectOfType<EnemyAttackZone>();
        swordController = FindObjectOfType<SwordController>();
        flag.Off((uint)StateFlag.Chase);
        vec.x = 1;
        damageFlag = false;
        visionFlag = false;
        attackFlag = false;
        knockBackCount = 0;
        rushAttackCount = 0;
        buildUpPowerCount = 0;
    }

    //移動
    public override void Move()
    {
        //進む向きで画像を反転する
        transform.localScale = new Vector3(scale * (vec.x / Math.Abs(vec.x)), transform.localScale.y, transform.localScale.z);

        if (visionFlag)
        {
            flag.On((uint)StateFlag.Chase);
            playerPos = enemyVision.PlayerPos;
        }
        else
        {
            flag.Off((uint)StateFlag.Chase);
        }

        if (!damageFlag)
        {
            if (!attackFlag)
            {
                if (flag.Is((uint)StateFlag.Chase))
                {
                    Chase();
                }
                else
                {
                    Loiter();
                }
            }
            else
            {
                RushAttack();
            }

        }
        else
        {
            playerPos = swordController.PlayerPos;
            KnockBack();

        }

        Debug.Log((vec.x));
        Debug.DrawLine(new Vector3(transform.position.x + 1, transform.position.y, 0), new Vector3(transform.position.x + 1, transform.position.y - 2, 0), Color.red);
        Debug.DrawLine(new Vector3(transform.position.x - 1, transform.position.y, 0), new Vector3(transform.position.x - 1, transform.position.y - 2, 0), Color.red);
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(transform.position.x - 2, transform.position.y, 0), Color.red);
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(transform.position.x + 2, transform.position.y, 0), Color.red);

    }

    //追いかける
    private void Chase()
    {
        if (transform.position.x > playerPos.x)
        {
            vec.x = -2;
        }

        if (transform.position.x < playerPos.x)
        {
            vec.x = 2;
        }
    }

    //徘徊
    private void Loiter()
    {
        //右の地面判定
        if (!Physics.Linecast(new Vector3(transform.position.x + 1, transform.position.y, 0), new Vector3(transform.position.x + 1, transform.position.y - 1, 0), layerMask))
        {
            vec.x = -1;
        }
        //左の地面判定
        if (!Physics.Linecast(new Vector3(transform.position.x - 1, transform.position.y, 0), new Vector3(transform.position.x - 1, transform.position.y - 1, 0), layerMask))
        {
            vec.x = 1;
        }
        //右の地面判定
        if (Physics.Linecast(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(transform.position.x + 1, transform.position.y, 0), layerMask))
        {
            vec.x = -1;
        }
        //左の地面判定
        if (Physics.Linecast(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(transform.position.x - 1, transform.position.y, 0), layerMask))
        {
            vec.x = 1;
        }
    }

    //ノックバック
    private void KnockBack()
    {
        knockBackCount++;
        if (transform.position.x > playerPos.x)
        {
            vec.x = -5;
        }
        else
        {
            vec.x = 5;
        }

        //画像の向きを合わせる
        transform.localScale = new Vector3(-(scale * (vec.x / Math.Abs(vec.x))), transform.localScale.y, transform.localScale.z);

        //0.5フレームノックバックする
        if (knockBackCount > knockBackTime)
        {
            vec.x = 1;
            attackFlag = false;
            damageFlag = false;
            knockBackCount = 0;
            rushAttackCount = 0;
            buildUpPowerCount = 0;
        }

    }

    //突進攻撃
    private void RushAttack()
    {
        playerPos = enemyAttackZone.PlayerPos;

        buildUpPowerCount++;
        //力をためる（後ろに下がる）
        if (buildUpPowerCount < buildUpPowerTime)
        {
            if (transform.position.x > playerPos.x)
            {
                vec.x = 5;
                transform.localScale = new Vector3(-(scale * (vec.x / Math.Abs(vec.x))), transform.localScale.y, transform.localScale.z);
            }

            if (transform.position.x < playerPos.x)
            {
                vec.x = -5;
                transform.localScale = new Vector3(-(scale * (vec.x / Math.Abs(vec.x))), transform.localScale.y, transform.localScale.z);
            }

        }

        //突進する
        if (buildUpPowerCount > buildUpPowerTime)
        {
            rushAttackCount++;
            if (transform.position.x > playerPos.x)
            {
                vec.x = -10;
            }

            if (transform.position.x < playerPos.x)
            {
                vec.x = 10;
            }
        }

        //終了
        if (rushAttackCount > rushAttackTime)
        {
            buildUpPowerCount = 0;
            rushAttackCount = 0;
            attackFlag = false;
            if (vec.x >= 0)
            {
                vec.x = 1;
            }
            else
            {
                vec.x = -1;
            }
        }
    }

    public bool DamageFlag
    {
        get { return damageFlag; }
        set { damageFlag = value; }
    }
    public bool VisionFlag
    {
        get { return visionFlag; }
        set { visionFlag = value; }
    }
    public bool AttackFlag
    {
        get { return attackFlag; }
        set { attackFlag = value; }
    }
}
