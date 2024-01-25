using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicUtil
{
    public static bool ThreeRaycasts(Vector3 origin, Vector3 dir, float spacing, Transform transform, out List<RaycastHit> hits,
        float distance, LayerMask layer, bool debugDraw = false)
    {
        bool centerHitFound = Physics.Raycast(origin, Vector3.down, out RaycastHit centerHit, distance, layer);
        bool leftHitFound = Physics.Raycast(origin - transform.right * spacing, Vector3.down, out RaycastHit lefthit, distance, layer);
        bool rightHitFound = Physics.Raycast(origin + transform.right * spacing, Vector3.down, out RaycastHit rightHit, distance, layer);

        hits = new List<RaycastHit>() {centerHit,lefthit,rightHit};

        bool hitFound = centerHitFound || leftHitFound || rightHitFound;

        if(hitFound && debugDraw)
        {
            Debug.DrawLine(origin, centerHit.point, Color.red);
            Debug.DrawLine(origin - transform.right * spacing, lefthit.point, Color.red);
            Debug.DrawLine(origin + transform.right *spacing, rightHit.point, Color.red);
        }

        return hitFound;
    }
}
