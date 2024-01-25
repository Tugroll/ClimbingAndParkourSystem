using UnityEngine;

[CreateAssetMenu(menuName = "CheckingSystem/GroundCheck")]
public class GroundCheck : CheckArea
{
   
    [SerializeField] Vector3 groundCheckOffset;
  
    public  bool isGrounded;
    public  void Check(Transform transform)
    {
       isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), lenght/*radius*/, layerMask);
       
    }
}
