
using System;
using UnityEngine;

[Serializable]
public class ButtonOperation
{
    public Operation Operation;
    public GameObject OperationVFX;
}

[Serializable]
public enum Operation
{
    SUM,
    SUBSTRACT,
    MULTIPLY,
    DIVIDE,
    RANDOM,
    SWAP,
    SHUFFLE,
    REMOVESX,
    REMOVEDX,
    CONFIRM
}
