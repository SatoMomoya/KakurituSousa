//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   ItemCacthTec.cs
//!
//! @brief  アイテムキャッチ関連のC++ファイル
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
//! @brief アイテムキャッチ
//!
//----------------------------------------------------------------------
public class ItemCacthTec : MonoBehaviour
{
    
    public UnityEngine.GameObject m_itemButton; // アイテムボタン

    // 初期化処理
    void Start()
    {
           
    }

    // 更新処理
    void Update()
    {
        // 下キーを押した瞬間
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // フラグを変える
            m_itemButton.GetComponent<Animator>().SetBool("ButtonFlag", false);
        }
    }

    // 当たらり判定
    public void OnTriggerStay(Collider other)
    {
        // アイテムだったら
        if (other.transform.tag == "Item")
        {
            // フラグを変える
            m_itemButton.GetComponent< Animator >().SetBool("ButtonFlag", true);
        }
    }
    // 当たり判定
    public void OnTriggerExit(Collider other)
    {
        // アイテムだったら
        if (other.transform.tag == "Item")
        {
            // フラグを変える
            m_itemButton.GetComponent<Animator>().SetBool("ButtonFlag", false);
        }
    }
}
