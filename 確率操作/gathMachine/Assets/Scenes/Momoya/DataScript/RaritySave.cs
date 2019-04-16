using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaritySave : MonoBehaviour
{
    [SerializeField]
    Momoya.Player player;

    
    public static int rarity = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GoalFlag())
        {
            rarity = player.Rarity;
        }
    }
}
