using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : BaseState
{
    protected FishSM _sm;

    protected Vector3 destination;

    public MovingState(string name, FishSM stateMachine) : base(name, stateMachine)
    {
        _sm = (FishSM)stateMachine;
    }

    public override void UpdatePhysics()
    {
        StepToDestination();
    }

    void StepToDestination()
    {
        Transform currentTransform = _sm.transform;
        Vector3 direction = (destination - currentTransform.position - _sm.fishHead.transform.position);
        Quaternion lookDirection = Quaternion.LookRotation(direction.normalized);
        if (Mathf.Abs(Quaternion.Angle(currentTransform.rotation, lookDirection)) > Mathf.Epsilon)
        {
            currentTransform.rotation = Quaternion.RotateTowards(
                currentTransform.rotation, lookDirection, Time.deltaTime * _sm.rotateSpeed
            );
        }

        Vector3 displacement = direction.normalized * Time.deltaTime * _sm.translateSpeed;
        if (direction.magnitude > Mathf.Epsilon)
        {
            currentTransform.Translate(displacement, Space.World);
        }
    }
}
