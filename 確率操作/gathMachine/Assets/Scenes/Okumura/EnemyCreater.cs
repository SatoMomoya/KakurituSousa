//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   EnemyCreater.cs
//!
//! @brief  ゲーム関連のC++ファイル
//!
//! @date   2019/5/15
//!
//! @author オクムラ イヤゴ
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

// 名前空間の使用 ==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------------
//!
//! @brief 敵の作成クラス
//!
//----------------------------------------------------------------------
public class EnemyCreater : MonoBehaviour
{
    public UnityEngine.GameObject []m_enemyObject; // 敵のオブジェクト
    public int []m_enemyNum; // 敵の数
    public Material []m_enemyMaterial; // 敵のマテリアル
    public int m_rows;
    public int m_columns;
    float m_tileWidth = 1.0f;
    float m_tileHeight = 1.0f;

    private GaugeController gauge;//HPゲージ
    public GaugeDirector hpDirector;//HPゲージディレクター
    private Momoya.Monster monster;//敵

    public GameObject hpCanvas;//HPUIのキャンバス
    public GameObject hpGaugePrehab;//HPゲージプレハブ
    public string m_enemyName; // 敵の名前
    

    // 初期化処理
    private void Start()
    {
        int length = m_enemyNum.Length;
        // 敵の数分回す
        for (int j = 0; j < length; j++)
        {
            for (int i = 0; i < m_enemyNum[j]; i++)
            {
                // 乱数の初期化
                int row = UnityEngine.Random.Range(1, m_rows);
                int column = UnityEngine.Random.Range(1, m_columns);
                // アイテムの描画
                Enemy(column, row, j);
                Debug.Log("へいひえひえひえひいいｈヴぉいｓでょｇふぃうおういお");
            }
        }
        Debug.Log("aaa");
    }

    
    // 敵の作成処理
    void Enemy(int row, int column, int arrayNum)
    {
        // その位置のブロックの中身を入れる
        UnityEngine.GameObject cube = UnityEngine.GameObject.Find(string.Format("AutoTile_{0}_{1}", row, column));

        // そのブロックが存在してたら
        if (cube != null)
        {
            // その位置の中にアイテムを入れる
            UnityEngine.GameObject enemy = UnityEngine.GameObject.Instantiate(m_enemyObject[arrayNum]);

            // 球の作成
            UnityEngine.GameObject sphere = UnityEngine.GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // 敵の位置の初期化
            Vector3 tilePositionInLocalSpace = new Vector3((row * m_tileWidth) + (m_tileWidth / 2), ((column + 1) * m_tileHeight) + (m_tileHeight / 2), 0);
            enemy.transform.position = transform.position + tilePositionInLocalSpace;

            // 球の位置の初期化
            sphere.transform.position = transform.position + tilePositionInLocalSpace;

            // サイズの初期化
            enemy.transform.localScale = new Vector3(m_tileWidth, m_tileHeight, 1);

            // サイズの初期化
            sphere.transform.localScale = new Vector3(m_tileWidth, m_tileHeight, 1);

            // 親子関係を結ぶ
            enemy.transform.parent = transform;

            // 親子関係を結ぶ
            sphere.transform.parent = enemy.transform;

            // 敵の名前の初期化
            enemy.name = string.Format(m_enemyName+arrayNum.ToString()+"_{0}_{1}", row + 1, column);

            // 球のなめの初期化
            sphere.name = string.Format(m_enemyName+arrayNum.ToString()+"_Sphere_{0}_{1}", row + 1, column);

            // マテリアルの初期化
            sphere.GetComponent<Renderer>().material = m_enemyMaterial[arrayNum];

            //HPゲージの生成
            GameObject hpObj = Instantiate(hpGaugePrehab) as GameObject;
            //HPUIキャンバスの子に設定
            hpObj.transform.SetParent(hpCanvas.transform, false);
            //ゲージのスクリプトを取得
            gauge = hpObj.GetComponent<GaugeController>();

            Debug.Log("Create");
            //リストに追加
            if (gauge)
            {
                hpDirector.addHPGauge(gauge);
            }
            else
            {
                Debug.Log("入ってないんですけど～");
            }

            //敵のレイヤーでモンスターにスクリプトを入れる
            if (enemy.layer == LayerMask.NameToLayer("Enemy1"))
            {
                monster = enemy.transform.GetComponent<EnemyController>();
            }
            if (enemy.gameObject.layer == LayerMask.NameToLayer("Enemy2"))
            {
                monster = enemy.transform.GetComponent<LoiterEnemyController>();

            }
            if (enemy.gameObject.layer == LayerMask.NameToLayer("Enemy3"))
            {
                monster = enemy.transform.GetComponent<MetalEnemyController>();

            }
            //リストに追加
            hpDirector.addMonster(monster);

            // レイヤーの追加
            sphere.layer = 11;

        }

        else
        {
            // 乱数の初期化
            int r = UnityEngine.Random.Range(1, m_rows);
            int c = UnityEngine.Random.Range(1, m_columns);
            // 敵の描画
            Enemy(c, r, arrayNum);
        }

    }
}
