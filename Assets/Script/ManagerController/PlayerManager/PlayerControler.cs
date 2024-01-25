using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static System.Collections.Specialized.BitVector32;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] InputandMovementData InputandMovement;

    CameraControler camControler;
    CharacterController characterController;
    [SerializeField] GroundCheck groundCheck;



    public LedgeData LedgeData { get; set; }

 
    public bool isobstacle;
    //public bool isHang;
    bool hasControl = true;

    public bool isAction { get; private set; }
    public bool isLedge { get; set; }

    Animator animator;
   
    public DoClimbAction doClimb;
    public LedgeCheckAr ledgeCheck;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        camControler = Camera.main.GetComponent<CameraControler>();
        animator = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasControl)
            return;



        InputandMovement.GetInput(camControler);

        if (doClimb.isHang) return;



      
        animator.SetBool("isGrounded", groundCheck.isGrounded);

        groundCheck.Check(this.transform);
        if (groundCheck.isGrounded)
        {

            InputandMovement.OnGround();
            isLedge = ledgeCheck.LedgeCheck(InputandMovement.desiredMoveDir, out LedgeData ledgeData, this.transform);/*envScanner.LedgeCheck(InputandMovement.desiredMoveDir, out LedgeData ledgeData);*/
            if (isLedge == true)
            {


                LedgeData = ledgeData;

                InputandMovement.LedgeMovement(LedgeData);
                Debug.Log("LEEDGE");
            }
            animator.SetFloat("moveAmount", InputandMovement.velocity.magnitude / InputandMovement.moveSpeed, 0.2f, Time.deltaTime);
        }
        else
        {
            InputandMovement.ySpeed += (Physics.gravity.y) * Time.deltaTime;
            InputandMovement.velocity = transform.forward * InputandMovement.moveSpeed / 2;
        }


        InputandMovement.Move(characterController);

        if (InputandMovement.moveAmount > 0 && InputandMovement.moveDir.magnitude > 0.1f)
        {
            InputandMovement.TargetRotation();
        }


        transform.rotation = Quaternion.RotateTowards(transform.rotation, InputandMovement.targetRotation,
            InputandMovement.rotationSpeed * Time.deltaTime);
    }


    public void SetControl(bool hasCo)
    {
        hasControl = hasCo;
        if (!hasControl)
        {
            animator.SetFloat("moveAmount", 0);
        }

    }
   
  
    public bool HasControl
    {
        get => hasControl;
        set => hasControl = value;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red * new Color(1, 1, 1, .5f);
        //Gizmos.DrawSphere(transform.TransformPoint(groundCheck.), groundCheckRadius);
    }

    public void ResetTargetRotation()
    {
        InputandMovement.targetRotation = transform.rotation;
    }
}

public class MatchTargetParams
{
    public Vector3 pos;
    public AvatarTarget bodyPart;
    public Vector3 posWeight;
    public float matchStartTime;
    public float matchTargetTime;
}
