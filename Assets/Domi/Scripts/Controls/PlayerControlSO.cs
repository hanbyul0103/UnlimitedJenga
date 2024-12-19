using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum ControlType : byte
{
    Player1 = 0,
    Player2
}

[CreateAssetMenu(fileName = "PlayerControlSO", menuName = "SO/Control/PlayerControlSO")]
public class PlayerControlSO : ScriptableObject, Controls.IPlayerActions
{
    [SerializeField] ControlType playerType;
    public event Action SkillEvent;
    public event Action MoveEvent;
    public event Action<bool> RotateLEvent;
    public event Action<bool> RotateREvent;
    public event Action<bool> ShiftEvent;

    private Controls controls = null;

    private void OnEnable()
    {
        controls = new();
        controls.Player.SetCallbacks(this);
        LoadKeyBind();

        controls.Player.Enable(); // 켜ㅓㅓ
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void LoadKeyBind()
    {
        // 아직 저장기능이 구현 안되어있음
        // 저장된 바인딩 데이터가 있으면 그걸로 적용함
        // ... code

        // defualt 키 지정
        Debug.Log($"Load Key Bind... Control/base_{playerType.ToString()}");
        TextAsset data = Resources.Load<TextAsset>($"Control/base_{playerType.ToString()}");
        if (data == null) return; // 없는디??

        Debug.Log("Loaded Key Bind!");
        controls.LoadBindingOverridesFromJson(data.ToString());
    }

    public Vector2 GetMoveDirection()
    {
        return controls.Player.Move.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context) {}

    public void OnRotateR(InputAction.CallbackContext context)
    {
        if (context.performed)
            RotateREvent?.Invoke(true);
        else if (context.canceled)
            RotateREvent?.Invoke(false);
    }

    public void OnRotateL(InputAction.CallbackContext context)
    {
        if (context.performed)
            RotateLEvent?.Invoke(true);
        else if (context.canceled)
            RotateLEvent?.Invoke(false);
    }

    public void OnSlow(InputAction.CallbackContext context)
    {
        if (context.performed)
            ShiftEvent?.Invoke(true);
        else if (context.canceled)
            ShiftEvent?.Invoke(false);
    }
}
