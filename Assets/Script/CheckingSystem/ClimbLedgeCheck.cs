using UnityEngine;

[CreateAssetMenu(menuName = "CheckingSystem/ClimbCheck")]
public class ClimbLedgeCheck : CheckArea
{
    
    public bool _ClimbLedgeCheck(Vector3 dir,Transform transform, out RaycastHit ledgeHit)
    {
        //aynı
        ledgeHit = new RaycastHit();
        if (dir == Vector3.zero)
            return false;

        Vector3 origin = transform.position + Vector3.up * 1.5f;
        Vector3 offSet = new Vector3(0, .18f, 0);
        for (int i = 0; i < 15; i++)
        {
            Debug.DrawRay(origin + offSet * i, dir);
            if (Physics.Raycast(origin + offSet * i, dir, out RaycastHit hit, lenght, layerMask))
            {
                ledgeHit = hit;
                return true;
            }
        }
        return false;
    }

}
