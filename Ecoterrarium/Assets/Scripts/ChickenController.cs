using UnityEngine;
using UnityEngine.AI;

public class ChickenController : MonoBehaviour
{

    public Camera cam;

    public NavMeshAgent agent;

    public float walkRadius = 20;

    Animator anim;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void NuevoDestino()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(agent.velocity.magnitude);
        if (agent.velocity.magnitude > 0.05 && agent.velocity != Vector3.zero)
        {
            anim.SetInteger("Walk", 1);
        }
        else
        {
            anim.SetInteger("Walk", 0);
            NuevoDestino();           
        }

 /*       if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // MOVE AGENT
                agent.SetDestination(hit.point);
                anim.SetInteger("Walk", 1);
            }
        }*/
    }
}
