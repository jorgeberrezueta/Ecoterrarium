using UnityEngine;
using UnityEngine.AI;

public class LionController : MonoBehaviour
{

    public NavMeshAgent agent;
    public bool log = false;

    public float radioDePathfinding = 20;
    public float distanciaMaximaComida = 5;
    [Range(0.0f, 10.0f)]
    public float tiempoEnComer = 5;

    Animator anim;
    Rigidbody rb;

    bool lleno = false;
    System.DateTime ultimoNuevoDestino = System.DateTime.Now.AddSeconds(-3);
    System.DateTime comiendoDesde = System.DateTime.Now.AddSeconds(-3);


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void NuevoDestino()
    {
        if (MilisegundosDesde(ultimoNuevoDestino) > 1000)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radioDePathfinding;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, radioDePathfinding, 1);
            Vector3 finalPosition = hit.position;
            agent.SetDestination(finalPosition);
            ultimoNuevoDestino = System.DateTime.Now;
        }
    }

    // Update is called once per frame
    void Update()
    {
        double milis = MilisegundosDesde(comiendoDesde);
        //Debug.Log(agent.velocity.magnitude);
        if (milis > tiempoEnComer * 1000 && agent.velocity.magnitude > 0.1 && agent.velocity != Vector3.zero)
        {
            anim.SetInteger("Walk", 1);
            if (!lleno)
            {
                GameObject obj = BuscarGallina(transform.position);
                if (obj != null)
                {
                    agent.SetDestination(obj.transform.position);
                    float distance = Vector3.Distance(transform.position, obj.transform.position);
                    if (distance <= 1.1)
                    {
                        Destroy(obj);
                        comiendoDesde = System.DateTime.Now;
                        //lleno = true;
                    }
                }
            }
        }
        else
        {
            anim.SetInteger("Walk", 0);
            if (milis > tiempoEnComer * 1000)
            {
                NuevoDestino();
            }
        }

        if (log)
        {
            Debug.Log(agent.velocity.magnitude);
        }
    }

    GameObject BuscarGallina(Vector3 center)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Gallina");
        GameObject closestObject = null;
        foreach (GameObject obj in objs)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < distanciaMaximaComida)
            {
                if (closestObject == null)
                {
                    closestObject = obj;
                }

                if (distance <= Vector3.Distance(transform.position, closestObject.transform.position))
                {
                    closestObject = obj;
                }
            }
        }
        return closestObject;
    }

    double MilisegundosDesde(System.DateTime time)
    {
        System.TimeSpan span = System.DateTime.Now - time;
        return span.TotalMilliseconds;
    }
}
