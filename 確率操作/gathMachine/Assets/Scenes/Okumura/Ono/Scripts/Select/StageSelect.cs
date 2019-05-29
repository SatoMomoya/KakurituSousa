using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    Image m_sprite;
    Vector2Int m_size;
    Vector2Int m_index;
    Vector2Int m_minCount;
    public List<Sprite> m_spriteList;
    // Use this for initialization



    //-----------------------------------------------------------------
    //! @summary   初期化処理
    //!
    //! @parameter [void] なし
    //!
    //! @return    なし
    //-----------------------------------------------------------------
    void Start()
    {
        m_index = new Vector2Int(0, 0);
        m_minCount = new Vector2Int(20, 20);
        m_sprite = gameObject.GetComponent<Image>();
    }



    //-----------------------------------------------------------------
    //! @summary   更新処理
    //!
    //! @parameter [void] なし
    //!
    //! @return    なし
    //-----------------------------------------------------------------
    void Update()
    {
        SelectIndex();
        m_size = m_index;
    }



    //-----------------------------------------------------------------
    //! @summary   画面サイズの取得
    //!
    //! @parameter [void] なし
    //!
    //! @return    なし
    //-----------------------------------------------------------------
    Vector2Int GetSize()
    {
        return (m_size+ m_minCount) * 2;
    }


    //-----------------------------------------------------------------
    //! @summary   インデックスの選択
    //!
    //! @parameter [void] なし
    //!
    //! @return    なし
    //-----------------------------------------------------------------
    void SelectIndex()
    {
        // インデックスの選択
        if (Input.GetKeyDown(KeyCode.LeftArrow)) m_index.x--;
        if (Input.GetKeyDown(KeyCode.RightArrow)) m_index.x++;
        if (m_index.x >= m_spriteList.Count) m_index.x = 0;
        if (m_index.x < 0) m_index.x = m_spriteList.Count - 1;


        m_sprite.sprite = m_spriteList[m_index.x];
    }

}
