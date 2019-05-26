using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Momoya.Enemy
{
    private Vector3 playerPos;  //プレイヤーの座標
    private bool hitFlag;    //ダメージを受けたかどうかのフラグ
    private bool visionFlag;    //視界に入ったかどうかのフラグ
    private bool attackFlag;    //攻撃フラグ
    //private EnemyVision enemyVision;
    private EnemyAttackZone enemyAttackZone;
    private SwordController swordController;

    private int knockBackCount;     //ノックバックするカウント
    private int rushAttackCount;    //突進攻撃するカウント
    private int buildUpPowerCount;  //突進前の溜めのカウント

    private int knockBackTime;     //ノックバックする時間

    public int rushAttackTime;    //突進攻撃する時間
    public int buildUpPowerTime;  //突進前の溜めの時間

    public float floorDistanceX;

    private bool lastGroundFlag;

    private Momoya.Player player;
    private GameObject playerObj;

    private bool damageFlag;
    private Goto.Damage damageSc;

    //float count;
    //初期化
    public override void Initialize()
    {
        //enemyVision = FindObjectOfType<EnemyVision>();
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Momoya.Player>();
        enemyAttackZone = FindObjectOfType<EnemyAttackZone>();
        swordController = FindObjectOfType<SwordController>();
        flag.Off((uint)StateFlag.Chase);
        damageSc = GetComponent<Goto.Damage>();
        vec.x = 1;
        hitFlag = false;
        visionFlag = false;
        attackFlag = false;
        knockBackCount = 0;
        rushAttackCount = 0;
        buildUpPowerCount = 0;
        lastGroundFlag = false;
        
        knockBackTime = 15;
        damageFlag = false;
        rarity = startRarity;
    }

    //移動
    public override void Move()
    {
        //進む向きで画像を反転する
        if (vec.x > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        if (visionFlag)
        {
            flag.On((uint)StateFlag.Chase);
            //playerPos = enemyVision.PlayerPos;
        }
        else
        {
            flag.Off((uint)StateFlag.Chase);
        }

        if (!hitFlag)
        {
            if (!attackFlag)
            {
                if (flag.Is((uint)StateFlag.Chase))
                {
                    //追いかける
                    Chase();
                }
                else
                {
                    //徘徊
                    Loiter();
                }
            }
        }
        else
        {
            //ノックバック
            KnockBack();

        }

        //当たってなくて攻撃フラグが立っていたら攻撃する
        if(!hitFlag)
        {
            if (attackFlag)
            {
                RushAttack();
            }
        }

        //壁に引っかかった時
        CatchOnWall();

        //攻撃を受けたらHPを減らす
        if(hitFlag)
        {
            
            if (damageFlag)
            {
                damageSc.DamageFlag = true;
                float damege = Damege(player, this);
                status.hp = status.hp - (int)damege;
                damageFlag = false;
                Debug.Log("HP=" + status.hp);
            }
        }
        
        //HPが0以下になったら消える
        if (status.hp <= 0)
        {
            Finish();
        }

        Debug.DrawLine(new Vector3(transform.position.x + floorDistanceX, transform.position.y, 0), new Vector3(transform.position.x + floorDistanceX + 0.4f, transform.position.y - 1, 0), Color.red);
        Debug.DrawLine(new Vector3(transform.position.x - floorDistanceX, transform.position.y, 0), new Vector3(transform.position.x - floorDistanceX - 0.4f, transform.position.y - 1, 0), Color.red);
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(transform.position.x - 0.6f, transform.position.y, 0), Color.red);
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(transform.position.x + 0.6f, transform.position.y, 0), Color.red);

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
        if (!Physics.Linecast(new Vector3(transform.position.x + floorDistanceX, transform.position.y, 0), new Vector3(transform.position.x + floorDistanceX +0.4f, transform.position.y - 1, 0), layerMask))
        {
            vec.x = -1;  
        }
        //左の地面判定
        if (!Physics.Linecast(new Vector3(transform.position.x - floorDistanceX, transform.position.y, 0), new Vector3(transform.position.x - floorDistanceX -0.4f, transform.position.y - 1, 0), layerMask))
        {
            vec.x = 1;
        }
        //右の壁判定
        if (Physics.Linecast(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(transform.position.x + 0.6f, transform.position.y, 0), layerMask))
        {
            vec.x = -1;
        }
        //左の壁判定
        if (Physics.Linecast(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(transform.position.x - 0.6f, transform.position.y, 0), layerMask))
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
            vec.x = 5;
            this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

        }
        else
        {
            vec.x = -5;
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

        }

       
        //0.5フレームノックバックする
        if (knockBackCount > knockBackTime)
        {
            damageSc.DamageFlag = false;
            attackFlag = false;
            
            knockBackCount = 0;
            rushAttackCount = 0;
            buildUpPowerCount = 0;

            if(vec.x >= 0)
            {
                vec.x = 1;
            }
            else
            {
                vec.x = -1;
            }

            hitFlag = false;
        }

    }

    //突進攻撃
    private void RushAttack()
    {
        playerPos = playerObj.transform.position;

        buildUpPowerCount++;
        //力をためる（後ろに下がる）
        if (buildUpPowerCount < buildUpPowerTime)
        {
            if (transform.position.x > playerPos.x)
            {
                vec.x = 5;
                this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            }
            else
            {
                vec.x = -5;
                this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
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

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            hitFlag = true;
            playerPos = collision.transform.position;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "SwordCollision")
        {
            hitFlag = true;
            playerPos = other.transform.position;
            damageFlag = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //視界に入っている間
            visionFlag = true;
            playerPos = other.transform.position;
            attackFlag = true;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //視界から出たら
            visionFlag = false;
        }
    }

    public bool HitFlag
    {
        get { return hitFlag; }
        set { hitFlag = value; }
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
