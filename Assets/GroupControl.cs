using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupControl : MonoBehaviour
{
    private GameObject[,] crowd;
    public int a;//for first index in 2D array
    public int b;//for second index in 2D array
    public Vector3 originPos;
    public float deltaX = 0f;//distance in x aixs between 2 rolls
    public float deltaY = 0f;//height delta betewen 2 rolls
    //both x and y change with the first index
    public float deltaZ = 0f;//distance in z axis between 2 rolls
    public GameObject peopleReference;
    public float gapTime = 0f;
    public List<GameObject> unselected;


    // Start is called before the first frame update
    void Start()
    {
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
        //Transform spawnTrans = peopleReference.GetComponent<Transform>();
        //spawnTrans.position = spawnPos;
        GameObject clone = Instantiate(peopleReference, spawnPos,Quaternion.identity);
        clone.GetComponent<Transform>().SetParent(this.gameObject.GetComponent<Transform>(), true);
        clone.GetComponent<PeopleBehavior>().standHeight = peopleReference.GetComponent<PeopleBehavior>().standHeight;
        clone.GetComponent<PeopleBehavior>().blockedTimeThreshhold = gapTime;
        clone.GetComponent<PeopleBehavior>().gp = this;
        crowd[i,j] = clone;
        unselected.Add(clone);

    }

    public void randomStandUp() 
    {
        

        int u = (int)Random.Range(0f, unselected.Count - 1);
        unselected[u].GetComponent<PeopleBehavior>().standUp();

        
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

    /*
    public void standuuup(int _c, int _d) 
    {
        this.gameObject.GetComponent<Transform>().GetChild(_c * b + _d).gameObject.GetComponent<PeopleBehavior>().standUp();
        
        if (_c >= a-1) return;

        standuuup(_c + 1, _d);
        if (_d == 0)
        {
            standuuup(_c + 1, _d + 1);
        }
        else if (_d == b - 1)
        {
            standuuup(_c + 1, _d - 1);
        }

        else 
        {
            standuuup(_c + 1, _d - 1);
            standuuup(_c + 1, _d + 1);
        }
        
    }
    */
    
}
