using UnityEngine;

[CreateAssetMenu(menuName = "CheckingSystem/ObstacleCheck")]
public class ObstacleCheck : CheckArea
{
    [SerializeField] Vector3 forwardOffset = new Vector3(0, 0.25f, 0);
    [SerializeField] float heightLength = 2f;

    public ObstacleData _obstacleCheck(Transform transform)
    {
        ObstacleData obstacleData = new ObstacleData();

        // checking forward object
        Vector3 pos = transform.position + forwardOffset;
        obstacleData.forwardHitFound = Physics.Raycast(pos, transform.forward, out obstacleData.forwardHit, lenght, layerMask);

        Debug.DrawRay(pos, transform.forward * lenght, (obstacleData.forwardHitFound) ? Color.red : Color.green);


        // checking height of the forward object
        if (obstacleData.forwardHitFound)
        {
            Vector3 heightOrigin = obstacleData.forwardHit.point + Vector3.up * heightLength;

            obstacleData.heightHitFound = Physics.Raycast(heightOrigin, Vector3.down, out obstacleData.heightHit, heightLength, layerMask);

            Debug.DrawRay(heightOrigin, Vector3.down * heightLength, (obstacleData.heightHitFound) ? Color.red : Color.green);
        }

        return obstacleData;
    }
}
