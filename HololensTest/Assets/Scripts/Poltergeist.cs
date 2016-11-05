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
            }
            return response;
        }
    }

    [SerializeField]
    ResponseSet _negativeResponses;
    [SerializeField]
    ResponseSet _positiveResponses;


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
        // TODO(JULIAN): Speak a response with SayClipRoutine, and then yield return null from the coroutine
        yield return null;
    }

    public IEnumerator GivePasswordChunkRoutine ()
    {
        // TODO(JULIAN): IMPLEMENT ME
        yield return null;
    }

    public IEnumerator JumpToNewLocationRoutine (bool shouldLaugh)
    {
        // TODO(JULIAN): IMPLEMENT ME
        yield return null;
    }

    public void TeleportToJumpTarget (JumpTarget target)
    {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
    }
}
