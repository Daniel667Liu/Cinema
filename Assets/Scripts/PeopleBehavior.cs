using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleBehavior : MonoBehaviour
{
    private float sitPositionY;//the position Y for each human when they are sitting
    private float standPositionY;
    public float standHeight = 1f;//the Y delta between sitting and standing
    [HideInInspector]
    public float blockedTimeThreshhold = 1f;
    private bool stand = false;
    [HideInInspector]
    public GameObject block1;
    [HideInInspector]
    public GameObject block2;
    [HideInInspector]
    public GameObject block3;
    [HideInInspector]
    public GroupControl gp;
    [HideInInspector]
    public int area;
    public Material sitMat;
    public Material standMat;
    private AudioSource audio;
    private bool clickable = false;
    //public Vector2Int test;

    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        sitPositionY = this.gameObject.GetComponent<Transform>().position.y;
        standPositionY = sitPositionY + standHeight;//record the initial sitting height and standing height
        gameObject.GetComponent<MeshRenderer>().material = sitMat;
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       



    }


    public void sitDown() 
    {

        
        if (clickable) 
        {
            //this.gameObject.GetComponent<Transform>().position = new Vector3(this.gameObject.GetComponent<Transform>().position.x, sitPositionY, this.gameObject.GetComponent<Transform>().position.z);
            gameObject.GetComponent<MeshRenderer>().material = sitMat;
            stand = false;
            gp.unselected.Add(this.gameObject);
            clickable = false;
        }
        
       
    }

    public void standUp() 
    {

        if (stand) 
        {
            return;
        }

        stand = true;
        coroutine = WaitAndStand(blockedTimeThreshhold);
        StartCoroutine(coroutine);

        

        

    }

    private IEnumerator WaitAndStand(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            gp.unselected.Remove(this.gameObject);
            clickable = true;
            //this.gameObject.GetComponent<Transform>().position = new Vector3(this.gameObject.GetComponent<Transform>().position.x, standPositionY, this.gameObject.GetComponent<Transform>().position.z);
            gameObject.GetComponent<MeshRenderer>().material = standMat;
            audio.Play();

            if (block1 != null && block2 != null && block3 != null)
            {
                block1.GetComponent<PeopleBehavior>().standUp();
                block2.GetComponent<PeopleBehavior>().standUp();
                block3.GetComponent<PeopleBehavior>().standUp();
            }
            else if (block1!=null&&block2!=null) 
            {
                block1.GetComponent<PeopleBehavior>().standUp();
                block2.GetComponent<PeopleBehavior>().standUp();
            }
            else if (block3 != null && block2 != null) 
            {
                block3.GetComponent<PeopleBehavior>().standUp();
                block2.GetComponent<PeopleBehavior>().standUp();
            }
            
            //Debug.Log(test);
            
            

            StopCoroutine(coroutine);
        }
    }
    /*
    public void standControl() 
    {
        stand = true;
    }
    */

    

   
}
