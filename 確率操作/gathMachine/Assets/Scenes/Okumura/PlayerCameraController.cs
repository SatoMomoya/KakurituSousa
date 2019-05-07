//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   PlayerCameraController.cs
//!
//! @brief  プレイヤーカメラコントローラー関連のC++ファイル
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
    public UnityEngine.GameObject m_player; // プレイヤーs
    public float m_leftMaxPos;  // 左の最大値
    public float m_rightMaxPos; // 右の最大値
    public float m_cameraTop;   // カメラ


    // 初期化処理
    void Start()
    {
        // プレイヤーの位置の初期化
        m_playerPos = m_player.transform.localPosition;
    }

    // 更新処理
    void Update()
    {
        // プレイヤーの位置の更新
        m_playerPos = m_player.transform.localPosition;

        // プレイヤーの位置が左の最大値を超えたら
        if (m_playerPos.x < m_leftMaxPos)
        {
            // 最大値にする
            this.transform.localPosition = new Vector3(m_leftMaxPos, m_playerPos.y+ m_cameraTop, this.transform.localPosition.z);
        }
        else
        {
            // プレイヤーの位置が右の最大値を超えたら
            if (m_playerPos.x > m_rightMaxPos)
            {
                // 最大値にする
                this.transform.localPosition = new Vector3(m_rightMaxPos, m_playerPos.y+ m_cameraTop, this.transform.localPosition.z);
            }
            else
            {
                // プレイヤーを中心にする
                this.transform.localPosition = new Vector3(m_playerPos.x, m_playerPos.y+ m_cameraTop, this.transform.localPosition.z);
            }
        }
    }
}
