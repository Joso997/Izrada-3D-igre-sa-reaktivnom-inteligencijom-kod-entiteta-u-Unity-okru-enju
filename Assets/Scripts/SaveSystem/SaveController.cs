using System.Collections.Generic;
using UnityEngine;
using NamespaceLevels;

 class SaveController : SaveDatabase {
    string alldata;
	public void SaveLevelChanges(List<LevelsList> data)
    {
        foreach (LevelsList element in data)
        {
            alldata = alldata + JsonUtility.ToJson(element);
            if (data.IndexOf(element) != data.Count-1)
            {
                alldata = alldata + ",";
            }
        }
        JsonFileReader.ClearResource("/StreamingAssets/Levels.json");
        JsonFileReader.ToJsonAsResource("/StreamingAssets/Levels.json", "{"+'"'+"Level" + '"' + ":[" + alldata + "]" + "}");
        alldata = null;
    }
}
