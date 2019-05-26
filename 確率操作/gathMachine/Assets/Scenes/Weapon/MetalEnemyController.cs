using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalEnemyController : Momoya.Enemy
{
    private EnemyVision enemyVision;
    private bool visionFlag;            //見えたか見えてないか
    private Vector3 playerPos;          //プレイヤーの座標
    private int knockBackCount;     //ノックバックするカウント
    private int knockBackTime;     //ノックバックする時間
    public float floorDistanceX;
    private bool lastGroundFlag;        
    private bool damageFlag;
    private bool lookFlag;          //見えたフラグ
    private int escapeCount;        //逃げるカウント
    private int escapeTime;         //逃げ切れる時間
    public LayerMask playerLayer;
    private Momoya.Player player;
    private GameObject playerObj;
    private Goto.Damage damageSc;
    private bool knockBackFlag;

    //初期化
    public override void Initialize()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Momoya.Player>();
        enemyVision = GetComponentInChildren<EnemyVision>();
        damageSc = GetComponent<Goto.Damage>();
        vec.x = 0;
        knockBackCount = 0;

        lastGroundFlag = false;

        knockBackTime = 15;
        damageFlag = false;
        lookFlag = false;
        escapeCount = 0;
        escapeTime = 300;
        rarity = startRarity;
        knockBackFlag = false;
    }

    //移動
    public override void Move()
    {
        //進む向きで画像を反転する
        if(vec.x > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }

        visionFlag = enemyVision.VisionFlag;
        if (visionFlag)
        {
            lookFlag = true;
        }
        else
        {
            vec.x = 0;
        }
        //見つかったら逃げる
        if(lookFlag)
        {
            escapeCount++;
            Escape(enemyVision.PlayerPos);
        }

        //壁に引っかかった時
        CatchOnWall();

        //逃げ切ったら消える
        if (escapeCount > escapeTime)
        {
            Destroy(gameObject);
            
        }

        //攻撃を受けたらHPを減らす
        if(knockBackFlag)
        {
            KnockBack();
            if (damageFlag)
            {
                damageSc.DamageFlag = true;
                float damege = Damege(player, this);
                status.hp = status.hp - (int)damege;
                damageFlag = false;
            }
        }
       
        //HPが0以下になったら消える
        if (status.hp <= 0)
        {
            Finish();
        }

        
    }

    //逃げる
    private void Escape(Vector3 playerPos)
    {
        if (transform.position.x > playerPos.x)
        {
            vec.x = 4;
        }
        else
        {
            vec.x = -4;
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
            knockBackCount = 0;
            if (vec.x >= 0)
            {
                vec.x = 1;
            }
            else
            {
                vec.x = -1;
            }
            knockBackFlag = false;
        }

    }

    
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "SwordCollision")
        {

            playerPos = other.transform.position;
            knockBackFlag = true;
            damageFlag = true;
        }
    }
}
