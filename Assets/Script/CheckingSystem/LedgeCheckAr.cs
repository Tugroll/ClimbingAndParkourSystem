using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "CheckingSystem/LedgeCheck")]
public class LedgeCheckAr : CheckArea
{
    
    [SerializeField] float ledgeThresHold = .75f;
    public bool LedgeCheck(Vector3 moveDir, out LedgeData ledgeData,Transform transform)
    {
        //aynı
        ledgeData = new LedgeData();
        if (moveDir == Vector3.zero)
            return false;
        float originOffset = 0.3f;

        Vector3 origin = transform.position + moveDir * originOffset + Vector3.up;


        if (PhysicUtil.ThreeRaycasts(origin, Vector3.down, .25f, transform, out List<RaycastHit> hits, lenght,layerMask , true))
        {
            List<RaycastHit> validHits = hits.Where(h => transform.position.y - h.point.y > ledgeThresHold).ToList();

            if (validHits.Count > 0)
            {

                var surfaceRayOrigin = validHits[0].point;
                surfaceRayOrigin.y = transform.position.y - .1f;

                if (Physics.Raycast(surfaceRayOrigin, transform.position - surfaceRayOrigin, out RaycastHit surfaceHit, 4, layerMask))
                {

                    Debug.DrawLine(surfaceRayOrigin, transform.position, Color.cyan);
                    float height = transform.position.y - validHits[0].point.y;


                    ledgeData.angle = Vector3.Angle(transform.forward, surfaceHit.normal);
                    ledgeData.height = height;
                    ledgeData.surfacetHit = surfaceHit;

                    return true;


                }
            }
        }

        return false;
    }
}