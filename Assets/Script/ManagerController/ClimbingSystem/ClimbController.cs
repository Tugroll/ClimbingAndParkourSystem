using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbController : MonoBehaviour
{
    ClimbPoint currentPoint;
    PlayerControler playerControler;
    public GroundCheck groundCheck;
    public DoActionParkour doParkour;
    public DoClimbAction doClimbAction;
    public ClimbLedgeCheck climbCheck;
    Animator animator;

    private void Awake()
    {
       
        playerControler = GetComponent<PlayerControler>();
        animator  = GetComponent<Animator>();
    }
    void Update()
    {

        if (!doClimbAction.isHang)
        {
            if (Input.GetButton("Jump") && !doParkour.isAction)
            {
                if (climbCheck._ClimbLedgeCheck(transform.forward, this.transform,out RaycastHit ledgeHit))
                {
                    currentPoint = ledgeHit.transform.GetComponent<ClimbPoint>();
                    playerControler.SetControl(false);
                    groundCheck.isGrounded = false;
                    StartCoroutine(doClimbAction.JumpToLedge("BracedHang", ledgeHit.transform, .41f, .54f,this.transform,animator));

                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.C) && !doParkour.isAction)
            {

                StartCoroutine(JumpFromHang());
                return;
            }


            float x = Mathf.Round(Input.GetAxisRaw("Horizontal"));
            float y = Mathf.Round(Input.GetAxisRaw("Vertical"));

            var inputDr = new Vector2(x, y);

            var neighb = currentPoint.GetNeighbours(inputDr);

            if (doParkour.isAction || inputDr == Vector2.zero) return;

            if (currentPoint.mountPoint && inputDr.y == 1) 
            {
                StartCoroutine(MountFromHang());
                return;
            }

            #region //ClimbActions//
            if (neighb == null)
                return;
            if (neighb.connectionType == ConnectionType.Jump && Input.GetButton("Jump"))
            {
                currentPoint = neighb.point;
                if (neighb.direction.y == 1)
                    StartCoroutine(doClimbAction.JumpToLedge("HangHopUp", currentPoint.transform, 0.35f, .65f, this.transform, animator, handOffset: new Vector3(.25f, .09f, .13f)));

                else if (neighb.direction.y == -1)
                    StartCoroutine(doClimbAction.JumpToLedge("HangHopDown", currentPoint.transform, .31f, .65f, this.transform, animator, handOffset: new Vector3(.25f, .09f, .13f)));

                else if (neighb.direction.x == 1)
                    StartCoroutine(doClimbAction.JumpToLedge("HangHopRight", currentPoint.transform, 0.20f, .50f, this.transform, animator, handOffset: new Vector3(.25f, .15f, .13f)));

                else if (neighb.direction.x == -1)
                    StartCoroutine(doClimbAction.JumpToLedge("HangHopLeft", currentPoint.transform, 0.20f, .50f, this.transform, animator, handOffset: new Vector3(.25f, .15f, .13f)));
            }
            else if (neighb.connectionType == ConnectionType.Move)
            {
                currentPoint = neighb.point;
                if (neighb.direction.x == 1)
                    StartCoroutine(doClimbAction.JumpToLedge("ShimmyRight", currentPoint.transform, 0f, .38f, this.transform, animator, handOffset: new Vector3(.25f, .05f, -.1f)));

                else if (neighb.direction.x == -1)
                    StartCoroutine(doClimbAction.JumpToLedge("ShimmyLeft", currentPoint.transform, 0f, .38f, this.transform, animator, AvatarTarget.LeftHand, handOffset: new Vector3(.25f, .05f, -.1f)));
            }
            #endregion
        }
    }

    Vector3 GetHandPos(Transform ledge, AvatarTarget hand, Vector3? handOffset)
    {
        var offVal = handOffset != null ? handOffset.Value : new Vector3(.25f, .1f, .1f);

        var hDir = hand == AvatarTarget.RightHand ? ledge.right : -ledge.right;
        return ledge.position + ledge.forward * offVal.z + Vector3.up * offVal.y - hDir * offVal.x;
    }

    IEnumerator JumpFromHang()
    {
        doClimbAction.isHang = false;

        yield return doParkour.DorAction("WallJump", this.transform, animator,null, transform.rotation, true);
         
        playerControler.SetControl(true);
    }

    IEnumerator MountFromHang()
    {
        doClimbAction.isHang=false;
        yield return doParkour.DorAction("LedgeClimbUp", this.transform, animator, null, transform.rotation, true);
        playerControler.SetControl(true);
    }
}
