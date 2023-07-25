using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCell : MonoBehaviour
{
    public GameObject CellObject;
    public int num_cells;
    public float time_spawn = 0.5f;
    public float rango;
    private Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)(Time.time * 1000));
        StartCoroutine(repeat_spawn());
    }
    IEnumerator repeat_spawn()
    {
        for (int i = 0; i < this.num_cells; i++)
        {
            yield return new WaitForSeconds(this.time_spawn);
            spawn();
        }
        Destroy(gameObject);
    }
    public void spawn()
    {
        this.center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject a = Instantiate(this.CellObject) as GameObject;
        
        float modulo = Random.Range(0.0f,(float)this.rango);
        float angulo = Random.Range(0.0f, 2*UnityEngine.Mathf.PI);

        Vector3 pos = new Vector3(modulo * UnityEngine.Mathf.Cos(angulo) + this.center.x, this.center.y, modulo * UnityEngine.Mathf.Sin(angulo) + this.center.z);
        a.tag = "cells";
        a.transform.position = pos;
        a.transform.localScale = new Vector3(1f,1f,1f);
        a.GetComponent<GrowthSCerevisiae>().InitialCell = true;
        a.GetComponent<GrowthSCerevisiae>().FullGrown = true;
        //a.GetComponent<GrowthSCerevisiae>().BeginGrowth();
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,this.rango);
    }
}