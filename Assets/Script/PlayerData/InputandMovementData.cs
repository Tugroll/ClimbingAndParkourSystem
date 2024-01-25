using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Input/InputandMovementData")]
public class InputandMovementData : ScriptableObject
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 0.01f;
    public float ySpeed;
    public Vector3 desiredMoveDir;
    public Vector3 moveDir;
    public float moveAmount;
    public Vector3 velocity;


   public Quaternion targetRotation;
    public void GetInput(CameraControler camControler)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveAmount = Mathf.Abs(x) + Mathf.Abs(z);
        var moveInput = (new Vector3(x, 0, z)).normalized;

        desiredMoveDir = camControler.PlanarRotation() * moveInput;
        moveDir = desiredMoveDir;
        velocity = Vector3.zero;
    }

    public void OnGround()
    {
        velocity = moveDir * moveSpeed;
        ySpeed = -.5f;

    }
    public void Move(CharacterController characterController)
    {
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void LedgeMovement(LedgeData ledgeData)
    {
        float angle = Vector3.Angle(ledgeData.surfacetHit.normal, desiredMoveDir);

        if (angle > 0)
        {
            velocity = Vector3.zero; 
           moveDir = Vector3.zero;
        }
    }

    public void TargetRotation()

    {
        targetRotation = Quaternion.LookRotation(moveDir);
       
    }

}
