using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    bool[] UpDownLeftRight = new bool[4];
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float friction = 0.025f;
    [SerializeField] Vector2 wiggleSpeedAndStr = new Vector2(0.2f, 2f);
    [SerializeField] float stoppedThreshold = 0.2f;
    Vector3 localScale;

    public void CheckUp(InputAction.CallbackContext ctx)
    {
        if (ctx.started) UpDownLeftRight[0] = true;
        if (ctx.canceled) UpDownLeftRight[0] = false;
    }
    public void CheckDown(InputAction.CallbackContext ctx)
    {
        if (ctx.started) UpDownLeftRight[1] = true;
        if (ctx.canceled) UpDownLeftRight[1] = false;
    }
    public void CheckLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.started) UpDownLeftRight[2] = true;
        if (ctx.canceled) UpDownLeftRight[2] = false;
    }
    public void CheckRight(InputAction.CallbackContext ctx)
    {
        if (ctx.started) UpDownLeftRight[3] = true;
        if (ctx.canceled) UpDownLeftRight[3] = false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
    }

    private void Update()
    {
        MovePlayer();
        AnimatePlayer();
    }

    void AnimatePlayer()
    {
        if (Vector2.Distance(rb.velocity, Vector2.zero) <= stoppedThreshold) return;

        var newLocalAngles = localScale + (Vector3.one * (Mathf.Sin(Time.time * wiggleSpeedAndStr.x) * wiggleSpeedAndStr.y));
        newLocalAngles.x = localScale.x;
        transform.localScale = Vector3.Lerp(transform.localScale, newLocalAngles, friction);
    }

    void MovePlayer()
    {
        var vel = GetInputVel();
        rb.velocity = Vector2.Lerp(rb.velocity, vel, friction);
    }

    Vector2 GetInputVel()
    {
        var vel = Vector2.zero;
        if (UpDownLeftRight[0]) vel.y = speed;
        if (UpDownLeftRight[1]) vel.y -= speed;
        if (UpDownLeftRight[2]) vel.x -= speed;
        if (UpDownLeftRight[3]) vel.x = speed;

        return vel;
    }
}
