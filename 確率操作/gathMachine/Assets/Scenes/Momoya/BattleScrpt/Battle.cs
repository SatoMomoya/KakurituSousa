using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Momoya;
public class Battle : MonoBehaviour
{
  
    /// <summary>
    /// モンスターをバトルさせる関数
    /// </summary>
    /// <param name="attacker">攻撃側</param>
    /// <param name="defender">攻撃を受ける側</param>
    static public void MonsterBattle(Monster attacker, Monster defender)
    {
        defender.HP = defender.HP - (attacker.Attack + Random.Range(0, 3));
    }
}
