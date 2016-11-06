using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poltergeist : MonoBehaviour {

    JumpTarget _currTarget;
    AudioSource _source;

    [System.Serializable]
    class ResponseSet
    {
        [SerializeField]
        AudioClip[] _responses;
        int _responseIndex = 0;
        public AudioClip GetNextResponse()
        {
            AudioClip response = null;
            if (_responseIndex < _responses.Length)
            {
                response = _responses[_responseIndex];
                _responseIndex++;
            } else
            {
                _responseIndex = 0;
                response = _responses[_responseIndex];
                _responseIndex++;
            }
            return response;
        }
        public AudioClip RandomClip ()
        {
            return _responses[Random.Range(0, _responses.Length)];
        }
    }

    [SerializeField]
    ResponseSet _negativeResponses;
    [SerializeField]
    ResponseSet _positiveResponses;
    [SerializeField]
    ResponseSet _laughter;
    [SerializeField]
    ResponseSet _passwordSections;
    [SerializeField]
    AudioClip _finaleAudio;


    void Awake ()
    {
        _source = GetComponent<AudioSource>();
    }

    public JumpTarget CurrJumpTarget
    {
        get
        {
            return _currTarget;
        }
    }

    public Vector3 Position
    {
        get {
            return transform.position;
        }
    }

    public IEnumerator SayClipRoutine (AudioClip clip)
    {
        Debug.Log("Start Saying " + clip.name);
        // TODO(JULIAN): What if a clip is already playing?
        _source.clip = clip;
        _source.Play();
        while (_source.isPlaying)
        {
            yield return null;
        }
    }

    public IEnumerator SayResponseToQuestion (int askedInThisArea)
    {
        var clip = askedInThisArea == 0 ? _negativeResponses.GetNextResponse() : _positiveResponses.GetNextResponse();
        yield return StartCoroutine(SayClipRoutine(clip));
    }

    public IEnumerator GivePasswordChunkRoutine ()
    {
        yield return StartCoroutine(SayClipRoutine(_passwordSections.GetNextResponse()));
    }

    public IEnumerator JumpToNewLocationRoutine (bool shouldLaugh, JumpTarget[] allLocations)
    {
        int choice = 0;
        while (allLocations[choice] == _currTarget)
        {
            choice = Random.Range(0, allLocations.Length);
        }
        TeleportToJumpTarget(allLocations[choice]);

        if (shouldLaugh)
        {
            yield return StartCoroutine(SayClipRoutine(_laughter.RandomClip()));
        }
    }

    public IEnumerator SayFinaleRoutine ()
    {
        yield return StartCoroutine(SayClipRoutine(_finaleAudio));
    }

    public void TeleportToJumpTarget (JumpTarget target)
    {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
        _currTarget = target;
    }
}
