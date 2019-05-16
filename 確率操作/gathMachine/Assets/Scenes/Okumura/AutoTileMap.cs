//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   AutoTileMap.cs
//!
//! @brief  ゲーム関連のC++ファイル
//!
//! @date   2019/4/8
//!
//! @author オクムラ イヤゴ
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

// 名前空間の使用 ==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//----------------------------------------------------------------------
//!
//! @brief 自動タイルマップ
//!
//----------------------------------------------------------------------
public class AutoTileMap : MonoBehaviour
{

    public int m_rows = 10;   // タイルの行数
    public int m_columns = 20;　// タイルの列数
    public float m_tileWidth = 1;　// タイル幅
    public float m_tileHeight = 1;　// タイル高さ
    public Color m_outLineColor = Color.blue; // 外の線の色
    public Color m_mapLineColor = Color.white; // マップの線の色
    public Material m_material; // マテリアル
    public string m_tagName; // タグ
    public int m_item;  // アイテムの出現の数
    public UnityEngine.GameObject[] m_itemObject; // アイテムのオブジェクト
    public int m_enemy; // 敵の出現の数
    public UnityEngine.GameObject m_enemyObject; // 敵のオブジェクト
    public Material m_enemyMaterial; // 敵のマテリアル
    [SerializeField, Range(0, 100)]
    public int[] m_stagePercent; // ブロックの出る確率
    int m_itemNum; // アイテムの番号

    public Momoya.Player m_player;//プレイヤー
    private GaugeController gauge;//HPゲージ
    private GameObject hpDirector;//HPゲージディレクター
    private GaugeDirector gaugeDirector;
    private Momoya.Monster monster;//敵

    public GameObject hpCanvas;//HPUIのキャンバス
    public GameObject hpGaugePrehab;//HPゲージプレハブ
    // 初期化処理
    private void Start()
    {
        Debug.Log("bbbbbbbbbbbbbbbbbb");
        hpDirector = GameObject.Find("HPGaugeDirector");
        gaugeDirector = hpDirector.GetComponent<GaugeDirector>();

        // アイテムの番号の初期化
        m_itemNum = 0;

        // 行分回す
        for (int i = 0; i < m_rows; i++)
        {
            // 列分回す
            for (int j = 0; j < m_columns; j++)
            {
                // 乱数の初期化
                int r = UnityEngine.Random.Range(0, 100);
                // ブロックの描画
                Draw(j, i, r);
            }
        }

        // アイテムの数分回す
        for (int i = 0; i < m_item; i++)
        {
            // 乱数の初期化
            int row = UnityEngine.Random.Range(1, m_rows);
            int column = UnityEngine.Random.Range(1, m_columns);
            // アイテムの描画
            Item(column, row);
        }


        // 敵の数分回す
        for (int i = 0; i < m_enemy; i++)
        {
            // 乱数の初期化
            int row = UnityEngine.Random.Range(1, m_rows);
            int column = UnityEngine.Random.Range(1, m_columns);
            // アイテムの描画
            Enemy(column, row);
        }

    }

    // 更新処理
    private void Update()
    {
        UnityEngine.GameObject body = UnityEngine.GameObject.Find("BodyImage");
        UnityEngine.GameObject head = UnityEngine.GameObject.Find("HeadImage");
        UnityEngine.GameObject sword = UnityEngine.GameObject.Find("SwordImage");

        body.GetComponent<UnityEngine.UI.RawImage>().texture = m_player.haveItem[2].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.texture;
        head.GetComponent<UnityEngine.UI.RawImage>().texture = m_player.haveItem[1].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.texture;
        sword.GetComponent<UnityEngine.UI.RawImage>().texture = m_player.haveItem[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.texture;

    }

    // グリッドの描画処理
    private void OnDrawGizmosSelected()
    {

        // オブジェクトの初期位置の取得
        Vector3 position = transform.position;
        // 外の線の色を決める
        Gizmos.color = m_outLineColor;
        // 左下から右下の線を引く
        Gizmos.DrawLine(position,
            position + new Vector3(m_columns * m_tileWidth, 0, 0));
        // 左下から左上の線を引く
        Gizmos.DrawLine(position,
            position + new Vector3(0, m_rows * m_tileHeight, 0));
        // 右下から右上の線を引く
        Gizmos.DrawLine(position + new Vector3(m_columns * m_tileWidth, 0, 0),
            position + new Vector3(m_columns * m_tileWidth, m_rows * m_tileHeight, 0));
        // 左上から右上の線を引く
        Gizmos.DrawLine(position + new Vector3(0, m_rows * m_tileHeight, 0)
            , position + new Vector3(m_columns * m_tileWidth, m_rows * m_tileHeight, 0));
        // マップの色を決める
        Gizmos.color = m_mapLineColor;
        // 列数分回す
        for (float i = 1; i < m_columns; i++)
        {
            // 横の線を引く
            Gizmos.DrawLine(position + new Vector3(i * m_tileWidth, 0, 0), position + new Vector3(i * m_tileWidth, m_rows * m_tileHeight, 0));
        }
        // 行数分回す
        for (float i = 1; i < m_rows; i++)
        {
            // 縦の線を引く
            Gizmos.DrawLine(position + new Vector3(0, i * m_tileHeight, 0), position + new Vector3(m_columns * m_tileWidth, i * m_tileHeight, 0));
        }

    }

    // 敵の描画処理
    private void Enemy(int row, int column)
    {
        //// その位置のブロックの中身を入れる
        //UnityEngine.GameObject cube = UnityEngine.GameObject.Find(string.Format("AutoTile_{0}_{1}", row, column));

        //// そのブロックが存在してたら
        //if (cube != null)
        //{
        //    // その位置の中にアイテムを入れる
        //    UnityEngine.GameObject enemy = UnityEngine.GameObject.Instantiate(m_enemyObject);

        //    // 球の作成
        //    UnityEngine.GameObject sphere = UnityEngine.GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //    sphere.GetComponent<Collider>().isTrigger = true;
        //    // 敵の位置の初期化
        //    Vector3 tilePositionInLocalSpace = new Vector3((row * m_tileWidth) + (m_tileWidth / 2), ((column + 1) * m_tileHeight) + (m_tileHeight / 2), 0);
        //    enemy.transform.position = transform.position + tilePositionInLocalSpace;

        //    // 球の位置の初期化
        //    sphere.transform.position = transform.position + tilePositionInLocalSpace;

        //    // サイズの初期化
        //    enemy.transform.localScale = new Vector3(m_tileWidth, m_tileHeight, 1);

        //    // サイズの初期化
        //    sphere.transform.localScale = new Vector3(m_tileWidth, m_tileHeight, 1);

        //    // 親子関係を結ぶ
        //    enemy.transform.parent = transform;

        //    // 親子関係を結ぶ
        //    sphere.transform.parent = enemy.transform;

        //    // 敵の名前の初期化
        //    enemy.name = string.Format("Enemy_{0}_{1}", row + 1, column);

        //    // 球のなめの初期化
        //    sphere.name = string.Format("Enemy_Sphere_{0}_{1}", row + 1, column);

        //    // マテリアルの初期化
        //    sphere.GetComponent<Renderer>().material = m_enemyMaterial;

           
        //    // レイヤーの追加
        //    sphere.layer = 11;

        //    //HPゲージの生成
        //    GameObject hpObj = Instantiate(hpGaugePrehab) as GameObject;
        //    //HPUIキャンバスの子に設定
        //    hpObj.transform.SetParent(hpCanvas.transform, false);
        //    //ゲージのスクリプトを取得
        //    gauge = hpObj.GetComponent<GaugeController>();

        //    //リストに追加
        //    if (gauge)
        //    {
        //        Debug.Log("sasadfas");
        //        gaugeDirector.addHPGauge(gauge);
        //    }
        //    else
        //    {
        //        //Debug.Log("入ってないんですけど～");
        //    }
        //    //Debug.Log("bbbbbb");
        //    //敵のレイヤーでモンスターにスクリプトを入れる
        //    if (enemy.layer == LayerMask.NameToLayer("Enemy1"))
        //    {
        //        monster = enemy.transform.GetComponent<EnemyController>();
        //    }
        //    if (enemy.gameObject.layer == LayerMask.NameToLayer("Enemy2"))
        //    {
        //        monster = enemy.transform.GetComponent<LoiterEnemyController>();

        //    }
        //    if (enemy.gameObject.layer == LayerMask.NameToLayer("Enemy3"))
        //    {
        //        monster = enemy.transform.GetComponent<MetalEnemyController>();

        //    }
        //    //リストに追加
        //    gaugeDirector.addMonster(monster);

        //}

        //else
        //{
        //    // 乱数の初期化
        //    int r = UnityEngine.Random.Range(1, m_rows);
        //    int c = UnityEngine.Random.Range(1, m_columns);
        //    // 敵の描画
        //    Enemy(c, r);
        //}

    }

    // アイテムの描画処理
    private void Item(int row, int column)
    {
        // その位置のブロックの中身を入れる
        UnityEngine.GameObject cube = UnityEngine.GameObject.Find(string.Format("AutoTile_{0}_{1}", row, column));

        // そのブロックが存在してたら
        if (cube != null)
        {
            // その位置の中にアイテムを入れる
            UnityEngine.GameObject item = UnityEngine.GameObject.Instantiate(m_itemObject[m_itemNum]);

            // 球の作成
            UnityEngine.GameObject sphere = UnityEngine.GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // アイテムの位置の初期化
            Vector3 tilePositionInLocalSpace = new Vector3((row * m_tileWidth) + (m_tileWidth / 2), ((column + 1) * m_tileHeight) + (m_tileHeight / 2), 0);
            item.transform.position = transform.position + tilePositionInLocalSpace;

            // 球の位置の初期化
            sphere.transform.position = transform.position + tilePositionInLocalSpace;

            // サイズの初期化
            item.transform.localScale = new Vector3(m_tileWidth, m_tileHeight, 1);

            // サイズの初期化
            sphere.transform.localScale = new Vector3(m_tileWidth, m_tileHeight, 1);

            // 親子関係を結ぶ
            item.transform.parent = transform;

            // 親子関係を結ぶ
            sphere.transform.parent = item.transform;

            // アイテムの名前の初期化
            item.name = string.Format("Item_{0}_{1}", row + 1, column);

            // 球のなめの初期化
            sphere.name = string.Format("Item_Sphere_{0}_{1}", row + 1, column);

            // タグの追加
            item.tag = "Item";

            // レイヤーの追加
            sphere.layer = 11;


             //トリガーに設定
            sphere.GetComponent<SphereCollider>().isTrigger = true;
            // 次のアイテム
            m_itemNum++;

            // 次のアイテムがなかったら
            if (m_itemNum > 14)
            {
                // ０に戻す
                m_itemNum = 0;
            }

        }

        else
        {
            // 乱数の初期化
            int r = UnityEngine.Random.Range(1, m_rows);
            int c = UnityEngine.Random.Range(1, m_columns);
            // アイテムの描画
            Item(c, r);
        }


    }

    // ブロックの描画
    private void Draw(int x, int y, int random)
    {

        // マウスの位置がどのタイルにあるか取得
        Vector2 tilePos = new Vector2(x, y);


        // その位置のブロックの中身を入れる
        UnityEngine.GameObject cube = UnityEngine.GameObject.Find(string.Format("AutoTile_{0}_{1}", tilePos.x, tilePos.y));
        UnityEngine.GameObject cube2 = UnityEngine.GameObject.Find(string.Format("AutoTile2_{0}_{1}", tilePos.x, tilePos.y));

        if (m_stagePercent.Length >= y)
        {
            if (m_stagePercent[y] > random)
            {
                // そのブロックが存在してたら返す
                if (cube != null && cube.transform.parent != transform)
                {
                    return;
                }

                //　nullだったら
                if (cube == null)
                {
                    // ブロックを作成する
                    cube = UnityEngine.GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube2 = UnityEngine.GameObject.CreatePrimitive(PrimitiveType.Cube);
                }

                // ブロックの位置の初期化
                Vector3 tilePositionInLocalSpace = new Vector3((tilePos.x * m_tileWidth) + (m_tileWidth / 2), (tilePos.y * m_tileHeight) + (m_tileHeight / 2));
                cube.transform.position = transform.position + tilePositionInLocalSpace;
                cube2.transform.position = transform.position + tilePositionInLocalSpace;

                // サイズの初期化
                cube.transform.localScale = new Vector3(m_tileWidth, m_tileHeight, 1);
                cube2.transform.localScale = new Vector3(m_tileWidth, m_tileHeight, 1);

                // 親子関係を結ぶ
                cube.transform.parent = transform;
                cube2.transform.parent = transform;

                // マテリアルの初期化
                cube.GetComponent<Renderer>().material = m_material;
                cube2.GetComponent<Renderer>().material = m_material;

                // ブロックの名前の初期化
                cube.name = string.Format("AutoTile_{0}_{1}", tilePos.x, tilePos.y);
                cube2.name = string.Format("AutoTile2_{0}_{1}", tilePos.x, tilePos.y);

                // タグの追加
                cube.tag = m_tagName;
                cube2.tag = "Wall";

                //レイヤーの追加
                cube.layer = 9;
                cube2.layer = 9;

                // コライダーのサイズを決める
                cube.GetComponent<BoxCollider>().size = new Vector3(1.0f, 0.1f, 1);
                cube.GetComponent<BoxCollider>().center = new Vector3(0, 0.45f, 0);
            }
            else
            {
                // ブロックを消す
                Erase(x, y);
            }
        }

    }

    // ブロックを消す処理
    private void Erase(int x, int y)
    {


        // マウスの位置がどのタイルにあるか取得
        Vector2 tilePos = new Vector2(x, y);

        // 当たってるタイルを取得
        UnityEngine.GameObject cube = UnityEngine.GameObject.Find(string.Format("AutoTile_{0}_{1}", tilePos.x, tilePos.y));

        // ブロックの中がなかったら
        if (cube != null && cube.transform.parent == transform)
        {
            // そのブロックを消す
            UnityEngine.Object.DestroyImmediate(cube);
        }

    }
}
