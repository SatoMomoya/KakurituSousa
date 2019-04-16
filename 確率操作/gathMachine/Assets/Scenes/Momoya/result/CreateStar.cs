using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStar : MonoBehaviour
{
    [SerializeField]
    int maxRarity = 5;

    public Momoya.Player player;
    public Momoya.SavePlayer load;
    public GameObject star001;
    public GameObject star002;
    [SerializeField]
    float width;
    bool geneFlag;

    float createtime = 3.0f;
    float time;
   
    // Start is called before the first frame update
    void Start()
    {
        geneFlag = false;   
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(geneFlag)
        {
            return;
        }
        if (time > createtime)
        {

                for (int i = 0; i < maxRarity; i++)
                {
                    if (i < RaritySave.rarity)
                    {
                        GameObject go = Instantiate(star001) as GameObject;
                        go.transform.position = new Vector3(transform.position.x + (i * width), transform.position.y, transform.position.z);

                    }
                    else
                    {
                        GameObject go = Instantiate(star002) as GameObject;
                        go.transform.position = new Vector3(transform.position.x + (i * width), transform.position.y, transform.position.z);

                    }

                }
                geneFlag = true;
            }
        
    }
}
