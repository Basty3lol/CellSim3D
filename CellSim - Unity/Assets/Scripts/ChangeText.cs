using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public Text textoObjeto; // Referencia al componente Text del objeto
    private GameObject Controller;

    void Start()
    {
        this.Controller = GameObject.FindGameObjectsWithTag("Controller")[0];
        StartCoroutine(LStart());
    }

    IEnumerator LStart()
    {   
        while(true)
        {
            yield return new WaitForSeconds(0.0333334f);
            string myNewString = "Cells: " +  this.Controller.GetComponent<count_cells>().CellsCount;
            CambiarTextoObjeto(myNewString);
        }
    }

    public void CambiarTextoObjeto(string nuevoTexto)
    {
        // Cambiar el texto del objeto
        textoObjeto.text = nuevoTexto;
    }
}
