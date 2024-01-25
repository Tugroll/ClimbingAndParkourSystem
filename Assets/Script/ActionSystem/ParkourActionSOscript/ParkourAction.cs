using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Parkour System/New Parkour Action")]
public class ParkourAction : ScriptableObject
{
    [SerializeField] string aniName;
    [SerializeField] bool rotateBool;
  

    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;

    [SerializeField] string tagName;

    [SerializeField] float postActionDelayed;
    [SerializeField] bool enableTargetMatching = true;
    [SerializeField] protected AvatarTarget matchBodyPart;
    [SerializeField] float matchStartTime;
    [SerializeField] float matchTargetTime;
    [SerializeField] Vector3 matchPosWeight = new Vector3(0,1,0);
    public Quaternion targetRotation { get; set; }
    public Vector3 matchPos { get; set; }

    public bool mirror;

    public virtual bool CheckIfPossible(ObstacleData hitData, Transform transform)
    {
        if (!string.IsNullOrEmpty(tagName) && tagName != hitData.forwardHit.transform.tag)
         return false;

        
       float height = hitData.heightHit.point.y - transform.position.y;
        float weight = hitData.forwardHit.point.z;

        if (height < minHeight || height > maxHeight) 
            return false;

        if (rotateBool == true)
        {
            targetRotation = Quaternion.LookRotation(-hitData.forwardHit.normal);
        }
        if (enableTargetMatching)
            matchPos = hitData.heightHit.point;


        
        return true;
    }


    public string _animName => aniName;
    public bool _RotateBool => rotateBool;

    public bool _enableTargetMatching => enableTargetMatching;
    public AvatarTarget _matchBodyPart => matchBodyPart;
    public float _matchStartTime => matchStartTime;
    public float _matchTargetTime => matchTargetTime;

    public Vector3 _matchPosWeight => matchPosWeight;
    public float _postActionDelayed => postActionDelayed;
}
