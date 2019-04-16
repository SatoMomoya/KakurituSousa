using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//プレイヤーのスクリプト

namespace Momoya
{

    public class Player : Monster
    {
        //列挙型の定義
       public enum HaveItem
        {
            Weapon, //武器
            Head,   //頭
            Body,    //体

            More,
        }
        public enum Mode
        {
            Play,
            Result,

            More
        }


        //定数の定義
        

        public  const float cooltimeCount = 0.0f; //技を出したときにクールタイムを
       //変数の定義
  

        public Item[] haveItem = new Item[(int)HaveItem.More];
        [SerializeField]
        Vector3[] itemPos = new Vector3[(int)HaveItem.More];
        private float cooltime;                   //クールタイムをカウントする
       
        private Flag attackStateFlag;             //攻撃時のフラグ

        private Collision hitObject;              //ヒットしたオブジェクト

        [SerializeField] private Vector3 gravity;                  //重力
        [SerializeField] private Vector3 gravity2;                 //重力2

        private bool jumpFallFalg;

        private Vector3 lastPos;

        private Animator animator;
        [SerializeField]
        private  Mode mode;
        [SerializeField]
        private float goalTime = 1.0f;
        private float time;


        Color color;

       // Texture2D screenTexture;
       // public Camera camera;


        //public void Awake()
        //{
        //    // 1pixel のTexture2D.
        //    screenTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        //    // 黒のアルファ0.5で薄暗い感じにする.
        //    screenTexture.SetPixel(0, 0, new Color(0, 0, 0, 0.0f));
        //    // これをしないと色が適用されない.
        //    screenTexture.Apply();
        //}

        //public void OnGUI()
        //{
        //    // カメラのサイズで画面全体に描画.
        //    GUI.DrawTexture(Camera.main.pixelRect, screenTexture);
        //}

        //列挙型の定義
        public enum AttackState                         //攻撃用のステート
        {
            CanAttack    = (1 << 0),   //攻撃可能
            CanNotAttack = (1 << 1),   //攻撃不可能
        }

        //初期化関数
        public override void Initialize()
        {
           
            this.attackStateFlag = GetComponent<Flag>();
            
            attackStateFlag.Off((uint)AttackState.CanAttack);       
            attackStateFlag.Off((uint)AttackState.CanNotAttack);    //アタックフラグをfalseに

            animator = GetComponent<Animator>();

            jumpFallFalg = false;

            lastPos = transform.position;
            if(mode == Mode.Play)
            {
              //  rarity = startRarity; //初期レアリティをセット
                                      //装備しているステータスの合計をステータスにする
                for (int i = 0; i < (int)HaveItem.More; i++)
                {
                    haveItem[i].SetStatus();
                }
            }

            color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            transform.GetComponent<SpriteRenderer>().color = color;
        }

        //Move関数
        public override void Move()
        {
           
            if (mode == Mode.Play)
            {
                if (flag.Is((uint)StateFlag.Goal))
                {
                    time += Time.deltaTime;



                    if(time > goalTime)
                    {
                        SceneManager.LoadScene("TunagiScene");
                    }


                   
                    return;
                }

                if(Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene("Stagetest");
                }


                //アイテムのポジション設定
                SetItemPos();
                //プレイヤーレアリティ
                PlayerRarity();
                //プレイヤーのステース
                PlayerStatus();
                //十字キーの入力をセット
                vec.x = Input.GetAxis("Horizontal");
                vec.y = Input.GetAxis("Vertical");

                //プレイヤーの向きの転換
                if(vec.x <= -0.1f)
                {
                    this.transform.localScale = new Vector3( -Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                }
                else
                {
                    this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                }


                //ベクトルxが0.0じゃない場合動いている
                if (Mathf.Abs(vec.x) != 0.0f)
                {
                    flag.On((uint)StateFlag.Move);
                    animator.SetBool("IsWalk", true);
                }

                //ベクトルxが0.0なら止まっている
                if (Mathf.Abs(vec.x) == 0.0f)
                {
                    flag.Off((uint)StateFlag.Move);
                    animator.SetBool("IsWalk", false);
                }

                StealRarity();

                GiveRarity();


                if (!flag.Is((uint)StateFlag.Jump))
                {
                    animator.SetBool("IsJump", true);
                    //重力を消す
                    GetComponent<Rigidbody>().useGravity = false;

                    if (!jumpFallFalg)
                    {
                        GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Acceleration);
                    }
                    else
                    {
                        GetComponent<Rigidbody>().AddForce(gravity2, ForceMode.Acceleration);
                    }

                    float posY = transform.position.y - lastPos.y;
                    if (posY > 0)
                    {
                        animator.SetBool("IsJump", false);
                        animator.SetBool("IsJumpDown", true);
                        jumpFallFalg = true;
                    }
                }
                else
                {
                    animator.SetBool("IsJump", false);
                    animator.SetBool("IsJumpDown", false);
                }

                //   Debug.Log(vec);

                Jump(); //ジャンプ

                //最後の座標を入れる
                lastPos = transform.position;

                //if (Input.GetKeyDown(KeyCode.Q))
                //{
                //    SavePlayer.Save(this);
                //}
            }
            else
            {
               
            }
        }

        //レアリティを奪う関数
        private void StealRarity()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //if (GetComponentInChildren<AttackZone>().HitFlag)
                //{
                //    //当たっているオブジェクトのレアリティを下げ自分のレアリティを上げる
                //    hitObject.gameObject.GetComponent<Object>().Rarity = hitObject.gameObject.GetComponent<Monster>().Rarity - 1;
                //    this.rarity += 1;


                //}
            }
        }

        //レアリティをあげる関数
        private void GiveRarity()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                //if (GetComponentInChildren<AttackZone>().HitFlag)
                //{
                //    //当たっているオブジェクトのレアリティを上げ自分のレアリティを下げる
                //    hitObject.gameObject.GetComponent<Monster>().Rarity = hitObject.gameObject.GetComponent<Monster>().Rarity + 1;
                //    this.rarity -= 1;
                //}
            }
        }

        //アイテムのポジション設定
        public void SetItemPos()
        {
            for(int i= 0; i < (int)HaveItem.More; i++)
            {
                //アイテムのポジションを調整
                haveItem[i].transform.position = transform.position +  itemPos[i];
                if(vec.x <= -0.1f)
                {
                    //アイテムのポジションを調整
                    haveItem[i].transform.localScale = new Vector3(-1.0f, haveItem[i].transform.localScale.y, haveItem[i].transform.localScale.z);
                    //アイテムのポジションを調整
                    haveItem[i].transform.position = transform.position + new Vector3(-itemPos[i].x, itemPos[i].y, itemPos[i].z);
                   
                }
               
            }
        }

        //プレイヤーのレアリティを決める
        public void PlayerRarity()
        {
            int rarityCount = 0;
            for (int i = 0; i < (int)HaveItem.More; i++)
            {
                rarityCount += haveItem[i].Rarity;
            }

            rarity = rarityCount / (int)HaveItem.More ;
            Debug.Log("PlayerRarity" + rarity);
        }

        //プレイヤーのステータスを設定する
        public void PlayerStatus()
        {
            ObjectStatus.Status statustmp;
            statustmp.hp = 0;
            statustmp.attack = 0;
            statustmp.speed = 0;
            //装備しているステータスの合計をステータスにする
            for(int i = 0; i < (int)HaveItem.More; i++)
            {
                statustmp.hp +=  haveItem[i].HP;
                statustmp.attack += haveItem[i].Attack;
                statustmp.speed += haveItem[i].Speed;
            }

            status = statustmp;
            //確認用
            Debug.Log("HP" + status.hp);
            Debug.Log("Attack" + status.attack);
            Debug.Log("Speed" + status.speed);
        }

        //アイテムの変更
        public void ChangeItem(Item item,int itemgroup)
        {
            Destroy(haveItem[(int)itemgroup].gameObject);
            haveItem[(int)itemgroup] = item;
        }


        //ジャンプするための関数
        private void Jump()
        {

            //スペースキーを押された時、地面についていればジャンプする
            if (Input.GetKeyDown(KeyCode.Space) && flag.Is((uint)StateFlag.Jump))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower);
                
            }
        }

        //何が当たったのか知らせるプロパティ
        public Collision HitObject
        {
            get { return hitObject; }
            set { hitObject = value; }

        }

        


    }

}