using UnityEngine.AI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Camera cam;

    [SerializeField] NavMeshAgent agent;

    private void Awake()
    {
        cam = Camera.main;
        agent = GameObject.Find("Enemy").GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) //Requiere poner un boxCollider al area
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}