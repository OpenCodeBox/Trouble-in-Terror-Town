using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TTTSC/Player/Character/Role")]
public class RoleData : ScriptableObject
{
    public int roleID;
    public string roleName;
    public string roleHexColor;
    public bool randomlyAssignable;
    public int roleTeamID;
    public bool canUseShop;
    public int[] EnemyTeamIds;

}
