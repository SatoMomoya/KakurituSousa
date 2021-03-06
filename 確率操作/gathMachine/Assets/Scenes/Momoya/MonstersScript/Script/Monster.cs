﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Momoya;
//writer name is SatoMomoya
//モンスターにとって必要な情報を入れたクラス
namespace Momoya
{
 abstract public class Monster : Object
    {
      

        //変数の宣言

        [SerializeField]
        protected  float speed ;                       //スピード
        [SerializeField]
        protected  float jumpPower;                  //ジャンプパワー

        [SerializeField]
        protected int startRarity;//モンスターの初期レアリティ
        [SerializeField]
        protected int setHp = 10;    //ステータス用のhp
        [SerializeField]
        protected int setAttack = 10;//ステータス用の攻撃
        [SerializeField]
        protected int setSpeed = 10; //ステータス用のスピード
        protected ObjectStatus.Status status;

        protected Vector3 monsterSpeed; //モンスターのスピード
        protected Flag flag;      //フラグ

        private int minDamege;
       
        //列挙型の定義
        protected enum StateFlag //状態のフラグ
        {
            Move = (1 << 0),  //移動フラグ
            Jump = (1 << 1),  //ジャンプフラグ
            Deth = (1 << 2),  //死亡フラグ
            Goal = (1 << 3),  //ゴールフラグ
            Finish = (1 << 4), //終了
            Chase = (1 << 5),
        }

        protected enum MonsterState
        {
            Normal, //普通の状態
            Jump  , //ジャンプ状態

            NumState,
        }

        // Use this for initialization
        void Start()
        {
           // 
            this.startPos = this.transform.position;
            this.vec = this.GetComponent<Rigidbody>().velocity;

            this.flag = GetComponent<Flag>();
            //ステータスをセット
            status.hp = setHp;
            status.attack = setAttack;
            status.speed  = setSpeed;

            minDamege = 1;

            //初期化処理(子クラス用)
            Initialize();

        }

        // Update is called once per frame
        void Update()
        {

            Move();         //移動の処理
            PositionCtrl(); //ポジションの処理

            
        }
     
        

        //一番最初のレアリティをゲットとセットするプロパティ
        public int StartRarity
        {
            get { return startRarity; }
            set { startRarity = value; }
        }

        //HPのプロパティ
        public int HP
        {
            get { return status.hp; }
            set { status.hp = value; }
        }

        //初期HPのプロパティ
        public int StartHP
        {
            get { return setHp; }

        }

        //Attackのプロパティ
        public int Attack
        {
            get { return status.attack; }
            set { status.attack = value; }
        }

        //Speedのプロパティ
        public int Speed
        {
            get { return status.speed; }
            set { status.speed = value; }
        }

        //ポジションをコントロールする関数
        protected void PositionCtrl()
        {
            Vector3 direction = new Vector3(vec.x, vec.y, vec.z) * speed;

            //移動させる
            GetComponent<Rigidbody>().velocity = new Vector3(direction.x, GetComponent<Rigidbody>().velocity.y, direction.z);

            if(transform.position.y < -100.0f)
            {
                this.transform.position = startPos;
            }
           
        }

        //初期化する関数(子クラス用)
        public virtual void Initialize()
        {

        }
        
        //移動する関数
        public abstract void Move();

        //HP0のとき終了する処理をかく


        //ダメージ計算
        public float Damege(Momoya.Monster attack,Momoya.Monster defense)
        {
            Debug.Log("at = "+attack.Attack);
            float damege = attack.Attack * (((attack.Attack / defense.rarity) * minDamege) / 100.0f);
           
            Debug.Log("damegeLast = " + damege);
            return damege;
        }

        //何かと離れたときの関数
        protected void OnCollisionExit(Collision collision)
        {
            //離れた何かのタグを調べる
            switch (collision.transform.tag)
            {
                //case "Ground": flag.Off((uint)StateFlag.Jump); break; //groundを離れたらジャンプフラグoffにする
            }
        }

        public bool GoalFlag()
        {
            return flag.Is((uint)StateFlag.Goal);
        }
    }

}