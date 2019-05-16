using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Momoya
{

    public class Enemy : Monster
    {
        //列挙型の宣言

        //ドロップモード
        enum DropMode
        {
            Normal, //通常
            Confirm,//確定

            NUM
        }
        //定数の宣言
        public ItemBox dropItem;
        private GameObject dropObject;

        //変数の宣言
        [SerializeField]
        private uint dropNum; //何個アイテムを落とすか
        [SerializeField]
        private uint dropProbability;//ドロップする確率
        [SerializeField]
        private DropMode dropMode; //ドロップモード

        public const float line = 0.5f;             //線
        public LayerMask layerMask; //レイヤーマスク

        public override void Initialize()
        {
            rarity = startRarity;
        }


        public override void Move()
        {
            //もしHPが0以下になった場合
            if(status.hp <= 0)
            {
                Finish();
            }
           //  何もしない
        }
        //HPが0のときの処理
        public void Finish()
        {
            int tmpRand = UnityEngine.Random.Range(0, 100);
          

            switch(dropMode)
            {
                case DropMode.Normal:
               //ドロップ確率を超えてきた場合装備をドロップさせる
                    if (dropProbability >= tmpRand)
                    {
                        DropItem();
                    }
                    break;
                case DropMode.Confirm:
                    //アイテムを確定でドロップさせる
                    DropItem();
                    break;
            }


            //消す
            Destroy(this.gameObject);
        }
        //アイテムをドロップさせる関数
        public void DropItem()
        {
            dropObject = dropItem.GiveRandomObjectBox();

            GameObject go = Instantiate(dropObject) as GameObject;
            ////ドロップさせたアイテムのレアリティを変える
            //go.GetComponent<Item>().Rarity = this.rarity;
            go.transform.position = this.transform.position;
        }

        //壁に引っかかった時その速度分引く
        public void CatchOnWall()
        {
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(transform.position.x, transform.position.y - line, 0), Color.red);
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y - (line / 2.0f), 0), new Vector3(transform.position.x + line, transform.position.y - (line / 2.0f), 0), Color.red);
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y - (line / 2.0f), 0), new Vector3(transform.position.x - line, transform.position.y - (line / 2.0f), 0), Color.red);
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + (line / 2.0f), 0), new Vector3(transform.position.x + line, transform.position.y + (line / 2.0f), 0), Color.red);
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + (line / 2.0f), 0), new Vector3(transform.position.x - line, transform.position.y + (line / 2.0f), 0), Color.red);

            //地面についてないとき
            if (!Physics.Linecast(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x, transform.position.y - line, transform.position.z), layerMask))
            {
                //壁に当たっているとき
                if (Physics.Linecast(new Vector3(transform.position.x, transform.position.y - (line / 2.0f), transform.position.z), new Vector3(transform.position.x + line, transform.position.y - (line / 2.0f), transform.position.z), layerMask) ||
                    Physics.Linecast(new Vector3(transform.position.x, transform.position.y - (line / 2.0f), transform.position.z), new Vector3(transform.position.x - line, transform.position.y - (line / 2.0f), transform.position.z), layerMask) ||
                    Physics.Linecast(new Vector3(transform.position.x, transform.position.y + (line / 2.0f), transform.position.z), new Vector3(transform.position.x + line, transform.position.y + (line / 2.0f), transform.position.z), layerMask) ||
                    Physics.Linecast(new Vector3(transform.position.x, transform.position.y + (line / 2.0f), transform.position.z), new Vector3(transform.position.x + line, transform.position.y + (line / 2.0f), transform.position.z), layerMask))
                {
                    //速度を引いて0に
                    if (vec.x > 0)
                    {
                        vec.x = vec.x + (-Math.Abs(vec.x));
                    }
                    else
                    {
                        vec.x = vec.x + (Math.Abs(vec.x));
                    }
                }
            }
        }

        
    }
}