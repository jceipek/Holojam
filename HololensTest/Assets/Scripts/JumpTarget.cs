using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class JumpTarget : MonoBehaviour {

    TapToPlace _tapToPlace;
    void Awake() {
        _tapToPlace = GetComponent<TapToPlace>();
    }

    public void SetTapToPlaceAbility (bool to)
    {
        _tapToPlace.enabled = to;
    }
}
