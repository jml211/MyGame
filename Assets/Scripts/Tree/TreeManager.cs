using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TreeStage{
Seed=0,
Short=1,
Normal=2,
Tall=3,
Old=4
}

public class TreeManager : MonoBehaviour
{
    
    public TreeStage treeStage;

    public bool isDead;

    private void Awake()
    {
        SetCurrentStage();
    }
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(GetTimeObj());
    }

    IEnumerator GetTimeObj() {
        yield return new WaitUntil(() => TimeManager.instance != null);

            TimeManager.instance.ValueChanged += GrowUp;

       
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrowUp(object sender, EventArgs e) {
        if (!isDead)
        {
            if ((int)treeStage == 4)
            {
                transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).GetChild(((int)treeStage) - 1).gameObject.SetActive(false);
            }
            else
            {
                if ((int)treeStage == 0)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(1).GetChild(((int)treeStage) - 1).gameObject.SetActive(false);
                }

                transform.GetChild(1).GetChild(((int)treeStage)).gameObject.SetActive(true);

            }
            SetCurrentStage();
        }
        else
        {

        }
       
    }

    public void SetCurrentStage() {
        if (transform.GetChild(0).gameObject.activeInHierarchy)
        {
            treeStage = TreeStage.Seed;
        }
        int GrowIndex=-1;
        for (int i = 0; i < transform.Find("Grow").childCount; i++)
        {
            if (transform.Find("Grow").GetChild(i).gameObject.activeInHierarchy)
            {
                GrowIndex = i;
            }
        }
        switch (GrowIndex)
        {
            case 0:
                treeStage = TreeStage.Short;
                break;
            case 1:
                treeStage = TreeStage.Normal;
                break;
            case 2:
                treeStage = TreeStage.Tall;
                break;
            case 3:
                treeStage = TreeStage.Old;
                break;
            default:
                break;
        }
    }
}
