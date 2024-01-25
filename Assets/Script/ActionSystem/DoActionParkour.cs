using System.Collections;
using UnityEngine;
using UnityEngine.XR;

[CreateAssetMenu(menuName = "ParkourIenum/ParkourIenum")]
public  class DoActionParkour : ScriptableObject
{

    public bool isAction;
    public  IEnumerator DorAction(string animName, Transform transform, Animator animator, MatchTargetParams matchTargetParams = null,
       Quaternion targetRotation = new Quaternion(),
          bool rotate = false, float postActionDelayed = 0f, bool mirror = false)
    {
        isAction = true;


        animator.SetBool("mirrorAction", mirror);
        animator.CrossFadeInFixedTime(animName, 0.2f);
        yield return null;

        var animState = animator.GetNextAnimatorStateInfo(0);
        if (!animState.IsName(animName))
            Debug.LogError("The parkour animation is wrong!");


        float rotateStartTime = (matchTargetParams != null) ? matchTargetParams.matchStartTime : 0f;

        float timer = 0f;
        while (timer <= animState.length)
        {
            timer += Time.deltaTime;

            float normalizedTime = timer / animState.length;

            if (rotate && normalizedTime > rotateStartTime)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500 * Time.deltaTime);

            if (matchTargetParams != null)
                MatchTarget(matchTargetParams, animator, transform);

            if (animator.IsInTransition(0) && timer > 0.5f)
                break;

            yield return null;
        }

        yield return new WaitForSeconds(postActionDelayed);

        isAction = false;
    }
    public void MatchTarget(MatchTargetParams mp, Animator animator, Transform transform)
    {
        if (animator.IsInTransition(0)) return;
        if (animator.isMatchingTarget) return;
        

        animator.MatchTarget(mp.pos, transform.rotation, mp.bodyPart, new MatchTargetWeightMask(mp.posWeight, 0),
            mp.matchStartTime, mp.matchTargetTime);
    }





}
