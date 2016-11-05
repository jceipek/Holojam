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

    bool _askedAQuestionTrigger = false;
    public void AskedAQuestion ()
    {
        _askedAQuestionTrigger = true;
    }

    public void HeardPassword() {
        StopAllCoroutines();
        StartCoroutine(FinaleRoutine());
    }

    IEnumerator Start ()
    {
        yield return StartCoroutine(PlacingObjectState());
    }

    IEnumerator PlacingObjectState ()
    {
        _experienceBeginTrigger = false;
        foreach (var jumpTarget in _jumpTargets)
        {
            jumpTarget.SetTapToPlaceAbility(true);
        }
        while (true)
        {
            if (_experienceBeginTrigger) // Set by HeardExperienceBegin()
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
        _poltergeist.TeleportToJumpTarget(_jumpTargets[0]);
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
                // TODO(JULIAN): Laugh all over
                yield return StartCoroutine(_poltergeist.JumpToNewLocationRoutine(shouldLaugh: false));
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator AskingQuestionsState() {
        _askedAQuestionTrigger = false;
        int questionCount = 0;
        while (questionCount < 2)
        {
            if (_askedAQuestionTrigger) // Set by AskedAQuestion()
            {
                //XXX(JULIAN): What should we do if the player tries to interrupt the poltergeist response?
                yield return _poltergeist.SayResponseToQuestion(askedInThisArea: questionCount); // won't advance till the response has finished
                questionCount++;
            }
            yield return null;
        }
        yield return StartCoroutine(_poltergeist.GivePasswordChunkRoutine());
        yield return StartCoroutine(_poltergeist.JumpToNewLocationRoutine(shouldLaugh: true));
        //NOTE(JULIAN): This returns to Experience State
    }

    IEnumerator FinaleRoutine()
    {
        // TODO(JULIAN): IMPLEMENT ME!!!
        yield return null;
    }

    bool PlayerInRangeOfPoltergeist()
    {
        //TODO(JULIAN): IMPLEMENT ME
        return false;
    }

    bool TimeUntilJumpElapsed(float time)
    {
        return time >= _timeUntilNextJump;
    }
}
