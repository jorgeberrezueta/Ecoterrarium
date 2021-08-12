using UnityEngine;
using UnityEngine.AI;

public class LionController : MonoBehaviour
{

    public NavMeshAgent agent;
    public bool log = false;

    public float walkRadius = 20;

    Animator anim;
    Rigidbody rb;

    System.DateTime ultimoNuevoDestino = System.DateTime.Now.AddSeconds(-3);

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void NuevoDestino()
    {
        System.TimeSpan span = System.DateTime.Now - ultimoNuevoDestino;
        if (span.TotalMilliseconds > 3000)
        {
            Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
            Vector3 finalPosition = hit.position;
            agent.SetDestination(finalPosition);
            ultimoNuevoDestino = System.DateTime.Now;
        }
    }
    void Update()
    {
        if (agent.velocity.magnitude > 0.2 && agent.velocity != Vector3.zero)
        {
            anim.SetInteger("Walk", 1);
        }
        else
        {
            anim.SetInteger("Walk", 0);
            NuevoDestino();
        }

        if (log)
        {
            Debug.Log(agent.velocity.magnitude);
        }
    }
}
