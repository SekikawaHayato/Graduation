using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class GameState
{
    public List<Coroutine> coroutineList;
    public bool onSpaceKeyDown;


    [Header("RotationSettings")]
    [System.NonSerialized] public RotationElement rotation;
    public Transform center;
    public float rotationTime;

    [Header("UI")]
    public Button resetButton;
    public Button playButton;
    public TMP_InputField inputField;
}
