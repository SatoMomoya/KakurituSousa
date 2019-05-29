using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip sound;
    private AudioSource audioSound;
    int time;
    bool flag;
    // Start is called before the first frame update
    void Start()
    {
        audioSound = gameObject.GetComponent<AudioSource>();
        time = 0;
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
      
       

        if (time == 340)
        {
            audioSound.PlayOneShot(sound);
        }
       

        {
            time += 1;
        }
    }
}
