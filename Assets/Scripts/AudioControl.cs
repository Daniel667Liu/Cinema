using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{

    public AudioSource movieSound;
    public AudioSource crowdSound;
    
    private GroupControl gp;
    private int num;
    // Start is called before the first frame update
    void Start()
    {
        gp = FindObjectOfType<GroupControl>();
        num = gp.a * gp.b;
    }

    // Update is called once per frame
    void Update()
    {
        movieSound.volume = 1f - crowdSound.volume;
        crowdSound.volume = ((num-gp.unselected.Count)%num)*0.01f+0.05f;
    }

    public void peopelPlay() 
    {
        crowdSound.Play();
    }
}
