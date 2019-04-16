using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneLateItem : MonoBehaviour
{
    public List<GameObject> geneItem;
    [SerializeField]
    uint geneCount;
    // Start is called before the first frame update
    void Start()
    {


        for(int i = 0;i < geneCount;i++)
        {
            int geneType = Random.Range(0, geneItem.Count);
            Vector3 pos = new Vector3(Random.Range(-5, 5), 0, 0);
            GameObject go = Instantiate(geneItem[geneType]) as GameObject;
            go.transform.position = pos;
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
