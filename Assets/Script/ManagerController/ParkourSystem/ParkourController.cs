using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
    [SerializeField] DoActionParkour doParkour;
    [SerializeField] List<ParkourAction> parkourActions;
    [SerializeField] ParkourAction jumpDownAction;
    [SerializeField] ObstacleCheck obstackleCheck;


   
    Animator animator;
    PlayerControler playerController;
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerControler>();
    }

    private void Update()
    {
        var hitData = obstackleCheck._obstacleCheck(this.transform);
        if (Input.GetButton("Jump") && !doParkour.isAction)
        {

            if (hitData.forwardHitFound)
            {
                foreach (var action in parkourActions)
                {
                    if (action.CheckIfPossible(hitData, transform))
                    {
                        StartCoroutine(DoParkourAction(action));
                        break;
                    }
                }
            }
        }

        if (playerController.isLedge == true && !doParkour.isAction && !hitData.forwardHitFound && Input.GetButton("Jump"))
        {
            if (playerController.LedgeData.angle <= 30)
            {
                playerController.isLedge = false;
                StartCoroutine(DoParkourAction(jumpDownAction));
            }
        }
    }

    IEnumerator DoParkourAction(ParkourAction action)
    {
        playerController.SetControl(false);
        MatchTargetParams matchParams = null;
        if (action._enableTargetMatching)
        {
            matchParams = new MatchTargetParams()
            {
                bodyPart = action._matchBodyPart,
                pos = action.matchPos,
                matchStartTime = action._matchStartTime,
                posWeight = action._matchPosWeight,
                matchTargetTime = action._matchTargetTime,
            };
        }
        yield return doParkour.DorAction(action._animName, transform, animator, matchParams, action.targetRotation, action._RotateBool, action._postActionDelayed, action.mirror); 
            /*playerController.DorAction(action._animName, matchParams, action.targetRotation,action._RotateBool, action._postActionDelayed, action.mirror);*/

        playerController.SetControl(true);
    }



}
