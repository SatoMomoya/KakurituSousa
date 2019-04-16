//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   TileMap.cs
//!
//! @brief  ゲーム関連のC++ファイル
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
//! @brief タイルマップ
//!
//----------------------------------------------------------------------
public class TileMap : MonoBehaviour
{
    public int m_rows = 10;   // タイルの行数
    public int m_columns = 20;　// タイルの列数
    public float m_tileWidth = 1;　// タイル幅
    public float m_tileHeight = 1;　// タイル高さ
    public Color m_selectLineColor = Color.red; // 選択している所の線の色
    public Color m_outLineColor = Color.blue; // 外の線の色
    public Color m_mapLineColor = Color.white; // マップの線の色
    public Material m_material; // マテリアル
    public string m_tagName; // タグ
    [HideInInspector]
    public Vector3 m_selectPosition; // 選択位置

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
            position + new Vector3(0, m_rows * m_tileHeight , 0));
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
        // 選択しているところの色を決める
        Gizmos.color = m_selectLineColor;
        // 選択している所に資格を書く
        Gizmos.DrawWireCube(m_selectPosition, new Vector3(m_tileWidth, m_tileHeight, 1) * 1.1f);
    }
}
