using System;
using UnityEngine;

public class HandMovement : AgentMovement
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float slowMoveSpeed = 3f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float slowRotateSpeed = 3f;
    
    private Hand handAgent;
    private bool downRotateR = false;
    private bool downRotateL = false;
    private bool downSlow = false;

    protected override void Awake() {
        base.Awake();
    }

    public override void Init()
    {
        base.Init();
        handAgent = agent as Hand;

        handAgent.Control.RotateREvent += HandleRotateRight;
        handAgent.Control.RotateLEvent += HandleRotateLeft;
        handAgent.Control.ShiftEvent += HandleShift;
    }

    private void OnDestroy() {
        handAgent.Control.RotateREvent -= HandleRotateRight;
        handAgent.Control.RotateLEvent -= HandleRotateLeft;
        handAgent.Control.ShiftEvent -= HandleShift;
    }

    private void HandleShift(bool value)
    {
        downSlow = value;
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
        float speed = downSlow ? slowRotateSpeed : rotateSpeed;
        if (downRotateR)
            plusRotate -= speed;

        if (downRotateL)
            plusRotate += speed;

        rigid.MoveRotation(rigid.rotation + plusRotate);
    }

    public override void Move(Vector2 dir)
    {
        float speed = downSlow ? slowMoveSpeed : moveSpeed;
        rigid.linearVelocity = dir * speed;
    }
}
