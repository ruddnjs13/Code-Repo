using UnityEngine;

public class Agent : MonoBehaviour
{
    #region component region
    public AgentMove MovementCompo { get; protected set; }
    public Animator AnimatorCompo { get; protected set; }
    #endregion

    public bool isDead;

    protected float _timeInAir;

    protected virtual void Awake()  //여기 Initialize로 바꾸고 Agent넣어.(나중을 위해)
    {
        MovementCompo = GetComponent<AgentMove>();
        AnimatorCompo = transform.Find("Visual").GetComponent<Animator>();
    }

    #region Flip Character
    public bool IsFacingRight()
    {
        return Mathf.Approximately(transform.eulerAngles.y, 0);
    }
    #endregion

    public void Flip(Vector3 targetPosition)
    {
        if (targetPosition.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
    }
}
