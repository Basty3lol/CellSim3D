using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthSCerevisiae : MonoBehaviour
{
    public GameObject CellType;
    public bool InitialCell = false;
    public bool FullGrown;
    public float DeltaTime = 0.5f;
    
    private GameObject DataController;
    private GameObject parent = null;

    private Vector3 ActualPosition;
    private Vector3 Direction;
    private bool isGrowing = false;
    private float MaxGrowthTime = 100f;
    private float DetachParentTime = 70f;
    private float ActualTime = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        if(this.InitialCell)
        {
            this.DataController = GameObject.FindGameObjectsWithTag("Controller")[0];
            StartCoroutine(LStart());
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (this.isGrowing)
        {
            Growth();
        }
    }

    IEnumerator LStart()
    {
        while(!this.DataController.GetComponent<CellDataController>().isReady())
        {
            yield return new WaitForSeconds(0.5f);
        }
        
        if(this.InitialCell)
        {
            CellInit();
            BeginGrowth();
        }
    }

    void CellInit()
    {
        this.MaxGrowthTime = this.DataController.GetComponent<CellDataController>().GetSCerevisiaeRandomTime();
        this.DetachParentTime = this.MaxGrowthTime*0.7f;
    }

    public void BeginGrowth()
    {
        if(this.FullGrown)
        {
            CreateSon();
        }
        else
        {
            this.isGrowing = true;
            //StartCoroutine(mitosis());
        }
    }

    void CreateSon()
    {
        GameObject son = Instantiate(this.CellType) as GameObject;
        Destroy(son.GetComponent<Rigidbody>());
        son.gameObject.tag = "Untagged";
        son.GetComponent<GrowthSCerevisiae>().InitialCell = false;
        son.GetComponent<GrowthSCerevisiae>().DeltaTime = this.DeltaTime;
        son.GetComponent<GrowthSCerevisiae>().FullGrown = false;
        son.GetComponent<GrowthSCerevisiae>().parent = this.gameObject;

        Vector3 position = Random.insideUnitSphere.normalized;
        
        son.transform.localScale = new Vector3(0f,0f,0f);
        son.transform.parent = transform;
        son.transform.localPosition = position;
        

        son.GetComponent<GrowthSCerevisiae>().ActualPosition = position* 0.25f;
        son.GetComponent<GrowthSCerevisiae>().Direction = position;
        son.GetComponent<GrowthSCerevisiae>().DataController = this.DataController;
        son.GetComponent<GrowthSCerevisiae>().CellInit();
        son.GetComponent<GrowthSCerevisiae>().BeginGrowth();
    }

    void Growth()
    {
        float ATime = this.ActualTime + this.DeltaTime;

        if(ATime > this.MaxGrowthTime)
        {
            ATime = this.MaxGrowthTime;
        }
        this.ActualTime = ATime;

        float scale = ATime/this.MaxGrowthTime;
        this.transform.localScale = new Vector3(scale, scale, scale);
        
        if(this.parent != null)
        {   
            float dRadius = 0.55f*(this.DeltaTime/this.DetachParentTime);
            
            Vector3 delta_pos = Direction*dRadius;
            Vector3 newPos = this.ActualPosition + delta_pos;

            this.ActualPosition = newPos;
            this.gameObject.transform.localPosition = newPos;
            
            if(this.ActualTime >= this.DetachParentTime)
            {
                this.transform.parent = null;
                this.parent.GetComponent<GrowthSCerevisiae>().BeginGrowth();
                this.parent = null;

                this.gameObject.AddComponent<Rigidbody>();
                this.gameObject.GetComponent<Rigidbody>().mass = 5f;
                this.gameObject.GetComponent<Rigidbody>().drag = 0.1f;
                this.gameObject.GetComponent<Rigidbody>().angularDrag = 1f;
            }
        }
        else
        {
            if(this.ActualTime >= this.MaxGrowthTime)
            {
                this.isGrowing = false;
                this.FullGrown = true;
                this.gameObject.tag = "cells";
                this.BeginGrowth();
            }
        }
    }
    
}