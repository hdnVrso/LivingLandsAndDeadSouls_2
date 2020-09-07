﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class OutfitSlot : MonoBehaviour
    {
        void Start()
        {
            _gunSlot = GameObject.Find("GunSlot").GetComponent<GunSlot>();
            _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }
        
        public void ChangeSkin()
        {
            _gunSlot.ChangeSkin(skinIndex);
        }

        public void SwapOutfits(int i)
        {
            var child1 = this.gameObject.transform.GetChild(0);
            var child2 = this.gameObject.transform.GetChild(1);
            itemButton.SetActive(true);
            itemButton.GetComponent<OutfitSelect>().slotIndex = i;
            var gameObj =  Instantiate(itemButton, _playerInventory.buttonSlots[i].transform, false);
            var imageObj = Instantiate(itemButton.GetComponent<OutfitSelect>().outfitImage, _playerInventory.slots[i].transform,
                false);
            gameObj.GetComponent<OutfitSelect>().imageObj = imageObj;
            imageObj.GetComponent<DropItem>().buttonSlot = gameObj;
            Destroy(child1.gameObject);
            Destroy(child2.gameObject);
        }

        public void SetOutfit(GameObject itemButton)
        {
            gameObject.GetComponent<DropToInventory>().isEmpty = false;
            this.itemButton = Instantiate(itemButton, this.transform, false);
            var outfitSelectComponent = itemButton.GetComponent<OutfitSelect>();
            Instantiate(outfitSelectComponent.outfitImage, this.transform, false);
            skinIndex = outfitSelectComponent.skinIndex;
            this.itemButton.SetActive(false);
            ChangeSkin();
            gameObject.transform.GetChild(1).GetComponent<DropItem>().isActive = false;
        }
        
        //data members
        public int skinIndex;
        public GameObject itemButton;

        private Inventory _playerInventory;
        private GunSlot _gunSlot;
    }
}// end of namespace InventorySystem
