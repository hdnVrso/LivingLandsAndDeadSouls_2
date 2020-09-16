﻿using GenerateMap;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SaveLoadSystem.DataManagers {
  public class MapDataManager : IDataManager {
    public void Save() {
      var generator = GameObject.Find("Generate").GetComponent<Generator>();
      var formatter = new BinaryFormatter();
      var path = Application.persistentDataPath + "/map.data";
      var season = GameObject.Find("ClockControl").GetComponent<TimeSystem.TimeController>().season;
      var year = GameObject.Find("ClockControl").GetComponent<TimeSystem.TimeController>().year;
      var day = GameObject.Find("ClockControl").GetComponent<TimeSystem.TimeController>().day;
      var data = new DTO.MapData(generator.bushList, generator.treeList, generator.rockList, generator.houseList,
        generator.houseTypeList, season, year, day, generator.tmpSize);
      var stream = new FileStream(path, FileMode.Create);
      formatter.Serialize(stream, data);
      stream.Close();
    }

    public void Load() {
      var path = Application.persistentDataPath + "/map.data";
      if (!File.Exists(path)) return;
      var formatter = new BinaryFormatter();
      var stream = new FileStream(path, FileMode.Open);
      var data = formatter.Deserialize(stream) as DTO.MapData;
      stream.Close();
      var generator = GameObject.Find("Generate").GetComponent<Generator>();
      var tmpSize = new Vector3Int(data.tmpSizeX, data.tmpSizeY, data.tmpSizeZ);
      generator.GenerateMap(data, tmpSize);
      var timeController = GameObject.Find("ClockControl").GetComponent<TimeSystem.TimeController>();
      timeController.year = data.year;
      timeController.day = data.day;
    }

    public void DeleteSaves() {
      var path = Application.persistentDataPath + "/map.data";
      if (!File.Exists(path)) return;
      File.Delete(path);
    }
  }
}