using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CourtState
{
    OldMan,
    Hitler,
    ElonMusk
}

public class CourtRoomController : LocalSingleton<CourtRoomController>
{

    [SerializeField] PryingMan pryingMan;
    [SerializeField] Hitler hitler;
    [SerializeField] ElonMusk elonMusk;
    [SerializeField] Transform speechBubbleTransform;
    [SerializeField] Transform speechBubleButton;
    [SerializeField] SpeechBubbleCtrl speechBubble;
    [SerializeField] Button buttonPunishment;
    [SerializeField] Button buttonForgive;
    [SerializeField] Judge judge;
    [SerializeField] List<Transform> documents;
    [SerializeField] Transform documentPosUp;
    [SerializeField] Transform documentPosDown;

    CourtState courtState;
    // Start is called before the first frame update
    void Start()
    {
        speechBubbleTransform.transform.localScale = Vector3.zero;
        speechBubleButton.transform.localScale = Vector3.zero;
    }

    private void ScaleTo(Transform transform, Vector3 vector3)
    {
        transform.DOScale(vector3, .1f);
    }

    private void ButtonAnimation(Transform transform, Vector3 scale)
    {
        transform.DOScale(scale, 1).SetEase(Ease.OutBounce);
    }

    public void ObjectScaleTo(Transform transform, Vector3 scale, string speechText)
    {
        transform.DOScale(scale, 1).SetEase(Ease.OutBounce);
        speechBubble.setText(speechText);
    }

    private IEnumerator ForgivenHappiness(CourtState courtState)
    {
        yield return new WaitForSeconds(1);
        if (courtState == CourtState.OldMan)
        {
            DocumentMovement(documents[0], documentPosDown);
            StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.HappyMan));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        else if (courtState == CourtState.ElonMusk)
        {
            DocumentMovement(documents[2], documentPosDown);
            StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.HappyMan));
        }
        else if (courtState == CourtState.Hitler)
        {
            DocumentMovement(documents[1], documentPosDown);
            StartCoroutine(hitler.PlayMixamoAnimation(AnimationType.HappyHitler));
        }
    }
    private IEnumerator PunishmentSadness(CourtState courtState)
    {
        yield return new WaitForSeconds(1);
        if (courtState == CourtState.OldMan)
        {
            DocumentMovement(documents[0], documentPosDown);
            StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.SadMan));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        else if (courtState == CourtState.ElonMusk)
        {
            DocumentMovement(documents[2], documentPosDown);
            StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.SadMan));
        }
        else if (courtState == CourtState.Hitler)
        {
            DocumentMovement(documents[1], documentPosDown);
            StartCoroutine(hitler.PlayMixamoAnimation(AnimationType.SadManHitler));
        }
    }

    public void Forgive()
    {
        ButtonAnimation(buttonPunishment.transform, Vector3.zero);
        ButtonAnimation(buttonForgive.transform, Vector3.zero);
        StartCoroutine(ForgivenHappiness(courtState));
        StartCoroutine(judge.JudgeHit());
    }
    public void Punishment()
    {
        ButtonAnimation(buttonPunishment.transform, Vector3.zero);
        ButtonAnimation(buttonForgive.transform, Vector3.zero);
        StartCoroutine(PunishmentSadness(courtState));
        StartCoroutine(judge.JudgeHit());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            courtState = CourtState.OldMan;
            ScaleTo(pryingMan.transform, new Vector3(2, 2, 2));
            ScaleTo(hitler.transform, Vector3.zero);
            ScaleTo(elonMusk.transform, Vector3.zero);
            pryingMan.PlayAnim(AnimationType.StandingArguing1);
            ObjectScaleTo(speechBubbleTransform, Vector3.one, "I'm not guilty. I did nothing.");
            speechBubleButton.localScale = Vector3.one;
            ButtonAnimation(buttonPunishment.transform, Vector3.one);
            ButtonAnimation(buttonForgive.transform, Vector3.one);
            DocumentMovement(documents[0], documentPosUp);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            courtState = CourtState.Hitler;
            ScaleTo(hitler.transform, new Vector3(.02f, .02f, .02f));
            ScaleTo(pryingMan.transform, Vector3.zero);
            ScaleTo(elonMusk.transform, Vector3.zero);
            hitler.PlayAnim(AnimationType.StandingArguingHitler);
            ButtonAnimation(buttonPunishment.transform, Vector3.one);
            ButtonAnimation(buttonForgive.transform, Vector3.one);
            DocumentMovement(documents[1], documentPosUp);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            courtState = CourtState.ElonMusk;
            ScaleTo(elonMusk.transform, new Vector3(2, 2, 2));
            ScaleTo(pryingMan.transform, Vector3.zero);
            ScaleTo(hitler.transform, Vector3.zero);
            elonMusk.PlayAnim(AnimationType.StandingArguing2);
            ButtonAnimation(buttonPunishment.transform, Vector3.one);
            ButtonAnimation(buttonForgive.transform, Vector3.one);
            DocumentMovement(documents[2], documentPosUp);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(judge.JudgeHit());
        }
        //else
        //{
        //    courtState = CourtState.OldMan;
        //    ScaleTo(pryingMan.transform, new Vector3(2, 2, 2));
        //    ScaleTo(hitler.transform, Vector3.zero);
        //    ScaleTo(elonMusk.transform, Vector3.zero);
        //}
    }

    public void DocumentMovement(Transform documentTransform, Transform finalPos)
    {
        documentTransform.DOMove(finalPos.position, .5f);
        documentTransform.DORotateQuaternion(finalPos.rotation, .5f);
    }
}
