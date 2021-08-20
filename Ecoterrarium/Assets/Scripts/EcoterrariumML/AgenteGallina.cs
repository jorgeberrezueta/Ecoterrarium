using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class AgenteGallina : Agent
{
    //FoodCollectorSettings m_FoodCollecterSettings;
    public GameObject area;
    AreaAnimales areaAnimales;
    Rigidbody m_AgentRb;
    // Speed of agent rotation.
    public float turnSpeed = 300;
    // Speed of agent movement.
    public float moveSpeed = 2;

    EnvironmentParameters m_ResetParams;

    public override void Initialize()
    {
        //Debug.Log("Init");
        m_AgentRb = GetComponent<Rigidbody>();
        areaAnimales = area.GetComponent<AreaAnimales>();
        //m_FoodCollecterSettings = FindObjectOfType<FoodCollectorSettings>();
        m_ResetParams = Academy.Instance.EnvironmentParameters;
        SetResetParameters();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var localVelocity = transform.InverseTransformDirection(m_AgentRb.velocity);
        //Debug.Log("sensor " + (sensor != null));
        sensor.AddObservation(localVelocity.x);
        sensor.AddObservation(localVelocity.z);
    }

    public void MoveAgent(ActionBuffers actionBuffers)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var continuousActions = actionBuffers.ContinuousActions;

        var forward = Mathf.Clamp(continuousActions[0], 0f, 1f);
        var right = Mathf.Clamp(continuousActions[1], -1f, 1f);
        var rotate = Mathf.Clamp(continuousActions[2], -1f, 1f);

        dirToGo = transform.forward * forward;
        dirToGo += transform.right * right;
        rotateDir = -transform.up * rotate;

        m_AgentRb.AddForce(dirToGo * moveSpeed, ForceMode.VelocityChange);
        transform.Rotate(rotateDir, Time.fixedDeltaTime * turnSpeed);

        if (m_AgentRb.velocity.sqrMagnitude > 10f) // slow it down
        {
            m_AgentRb.velocity *= 0.95f;
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        MoveAgent(actionBuffers);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        if (Input.GetKey(KeyCode.D))
        {
            continuousActionsOut[2] = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            continuousActionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            continuousActionsOut[2] = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            continuousActionsOut[0] = -1;
        }
        //var discreteActionsOut = actionsOut.DiscreteActions;
        //discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    public override void OnEpisodeBegin()
    {
        UbicacionAleatoria();
        SetResetParameters();
    }

    public void UbicacionAleatoria()
    {
        m_AgentRb.velocity = Vector3.zero;
        transform.position = new Vector3(Random.Range(-areaAnimales.rango, areaAnimales.rango),
            0f, Random.Range(-areaAnimales.rango, areaAnimales.rango))
            + area.transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out LogicaComida comida))
        {
            //Debug.Log("Colision comida");
            other.gameObject.GetComponent<LogicaComida>().OnEaten();
            AddReward(1f);
        }
        /*if (collision.gameObject.TryGetComponent(out Pared pared))
        {
            AddReward(-1f);
        }*/
    }

    public void SetAgentScale()
    {
        float agentScale = m_ResetParams.GetWithDefault("agent_scale", 1.0f);
        gameObject.transform.localScale = new Vector3(agentScale, agentScale, agentScale);
    }

    public void SetResetParameters()
    {
        SetAgentScale();
    }
}
