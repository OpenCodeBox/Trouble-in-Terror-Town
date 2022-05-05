using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoleLoader : MonoBehaviour
{
    private string roleDirectory;

    public static long RoleCount(DirectoryInfo directoryInfo)
    {
        long _files = 0;
        // Add file sizes.
        FileInfo[] files = directoryInfo.GetFiles();
        foreach (FileInfo file in files)
        {
            if (file.Extension.Contains("json"))
                _files++;
        }
        return _files;
    }

    [System.Serializable]
    public class Role
    {
        public int roleID;
        public string roleName;
        public string roleHexColor;
        public bool randomlyAssignable;
        public int roleTeamID;
        public bool canUseShop;
        public int[] EnemyTeamIds;
    }

    [System.Serializable]
    public class RoleList
    {
        public Role[] role;
    }

    public List<RoleList> roleList;
    // Start is called before the first frame update
    void Start()
    {
        roleDirectory = Application.dataPath + "/CharacterRoles";
        DirectoryInfo directoryInfo = new DirectoryInfo(roleDirectory);
        long roleCount = RoleCount(directoryInfo);

        Debug.Log(roleCount + " roles");

        FileInfo[] files = directoryInfo.GetFiles();
        foreach (FileInfo file in files)
        {
            if (file.Extension.Contains("json"))
            {
                StreamReader roleFile = new StreamReader(roleDirectory + "/" + file.Name);
                roleList.Add(JsonUtility.FromJson<RoleList>(roleFile.ReadToEnd()));
            }
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
