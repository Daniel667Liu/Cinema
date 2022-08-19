using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupControl : MonoBehaviour
{
    public GameObject peopleReference;
    public GameObject chairReference;
    private GameObject[,] crowd;
    public int a;//for first index in 2D array
    public int b;//for second index in 2D array
    private Vector3 originPos;
    public float deltaX = 0f;//distance in x aixs between 2 rolls
    public float deltaY = 0f;//height delta betewen 2 rolls
    //both x and y change with the first index
    public float deltaZ = 0f;//distance in z axis between 2 rolls
    
    public float gapTime = 0f;
    public int areaContolling = 1;
    [HideInInspector]
    public List<GameObject> unselected;
    


    // Start is called before the first frame update
    void Start()
    {
        originPos = gameObject.GetComponent<Transform>().position;
        crowd = new GameObject[a, b];
        spawnAllPeople();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            //spawnAllPeople();
            randomStandUp();
        }

        if (Input.GetMouseButtonDown(0))
        {



           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);
            if (hit.collider != null) 
            {
                if (hit.collider.gameObject.GetComponent<PeopleBehavior>().area==areaContolling) 
                {
                    if (hit.collider.gameObject.CompareTag("people"))
                    {
                        hit.collider.gameObject.GetComponent<PeopleBehavior>().sitDown();
                    }
                }
                
            }
            


        }
    }

    public void spawnAllPeople() 
    {
        for (int i=0;i<a;i++) 
        {
            for (int j = 0; j < b; j++) 
            {
                spawnPeople(i, j);
            }
        }
        setSeat();
    }


    public void spawnPeople(int i, int j) 
    {
        Vector3 spawnPos = originPos + new Vector3(j * deltaX, i * deltaY, i * deltaZ);
        GameObject clone = Instantiate(peopleReference, spawnPos,Quaternion.identity);
        clone.transform.rotation = peopleReference.GetComponent<Transform>().rotation;
        clone.transform.localScale = peopleReference.GetComponent<Transform>().localScale;
        clone.GetComponent<Transform>().SetParent(this.gameObject.GetComponent<Transform>(), true);
        clone.GetComponent<PeopleBehavior>().standHeight = peopleReference.GetComponent<PeopleBehavior>().standHeight;
        clone.GetComponent<PeopleBehavior>().blockedTimeThreshhold = gapTime;
        clone.GetComponent<PeopleBehavior>().gp = this;
        clone.GetComponent<PeopleBehavior>().area = areaContolling;
        
        crowd[i,j] = clone;
        unselected.Add(clone);
        //clone.GetComponent<PeopleBehavior>().test = new Vector2Int(i, j);


        
        Vector3 spawnPos1 = originPos + new Vector3(j * deltaX, i * deltaY, i * deltaZ+0.3f);
        GameObject chair = Instantiate(chairReference, spawnPos1, Quaternion.identity);
        chair.transform.rotation = chairReference.GetComponent<Transform>().rotation;
        chair.transform.localScale = chairReference.GetComponent<Transform>().localScale;
        chair.GetComponent<Transform>().SetParent(clone.GetComponent<Transform>(), true);
        
    }

    public void randomStandUp() 
    {

        if (unselected.Count > 0) 
        {
            int u = (int)Random.Range(0f, unselected.Count - 1);
            unselected[u].GetComponent<PeopleBehavior>().standUp();
        }
        

        
    }

    public void setSeat() 
    {
        for (int i = 0; i < a; i++)
        {
            for (int j = 0; j < b; j++)
            {
                if (i == a - 1)
                {
                    crowd[i,j].GetComponent<PeopleBehavior>().block1 = null;
                    crowd[i, j].GetComponent<PeopleBehavior>().block2 = null;
                    crowd[i, j].GetComponent<PeopleBehavior>().block3 = null;
                }

                else if (j == 0)
                {
                    crowd[i, j].GetComponent<PeopleBehavior>().block1 = null;
                    crowd[i, j].GetComponent<PeopleBehavior>().block2 = this.gameObject.GetComponent<Transform>().GetChild((i + 1) * b).gameObject;
                    crowd[i, j].GetComponent<PeopleBehavior>().block3 = this.gameObject.GetComponent<Transform>().GetChild((i + 1) * b + 1).gameObject;
                }

                else if (j == b - 1)
                {
                    crowd[i, j].GetComponent<PeopleBehavior>().block1 = this.gameObject.GetComponent<Transform>().GetChild((i + 1) * b + j - 1).gameObject;
                    crowd[i, j].GetComponent<PeopleBehavior>().block2 = this.gameObject.GetComponent<Transform>().GetChild((i + 1) * b + j).gameObject;
                    crowd[i, j].GetComponent<PeopleBehavior>().block3 = null;
                }





                else
                {
                    crowd[i, j].GetComponent<PeopleBehavior>().block1 = this.gameObject.GetComponent<Transform>().GetChild((i + 1) * b + j - 1).gameObject;
                    crowd[i, j].GetComponent<PeopleBehavior>().block2 = this.gameObject.GetComponent<Transform>().GetChild((i + 1) * b + j).gameObject;
                    crowd[i, j].GetComponent<PeopleBehavior>().block3 = this.gameObject.GetComponent<Transform>().GetChild((i + 1) * b + j + 1).gameObject;
                }
            }
        }
    }

   
    
}
