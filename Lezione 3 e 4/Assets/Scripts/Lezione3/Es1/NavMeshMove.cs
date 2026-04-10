/*
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class NavMeshMove : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    List<Tranform> dests;
    public int currentDestIndex = 0;

    private void awake()
    {
        if(agent == null)
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(!agent.path.Pending && agent.remaningDistance <= agent.stoppingDistance)
        {
            int ix = Random.Range(0, dests.Count);
            Transform dest = dests[ix];

            currentDestIndex = ix;

            agent.SetDestination(dests.position);

        }
    }
}
*/
