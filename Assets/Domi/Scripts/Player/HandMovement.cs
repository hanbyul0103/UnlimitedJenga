using System;
using UnityEngine;

public class HandMovement : AgentMovement
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    
    private Hand handAgent;
    private bool downRotateR = false;
    private bool downRotateL = false;

    protected override void Awake() {
        base.Awake();
    }

    public override void Init()
    {
        base.Init();
        handAgent = agent as Hand;

        handAgent.Control.RotateREvent += HandleRotateRight;
        handAgent.Control.RotateLEvent += HandleRotateLeft;
    }

    private void OnDestroy() {
        handAgent.Control.RotateREvent -= HandleRotateRight;
        handAgent.Control.RotateLEvent -= HandleRotateLeft;
    }

    private void HandleRotateLeft(bool value)
    {
        downRotateL = value;
    }

    private void HandleRotateRight(bool value)
    {
        downRotateR = value;
    }

    private void FixedUpdate() {
        Vector2 dir = handAgent.Control.GetMoveDirection();
        Move(dir);

        // 방향 회전
        float plusRotate = 0;
        if (downRotateR)
            plusRotate -= rotateSpeed;

        if (downRotateL)
            plusRotate += rotateSpeed;

        rigid.MoveRotation(rigid.rotation + plusRotate);
    }

    public override void Move(Vector2 dir)
    {
        rigid.linearVelocity = dir * moveSpeed;
    }
}
