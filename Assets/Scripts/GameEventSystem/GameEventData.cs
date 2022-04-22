using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEditor;
using UnityEditorInternal;
using System.Threading.Tasks;

[Serializable]
public class GameEventData
{
    public string valueName;
    public enum returnValueTypeEnum
    {
        Bool,
        String,
        Int,
        Float,
        Vector2,
        Vector3
    }

    public returnValueTypeEnum valueType;


    //remember to make your own system of conditional fields

    [ConditionalField(nameof(valueType), false, returnValueTypeEnum.Bool)]
    public bool returnBool;
    [ConditionalField(nameof(valueType), false, returnValueTypeEnum.String)]
    public string returnString;
    [ConditionalField(nameof(valueType), false, returnValueTypeEnum.Int)]
    public int returnInt;
    [ConditionalField(nameof(valueType), false, returnValueTypeEnum.Float)]
    public float returnFloat;
    [ConditionalField(nameof(valueType), false, returnValueTypeEnum.Vector2)]
    public Vector2 returnVector2;
    [ConditionalField(nameof(valueType), false, returnValueTypeEnum.Vector3)]
    public Vector3 returnVector3;

    public static implicit operator Task(GameEventData v)
    {
        throw new NotImplementedException();
    }
}