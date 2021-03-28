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
    [SerializeField] Guard guard;
    [SerializeField] List<Transform> guardLocations;

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
        if (guard.gameObject.activeInHierarchy)
            guard.PlayAnim(AnimationType.GuardIdle);
    }

    private void ScaleTo(Transform transform, Vector3 vector3)
    {
        transform.DOScale(vector3, 1).SetEase(Ease.OutElastic);
        //transform.DOPunchScale(vector3, 1);
    }

    private void ButtonAnimation(Transform transform, Vector3 scale)
    {
        transform.DOScale(scale, 1).SetEase(Ease.InOutBack);
        //transform.DOPunchScale(scale, 1);
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
            pryingMan.isHumanGuilty = false;
            DocumentMovement(documents[0], documentPosDown);
            StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.HappyMan));
            ScaleTo(speechBubbleTransform, Vector3.zero);
            yield return new WaitForSeconds(1.2f);
            StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.Standing));

        }
        else if (courtState == CourtState.ElonMusk)
        {
            elonMusk.isHumanGuilty = false;
            DocumentMovement(documents[2], documentPosDown);
            StartCoroutine(elonMusk.PlayMixamoAnimation(AnimationType.HappyMan));
            ScaleTo(speechBubbleTransform, Vector3.zero);
            yield return new WaitForSeconds(1.2f);
            StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.Standing));
        }
        else if (courtState == CourtState.Hitler)
        {
            hitler.isHumanGuilty = false;
            DocumentMovement(documents[1], documentPosDown);
            StartCoroutine(hitler.PlayMixamoAnimation(AnimationType.HappyHitler));
            ScaleTo(speechBubbleTransform, Vector3.zero);
            yield return new WaitForSeconds(1.2f);
            StartCoroutine(pryingMan.PlayMixamoAnimation(AnimationType.HitlerStanding));
        }
        else if (courtState == CourtState.Spiderman)
        {
            spiderman.isHumanGuilty = false;
            DocumentMovement(documents[3], documentPosDown);
            StartCoroutine(spiderman.PlayMixamoAnimation(AnimationType.SpidermanHappy));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        else if (courtState == CourtState.Trump)
        {
            trump.isHumanGuilty = false;
            DocumentMovement(documents[4], documentPosDown);
            StartCoroutine(trump.PlayMixamoAnimation(AnimationType.TrumpHappy));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }

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
            DocumentMovement(documents[3], documentPosDown);
            StartCoroutine(spiderman.PlayMixamoAnimation(AnimationType.SpidermanSad));
            ScaleTo(speechBubbleTransform, Vector3.zero);
        }
        else if (courtState == CourtState.Trump)
        {
            DocumentMovement(documents[4], documentPosDown);
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
        StartCoroutine(PoliceScene(courtState));
        //StartCoroutine(PrisonScene(courtState));
    }

    IEnumerator PoliceScene(CourtState currentState)
    {
        yield return new WaitForSeconds(1);

        if (currentState == CourtState.OldMan)
        {
            Vector3 oldManPos = new Vector3(-0.16f, 0f, 0);
            pryingMan.isHumanGuilty = true;
            pryingMan.PlayAnim(AnimationType.SadMan, .3f, 1.2f);
            guard.PlayAnim(AnimationType.GuardWalking);
            guard.transform.DOMove(guardLocations[1].position, 2.5f);
            yield return new WaitForSeconds(2.5f);
            guard.PlayAnim(AnimationType.GuardIdle, .3f, 1.2f);
            yield return new WaitForSeconds(1);
            pryingMan.transform.DOMove(oldManPos, .58f);
            StartCoroutine(LeavingCourtRoom(pryingMan, AnimationType.ManTurnLeft, AnimationType.ManWalking, turnedManTransform[0], roomLeavingLocation));
            yield return new WaitForSeconds(1.5f);
            GameManager.Instance.FadeAnimation(true);
            guard.PlayAnim(AnimationType.GuardWalking);
            guard.transform.DOMove(roomLeavingLocation.position, 1.8f);
            yield return new WaitForSeconds(1.8f);
            guard.transform.position = guardLocations[0].position;
            GameManager.Instance.FadeAnimation(false);
            guard.PlayAnim(AnimationType.GuardIdle);
            pryingMan.gameObject.SetActive(false);
            //pryingMan.PlayAnim(AnimationType.Running);
        }
        if (currentState == CourtState.Hitler)
        {
            Vector3 oldManPos = new Vector3(-0.16f, 0f, 0);
            hitler.isHumanGuilty = true;
            hitler.PlayAnim(AnimationType.SadMan, .3f, 1.2f);
            guard.PlayAnim(AnimationType.GuardWalking);
            guard.transform.DOMove(guardLocations[1].position, 2.5f);
            yield return new WaitForSeconds(2.5f);
            guard.PlayAnim(AnimationType.GuardIdle, .3f, 1.2f);
            yield return new WaitForSeconds(1);
            hitler.transform.DOMove(oldManPos, .58f);
            StartCoroutine(LeavingCourtRoom(hitler, AnimationType.HitlerTurnLeft, AnimationType.HitlerWalking, turnedManTransform[0], roomLeavingLocation));
            yield return new WaitForSeconds(1.5f);
            GameManager.Instance.FadeAnimation(true);
            guard.PlayAnim(AnimationType.GuardWalking);
            guard.transform.DOMove(roomLeavingLocation.position, 1.8f);
            yield return new WaitForSeconds(1.8f);
            GameManager.Instance.FadeAnimation(false);
            guard.transform.position = guardLocations[0].position;
            guard.PlayAnim(AnimationType.GuardIdle);
            hitler.gameObject.SetActive(false);
            //pryingMan.PlayAnim(AnimationType.Running);
        }
         if (currentState == CourtState.Spiderman)
        {
            Vector3 oldManPos = new Vector3(-0.16f, 0f, 0);
            spiderman.isHumanGuilty = true;
            spiderman.PlayAnim(AnimationType.SpidermanSad, .3f, 1.2f);
            guard.PlayAnim(AnimationType.GuardWalking);
            guard.transform.DOMove(guardLocations[1].position, 2.5f);
            yield return new WaitForSeconds(2.5f);
            guard.PlayAnim(AnimationType.GuardIdle, .3f, 1.2f);
            yield return new WaitForSeconds(1);
            spiderman.transform.DOMove(oldManPos, .58f);
            StartCoroutine(LeavingCourtRoom(spiderman, AnimationType.SpidermanTurnLeft, AnimationType.SpidermanWalking, turnedManTransform[0], roomLeavingLocation));
            yield return new WaitForSeconds(1.5f);
            GameManager.Instance.FadeAnimation(true);
            guard.PlayAnim(AnimationType.GuardWalking);
            guard.transform.DOMove(roomLeavingLocation.position, 1.8f);
            yield return new WaitForSeconds(1.8f);
            GameManager.Instance.FadeAnimation(false);
            guard.transform.position = guardLocations[0].position;
            guard.PlayAnim(AnimationType.GuardIdle);
            spiderman.gameObject.SetActive(false);
            //pryingMan.PlayAnim(AnimationType.Running);
        }
         
         if (currentState == CourtState.Trump)
        {
            Vector3 oldManPos = new Vector3(-0.16f, 0f, 0);
            trump.isHumanGuilty = true;
            trump.PlayAnim(AnimationType.TrumpSad, .3f, 1.2f);
            guard.PlayAnim(AnimationType.GuardWalking);
            guard.transform.DOMove(guardLocations[1].position, 2.5f);
            yield return new WaitForSeconds(2.5f);
            guard.PlayAnim(AnimationType.GuardIdle, .3f, 1.2f);
            yield return new WaitForSeconds(1);
            trump.transform.DOMove(oldManPos, .58f);
            StartCoroutine(LeavingCourtRoom(trump, AnimationType.TrumpTurnLeft, AnimationType.TrumpWalking, turnedManTransform[0], roomLeavingLocation));
            yield return new WaitForSeconds(1.5f);
            GameManager.Instance.FadeAnimation(true);
            guard.PlayAnim(AnimationType.GuardWalking);
            guard.transform.DOMove(roomLeavingLocation.position, 1.8f);
            yield return new WaitForSeconds(1.8f);
            GameManager.Instance.FadeAnimation(false);
            guard.transform.position = guardLocations[0].position;
            guard.PlayAnim(AnimationType.GuardIdle);
            trump.gameObject.SetActive(false);
            //pryingMan.PlayAnim(AnimationType.Running);
        }




        ButtonAnimation(buttonNextPerson.transform, Vector3.one);
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
            pryingMan.isHumanGuilty = true;
            yield return new WaitForSeconds(.25f);
            StartCoroutine(pryingMan.PlayDefaultAnimation(AnimationType.SadMan));
        }
        else if (currentState == CourtState.Hitler)
        {
            TransformMovement(hitler.transform, prisonManPos);
            hitler.isHumanGuilty = true;
            yield return new WaitForSeconds(.25f);
            StartCoroutine(hitler.PlayDefaultAnimation(AnimationType.SadManHitler));
        }
        else if (currentState == CourtState.ElonMusk)
        {
            TransformMovement(elonMusk.transform, prisonManPos);
            elonMusk.isHumanGuilty = true;
            yield return new WaitForSeconds(.25f);
            StartCoroutine(elonMusk.PlayDefaultAnimation(AnimationType.SadMan));
        }
        else if (currentState == CourtState.Trump)
        {
            TransformMovement(trump.transform, prisonManPos);
            trump.isHumanGuilty = true;
            yield return new WaitForSeconds(.25f);
            StartCoroutine(trump.PlayDefaultAnimation(AnimationType.TrumpSad));
        }
        else if (currentState == CourtState.Spiderman)
        {
            TransformMovement(spiderman.transform, prisonManPos);
            spiderman.isHumanGuilty = true;
            yield return new WaitForSeconds(.25f);
            StartCoroutine(spiderman.PlayDefaultAnimation(AnimationType.SpidermanSad));
        }
        GameManager.Instance.FadeAnimation(false);
        yield return new WaitForSeconds(1f);
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

        ButtonAnimation(buttonNextPerson.transform, Vector3.zero);
        yield return new WaitForSeconds(.4f);

        StartCoroutine(GameManager.Instance.LocateCamera(cameraPos[0], .5f));
        //StartCoroutine(ActiveStatue(pryingMan, pryingMan.isHumanGuilty));
        //StartCoroutine(ActiveStatue(hitler, hitler.isHumanGuilty));
        //StartCoroutine(ActiveStatue(elonMusk, elonMusk.isHumanGuilty));
        //StartCoroutine(ActiveStatue(spiderman, spiderman.isHumanGuilty));
        //StartCoroutine(ActiveStatue(trump, trump.isHumanGuilty));
        if (courtState == CourtState.OldMan)
        {
            if (pryingMan.gameObject.activeInHierarchy)
            {
                StartCoroutine(LeavingCourtRoom(pryingMan, AnimationType.ManTurnLeft, AnimationType.ManWalking, turnedManTransform[0], roomLeavingLocation));
                yield return new WaitForSeconds(1);
            }
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

            guard.transform.position = guardLocations[0].position;
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
        human.transform.position = new Vector3(-.16f, 0, -.25f);
        human.PlayAnim(turningLeftAnim,.3f,1.2f);
        human.transform.DOLocalRotateQuaternion(turnRotation.localRotation, 1);
        yield return new WaitForSeconds(1f);
        human.PlayAnim(walkingAnim, .3f, 1.2f);
        human.transform.DOMove(endPosition.position, 3);
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
        yield return new WaitForSeconds(1);
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
        DocumentMovement(documents[3], documentPosUp);
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
        DocumentMovement(documents[4], documentPosUp);
    }

    public void DocumentMovement(Transform documentTransform, Transform finalPos)
    {
        documentTransform.DOMove(finalPos.position, .5f);
        documentTransform.DORotateQuaternion(finalPos.rotation, .5f);
    }
}
