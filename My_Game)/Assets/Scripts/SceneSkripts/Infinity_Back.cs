using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infinity_Back : MonoBehaviour
{
    [SerializeField]
    private Vector2 Effect_Multiplayer;

    private Transform cameraTransform;
    private Vector3 last_cameraPos;
    private float textureUnitSizeX;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        last_cameraPos = cameraTransform.position;
     
    }

    private void LateUpdate()
    {
        Vector3 delta_Movement = cameraTransform.position - last_cameraPos;
   
        transform.position += new Vector3( delta_Movement.x * Effect_Multiplayer.x , delta_Movement.y * Effect_Multiplayer.y) ;
        last_cameraPos = cameraTransform.position;


    }
}
