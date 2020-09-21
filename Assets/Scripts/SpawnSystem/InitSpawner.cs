﻿using UnityEngine;

namespace SpawnSystem {
  public class InitSpawner : MonoBehaviour {
    private void Start() {
      _enemyCount = GameObject.Find("ParametersManager").GetComponent<Menu.ParameterManager>().hostileCharVal;
      _animalsCount = GameObject.Find("ParametersManager").GetComponent<Menu.ParameterManager>().neutralCharVal;
      for (var i = 0; i < enemyGameObjects.Length; ++i)
      for (var j = 0; j < _enemyCount / enemyGameObjects.Length; ++j) {
        _randX = Random.Range(xAxisBeginOfRange, xAxisEndOfRange);
        _randY = Random.Range(yAxisBeginOfRange, yAxisEndOfRange);
        _spawnPosition = new Vector2(_randX, _randY);
        Instantiate(enemyGameObjects[i], _spawnPosition, Quaternion.identity);
      }

      for (var i = 0; i < commonAnimals.Length; ++i)
      for (var j = 0; j < _animalsCount / commonAnimals.Length; ++j) {
        _randX = Random.Range(xAxisBeginOfRange, xAxisEndOfRange);
        _randY = Random.Range(yAxisBeginOfRange, yAxisEndOfRange);
        _spawnPosition = new Vector2(_randX, _randY);
        Instantiate(commonAnimals[i], _spawnPosition, Quaternion.identity);
      }

      Destroy(gameObject);
    }


    //data members
    public GameObject[] enemyGameObjects;
    public GameObject[] commonAnimals;
    public float xAxisBeginOfRange;
    public float xAxisEndOfRange;
    public float yAxisBeginOfRange;
    public float yAxisEndOfRange;

    private float _randX;
    private float _randY;
    private Vector2 _spawnPosition;
    private int _enemyCount;
    private int _animalsCount;
  }
}