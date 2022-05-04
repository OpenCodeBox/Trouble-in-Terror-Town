using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TTTSC/Player/Character/Role")]
public class RoleData : ScriptableObject
{
    public string roleName;
    public int roleId;
    public Color roleColor;

    public enum RoleTeam
    {
        Innocent,
        Traitor
    }

    public RoleTeam roleTeam;
}
