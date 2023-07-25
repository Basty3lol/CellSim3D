using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_child : MonoBehaviour
{
    public GameObject son;
    private GameObject child;
    private float A;
    private float B;
    private float C;
    private float theta_child;
    private float phi_child;

    // Start is called before the first frame update
    void Start()
    {
        this.A = this.transform.localScale.x;
        this.B = this.transform.localScale.y;
        this.C = this.transform.localScale.z;
        this.child = null;
        StartCoroutine(mitosis());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator mitosis()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            
            if(this.child == null) //&& Random.Range(0.0f,1.0f) <= 0.6f)
            {
                float radius = GetComponent<SphereCollider>().radius;

                //float modulo = Random.Range(0.0f,radius);

                this.theta_child = Random.Range(0.0f, 2*UnityEngine.Mathf.PI);
                this.phi_child = Random.Range((2*UnityEngine.Mathf.PI)/6, (4*UnityEngine.Mathf.PI)/6);

                Vector3 pos = new Vector3(((4*this.A)/10) * Mathf.Sin(this.phi_child)*Mathf.Cos(this.theta_child), ((4*this.B)/10) * Mathf.Cos(this.phi_child), ((4*this.C/10)) * Mathf.Sin(this.theta_child)*Mathf.Sin(this.phi_child));
                Vector3 look = new Vector3((2*Mathf.Sin(this.phi_child)*Mathf.Cos(this.theta_child)/A),(2*Mathf.Cos(this.phi_child)/B),(2*Mathf.Sin(this.theta_child)*Mathf.Sin(this.phi_child)/C));

                GameObject a = Instantiate(this.son) as GameObject;

                a.transform.parent = transform;

                Debug.Log(look);
                Debug.Log(this.theta_child);
                Debug.Log(this.phi_child);

                this.child = a;
                //a.transform.localRotation = Quaternion.LookRotation(look,Vector3.up);
                a.transform.localScale = new Vector3(0f,0f,0f);
                a.transform.localPosition = pos;
                
                StartCoroutine(grow_son());
            }
        }
    }

    IEnumerator grow_son()
    {
        this.child.transform.localScale = new Vector3(0f,0f,0f);

        float A = Random.Range(0.9f,1f);
        float B = Random.Range(0.9f,1f);
        float C = Random.Range(1.1f,1.2f);
        Vector3 look = new Vector3((2*Mathf.Sin(this.phi_child)*Mathf.Cos(this.theta_child)/A),(2*Mathf.Cos(this.phi_child)/B),(2*Mathf.Sin(this.theta_child)*Mathf.Sin(this.phi_child)/C));
        Vector3 look_normalized = Vector3.Normalize( look );

        for (float i = 1; i < 26; i++)
        {
            yield return new WaitForSeconds(0.2f);
            this.child.transform.localScale = new Vector3((((A*i))/25.0f)/this.A,((B*i)/25.0f)/this.B,((C*i)/25.0f)/this.C);
            //this.child.transform.localPosition += ((look_normalized)*C)/100f;
            //this.child.transform.localRotation = Quaternion.LookRotation(look,Vector3.up);
        }

        this.child.AddComponent<Rigidbody>();
        this.child.GetComponent<Rigidbody>().mass = 10;
        this.child.GetComponent<Rigidbody>().drag = 5.0f;
        this.child.GetComponent<Rigidbody>().angularDrag = 5.0f;
        this.child.transform.parent = null;
        this.child.tag = "Wawa";
        this.child.transform.localScale = new Vector3(A,B,C);

        this.child.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        this.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

        this.child.AddComponent<Create_child>();
        this.child.GetComponent<Create_child>().son = this.son;

        this.child = null;
    }
    
}