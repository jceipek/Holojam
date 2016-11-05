using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class JumpTarget : MonoBehaviour {

    [SerializeField]
    GameObject _visuals;

    Placeable _placeable;
    void Awake() {
        _placeable = GetComponent<Placeable>();
        _placeable.InitWithAnchorName(gameObject.name);
    }

    public void SetTapToPlaceAbility (bool to)
    {
        _placeable.enabled = to;
        _visuals.SetActive(to);
    }
}
