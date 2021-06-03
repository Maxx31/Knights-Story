using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Folow : MonoBehaviour
{
    public Transform Target;
    public float Smooth = 12.0f;
    public Vector3 Offset = new Vector3(0, 0, 0);

   private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + Offset, Time.deltaTime * Smooth);
    }
}
