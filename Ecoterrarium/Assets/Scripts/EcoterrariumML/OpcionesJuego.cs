using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class OpcionesJuego : MonoBehaviour
{

    [HideInInspector]
    public AgenteGallina[] agents;
    [HideInInspector]
    public AreaAnimales[] listArea;

    public void Awake()
    {
        Academy.Instance.OnEnvironmentReset += EnvironmentReset;
    }

    public void EnvironmentReset()
    {
        ClearObjects(FindObjectsOfType<LogicaComida>());

        agents = FindObjectsOfType<AgenteGallina>();
        listArea = FindObjectsOfType<AreaAnimales>();
        foreach (var fa in listArea)
        {
            Debug.Log("Reiniciando area: " + fa.name);
            fa.ReiniciarArea(agents);
        }
    }

    void ClearObjects(LogicaComida[] objects)
    {
        foreach (var comida in objects)
        {
            Destroy(comida.transform.gameObject);
        }
    }

}
