using UnityEngine;

public class LogicaComida : MonoBehaviour
{
    public bool reaparecer;
    public AreaAnimales area;

    public void OnEaten()
    {
        if (reaparecer)
        {
            transform.position = new Vector3(Random.Range(-area.rango, area.rango),
                0f,
                Random.Range(-area.rango, area.rango)) + area.transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
