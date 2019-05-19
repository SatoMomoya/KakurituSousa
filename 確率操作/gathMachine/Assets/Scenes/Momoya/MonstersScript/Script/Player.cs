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

        public ResultGo result;

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

        public LayerMask layerMask; //レイヤーマスク
        private float lineX;        //線の横の幅
        private float jumpLine;     //プレイヤーの下の線の幅
        Color color;

        private bool nowJump;           //ジャンプ中に壁に当たっているかどうか

        private float widthLineUpY;      //横向きの上の線
        private float widthLineDownY;    //横向きの下の線

        private int knockBackCount;     //ノックバックカウント
        private int knockBackTime;      //ノックバック時間
        private bool knockBackFlag;     //ノックバックするフラグ
        private Vector3 knockBackPos;   //ノックバックするときの敵の座標
        private GameObject swordCol;        //剣の当たり判定のオブジェクト 
        private const int SwordActionTime = 16;     //剣のモーション時間
        private int swordActionCount;               //剣のモーションカウント
        private bool swordActionFlag;               //剣のモーションフラグ
        private bool playerRightLeftFlag;       //trueの時は右向いている/falseの時は左向いている

        public GameObject bulletPrehab;
        private bool bulletCreateFlag;
        private bool wandActionFlag;
        private const int WandActionTime = 8;     //杖のモーション時間
        private int wandActionCount;               //杖のモーションカウント

        private int maxHP;

        public Monster enemy1;

        private LayerMask hitObjLayer;      //敵のレイヤー

        private GaugeController gauge;//HPゲージ
        public GaugeDirector hpDirector;//HPゲージディレクター
        private Momoya.Monster monster;//敵

        public GameObject hpCanvas;//HPUIのキャンバス
        public GameObject hpGaugePrehab;//HPゲージプレハブ

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

            swordCol = transform.Find("SwordCollision").gameObject;
            swordCol.SetActive(false);
            attackStateFlag.Off((uint)AttackState.CanAttack);       
            attackStateFlag.Off((uint)AttackState.CanNotAttack);    //アタックフラグをfalseに

            animator = GetComponent<Animator>();
            animator.SetBool("IsWepon", true);

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

            //HPゲージの生成
            GameObject hpObj = Instantiate(hpGaugePrehab) as GameObject;
            //HPUIキャンバスの子に設定
            hpObj.transform.SetParent(hpCanvas.transform, false);
            //ゲージのスクリプトを取得
            gauge = hpObj.GetComponent<GaugeController>();
            //リストに追加
            hpDirector.addHPGauge(gauge);
            //敵のレイヤーでモンスターにスクリプトを入れる
            monster = GetComponent<Player>();
            
            //リストに追加
            hpDirector.addMonster(monster);

            lineX = 0.3f;
            jumpLine = 0.95f;
            nowJump = false;
            widthLineUpY = 0.5f;
            widthLineDownY = 0.8f;
            knockBackCount = 0;
            knockBackTime = 5;
            knockBackFlag = false;
            swordActionCount = 0;
            swordActionFlag = false;

            bulletCreateFlag = false;
            wandActionFlag = false;
            wandActionCount = 0;
            playerRightLeftFlag = true;
        }

        //Move関数
        public override void Move()
        {
           
            if (mode == Mode.Play)
            {
                if (flag.Is((uint)StateFlag.Goal))
                {
                    //ゴールしたら速度をゼロにする
                    vec = Vector3.zero;
                    time += Time.deltaTime;

                    Debug.Log(time);

                   if(time > goalTime)
                    {
                        result.Flag();
                    }


                   
                    return;
                }
                //リセット用(デバッグ)
                if(Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene("Stagetest");
                }

                if (status.hp <= 0)
                {
                    SceneManager.LoadScene("teramotoTeast");
                }


                //アイテムのポジション設定
                SetItemPos();
                //プレイヤーレアリティ
                PlayerRarity();
                //プレイヤーのステース
                PlayerStatus();

                if(haveItem[0].gameObject.layer == LayerMask.NameToLayer("Sword"))
                {
                    animator.SetBool("IsWepon", true);
                }
                else
                {
                    animator.SetBool("IsWepon", false);
                }

                if (!knockBackFlag)
                {
                    //十字キーの入力をセット
                    vec.x = Input.GetAxis("Horizontal");
                    vec.y = Input.GetAxis("Vertical");

                    if(Input.GetKeyDown(KeyCode.E))
                    {

                        
                    }

                    //右押されたら右向き
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        playerRightLeftFlag = true;
                    }
                    //左押されたら左向き
                    if(Input.GetKey(KeyCode.LeftArrow))
                    {
                        playerRightLeftFlag = false;
                    }

                    //プレイヤーの向きの転換
                    if (playerRightLeftFlag)
                    {
                        this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                    }
                    else
                    {
                        this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                    }

                    ////ジャンプ中に横壁に当たった時
                    if (nowJump)
                    {
                        //速度を引いて0に
                        if(Input.GetKey(KeyCode.RightArrow))
                        {
                            vec.x = vec.x + (-Math.Abs(vec.x));
                        }
                        if(Input.GetKey(KeyCode.LeftArrow))
                        {
                            vec.x = vec.x + (Math.Abs(vec.x));
                        }
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
                        
                        //落ち始めたら重力を変える
                        if (!jumpFallFalg)
                        {
                            GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Acceleration);
                        }
                        else
                        {
                            //落下速度制限
                            if (GetComponent<Rigidbody>().velocity.magnitude < Math.Abs(gravity2.y))
                            {
                                GetComponent<Rigidbody>().AddForce(gravity2, ForceMode.Acceleration);
                            }
                           
                        }

                        float posY = transform.position.y - lastPos.y;
                        //落ち始めているかどうか
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

                    Jump(); //ジャンプ
                    
                    //zキーをしたら攻撃
                    if(Input.GetKeyDown(KeyCode.Z))
                    {
                        if(haveItem[0].gameObject.layer == LayerMask.NameToLayer("Sword"))
                        {
                            swordActionFlag = true;
                        }
                        else
                        {
                            wandActionFlag = true;
                        }
                        
                    }
                    if(swordActionFlag)
                    {
                        SwordAttack();
                    }
                    if(wandActionFlag)
                    {
                        WandAttack();
                    }
                }
                else
                {
                    status.hp = status.hp - (int)Damege(enemy1, this);

                    Debug.Log("HP = "+status.hp);
                    KnockBack(knockBackPos);

                }
                //下の赤い線
                Debug.DrawLine(new Vector3(transform.position.x + lineX, transform.position.y, transform.position.z), new Vector3(transform.position.x + lineX, transform.position.y - jumpLine, transform.position.z), Color.red);
                Debug.DrawLine(new Vector3(transform.position.x - lineX, transform.position.y, transform.position.z), new Vector3(transform.position.x - lineX, transform.position.y - jumpLine, transform.position.z), Color.red);

                //上の青い横の線
                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y - widthLineUpY, transform.position.z), new Vector3(transform.position.x + 0.8f, transform.position.y - widthLineUpY, transform.position.z), Color.blue);
                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y - widthLineUpY, transform.position.z), new Vector3(transform.position.x - 0.8f, transform.position.y - widthLineUpY, transform.position.z), Color.blue);
                //下の青い横の線
                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y - widthLineDownY, transform.position.z), new Vector3(transform.position.x + 0.5f, transform.position.y - widthLineDownY, transform.position.z), Color.blue);
                Debug.DrawLine(new Vector3(transform.position.x, transform.position.y - widthLineDownY, transform.position.z), new Vector3(transform.position.x - 0.5f, transform.position.y - widthLineDownY, transform.position.z), Color.blue);

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
            //Debug.Log("PlayerRarity" + rarity);
        }

        //プレイヤーのステータスを設定する
        public void PlayerStatus()
        {
            ObjectStatus.Status statustmp;
            statustmp.hp = status.hp;
            statustmp.attack = 0;
            statustmp.speed = 0;
            int maxHptmp = 0;
            //装備しているステータスの合計をステータスにする
            for (int i = 0; i < (int)HaveItem.More; i++)
            {
                maxHptmp += haveItem[i].HP;
                statustmp.attack += haveItem[i].Attack;
                statustmp.speed += haveItem[i].Speed;
            }
            maxHP = maxHptmp;
            
            status = statustmp;

            if (status.hp > maxHP)
            {
                status.hp = maxHP;
            }
            //確認用
            //Debug.Log("HP" + status.hp);
            //Debug.Log("Attack" + status.attack);
            //Debug.Log("Speed" + status.speed);
        }

        //アイテムの変更
        public void ChangeItem(Item item,int itemgroup)
        {
           // Destroy(haveItem[(int)itemgroup].gameObject);
            haveItem[(int)itemgroup] = item;
        }

        //剣で攻撃
        private void SwordAttack()
        {
            swordActionCount++;
            animator.SetBool("IsAttack", true);
            if(swordActionCount > 5)
            {
                swordCol.SetActive(true);
            }
           

            if(swordActionCount >= SwordActionTime)
            {
                animator.SetBool("IsAttack", false);
                swordCol.SetActive(false);
                swordActionCount = 0;
                swordActionFlag = false;
            }
        }

        //杖で攻撃
        private void WandAttack()
        {
            wandActionCount++;
            animator.SetBool("IsAttack", true);

            if(!bulletCreateFlag)
            {
                if(wandActionCount > 5)
                {
                    GameObject bullet = Instantiate(bulletPrehab) as GameObject;
                    
                    bulletPrehab.transform.position = transform.position;
                    
                    bulletCreateFlag = true;
                }
            }

            if(wandActionCount >= WandActionTime)
            {
                animator.SetBool("IsAttack", false);
                
                wandActionCount = 0;
                wandActionFlag = false;
                bulletCreateFlag = false;
            }

        }

        //ジャンプするための関数
        private void Jump()
        {
            //地面についている
            if (Physics.Linecast(new Vector3(transform.position.x + lineX, transform.position.y, transform.position.z), new Vector3(transform.position.x + lineX, transform.position.y - jumpLine, transform.position.z), layerMask)||
                Physics.Linecast(new Vector3(transform.position.x - lineX, transform.position.y, transform.position.z), new Vector3(transform.position.x - lineX, transform.position.y - jumpLine, transform.position.z), layerMask))
            {
               
                flag.On((uint)StateFlag.Jump);
                
            }

            //地面から離れた
            if (!Physics.Linecast(new Vector3(transform.position.x + lineX, transform.position.y, transform.position.z), new Vector3(transform.position.x + lineX, transform.position.y - jumpLine, transform.position.z), layerMask)&&
                !Physics.Linecast(new Vector3(transform.position.x - lineX, transform.position.y, transform.position.z), new Vector3(transform.position.x - lineX, transform.position.y - jumpLine, transform.position.z), layerMask))
            {
               
                flag.Off((uint)StateFlag.Jump);
                
            }

            //スペースキーを押された時、地面についていればジャンプする
            if (Input.GetKeyDown(KeyCode.Space) && flag.Is((uint)StateFlag.Jump))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower);
                animator.SetBool("IsJump", true);

            }
            
        }

        //ノックバック
        private void KnockBack(Vector3 pos)
        {
            if(knockBackFlag)
            {
                GetComponent<Rigidbody>().useGravity = false;
                knockBackCount++;

                if(transform.position.x > pos.x)
                {
                    vec.x = 1;
                    this.transform.localScale = new Vector3(-Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                }
                else
                {
                    vec.x = -1;
                    this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);

                }
            }

            if(knockBackCount > knockBackTime)
            {
                GetComponent<Rigidbody>().useGravity = true;
                vec.x = 0;
                knockBackCount = 0;
                knockBackFlag = false;
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.tag == "Monster")
            {
                knockBackFlag = true;
                knockBackPos = collision.transform.position;
                hitObjLayer = collision.gameObject.layer;

                if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy1"))
                {
                    enemy1 = collision.transform.GetComponent<EnemyController>();
                    
                }
                if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy2"))
                {
                    enemy1 = collision.transform.GetComponent<LoiterEnemyController>();
                   
                }
                if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy3"))
                {
                    enemy1 = collision.transform.GetComponent<MetalEnemyController>();
                    
                }
            }
        }

        public  void OnCollisionStay(Collision collision)
        {
            if (collision.transform.tag == "Goal")
            {
                flag.On((uint)StateFlag.Goal);
            }
            //当たった何かのタグを調べる
            switch (collision.transform.tag)
            {
                //case "Ground": flag.On((uint)StateFlag.Jump);   break; //groundと触れていればジャンプフラグをtrueにする
                case "Goal":
                    flag.On((uint)StateFlag.Goal);
                    Debug.Log("Goal");
                    break;
            }

            //ジャンプ中
            if (!flag.Is((uint)StateFlag.Jump))
            {
                //プレイヤーの横の線の当たり判定
                if (Physics.Linecast(new Vector3(transform.position.x, transform.position.y - widthLineUpY, transform.position.z), new Vector3(transform.position.x - 0.8f, transform.position.y - widthLineUpY, transform.position.z), layerMask) ||
                    Physics.Linecast(new Vector3(transform.position.x, transform.position.y - widthLineDownY, transform.position.z), new Vector3(transform.position.x - 0.5f, transform.position.y - widthLineDownY, transform.position.z), layerMask))
                {
                    //壁か床に当たったら
                    if (collision.transform.tag == "Wall" || collision.transform.tag == "Ground")
                    {
                        nowJump = true;
                    }
                    else
                    {
                        nowJump = false;
                    }
                }
                //プレイヤーの横の線の当たり判定
                if (Physics.Linecast(new Vector3(transform.position.x, transform.position.y - widthLineUpY, transform.position.z), new Vector3(transform.position.x + 0.8f, transform.position.y - widthLineUpY, transform.position.z), layerMask) ||
                    Physics.Linecast(new Vector3(transform.position.x, transform.position.y - widthLineDownY, transform.position.z), new Vector3(transform.position.x + 0.5f, transform.position.y - widthLineDownY, transform.position.z), layerMask))
                {
                    //壁か床に当たったら
                    if (collision.transform.tag == "Wall" || collision.transform.tag == "Ground")
                    {
                        nowJump = true;
                    }
                    else
                    {
                        nowJump = false;
                    }
                }

               
                
               
            }
            else
            {
                nowJump = false;
            }

             
        }


        //何が当たったのか知らせるプロパティ
        public Collision HitObject
        {
            get { return hitObject; }
            set { hitObject = value; }

        }

        public int MaxHP()
        {
            return maxHP;
        }

        public bool PlayerRightLeftFlag()
        {
            return playerRightLeftFlag;
        }

    }

}