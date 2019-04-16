using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//オブジェクトクラス

namespace Momoya
{

    public class Object : MonoBehaviour
    {
        //定数の宣言
        public const int MinimumRarity = 1;         //最低限のレアリティ
        public const int MaximumRarity = 6;         //最大限のレアリティ
        public const int FallPoint = -5;            //落下ポイント
        //変数の宣言
        protected int rarity;     //モンスターのレアリティ
        [SerializeField]
        protected string name = "ななしくん";    //モンスターの名前
        protected Vector3 startPos; //モンスターの初期位置
        protected Vector3 pos;    //モンスターのポジション
        protected Vector3 vec;    //モンスターのベクトル
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
          //pos = transform.position;
        }

        //ポジションのプロパティ
        public Vector3 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        //ベクトルのプロパティ
        public Vector3 Vec
        {
            get { return vec; }
            set { vec = value; }
        }

        //名前のプロパティ
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        //レアリティをゲットとセットするためのプロパティ
        public int Rarity
        {
            get { return this.rarity; }
            set
            {
                //レアリティの最小値のチェック
                if (value < MinimumRarity)
                {
                    Debug.Log("想定されていたレアリティより低い数値を渡されました");
                    value = MinimumRarity;//レアリティを設定されている数値に変える
                }
                rarity = value;
            }

        }

    }
}
