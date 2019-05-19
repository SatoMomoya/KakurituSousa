using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoiterEnemyController : Momoya.Enemy
{
    private Vector3 playerPos;  //プレイヤーの座標
    private int knockBackCount;     //ノックバックするカウント
    private int knockBackTime;     //ノックバックする時間
    public float floorDistanceX;

    private bool lastGroundFlag;

    private Momoya.Player player;
    private GameObject playerObj;

    private bool damageFlag;

    private int wallHitCount;       //壁に当たっているカウント
    private int wallHitTime;        //壁に当たっている時間
    private Vector3 lastVec;        //最後の移動

    //初期化
    public override void Initialize()
    {
        
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Momoya.Player>();

        flag.Off((uint)StateFlag.Chase);
        vec.x = 1;

        knockBackCount = 0;
        lastGroundFlag = false;
        knockBackTime = 15;
        damageFlag = false;
        wallHitCount = 0;
        wallHitTime = 30;
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
        if (!damageFlag)
        {
            //徘徊
            Loiter();
        }
        else
        {
            KnockBack();
        }

        //壁に引っかかった時
        CatchOnWall();

        //攻撃を受けたらHPを減らす
        if (damageFlag)
        {
            Debug.Log("aa" + player.Attack);
            float damege = Damege(player, this);
            status.hp = status.hp - (int)damege;
            damageFlag = false;
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

    //徘徊
    private void Loiter()
    {

        //右の地面判定
        if (!Physics.Linecast(new Vector3(transform.position.x + floorDistanceX, transform.position.y, 0), new Vector3(transform.position.x + floorDistanceX + 0.4f, transform.position.y - 1, 0), layerMask))
        {
            vec.x = -1;
        }
        //左の地面判定
        if (!Physics.Linecast(new Vector3(transform.position.x - floorDistanceX, transform.position.y, 0), new Vector3(transform.position.x - floorDistanceX - 0.4f, transform.position.y - 1, 0), layerMask))
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
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

        }
        else
        {
            vec.x = -5;
            this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

        }

        //0.5フレームノックバックする
        if (knockBackCount > knockBackTime)
        {
            knockBackCount = 0;

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

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "SwordCollision")
        {
            
            playerPos = other.transform.position;
            damageFlag = true;
        }
    }


}
