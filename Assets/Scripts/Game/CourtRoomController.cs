using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    CourtState courtState;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void ScaleTo(Transform transform, Vector3 vector3)
    {
        transform.DOScale(vector3,.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            courtState = CourtState.OldMan;
            ScaleTo(pryingMan.transform, new Vector3(2,2,2));
            ScaleTo(hitler.transform, Vector3.zero);
            ScaleTo(elonMusk.transform, Vector3.zero);
            pryingMan.PlayAnim(AnimationType.StandingArguing1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            courtState = CourtState.Hitler;
            ScaleTo(hitler.transform, new Vector3(.02f, .02f, .02f));
            ScaleTo(pryingMan.transform, Vector3.zero);
            ScaleTo(elonMusk.transform, Vector3.zero);
            hitler.PlayAnim(AnimationType.StandingArguingHitler);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            courtState = CourtState.ElonMusk;
            ScaleTo(elonMusk.transform, new Vector3(2, 2, 2));
            ScaleTo(pryingMan.transform, Vector3.zero);
            ScaleTo(hitler.transform, Vector3.zero);
            elonMusk.PlayAnim(AnimationType.StandingArguing2);
        }
        //else
        //{
        //    courtState = CourtState.OldMan;
        //    ScaleTo(pryingMan.transform, new Vector3(2, 2, 2));
        //    ScaleTo(hitler.transform, Vector3.zero);
        //    ScaleTo(elonMusk.transform, Vector3.zero);
        //}
    }


}
