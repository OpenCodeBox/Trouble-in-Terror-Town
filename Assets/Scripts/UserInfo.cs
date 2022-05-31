using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "OpenCodeBox/User/User Info")]
public class UserInfo : ScriptableObject
{
    public string userName;
    public string steamId;
    public Texture profilePicture;
}
