using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GaugeDirector : MonoBehaviour
{
    private List<Momoya.Monster> monsterList;
    private List<GaugeController> hpGaugeList;
    // Start is called before the first frame update
    void Start()
    {
        monsterList = new List<Momoya.Monster>();
        hpGaugeList = new List<GaugeController>();

        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            hpGaugeList[i].SetMonster(monsterList[i]);
        }
    }

    //モンスターリストにモンスターを追加
    public void addMonster(Momoya.Monster monster)
    {
        if (monster) monsterList.Add(monster);
    }
    //HPゲージオブジェクトを追加
    public void addHPGauge(GaugeController hpGauge)
    {
        if (hpGauge) hpGaugeList.Add(hpGauge);
    }

   
}
