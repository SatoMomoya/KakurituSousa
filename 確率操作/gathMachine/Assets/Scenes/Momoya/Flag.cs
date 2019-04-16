using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//フラグのクラス

namespace Momoya
{
    public class Flag : MonoBehaviour
    {

        private uint flag;  //フラグ

        void Start()
        {
            flag = 0; //初期化
        }

        public void On(uint flag) //フラグを立てる
        {
            this.flag |= flag;
        }
       
        public void  Off(uint flag)//フラグを伏せる
        {
            this.flag &= ~flag;   
        }

        public bool Is(uint flag) //フラグの確認
        {
            return (this.flag & flag) != 0;
        }

    }
}