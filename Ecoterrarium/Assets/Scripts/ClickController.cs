using UnityEngine;

public class ClickController : MonoBehaviour
{
    public Camera cam;
    public Transform parent;
    public Transform izq;
    public Transform cen;
    public Transform der;

    bool estadoIzq = false;
    bool estadoCen = false;
    bool estadoDer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool nuevoEstadoIzq = Input.GetMouseButton(0);
        bool nuevoEstadoCen = Input.GetMouseButton(2);
        bool nuevoEstadoDer = Input.GetMouseButton(1);
        if (nuevoEstadoIzq && estadoIzq != nuevoEstadoIzq)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Transform gallina = Instantiate(izq, hit.point, Quaternion.identity);
                gallina.parent = parent;
            }
        }
        if (nuevoEstadoCen && estadoCen != nuevoEstadoCen)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Transform gallina = Instantiate(cen, hit.point, Quaternion.identity);
                gallina.parent = parent;
            }
        }
        if (nuevoEstadoDer && estadoDer != nuevoEstadoDer)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Transform gallina = Instantiate(der, hit.point, Quaternion.identity);
                gallina.parent = parent;
            }
        }
        estadoIzq = nuevoEstadoIzq;
        estadoCen = nuevoEstadoCen;
        estadoDer = nuevoEstadoDer;
    }
}
