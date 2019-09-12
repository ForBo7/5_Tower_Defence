using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        print("Starting patrol...");

        foreach (Waypoint waypoint in path)
        {
            print("Visiting block: " + waypoint.name);
            transform.position = new Vector3 (waypoint.transform.position.x, 15f, waypoint.transform.position.z);
            yield return new WaitForSeconds(1f);
        }

        print("Ending patrol...");
    }
}
