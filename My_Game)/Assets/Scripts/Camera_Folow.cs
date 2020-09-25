using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Folow : MonoBehaviour
{
    public Transform target;
    public float smooth = 12.0f;
    public Vector3 offset = new Vector3(0, 0, 0);

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth);
    }
}
