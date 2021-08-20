using UnityEngine;

public class ClickController : MonoBehaviour
{
    public Camera cam;
    public Transform parent;
    public Transform clickIzquierdo;
    public Transform clickCentral;
    public Transform clickDerecho;

    bool estadoIzq = false;
    bool estadoCen = false;
    bool estadoDer = false;

    void Start()
    {
        
    }

    void Update()
    {
        bool nuevoEstadoIzq = Input.GetMouseButton(0);
        bool nuevoEstadoCen = Input.GetMouseButton(2);
        bool nuevoEstadoDer = Input.GetMouseButton(1);
        if (nuevoEstadoIzq && estadoIzq != nuevoEstadoIzq)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (clickIzquierdo != null && Physics.Raycast(ray, out hit))
            {
                Transform gallina = Instantiate(clickIzquierdo, hit.point, Quaternion.identity);
                gallina.parent = parent;
            }
        }
        if (nuevoEstadoCen && estadoCen != nuevoEstadoCen)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (clickCentral != null && Physics.Raycast(ray, out hit))
            {
                Transform gallina = Instantiate(clickCentral, hit.point, Quaternion.identity);
                gallina.parent = parent;
            }
        }
        if (nuevoEstadoDer && estadoDer != nuevoEstadoDer)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (clickDerecho != null && Physics.Raycast(ray, out hit))
            {
                Transform gallina = Instantiate(clickDerecho, hit.point, Quaternion.identity);
                gallina.parent = parent;
            }
        }
        estadoIzq = nuevoEstadoIzq;
        estadoCen = nuevoEstadoCen;
        estadoDer = nuevoEstadoDer;
    }
}
