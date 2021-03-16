using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using EpicToonFX;
using DG.Tweening;

//namespace EpicToonFX
//{
public class ETFXFireProjectile : MonoBehaviour
{
    public GameObject[] projectiles;
    [Header("Missile spawns at attached game object")]
    public Transform spawnPosition;
    [HideInInspector]
    public int currentProjectile = 0;
    public float speed = 500;
    public static int hitlerEvilDegree;
    public int value = 9;
    public Transform World;
    [SerializeField] Transform handGesture;
    [SerializeField] Transform handFinalPos;
    [SerializeField] Transform textBomb;
    [SerializeField] Transform evilEmoji;

    //    MyGUI _GUI;
    ETFXButtonScript selectedProjectileButton;

    void Start()
    {
        //selectedProjectileButton = GameObject.Find("Button").GetComponent<ETFXButtonScript>();
        handGesture.transform.localScale = Vector3.zero;
        StartCoroutine(HandGestureTutorial(handGesture, handFinalPos));
        StartCoroutine(TextBomb());
    }
    private IEnumerator TextBomb()
    {
        if (textBomb.localScale == Vector3.zero)
        {
            textBomb.DOScale(Vector3.one, 1).SetEase(Ease.InOutBack);
            yield return new WaitForSeconds(3);
            textBomb.DOScale(Vector3.zero, 1).SetEase(Ease.InOutBack);
        }
    }

    public IEnumerator HandGestureTutorial(Transform targetTransform, Transform finalPos)
    {

        if (targetTransform.gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(1);
            targetTransform.transform.localScale = Vector3.one;

            targetTransform.DOMove(finalPos.position, 1);
            targetTransform.DOScale(finalPos.localScale, .3f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(.2f);
            targetTransform.DOScale(Vector3.one, .3f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(.2f);
            targetTransform.DOScale(finalPos.localScale, .3f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(.2f);
            targetTransform.DOScale(Vector3.one, .3f).SetEase(Ease.OutBack);
        }

    }

    RaycastHit hit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextEffect();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            nextEffect();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            previousEffect();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            previousEffect();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) //On left mouse down-click
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f)) //Finds the point where you click with the mouse
            {
                GameObject projectile = Instantiate(projectiles[value], spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                projectile.transform.LookAt(hit.point); //Sets the projectiles rotation to look at the point clicked
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
                hitlerEvilDegree++;
                if (hitlerEvilDegree % 5 == 0)
                {
                    StartCoroutine(EvilEmojiAnimation());
                }
                StartCoroutine(ShakeWorld());
                if (handGesture.gameObject.activeInHierarchy)
                    handGesture.gameObject.SetActive(false);
                else
                    StartCoroutine(HandGestureTutorial(handGesture, handFinalPos));
            }

        }
        Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.yellow);
    }


    public IEnumerator EvilEmojiAnimation()
    {
        if (evilEmoji.localScale == Vector3.zero)
        {
            evilEmoji.DOScale(Vector3.one, .3f);
            yield return new WaitForSeconds(.3f);
            evilEmoji.DOLocalRotate(new Vector3(0, 0, 45f), .2f).SetEase (Ease.InOutElastic) ;
            yield return new WaitForSeconds(.2f);
            evilEmoji.DOLocalRotate(new Vector3(0, 0, -45f), .2f).SetEase(Ease.InOutElastic);
            yield return new WaitForSeconds(.2f);
            evilEmoji.DOLocalRotate(new Vector3(0, 0, -0f), .2f).SetEase(Ease.InOutElastic);
            yield return new WaitForSeconds(.2f);
            evilEmoji.DOScale(Vector3.zero, .5f).SetEase(Ease.InOutElastic);
        }
    }

    IEnumerator ShakeWorld()
    {
        yield return new WaitForSeconds(.3f);
        World.DOShakePosition(.3f, .2f, 50, 10);
        Camera.main.transform.DOShakePosition(.3f, .2f, 50, 10);
    }

    public void nextEffect() //Changes the selected projectile to the next. Used by UI
    {
        if (currentProjectile < projectiles.Length - 1)
            currentProjectile++;
        else
            currentProjectile = 0;
        selectedProjectileButton.getProjectileNames();
    }

    public void previousEffect() //Changes selected projectile to the previous. Used by UI
    {
        if (currentProjectile > 0)
            currentProjectile--;
        else
            currentProjectile = projectiles.Length - 1;
        selectedProjectileButton.getProjectileNames();
    }

    public void AdjustSpeed(float newSpeed) //Used by UI to set projectile speed
    {
        speed = newSpeed;
    }
}
//}