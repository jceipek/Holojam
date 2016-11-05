using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] JumpTarget[] _jumpTargets;
    [SerializeField] Poltergeist _poltergeist;
    [SerializeField] Player _player;
    [SerializeField] float _timeUntilNextJump = 10f;


    bool _experienceBeginTrigger = false;
    public void HeardExperienceBegin()
    {
        _experienceBeginTrigger = true;
    }

    IEnumerator Start ()
    {
        yield return StartCoroutine(PlacingObjectState());
    }

    IEnumerator PlacingObjectState ()
    {
        foreach (var jumpTarget in _jumpTargets)
        {
            jumpTarget.SetTapToPlaceAbility(true);
        }
        while (true)
        {
            if (_experienceBeginTrigger)
            {
                foreach (var jumpTarget in _jumpTargets)
                {
                    jumpTarget.SetTapToPlaceAbility(false);
                }
                yield return StartCoroutine(ExperienceState());
            }
            yield return null;
        }
    }

    IEnumerator ExperienceState ()
    {
        float timer = 0f;
        while (true)
        {
            if (PlayerInRangeOfPoltergeist())
            {
                // Now we need to ask questions
                yield return StartCoroutine(AskingQuestionsState());
            }
            else if (TimeUntilJumpElapsed(timer))
            {
                // Now the poltergeist needs to jump to a random location
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator AskingQuestionsState() {
        yield break;
    }

    IEnumerator JumpOnFailState() {
        yield break;
    }

    bool PlayerInRangeOfPoltergeist ()
    {
        //TODO(JULIAN): IMPLEMENT ME
        return false;
    }

    bool TimeUntilJumpElapsed (float time)
    {
        return time >= _timeUntilNextJump;
    }


    void MakePoltergeistJumpToRandomLocation ()
    {
        //TODO(JULIAN): IMPLEMENT ME
    }
}
