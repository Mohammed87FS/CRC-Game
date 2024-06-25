using UnityEngine;
using System.Collections.Generic;

public class BallController : MonoBehaviour
{
    private List<GameObject> targets;
    private System.Action<GameObject, GameObject> onCollision;
    private List<GameObject> type2Blocks;

    public void SetTarget(List<GameObject> targets, System.Action<GameObject, GameObject> onCollision, List<GameObject> type2Blocks)
    {
        this.targets = targets ?? new List<GameObject>();
        this.onCollision = onCollision;
        this.type2Blocks = type2Blocks ?? new List<GameObject>(); 
    }

    private void Update()
    {
        if (targets != null && targets.Count > 0)
        {
            Transform target = targets[0].transform;
            Vector3 direction = (target.position - transform.position).normalized;
            GetComponent<Rigidbody>().velocity = direction * 10f; 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (targets != null && targets.Contains(collision.gameObject))
        {
            onCollision?.Invoke(gameObject, collision.gameObject);
        }
        else if (type2Blocks != null && type2Blocks.Contains(collision.gameObject))
        {
            Destroy(gameObject); 
        }
    }
}
