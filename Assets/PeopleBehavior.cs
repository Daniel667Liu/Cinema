using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleBehavior : MonoBehaviour
{
    private float sitPositionY;//the position Y for each human when they are sitting
    private float standPositionY;
    
    
    public float standHeight = 1f;//the Y delta between sitting and standing
    


    //private float blockedTime = 0f;
    public float blockedTimeThreshhold = 1f;

    private bool stand = false;

    public GameObject block1;
    public GameObject block2;
    public GameObject block3;

    public GroupControl gp;

    public Vector2Int test;

    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        sitPositionY = this.gameObject.GetComponent<Transform>().position.y;
        standPositionY = sitPositionY + standHeight;//record the initial sitting height and standing height

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);
            if (hit.collider.gameObject.CompareTag("people")) 
            {
                hit.collider.gameObject.GetComponent<PeopleBehavior>().sitDown();
            }
        }
        
        
    }


    public void sitDown() 
    {
        
        this.gameObject.GetComponent<Transform>().position 
            = new Vector3(this.gameObject.GetComponent<Transform>().position.x, sitPositionY, this.gameObject.GetComponent<Transform>().position.z);
        stand = false;
        gp.unselected.Add(this.gameObject);
        // blockedTime = 0f;
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

            this.gameObject.GetComponent<Transform>().position
            = new Vector3(this.gameObject.GetComponent<Transform>().position.x, standPositionY, this.gameObject.GetComponent<Transform>().position.z);
            
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
            
            Debug.Log(test);
            
            

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
