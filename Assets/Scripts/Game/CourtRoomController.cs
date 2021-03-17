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
    [SerializeField] Transform prisonManPos;
    [SerializeField] List<Transform> cameraPos;
    [SerializeField] Button buttonNextPerson;

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
            StartCoroutine(elonMusk.PlayMixamoAnimation(AnimationType.HappyMan));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        else if (courtState == CourtState.Hitler)
        {
            DocumentMovement(documents[1], documentPosDown);
            StartCoroutine(hitler.PlayMixamoAnimation(AnimationType.HappyHitler));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        yield return new WaitForSeconds(3);

        ButtonAnimation(buttonNextPerson.transform, Vector3.one);
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
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        else if (courtState == CourtState.Hitler)
        {
            DocumentMovement(documents[1], documentPosDown);
            StartCoroutine(hitler.PlayMixamoAnimation(AnimationType.SadManHitler));
            ScaleTo(speechBubbleTransform, Vector3.zero);
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
        StartCoroutine(PrisonScene(courtState));
    }

    private IEnumerator PrisonScene(CourtState currentState)
    {
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(GameManager.Instance.LocateCamera(cameraPos[1], .4f));
        yield return new WaitForSeconds(.5f);

        if (currentState == CourtState.OldMan)
        {
            TransformMovement(pryingMan.transform, prisonManPos);
            yield return new WaitForSeconds(.5f);
            StartCoroutine(pryingMan.PlayDefaultAnimation(AnimationType.SadMan));

        }
        else if (currentState == CourtState.Hitler)
        {
            TransformMovement(hitler.transform, prisonManPos);
            yield return new WaitForSeconds(.5f);
            StartCoroutine(pryingMan.PlayDefaultAnimation(AnimationType.SadManHitler));
        }
        else if (currentState == CourtState.ElonMusk)
        {
            TransformMovement(elonMusk.transform, prisonManPos);
            yield return new WaitForSeconds(.5f);
            StartCoroutine(pryingMan.PlayDefaultAnimation(AnimationType.SadMan));
        }

        yield return new WaitForSeconds(2f);
        ButtonAnimation(buttonNextPerson.transform, Vector3.one);

    }

    public void ButtonNextPerson()
    {

        ButtonAnimation(buttonNextPerson.transform, Vector3.zero);

        StartCoroutine(GameManager.Instance.LocateCamera(cameraPos[0], .5f));

        if (courtState == CourtState.OldMan)
            StartCoroutine(HitlerState());
        else if (courtState == CourtState.Hitler)
            StartCoroutine(ElonMuskState());
        else if (courtState == CourtState.ElonMusk)
            StartCoroutine(PryingManState());
    }

    private void TransformMovement(Transform transform, Transform finalTransform)
    {
        transform.DOMove(finalTransform.position, .5f);
        transform.DORotateQuaternion(finalTransform.rotation, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(PryingManState());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(HitlerState());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(ElonMuskState());
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

    private IEnumerator ElonMuskState()
    {
        yield return new WaitForSeconds(1);
        speechBubble.isSayToClear = true;
        courtState = CourtState.ElonMusk;
        ScaleTo(elonMusk.transform, new Vector3(2, 2, 2));
        ScaleTo(pryingMan.transform, Vector3.zero);
        ScaleTo(hitler.transform, Vector3.zero);
        elonMusk.PlayAnim(AnimationType.StandingArguing2);
        ObjectScaleTo(speechBubbleTransform, Vector3.one, "I just want to take people to Mars");
        ButtonAnimation(buttonPunishment.transform, Vector3.one);
        ButtonAnimation(buttonForgive.transform, Vector3.one);
        DocumentMovement(documents[2], documentPosUp);
    }

    private IEnumerator HitlerState()
    {
        yield return new WaitForSeconds(1);
        speechBubble.isSayToClear = true;
        courtState = CourtState.Hitler;
        ScaleTo(hitler.transform, new Vector3(.02f, .02f, .02f));
        ScaleTo(pryingMan.transform, Vector3.zero);
        ScaleTo(elonMusk.transform, Vector3.zero);
        hitler.PlayAnim(AnimationType.SoldierSalute);
        yield return new WaitForSeconds(3);
        hitler.PlayAnim(AnimationType.StandingArguingHitler);
        ObjectScaleTo(speechBubbleTransform, Vector3.one, "I have done this for my people!");
        ButtonAnimation(buttonPunishment.transform, Vector3.one);
        ButtonAnimation(buttonForgive.transform, Vector3.one);
        DocumentMovement(documents[1], documentPosUp);
    }

    private IEnumerator PryingManState()
    {
        yield return new WaitForSeconds(1);
        speechBubble.isSayToClear = true;
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

    public void DocumentMovement(Transform documentTransform, Transform finalPos)
    {
        documentTransform.DOMove(finalPos.position, .5f);
        documentTransform.DORotateQuaternion(finalPos.rotation, .5f);
    }
}
