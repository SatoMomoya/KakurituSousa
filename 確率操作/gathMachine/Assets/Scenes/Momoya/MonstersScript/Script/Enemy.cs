using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            int tmpRand = Random.Range(0, 100);
          

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
    }
}