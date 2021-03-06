﻿using UnityEngine;

namespace Bots.Enemy {
  public class EnemyAnimator : MonoBehaviour {
    private void Start() {
      _animator = gameObject.GetComponent<Animator>();
      _enemyFollow = gameObject.GetComponent<EnemyFollow>();
    }

    private void Update() {
      if (isActive == false)
        return;
      if (_enemyFollow.directionX == 0) {
        if (_enemyFollow.directionY == 0)
          _animator.Play("IdleLeft");
        else if (_enemyFollow.directionY > 0)
          _animator.Play("MoveBack");
        else
          _animator.Play("MoveFace");
      }
      else if (_enemyFollow.directionX > 0) {
        _animator.Play("MoveRight");
      }
      else {
        _animator.Play("MoveLeft");
      }
    }

    //data members
    private Animator _animator;
    private EnemyFollow _enemyFollow;
    public bool isActive = false;
  }
} //end of namespace Enemy