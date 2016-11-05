using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class ExperienceManager : MonoBehaviour
{

    enum MainState
    {
        PlacingObjects,
        ExperienceLoop
    }

    MainState _mainState = MainState.PlacingObjects;

    [SerializeField] JumpTarget[] _jumpTargets;
    [SerializeField] Poltergeist _poltergeist;
    [SerializeField] Player _player;

    void MainStateTransitionTo(MainState oldState, MainState newState)
    {
        if (newState == MainState.ExperienceLoop)
        {
            foreach (var jumpTarget in _jumpTargets)
            {
                jumpTarget.SetTapToPlaceAbility(false);
            }
        }
        else if (newState == MainState.PlacingObjects)
        {
            foreach (var jumpTarget in _jumpTargets)
            {
                jumpTarget.SetTapToPlaceAbility(true);
            }
        }
        _mainState = newState;
    }

    void Update()
    {
        switch (_mainState)
        {
            case MainState.PlacingObjects:
                PlacingObjectUpdate();
                if (_experienceBeginTrigger)
                {
                    _experienceBeginTrigger = false;
                    MainStateTransitionTo(_mainState, MainState.ExperienceLoop);
                }
                break;
            case MainState.ExperienceLoop:
                ExperienceLoopUpdate();
                break;
        }
    }

    bool _experienceBeginTrigger = false;
    public void HeardExperienceBegin()
    {
        _experienceBeginTrigger = true;
    }

    void PlacingObjectUpdate()
    {

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

    enum ExperienceSubState {
        Seeking,
        Asking
    }

    ExperienceSubState _experienceSubState = ExperienceSubState.Seeking;


    [SerializeField] float _timeUntilNextJump = 10f;

    float _timer = 0f;
    void ExperienceLoopUpdate()
    {
        switch (_experienceSubState)
        {
            case ExperienceSubState.Seeking:
                if (PlayerInRangeOfPoltergeist())
                {
                    // Now we need to ask questions
                }
                else if (TimeUntilJumpElapsed(_timer))
                {
                    // Now the poltergeist needs to jump to a random location
                }
                break;
            case ExperienceSubState.Asking:
                break;
        }
        _timer += Time.deltaTime;

    }


}
