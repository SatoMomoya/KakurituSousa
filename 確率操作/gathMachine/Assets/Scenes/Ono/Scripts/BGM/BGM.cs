using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

    [SerializeField]
    private string bgm;

	// Use this for initialization
	void Start () {


        AudioManager.Instance.PlayBGM(bgm);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
