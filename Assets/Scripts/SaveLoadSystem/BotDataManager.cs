﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace SaveLoadSystem {
  public class BotDataManager : DataManager {
    public override void Save() {
      var formatter = new BinaryFormatter();
      var data = new BotData(-100, 100, -100, 100,
        GameObject.Find("ParametersManager").GetComponent<Menu.ParameterManager>().hostileCharVal,
        GameObject.Find("ParametersManager").GetComponent<Menu.ParameterManager>().neutralCharVal);
      var path = Application.persistentDataPath + "/bots.data";
      var stream = new FileStream(path, FileMode.Create);
      formatter.Serialize(stream, data);
      stream.Close();
    }

    public override void Load() {
      var path = Application.persistentDataPath + "/bots.data";
      if (!File.Exists(path)) return;
      var formatter = new BinaryFormatter();
      var stream = new FileStream(path, FileMode.Open);
      var data = formatter.Deserialize(stream) as BotData;
      stream.Close();
      var parameterManager = GameObject.Find("ParametersManager").GetComponent<Menu.ParameterManager>();
      parameterManager.hostileCharVal = data.enemyCount;
      parameterManager.neutralCharVal = data.animalsCount;
    }

    public override void DeleteSaves() {
      var path = Application.persistentDataPath + "/bots.data";
      if (!File.Exists(path)) return;
      File.Delete(path);
    }
  }
}
