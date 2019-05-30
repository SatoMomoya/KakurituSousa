//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   PlayerCameraController.cs
//!
//! @brief  プレイヤーカメラコントローラー関連のC#ファイル
//!
//! @date   2019/3/10
//!
//! @author オクムラ イヤゴ
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

// 名前空間の使用 ==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------------
//!
//! @brief プレイヤーカメラコントローラー
//!
//----------------------------------------------------------------------
public class PlayerCameraController : MonoBehaviour
{
    Vector3 m_playerPos; // プレイヤーの位置
    public UnityEngine.GameObject m_player; // プレイヤー
    public float m_leftMaxPos;  // 左の最大値
    public float m_rightMaxPos; // 右の最大値
    public float m_cameraTop;   // カメラ
    public float m_velocity;    // 速度
    Vector3 m_cameraPos;        // カメラの位置
    bool m_playerDistanceFlag;  // プレイヤーとの距離のフラグ
    public float m_cameraMaxDistance; // カメラの最大距離
    public UnityEngine.GameObject m_autoStage; // ステージ
    float m_stageZPos;　// の位置
    int m_stageChild;  // ステージの子の数
    public int m_waitMaxCount;   // 待つ最大カウント数
    int m_waitCount; // 待つカウント数
    bool m_startFlag; // フラグ

    private Momoya.Player player;

    public GameObject goalCapsel;
    private CapselHit capselHit;
    // 初期化処理
    void Start()
    {
        // プレイヤーの位置の初期化
        m_playerPos = m_player.transform.localPosition;

        // カメラの位置
        m_cameraPos = m_playerPos;

        // フラグの初期化
        m_playerDistanceFlag = false;

        // ステージの子の数の取得
        m_stageChild = m_autoStage.transform.childCount;

        // カウントの初期化
        m_waitCount = 0;

        // フラグの初期化
        m_startFlag = false;

        player = m_player.GetComponent<Momoya.Player>();

        capselHit = goalCapsel.GetComponent<CapselHit>();
    }

    // 更新処理
    void Update()
    {

        if(!player.GoalFlag())
        {
            //Debug.Log(m_stageChild + "この数");
            if (m_startFlag == false)
            {
                m_stageChild = m_autoStage.transform.childCount;
                m_startFlag = true;
            }

            // プレイヤーの位置の更新
            m_playerPos = m_player.transform.localPosition;

            // プレイヤーの位置が左の最大値を超えたら
            if (m_playerPos.x < m_leftMaxPos)
            {
                // 最大値にする
                this.transform.localPosition = new Vector3(m_leftMaxPos, m_cameraPos.y + m_cameraTop, this.transform.localPosition.z);
            }
            else
            {
                // プレイヤーの位置が右の最大値を超えたら
                if (m_playerPos.x > m_rightMaxPos)
                {
                    // 最大値にする
                    this.transform.localPosition = new Vector3(m_rightMaxPos, m_cameraPos.y + m_cameraTop, this.transform.localPosition.z);
                }
                else
                {
                    // プレイヤーを中心にする
                    this.transform.localPosition = new Vector3(m_playerPos.x, m_cameraPos.y + m_cameraTop, this.transform.localPosition.z);
                }
            }

            // カメラのY位置がプレイヤーの位置より低かったら
            if (m_cameraPos.y < m_playerPos.y)
            {
               
                    // スクロールする
                    m_cameraPos.y += -m_velocity;
                
                
                // カメラとプレイヤーの距離を取る
                float d = m_playerPos.y - m_cameraPos.y;

                // プレイヤーがカメラ外になったら
                if (d > m_cameraMaxDistance)
                {
                    if (m_playerDistanceFlag == false)
                    {
                        m_autoStage.transform.GetChild(0).gameObject.AddComponent<BackMove>();
                    }
                    // フラグを変える
                    m_playerDistanceFlag = true;

                }
                else
                {
                    // m_playerDistanceFlag = false;
                }

            }
            else
            {
                // プレイヤーのY位置にする
                m_cameraPos.y = m_playerPos.y;
            }

            // ステージを動かす
            // MoveStage(m_playerDistanceFlag);

            if (m_playerDistanceFlag)
            {
                m_waitCount++;
                if (m_waitCount > m_waitMaxCount)
                {
                    if (m_stageChild > 0)
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            m_autoStage.transform.GetChild(m_stageChild - 1).gameObject.AddComponent<BackMove>();
                            m_stageChild--;
                        }
                        m_waitCount = 0;
                    }

                }
            }
        }
        else
        {
            
        }
        
    }

    // ステージを動かす処理
    void MoveStage(bool flag)
    {
        // フラグがtrueだったら
        if (flag)
        {

            // 
            m_stageZPos += m_velocity;
            // ステージを変える
            m_autoStage.transform.localPosition = new Vector3(
                m_autoStage.transform.localPosition.x,
                m_autoStage.transform.localPosition.y,
                m_autoStage.transform.localPosition.z + m_stageZPos);
        }
    }

    public bool GetFlag()
    {
        return m_playerDistanceFlag;

} 
}
