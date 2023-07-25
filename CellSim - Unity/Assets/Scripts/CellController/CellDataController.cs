using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDataController : MonoBehaviour
{
    public TextAsset ecolicsv;
    public TextAsset scerevcsv;

    private float[] EColiGrowthTime;
    private float[] EColiGrowthVar;

    private float[] SCerevGrowthTime;
    private float[] SCerevGrowthVar;
    private bool ready=false;
    // Start is called before the first frame update
    private void Start()
    {
        // Splitting the dataset in the end of line
        var splitDatasetEC = this.ecolicsv.text.Split(new char[] {'\n'});
        float[] Var = new float[splitDatasetEC.Length-2];
        float[] Time = new float[splitDatasetEC.Length-2];
        // Iterating through the split dataset in order to spli into rows
        for (var i = 1; i < splitDatasetEC.Length-1; i++) {
            string[] row = splitDatasetEC[i].Split(new char[] {','});
            Var[i-1] = float.Parse(row[1],System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            Time[i-1] = float.Parse(row[2],System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
        }

        this.EColiGrowthTime = Time;
        this.EColiGrowthVar = Var;

        //==============================================================================================================

        var splitDatasetSC = this.scerevcsv.text.Split(new char[] {'\n'});
        float[] Var2 = new float[splitDatasetSC.Length-2];
        float[] Time2 = new float[splitDatasetSC.Length-2];
        // Iterating through the split dataset in order to spli into rows
        for (var i = 1; i < splitDatasetSC.Length-1; i++) {
            string[] row = splitDatasetSC[i].Split(new char[] {','});
            Var2[i-1] = float.Parse(row[1],System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
            Time2[i-1] = float.Parse(row[2],System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture);
        }

        this.SCerevGrowthTime = Time2;
        this.SCerevGrowthVar = Var2;
        this.ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isReady()
    {
        return this.ready;
    }

    private int BinarySearchIndex(float n, float[] array)
    {
        int start = 0;
        int end = array.Length-1;
        int mid;

        if(n <= array[start])
        {
            return start;
        }
        else if(n >= array[end])
        {
            return end;
        }

        while(start <= end)
        {
            mid = (start + end)/2;

            if(array[mid] < n)
            {
                start = mid+1;
            }
            else
            {
                end = mid-1;
            }
        }
        return start;
    }

    public float GetSCerevisiaeRandomTime()
    {
        float data = Random.Range(0.0f, 1.0f);
        int index = this.BinarySearchIndex(data, this.SCerevGrowthVar);
        return this.SCerevGrowthTime[index];
    }

    public float GetEColiRandomTime()
    {
        float data = Random.Range(0.0f, 1.0f);
        int index = this.BinarySearchIndex(data,this.EColiGrowthVar);
        return this.EColiGrowthTime[index];
    }
}
