//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   BackMove.cs
//!
//! @brief  オブジェクトを後ろに引く関連のC#ファイル
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
//! @brief オブジェクトを後ろに引く
//!
//----------------------------------------------------------------------
public class BackMove : MonoBehaviour
{
    float m_posZ; // Zの位置
    float m_vel; // 移動速度

    // 初期化処理
    void Start()
    {
        // 位置の初期化
        m_posZ = this.transform.localPosition.z;

        // 速度の初期化
        m_vel = 0.5f;
    }

    //更新処理
    void Update()
    {
        // 速度を足す
        m_posZ += m_vel;

        // 位置をずらす
        this.transform.localPosition = new Vector3(
                this.transform.localPosition.x,
                this.transform.localPosition.y,
                m_posZ);
    }
}
