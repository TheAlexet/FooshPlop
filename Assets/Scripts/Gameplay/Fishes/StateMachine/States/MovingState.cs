using UnityEngine;

public class MovingState : FishState
{
    public MovingState(string name, FishSM stateMachine) : base(name, stateMachine) { }
    protected Vector3 destination;
    protected Vector3 direction;
    private float epsilonMove = 0.1f;
    private float epsilonDegree = 1f;

    #region State Methods
    public override void UpdatePhysics() { StepToDestination(); }
    #endregion

    #region Main Methods
    private void StepToDestination()
    {
        direction = GetDirectionNonNormalized();
        UpdateRotationTowardsDirection(direction.normalized);
        UpdatePositionTowardsDirection(direction);
    }
    private void UpdateRotationTowardsDirection(Vector3 direction)
    {
        Quaternion lookDirection = Quaternion.LookRotation(direction);
        if (Mathf.Abs(Quaternion.Angle(_sm.transform.rotation, lookDirection)) > epsilonDegree)
        {
            _sm.transform.rotation = Quaternion.RotateTowards(
                _sm.transform.rotation, lookDirection, Time.deltaTime * _sm.Data.RotateSpeed
            );
        }
    }
    private void UpdatePositionTowardsDirection(Vector3 direction)
    {
        Vector3 displacement = direction.normalized * Time.deltaTime * _sm.Data.TranslateSpeed;
        if (direction.magnitude > epsilonMove) { _sm.transform.Translate(displacement, Space.World); }
    }
    protected Vector3 GetDirectionNonNormalized()
    {
        // return (destination - _sm.transform.position - _sm.fishHead.transform.localPosition);
        return (destination - _sm.fishHead.transform.position);
    }
    #endregion

    #region Reusable Methods
    protected Vector3 NextRandomDestination() { return _sm.fishArea.RandomPoint(); }
    #endregion
}
