using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ParkourIenum/ClimbEnum")]
public class DoClimbAction : ScriptableObject
{
    public DoActionParkour doParkour;
    public bool isHang;
    public IEnumerator JumpToLedge(string Anim, Transform ledge, float StartTime, float TargetTime,Transform transform, Animator animator,
      AvatarTarget hand = AvatarTarget.RightHand, Vector3? handOffset = null)
    {
        var matchParams = new MatchTargetParams()
        {
            pos = GetHandPos(ledge, hand, handOffset),
            bodyPart = hand,
            matchStartTime = StartTime,
            matchTargetTime = TargetTime,
            posWeight = Vector3.one
        };


        var targetRotate = Quaternion.LookRotation(-ledge.forward);
        yield return doParkour.DorAction(Anim, transform, animator, matchParams, targetRotate, true);

      
        isHang = true;

        //only reason to wait .2f is that the code doesn't work at runtime without it but it works  debug mode
        yield return new WaitForSeconds(.2f);

    }

    Vector3 GetHandPos(Transform ledge, AvatarTarget hand, Vector3? handOffset)
    {
        var offVal = handOffset != null ? handOffset.Value : new Vector3(.25f, .1f, .1f);

        var hDir = hand == AvatarTarget.RightHand ? ledge.right : -ledge.right;
        return ledge.position + ledge.forward * offVal.z + Vector3.up * offVal.y - hDir * offVal.x;
    }
}
