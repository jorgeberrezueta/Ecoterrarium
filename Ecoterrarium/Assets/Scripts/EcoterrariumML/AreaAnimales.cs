using UnityEngine;

public class AreaAnimales : Area
{
    public GameObject comida;
    public int numComida;
    public bool reaparecerComida;
    public float rango;

    void CrearComida(int num, GameObject type)
    {
        Debug.Log("nueva comida");
        for (int i = 0; i < num; i++)
        {
            GameObject f = Instantiate(
                type,
                transform.position + new Vector3(
                    Random.Range(-rango, rango), 
                    0f, 
                    Random.Range(-rango, rango)
                ),
                Quaternion.Euler(
                    new Vector3(
                        0f, 
                        //Random.Range(0f, 360f),
                        0f,
                        //90f
                        0f
                    )
                )
            );
            f.GetComponent<LogicaComida>().reaparecer = reaparecerComida;
            f.GetComponent<LogicaComida>().area = this;
            f.transform.parent = transform;
        }
    }

    public void ReiniciarArea()
    {
        AgenteGallina[] agents = GetComponents<AgenteGallina>();
        ReiniciarArea(agents);
    }

    public void ReiniciarArea(AgenteGallina[] agents)
    {
        foreach (AgenteGallina a in agents)
        {
            GameObject agent = a.gameObject;
            if (agent.transform.parent == gameObject.transform)
            {
                /*agent.transform.position = new Vector3(Random.Range(-rango, rango), 0f,
                    Random.Range(-rango, rango))
                    + transform.position;
                agent.transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));*/
                a.UbicacionAleatoria();
            }
        }

        CrearComida(numComida, comida);
    }

    public override void ResetArea()
    {
    }
}
