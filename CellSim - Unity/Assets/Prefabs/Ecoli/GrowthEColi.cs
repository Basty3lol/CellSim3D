using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthEColi : MonoBehaviour
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
    private float MaxGrowthTime;
    private float DetachParentTime;
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
        this.MaxGrowthTime = this.DataController.GetComponent<CellDataController>().GetEColiRandomTime();
        this.DetachParentTime = this.MaxGrowthTime;
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
        son.GetComponent<GrowthEColi>().InitialCell = false;
        son.GetComponent<GrowthEColi>().DeltaTime = this.DeltaTime;
        son.GetComponent<GrowthEColi>().FullGrown = false;
        son.GetComponent<GrowthEColi>().parent = this.gameObject;

        son.transform.parent = transform;
        son.transform.localPosition = new Vector3(0f, 0f, 0f);
        son.transform.rotation = son.transform.parent.rotation;
        
        son.GetComponent<GrowthEColi>().ActualPosition = new Vector3(0f, 0f, 0f);
        son.GetComponent<GrowthEColi>().Direction = new Vector3(0f, 1f, 0f);
        son.GetComponent<GrowthEColi>().DataController = this.DataController;
        son.GetComponent<GrowthEColi>().CellInit();
        son.GetComponent<GrowthEColi>().BeginGrowth();
    }

    void Growth()
    {
        float ATime = this.ActualTime + this.DeltaTime;

        if(ATime > this.MaxGrowthTime)
        {
            ATime = this.MaxGrowthTime;
        }
        this.ActualTime = ATime;
        
        float dRadius = 2f*(this.DeltaTime/this.MaxGrowthTime);
        
        Vector3 delta_pos = Direction*dRadius;
        Vector3 newPos = this.ActualPosition + delta_pos;

        this.ActualPosition = newPos;
        this.gameObject.transform.localPosition = newPos;
        
        if(this.ActualTime >= this.MaxGrowthTime)
        {   
            this.transform.parent = null;
            this.parent.GetComponent<GrowthEColi>().BeginGrowth();
            this.parent = null;

            this.gameObject.AddComponent<Rigidbody>();
            this.gameObject.GetComponent<Rigidbody>().mass = 50f;
            this.gameObject.GetComponent<Rigidbody>().drag = 0.1f;
            this.gameObject.GetComponent<Rigidbody>().angularDrag = 0.5f;

            this.isGrowing = false;
            this.FullGrown = true;
            this.gameObject.tag = "cells";
            this.BeginGrowth();
        }
    }
    
}