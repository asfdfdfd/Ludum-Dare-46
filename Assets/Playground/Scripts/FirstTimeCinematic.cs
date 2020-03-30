using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using DG.Tweening;

public class FirstTimeCinematic : MonoBehaviour
{
    public GameObject waypoint1;

    public GameObject waypoint2;

    public GameObject waypoint3;

    public RuntimeAnimatorController animatorControllerMarthaPink;

    public CharacterController girlGameObject;

    public DialogController dialogController;

    public CinematicManager cinematicManager;

    public void StartFirstTimeCinematic()
    {
        StartCoroutine(FirstTimeCinematicCoroutine());
    }

    private IEnumerator FirstTimeCinematicCoroutine()
    {
        cinematicManager.StartCinematic();

        yield return dialogController.Show("Мальчик", "Привет. Го тусить.");
        yield return dialogController.Show("Девочка", "Не могу, но может сестра пойдет.");
        yield return dialogController.Show("Мальчик", "У тебя есть сестра?");

        girlGameObject.SetMovementDirection(new Vector2(0.0f, 1.0f), true);

        yield return girlGameObject.transform.DOMove(waypoint1.transform.position, girlGameObject.Speed).SetSpeedBased().WaitForCompletion();
        
        girlGameObject.SetMovementDirection(new Vector2(0.0f, 1.0f), false);

        yield return new WaitForSeconds(1.0f);

        yield return dialogController.Show("Девочка", "Да, но она злая.");

        // TODO: I'd like to create animator parameter to switch between walk / idle. Speed, maybe?
        //girlGameObject.SetMovementDirection(new Vector2(0.0f, -1.0f));
        //girlGameObject.SetMovementDirection(new Vector2(0.0f, 0.0f));

        girlGameObject.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1.0f);

        girlGameObject.GetComponent<Animator>().runtimeAnimatorController = animatorControllerMarthaPink;

        girlGameObject.GetComponent<SpriteRenderer>().enabled = true;

        girlGameObject.SetMovementDirection(new Vector2(0.0f, -1.0f), true);

        yield return girlGameObject.transform.DOMove(waypoint2.transform.position, girlGameObject.Speed / 2.0f).SetSpeedBased().WaitForCompletion();

        girlGameObject.SetMovementDirection(new Vector2(-1.0f, 0.0f), false);

        yield return new WaitForSeconds(1.0f);

        yield return dialogController.Show("Мальчик", "Ты нацепила парик.");

        yield return new WaitForSeconds(1.0f);

        yield return dialogController.Show("Злая Сестра", "Да.");
        yield return dialogController.Show("Злая Сестра", "Пошли.");

        cinematicManager.StopCinematic();
    }
}
