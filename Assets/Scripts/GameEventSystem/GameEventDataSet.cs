using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MyBox;

[CreateAssetMenu(menuName = "OpenCodeBox/Game Events/Game Event Data Set")]
public class GameEventDataSet : ScriptableObject
{

    public List<GameEventData> gameEventData;

}