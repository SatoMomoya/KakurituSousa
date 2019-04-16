#if UNITY_EDITOR
namespace CBX.Unity.Editors.Editor
{
    using System;

    using CBX.Unity.Editors.Editor;



    using UnityEditor;


    using UnityEngine;


    [CustomEditor(typeof(TileMap))]
    public class TileMapEditor : Editor
    {

        // マウスの当たってる位置
        private Vector3 mouseHitPos;

        // エディタにシーンビューのイベントを処理
        private void OnSceneGUI()
        {

            // マウスの位置がタイルマップに当たってたら
            if (UpdateHitPosition())
            {
                // シーンビューの更新
                SceneView.RepaintAll();
            }

            // マウスの位置を計算して選択しているところを決める
            RecalculateMarkerPosition();

            // 現在のイベントへの参照を取得します
            Event current = Event.current;

            // マウスの位置がレイヤーの上だったら
            if (IsMouseOnLayer())
            {
                // マウスが押した瞬間もしくはスライド中だったら
                if (current.type == EventType.MouseDown || current.type == EventType.MouseDrag)
                {
                    // 押してるのが右ボタンだったら
                    if (current.button == 1)
                    {
                        // そのブロックを消す
                        Erase();
                        current.Use();
                    }
                    // 押してるのが左ボタンだったら
                    else if (current.button == 0)
                    {
                        // ブロックの描画処理
                        Draw();
                        current.Use();
                    }
                }
            }

            // シーンビューでUIチップを描画して、タイルの描画方法と消去方法をユーザーに通知します
            Handles.BeginGUI();
            GUI.Label(new Rect(10, Screen.height - 90, 100, 100), "LMB: Draw");
            GUI.Label(new Rect(10, Screen.height - 105, 100, 100), "RMB: Erase");
            Handles.EndGUI();

        }

        // 現在のツールを表示ツールに設定します
        private void OnEnable()
        {

            Tools.current = Tool.View;
            Tools.viewTool = ViewTool.FPS;

        }

        // ブロックの描画
        private void Draw()
        {

            // TileMapを取得
            TileMap map = (TileMap)target;

            // マウスの位置がどのタイルにあるか取得
            Vector2 tilePos = GetTilePositionFromMouseLocation();

            // その位置のブロックの中身を入れる
            GameObject cube = GameObject.Find(string.Format("Tile_{0}_{1}", tilePos.x, tilePos.y));

            // そのブロックが存在してたら返す
            if (cube != null && cube.transform.parent != map.transform)
            {
                return;
            }

            //　nullだったら
            if (cube == null)
            {
                // ブロックを作成する
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            }

            // ブロックの位置の初期化
            Vector3 tilePositionInLocalSpace = new Vector3((tilePos.x * map.m_tileWidth) + (map.m_tileWidth / 2), (tilePos.y * map.m_tileHeight) + (map.m_tileHeight / 2));
            cube.transform.position = map.transform.position + tilePositionInLocalSpace;

            // サイズの初期化
            cube.transform.localScale = new Vector3(map.m_tileWidth, map.m_tileHeight, 1);

            // 親子関係を結ぶ
            cube.transform.parent = map.transform;

            // マテリアルの初期化
            cube.GetComponent<Renderer>().material = map.m_material;

            // ブロックの名前の初期化
            cube.name = string.Format("Tile_{0}_{1}", tilePos.x, tilePos.y);

            // タグの追加
            cube.tag = map.m_tagName;

        }

        // ブロックを消す処理
        private void Erase()
        {

            // TileMapを取得
            TileMap map = (TileMap)target;

            // マウスの位置がどのタイルにあるか取得
            Vector2 tilePos = GetTilePositionFromMouseLocation();

            // 当たってるタイルを取得
            GameObject cube = GameObject.Find(string.Format("Tile_{0}_{1}", tilePos.x, tilePos.y));

            // ブロックの中がなかったら
            if (cube != null && cube.transform.parent == map.transform)
            {
                // そのブロックを消す
                UnityEngine.Object.DestroyImmediate(cube);
            }

        }

        // マウスの位置がどのタイルにあるか取得処理
        private Vector2 GetTilePositionFromMouseLocation()
        {

            // TileMapを取得s
            TileMap map = (TileMap)target;

            // マウスの当たってる位置から列と行の位置
            Vector3 pos = new Vector3(mouseHitPos.x / map.m_tileWidth, mouseHitPos.y / map.m_tileHeight, map.transform.position.z);

            // 四捨五入する
            pos = new Vector3((int)Math.Round(pos.x, 5, MidpointRounding.ToEven), (int)Math.Round(pos.y, 5, MidpointRounding.ToEven), 0);
            
            int col = (int)pos.x;
            int row = (int)pos.y;

            // 超えたら最大を入れる
            if (row < 0)row = 0;
            if (row > map.m_rows - 1) row = map.m_rows - 1;
            if (col < 0)col = 0;
            if (col > map.m_columns - 1) col = map.m_columns - 1;

            // 当たってる列と行を返す
            return new Vector2(col, row);

        }

        // マウスの位置がマップに当たってるかの処理
        private bool IsMouseOnLayer()
        {

            // TileMapの取得
            TileMap map = (TileMap)target;

            // 当たってたらtrueを返す
            if (mouseHitPos.x > 0 && mouseHitPos.x < (map.m_columns * map.m_tileWidth) &&
                   mouseHitPos.y > 0 && mouseHitPos.y < (map.m_rows * map.m_tileHeight))
                return true;

            // 違ったらfalseを返す
            return false;

        }

        // マウスの位置がどのブロックに当たってるか計算する処理
        private void RecalculateMarkerPosition()
        {

            // TileMapの取得
            TileMap map = (TileMap)target;

            // 当たってるブロックの場所を取得
            Vector2 tilepos = GetTilePositionFromMouseLocation();

            // 当たってるブロックの位置を取得
            Vector3 pos = new Vector3(tilepos.x * map.m_tileWidth, tilepos.y * map.m_tileHeight, 0);

            // 選択してるブロックを代入
            map.m_selectPosition = map.transform.position + 
                new Vector3(pos.x + (map.m_tileWidth / 2), pos.y + (map.m_tileHeight / 2), 0);

        }

        // マウスの位置がタイルマップに当たってたら
        private bool UpdateHitPosition()
        {

            // TileMapの取得
            TileMap map = (TileMap)target;

            // 平面オブジェクトを作る
            Plane p = new Plane(map.transform.TransformDirection(Vector3.forward), map.transform.position);

            // 現在のマウス位置からレイタイプを構築する
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

            // 当たってる位置を取得
            Vector3 hit = new Vector3();

            // 当たってる位置までの距離
            float dist;

            // 平面と交差する場所を特定するために光線を投影する
            if (p.Raycast(ray, out dist))
            {
                // 光線が平面に当たるので、ワールド空間での当たる位置を計算します。
                hit = ray.origin + (ray.direction.normalized * dist);
            }

            // 当たってる位置をワールド空間からローカル空間に変換する
            Vector3 value = map.transform.InverseTransformPoint(hit);

            // 値が異なる場合は、現在のマウスのヒット位置がtrue 
            if (value != mouseHitPos)
            {
                mouseHitPos = value;
                return true;
            }
            
            return false;
        }

    }

}
#endif

