using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poltergeist : MonoBehaviour {

    JumpTarget _currTarget;

    public JumpTarget CurrJumpTarget
    {
        get
        {
            return _currTarget;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 Position
    {
        get {
            return transform.position;
        }
    }

    public IEnumerator SayResponseToQuestion ()
    {
        // TODO(JULIAN): Speak a response, wait for it to complete in a yield return null loop, and then yield return null from the coroutine
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
