using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    void Monster_Die()
    {
        Debug.Log("Enemy is dead");
        Destroy(gameObject);
    }
}
