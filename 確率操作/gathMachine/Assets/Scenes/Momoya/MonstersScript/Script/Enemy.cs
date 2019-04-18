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
        public GameObject dropItem;


        //変数の宣言
        [SerializeField]
        private uint dropNum; //何個アイテムを落とすか
      

        public override void Move()
        {
           //  何もしない
        }
        //HPが0のときの処理
        public void Finish()
        {
            Destroy(this.gameObject);
        }
    }
}