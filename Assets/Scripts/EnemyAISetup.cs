using UnityEngine;
using Pathfinding; // Make sure this is included

public class AssignTargetToAI : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        AIDestinationSetter setter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && setter != null)
        {
            setter.target = player.transform;
        }
        else
        {
            Debug.LogWarning("Player or AIDestinationSetter not found.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position , transform.TransformDirection(Vector3.up));
            // transform.rotation = new Quaternion( 0 , 0 , rotation.z , rotation.w );

            // transform.LookAt(player.transform.position, Vector3.back);

            // Vector3 direction = player.transform.position - transform.position;
            // transform.right = direction.normalized;

            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
