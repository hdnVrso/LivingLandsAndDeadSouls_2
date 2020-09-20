﻿using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class RightButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    AudioSource Audio;
    GameObject player;
    PlayerController playerController;
    void Start()
    {
        Audio=GameObject.Find("AudioManager").GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        Audio.Play();
        print("RightPointerDown");
        playerController.Direction = 2;
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        Audio.Stop();
        print("RightPointerUp");
    }
}
