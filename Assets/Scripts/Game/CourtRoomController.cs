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
    ElonMusk,
    Spiderman,
    Trump
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
    [SerializeField] Spiderman spiderman;
    [SerializeField] Trump trump;
    [SerializeField] Transform speakingLocation;
    [SerializeField] Transform roomLeavingLocation;
    [SerializeField] List<Transform> turnedManTransform;

    CourtState courtState;
    // Start is called before the first frame update
    void Start()
    {
        speechBubbleTransform.transform.localScale = Vector3.zero;
        speechBubleButton.transform.localScale = Vector3.zero;

        hitler.PlayAnim(AnimationType.HitlerStanding);
        spiderman.PlayAnim(AnimationType.SpidermanStanding);
        trump.PlayAnim(AnimationType.TrumpStanding);
        StartCoroutine(PryingManState());
    }

    private void ScaleTo(Transform transform, Vector3 vector3)
    {
        transform.DOScale(vector3, 1).SetEase(Ease.OutElastic);
    }

    private void ButtonAnimation(Transform transform, Vector3 scale)
    {
        transform.DOScale(scale, 1).SetEase(Ease.OutElastic);
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
            pryingMan.isInPrison = false;
            DocumentMovement(documents[0], documentPosDown);
            StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.HappyMan));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        else if (courtState == CourtState.ElonMusk)
        {
            elonMusk.isInPrison = false;
            DocumentMovement(documents[2], documentPosDown);
            StartCoroutine(elonMusk.PlayMixamoAnimation(AnimationType.HappyMan));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        else if (courtState == CourtState.Hitler)
        {
            hitler.isInPrison = false;
            DocumentMovement(documents[1], documentPosDown);
            StartCoroutine(hitler.PlayMixamoAnimation(AnimationType.HappyHitler));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        else if (courtState == CourtState.Spiderman)
        {
            spiderman.isInPrison = false;
            DocumentMovement(documents[1], documentPosDown);
            StartCoroutine(spiderman.PlayMixamoAnimation(AnimationType.SpidermanHappy));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        else if (courtState == CourtState.Trump)
        {
            trump.isInPrison = false;
            DocumentMovement(documents[1], documentPosDown);
            StartCoroutine(trump.PlayMixamoAnimation(AnimationType.TrumpHappy));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        yield return new WaitForSeconds(3);

        ButtonAnimation(buttonNextPerson.transform, Vector3.one);
        yield return new WaitForSeconds(.5f);
        //if (courtState == CourtState.OldMan)
        //    pryingMan.transform.localScale = Vector3.zero;
        //else if (courtState == CourtState.Hitler)
        //    hitler.transform.localScale = Vector3.zero;
        //else if (courtState == CourtState.Spiderman)
        //    spiderman.transform.localScale = Vector3.zero;
        //else if (courtState == CourtState.Trump)
        //    elonMusk.transform.localScale = Vector3.zero;
        //else if (courtState == CourtState.ElonMusk)
        //    elonMusk.transform.localScale = Vector3.zero;
        //elonMusk.transform.localScale = Vector3.zero;
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
        else if (courtState == CourtState.Spiderman)
        {
            DocumentMovement(documents[1], documentPosDown);
            StartCoroutine(spiderman.PlayMixamoAnimation(AnimationType.SpidermanSad));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        else if (courtState == CourtState.Trump)
        {
            DocumentMovement(documents[1], documentPosDown);
            StartCoroutine(trump.PlayMixamoAnimation(AnimationType.TrumpSad));
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
        GameManager.Instance.FadeAnimation(true);
        StartCoroutine(GameManager.Instance.LocateCamera(cameraPos[1], 1f));
        yield return new WaitForSeconds(.5f);

        if (currentState == CourtState.OldMan)
        {
            TransformMovement(pryingMan.transform, prisonManPos);
            pryingMan.isInPrison = true;
            yield return new WaitForSeconds(.25f);
            StartCoroutine(pryingMan.PlayDefaultAnimation(AnimationType.SadMan));
        }
        else if (currentState == CourtState.Hitler)
        {
            TransformMovement(hitler.transform, prisonManPos);
            hitler.isInPrison = true;
            yield return new WaitForSeconds(.25f);
            StartCoroutine(hitler.PlayDefaultAnimation(AnimationType.SadManHitler));
        }
        else if (currentState == CourtState.ElonMusk)
        {
            TransformMovement(elonMusk.transform, prisonManPos);
            elonMusk.isInPrison = true;
            yield return new WaitForSeconds(.25f);
            StartCoroutine(elonMusk.PlayDefaultAnimation(AnimationType.SadMan));
        }
        else if (currentState == CourtState.Trump)
        {
            TransformMovement(trump.transform, prisonManPos);
            trump.isInPrison = true;
            yield return new WaitForSeconds(.25f);
            StartCoroutine(trump.PlayDefaultAnimation(AnimationType.TrumpSad));
        }
        else if (currentState == CourtState.Spiderman)
        {
            TransformMovement(spiderman.transform, prisonManPos);
            spiderman.isInPrison = true;
            yield return new WaitForSeconds(.25f);
            StartCoroutine(spiderman.PlayDefaultAnimation(AnimationType.SpidermanSad));
        }
        GameManager.Instance.FadeAnimation(false);
        yield return new WaitForSeconds(2f);
        ButtonAnimation(buttonNextPerson.transform, Vector3.one);

    }


    public void ButtonNextPerson()
    {
        StartCoroutine(NextPerson());
    }

    private IEnumerator ActiveStatue(Human human, bool isPrison)
    {
        if (isPrison)
        {
            yield return new WaitForSeconds(1);
            human.gameObject.SetActive(false);
        }
    }

    IEnumerator NextPerson()
    {
        yield return new WaitForSeconds(1);

        ButtonAnimation(buttonNextPerson.transform, Vector3.zero);

        StartCoroutine(GameManager.Instance.LocateCamera(cameraPos[0], .5f));
        StartCoroutine(ActiveStatue(pryingMan, pryingMan.isInPrison));
        StartCoroutine(ActiveStatue(hitler, hitler.isInPrison));
        StartCoroutine(ActiveStatue(elonMusk, elonMusk.isInPrison));
        StartCoroutine(ActiveStatue(spiderman, spiderman.isInPrison));
        StartCoroutine(ActiveStatue(trump, trump.isInPrison));
        if (courtState == CourtState.OldMan)
        {
            StartCoroutine(LeavingCourtRoom(pryingMan, AnimationType.ManTurnLeft, AnimationType.ManWalking, turnedManTransform[0], roomLeavingLocation));
            yield return new WaitForSeconds(1);
            StartCoroutine(HitlerState());
        }
        else if (courtState == CourtState.Hitler)
        {
            StartCoroutine(LeavingCourtRoom(hitler, AnimationType.HitlerTurnLeft, AnimationType.HitlerWalking, turnedManTransform[0], roomLeavingLocation));
            yield return new WaitForSeconds(1);
            StartCoroutine(SpidermanState());
        }
        else if (courtState == CourtState.Spiderman)
        {
            StartCoroutine(LeavingCourtRoom(spiderman, AnimationType.SpidermanTurnLeft, AnimationType.SpidermanWalking, turnedManTransform[0], roomLeavingLocation));
            yield return new WaitForSeconds(1);
            StartCoroutine(TrumpState());
        }
        else if (courtState == CourtState.Trump)
        {
            StartCoroutine(LeavingCourtRoom(trump, AnimationType.TrumpTurnLeft, AnimationType.TrumpWalking, turnedManTransform[0], roomLeavingLocation));
            yield return new WaitForSeconds(1);
            StartCoroutine(ElonMuskState());
        }
        else if (courtState == CourtState.ElonMusk)
        {
            StartCoroutine(LeavingCourtRoom(elonMusk, AnimationType.ManTurnLeft, AnimationType.ManWalking, turnedManTransform[0], roomLeavingLocation));
            yield return new WaitForSeconds(1);
            StartCoroutine(PryingManState());
        }
    }

    private void TransformMovement(Transform transform, Transform finalTransform)
    {
        transform.DOMove(finalTransform.position, .5f);
        transform.DORotateQuaternion(finalTransform.rotation, .5f);
    }

    private IEnumerator WalkToSpeakPoint(Human human, float time, AnimationType animationType, AnimationType standingAnimation)
    {
        human.PlayAnim(animationType);
        human.transform.DOMove(speakingLocation.position, time);
        yield return new WaitForSeconds(time);
        human.PlayAnim(standingAnimation);

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
            StartCoroutine(SpidermanState());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(TrumpState());
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(LeavingCourtRoom(pryingMan, AnimationType.ManTurnLeft, AnimationType.ManWalking, turnedManTransform[0], roomLeavingLocation));

            //StartCoroutine(judge.JudgeHit());
        }
        //else
        //{
        //    courtState = CourtState.OldMan;
        //    ScaleTo(pryingMan.transform, new Vector3(2, 2, 2));
        //    ScaleTo(hitler.transform, Vector3.zero);
        //    ScaleTo(elonMusk.transform, Vector3.zero);
        //}
    }

    private IEnumerator LeavingCourtRoom(Human human, AnimationType turningLeftAnim, AnimationType walkingAnim, Transform turnRotation, Transform endPosition)
    {
        print(human.isInPrison);
        if (!human.isInPrison)
        {
            human.transform.position = new Vector3(-.16f, 0, -.25f);
            human.PlayAnim(turningLeftAnim);
            human.transform.DOLocalRotateQuaternion(turnRotation.localRotation, 1);
            yield return new WaitForSeconds(1.5f);
            human.PlayAnim(walkingAnim);
            human.transform.DOMove(endPosition.position, 2);
        }
    }

    private IEnumerator PryingManState()
    {
        yield return new WaitForSeconds(1);
        speechBubble.isSayToClear = true;
        courtState = CourtState.OldMan;
        ScaleTo(pryingMan.transform, new Vector3(2, 2, 2));
        //ScaleTo(hitler.transform, Vector3.zero);
        //ScaleTo(elonMusk.transform, Vector3.zero);
        pryingMan.PlayAnim(AnimationType.StandingArguing1);
        ObjectScaleTo(speechBubbleTransform, Vector3.one, "I'm not guilty. I did nothing.");
        speechBubleButton.localScale = Vector3.one;
        ButtonAnimation(buttonPunishment.transform, Vector3.one);
        ButtonAnimation(buttonForgive.transform, Vector3.one);
        DocumentMovement(documents[0], documentPosUp);
    }

    private IEnumerator ElonMuskState()
    {
        yield return new WaitForSeconds(1);
        speechBubble.isSayToClear = true;
        courtState = CourtState.ElonMusk;
        ScaleTo(elonMusk.transform, new Vector3(2, 2, 2));
        //ScaleTo(pryingMan.transform, Vector3.zero);
        //ScaleTo(hitler.transform, Vector3.zero);
        elonMusk.PlayAnim(AnimationType.StandingArguing2);
        ObjectScaleTo(speechBubbleTransform, Vector3.one, "I just want to take people to Mars");
        ButtonAnimation(buttonPunishment.transform, Vector3.one);
        ButtonAnimation(buttonForgive.transform, Vector3.one);
        DocumentMovement(documents[2], documentPosUp);
    }

    private IEnumerator HitlerState()
    {
        yield return new WaitForSeconds(.5f);
        StartCoroutine(WalkToSpeakPoint(hitler, .5f, AnimationType.HitlerWalking, AnimationType.StandingArguingHitler));
        yield return new WaitForSeconds(1);
        speechBubble.isSayToClear = true;
        courtState = CourtState.Hitler;
        ScaleTo(hitler.transform, new Vector3(.02f, .02f, .02f));
        //ScaleTo(pryingMan.transform, Vector3.zero);
        //ScaleTo(elonMusk.transform, Vector3.zero);
        hitler.PlayAnim(AnimationType.SoldierSalute);
        yield return new WaitForSeconds(3);
        hitler.PlayAnim(AnimationType.StandingArguingHitler);
        ObjectScaleTo(speechBubbleTransform, Vector3.one, "I have done this for my people!");
        ButtonAnimation(buttonPunishment.transform, Vector3.one);
        ButtonAnimation(buttonForgive.transform, Vector3.one);
        DocumentMovement(documents[1], documentPosUp);
    }
    private IEnumerator SpidermanState()
    {
        StartCoroutine(WalkToSpeakPoint(spiderman, 1, AnimationType.SpidermanWalking, AnimationType.SpidermanTalking));
        yield return new WaitForSeconds(1);
        speechBubble.isSayToClear = true;
        courtState = CourtState.Spiderman;
        //ScaleTo(spiderman.transform, new Vector3(1.4f, 1.4f, 1.4f));
        //ScaleTo(pryingMan.transform, Vector3.zero);
        //ScaleTo(elonMusk.transform, Vector3.zero);
        ObjectScaleTo(speechBubbleTransform, Vector3.one, "HEY! I am a hero not a criminal!");
        ButtonAnimation(buttonPunishment.transform, Vector3.one);
        ButtonAnimation(buttonForgive.transform, Vector3.one);
        DocumentMovement(documents[1], documentPosUp);
    }

    private IEnumerator TrumpState()
    {
        StartCoroutine(WalkToSpeakPoint(trump, 1.4f, AnimationType.TrumpWalking, AnimationType.TrumpTalking));
        yield return new WaitForSeconds(1);
        speechBubble.isSayToClear = true;
        courtState = CourtState.Trump;
        //ScaleTo(hitler.transform, new Vector3(.02f, .02f, .02f));
        //ScaleTo(pryingMan.transform, Vector3.zero);
        //ScaleTo(elonMusk.transform, Vector3.zero);
        //hitler.PlayAnim(AnimationType.Standing);
        //yield return new WaitForSeconds(3);
        //hitler.PlayAnim(AnimationType.StandingArguingHitler);
        ObjectScaleTo(speechBubbleTransform, Vector3.one, "Let's make America great again.");
        ButtonAnimation(buttonPunishment.transform, Vector3.one);
        ButtonAnimation(buttonForgive.transform, Vector3.one);
        DocumentMovement(documents[1], documentPosUp);
    }

    public void DocumentMovement(Transform documentTransform, Transform finalPos)
    {
        documentTransform.DOMove(finalPos.position, .5f);
        documentTransform.DORotateQuaternion(finalPos.rotation, .5f);
    }
}
