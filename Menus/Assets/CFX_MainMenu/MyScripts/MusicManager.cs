using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioSource src;
    public AudioClip Musicclip1;
    public AudioClip Musicclip2;
    public AudioClip Musicclip3;
    // Use this for initialization
    void Awake()
    {
        src.clip = Musicclip2;
        src.Play();
    }
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Music1On ()
    {
        src.Stop();
        src.clip = Musicclip1;
        src.Play();
    }
}
