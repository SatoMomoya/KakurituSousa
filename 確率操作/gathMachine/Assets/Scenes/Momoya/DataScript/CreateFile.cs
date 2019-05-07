using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateFile : MonoBehaviour
{
    private string dataFile = "";
    private string dataCsv = "";

    private void Awake()
    {
        string txt = Application.dataPath;
        string txt2 = Application.persistentDataPath + "\\Data";
        dataFile =  txt2;
        dataCsv = txt2 + "\\PlayerData.csv";
        if(Directory.Exists(dataFile))
        {
           
        }
        else
        {
            Directory.CreateDirectory(dataFile);
            File.Create(dataCsv);
        }
       
    

    }

    // Start is called before the first frame update
    void Start()
    {
   
        //dataCsv = dataFile + "\\PlayerData.csv";


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
