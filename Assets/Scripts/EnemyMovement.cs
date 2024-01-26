using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private float NavMeshAgentspeed;
    

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            NavMeshAgent.speed = NavMeshAgentspeed;
        }
    }

}
