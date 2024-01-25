using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parkour System/Custom Action/ VaultCustom")]
public class VaultAction : ParkourAction
{
    public override bool CheckIfPossible(ObstacleData hitData, Transform transform)
    {
        if (!base.CheckIfPossible(hitData, transform))
            return false;

        var hitPoint = hitData.forwardHit.transform.InverseTransformPoint(hitData.forwardHit.point);

        if (hitPoint.z < .25 && hitPoint.x < 0 || hitPoint.z >= .25 && hitPoint.x > 0)
        {
            mirror = true;
            matchBodyPart = AvatarTarget.RightHand;
        }
        else
        {
            mirror = false;
            matchBodyPart = AvatarTarget.LeftHand;
        }

        return true;
    }
}
