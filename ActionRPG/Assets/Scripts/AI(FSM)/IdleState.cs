using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State<EnemyConyroller_Ghoul>
{
    private Animator animator;
    private CharacterController controller;

    protected int hasMove = Animator.StringToHash("Move");
    protected int hasMoveSpeed = Animator.StringToHash("MoveSpeed");
    public override void OnInitialized()
    {
        animator = context.GetComponent<Animator>();
        controller = context.GetComponent<CharacterController>();
    }

    public override void OnEnter(){
        animator?.SetBool(hasMove, false); // ?. 는 null이 아닐때만 실행해준다.
        animator?.SetFloat(hasMoveSpeed, 0.0f);
        controller?.Move(Vector3.zero);
    }

    public override void Update(float deltaTime)
    {
        Transform enemy = context.SearchEnemy();    // 적을 찾는다.
        if(enemy != null)
        // 적을 찾아보고 공격이 가능하다면 -> Attack,
        // 적을 찾아보고 공격이 불가능하다면 -> Move,
        {
            if(context.IsAvailableAttack)
            {
                stateMachine.ChangeState<AttackState>();
            }else
            {
                stateMachine.ChangeState<MoveState>();
            }
        }
    }

    public override void OnExit()
    {

    }
}
