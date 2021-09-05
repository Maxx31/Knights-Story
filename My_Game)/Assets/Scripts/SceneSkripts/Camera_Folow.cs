using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Folow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _smooth;

    public Vector3 _offset = new Vector3(0, 0, 0);
     
  
    private void Update()
    {
        
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime * _smooth );
    }
}
