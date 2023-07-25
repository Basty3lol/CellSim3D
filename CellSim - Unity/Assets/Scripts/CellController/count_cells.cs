using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count_cells : MonoBehaviour
{
    private GameObject[] cells;
    public int CellsCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(updateCount());
    }
    IEnumerator updateCount()
    {
        while(true)
        { 
            yield return new WaitForSeconds(0.1f);
            this.CellsCount = GameObject.FindGameObjectsWithTag("cells").Length;

        }
    }
    public void play()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("cells");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
