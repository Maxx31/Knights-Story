using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Rain : StateMachineBehaviour
{
    [SerializeField]
    private float _startPointX;
    [SerializeField]
    private float _endPointX;
    [SerializeField]
    private float _posY;
    [SerializeField]
    private float _offset; 
    [SerializeField]
    private float _step;

    [SerializeField]
    private FireRain _fireRain;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (float position = _startPointX; position < _endPointX; position += _step)
        {
            float pos = Random.Range(position - _offset, position + _offset);
            FireRain fire = Instantiate(_fireRain, new Vector3(position, _posY, 0), _fireRain.transform.rotation) as FireRain;

        }

    }


}
