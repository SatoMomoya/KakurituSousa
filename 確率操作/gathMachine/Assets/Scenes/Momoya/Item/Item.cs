using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイテムのクラス

namespace Momoya
{

    public  class Item : Object
    {
        //列挙型の定義
        enum ItemType
        {
            Weapon, //武器
            Head,   //頭
            Body,　 //体
        }
        //定数の定義

        //変数の宣言
        [SerializeField]
        protected int startRarity;//装備の初期レアリティ
        [SerializeField]
        protected int setHp = 10; //ステータス用のhp
        [SerializeField]
        protected int setAttack = 10;//ステータス用の攻撃
        [SerializeField]
        protected int setSpeed = 10; //ステータス用のスピード
        //アイテムタイプ
        [SerializeField]
        ItemType type;
        //ステータス
        ObjectStatus.Status status;
        //ゲームオブジェクト
        // public GameObject obj;
        //プレイヤー
        private GameObject player;
        //チェンジフラグ
        bool changeFlag;

       
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player");
            player.GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            

            //アイテムを入れ替える
            if(changeFlag)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {


                    player.GetComponent<Player>().ChangeItem(this, (int)type);
                    changeFlag = false;
                    BoxCollider component = this.gameObject.GetComponent<BoxCollider>();
                    // 指定したコンポーネントを削除
                    Destroy(component);
                    //ステータスセット  
                    SetStatus();
                }
               
            }

         //   obj.transform.position = transform.position;
        }

        //アイテム特有の能力
        public virtual void Action()
        {
           // status.attack = 10;
           
            //何もない
        }

        //ステータスのセット
        public virtual void SetStatus()
        {
            //レアリティセット
            rarity = startRarity;
            //ステータスをセット
            status.hp = setHp;
            status.attack = setAttack;
            status.speed = setSpeed;

          //  GameObject go = Instantiate(obj) as GameObject;
           // obj = go;

        }

        public void SetRandomRarity()
        {
            rarity = Random.Range(1, 5);

        }

        public void OnCollisionStay(Collision collision)
        {
           if (collision.transform.tag == "Player")
            {
                changeFlag = true;
            }
        }

        public void OnCollisionExit(Collision collision)
        {
            if(collision.transform.tag == "Player")
            {
                changeFlag = false;
            }

        }

        //HPのプロパティ
        public int HP
        {
            get { return status.hp; }
            set { status.hp = value; }
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

    }

}