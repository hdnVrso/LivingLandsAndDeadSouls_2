﻿using System.Collections.Generic;
using Menu;
using UnityEngine;


namespace SaveLoadSystem {
  public static class SaveSystem {
    public static void Save() {
      foreach (var dataManager in DataManagers)
        dataManager.Save();
    }

    public static void Load() {
      if (GameObject.Find("ParametersManager").GetComponent<ParameterManager>().needToLoad == false)
        return;
      foreach (var dataManager in DataManagers)
        dataManager.Load();
    }

    public static void DeleteSaves() {
      foreach (var dataManager in DataManagers)
        dataManager.DeleteSaves();
    }

    //data members
    public static List<DataManager> DataManagers;
  }
}
