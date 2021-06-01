using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move : MonoBehaviour
{
    public Transform pos1, pos2;
    private float speed = 8f;

    Vector3 nextPos;
    void Start()
    {
        nextPos = pos1.position;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        else if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.fixedDeltaTime);

    }
    private void OnDrawGizmos()
    {
    //    Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
