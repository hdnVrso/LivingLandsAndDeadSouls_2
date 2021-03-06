﻿using Components.Drop;
using Components.Player;
using DataTransferObjects;
using GenerateMap;
using GenerateMap.TileGenerator;
using HealthFight;
using InventorySystem;
using Items;
using Menu;
using SaveLoadSystem.DTO;
using SpawnSystem;
using TimeSystem;
using UI;
using UI.Controls;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Components {

  public class GameSetuper : MonoBehaviour {
    public HealthComponent playerHealthComponent;
    public OutfitComponent playerOutfitComponent;
    public GunComponent playerGunComponent;
    public GameObject player;
    public GunSlotControl gunSlotControl;
    public InventoryComponent playerInventory;
    public OutfitSlotControl outfitSlotControl;
    public CharacteristicsComponent playerCharacteristicsComponent;
    public InventoryUi inventoryUi;
    public CharacteristicsUi characteristicsUi;
    public LootUiData lootUiData;
    public OutfitsAnimators outfitsAnimators;
    public BotsSpawner botsSpawner;
    public LootSpawner lootSpawner;
    public EndGameController endGameController;
    public TimeController timeController;
    public MeatDrop meatDrop;
    public MapController mapController;
    public GameSaver gameSaver;
    public TileInstancesStorage tileInstanceStorage;
    public MapDataStorage mapDataStorage;
    public Tilemap landTileMap;

    private void Start() {
      SetDependencies();
      LoadMap();
      SetUpPlayer();
      SpawnBots();
      SpawnLoot();
      SetUpSeason();
    }
    
    private void SetDependencies() {
      mapController.Setup();
      playerHealthComponent.Setup();
      playerOutfitComponent.Setup();
      playerGunComponent.Setup();
      playerCharacteristicsComponent.Setup(playerHealthComponent.GetHealthEntity(), playerGunComponent, characteristicsUi);
      characteristicsUi.Setup(playerCharacteristicsComponent);
      inventoryUi.Setup();
      playerInventory.Setup();
      gunSlotControl.Setup();
      outfitSlotControl.Setup();
      botsSpawner.Setup(ParameterManager.Instance.HostileCharVal, 
                        ParameterManager.Instance.NeutralCharVal, 
                                    ParameterManager.Instance.MapSizeVector);
      timeController.SetUp(tileInstanceStorage, landTileMap, mapDataStorage);
      gameSaver.Setup();
    }

    private void LoadMap() {
      mapController.LoadMap();
    }
    
    private void SetUpPlayer() {
      SetupPlayerPosition();
      SetupPlayerHealth();
      SetupPlayerOutfit();
      SetupPlayerGun();
      SetupPlayerInventory();
      SetUpPlayerCharacteristics();
      playerHealthComponent.AddSubscriber(endGameController);
    }

    private void SpawnBots() {
      IHealthEventSubscriber[] subscribers = new IHealthEventSubscriber[1];
      subscribers[0] = playerCharacteristicsComponent;
      botsSpawner.SpawnEnemies(subscribers);
      
      subscribers = new IHealthEventSubscriber[2];
      subscribers[0] = playerCharacteristicsComponent;
      subscribers[1] = meatDrop;
      botsSpawner.SpawnAnimals(subscribers);
    }

    private void SpawnLoot() {
      lootSpawner.Spawn(ParameterManager.Instance.lootSize, ParameterManager.Instance.MapSizeVector);
    }

    private void SetUpSeason() {
      switch (ParameterManager.Instance.StartSeason) {
        case 0:
          break;
        case 1:
          timeController.ChangeToFall();
          break;
        case 2:
          timeController.ChangeToWinter();
          break;
        case 3:
          timeController.ChangeToSpring();
          break;
      }
    }

    private void SetupPlayerPosition() {
      var playerPosition = ParameterManager.Instance.playerPosition;
      player.transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
    }
    
    private void SetupPlayerHealth() {
      var playerHealth = player.GetComponent<HealthComponent>().GetHealthEntity();
      playerHealth.SetHealthPointsLimit(ParameterManager.Instance.healthLimit);
      playerHealth.SetHealthPoints(ParameterManager.Instance.health);
    }
    
    private void SetupPlayerGun() {
      playerGunComponent.SetDamageBuff(ParameterManager.Instance.damageBuff);
      if (ParameterManager.Instance.suitedGun == null) {
        return;
      }
      
      var gun = ParameterManager.Instance.suitedGun;
      var gunUi = lootUiData.GetGunUi(gun.GetGunType());
      gun.SetItemUi(gunUi);
      gun.SetGunComponent(playerGunComponent);
      gun.Use();
    }

    private void SetupPlayerOutfit() {
      playerOutfitComponent.SetCharacterDefaultOutfit(ParameterManager.Instance.defaultAnimatorController);
      if (ParameterManager.Instance.suitedOutfit == null) {
        return;
      }
      
      var outfit = ParameterManager.Instance.suitedOutfit;
      outfit.SetAnimatorOverrider(outfitsAnimators.GetAnimators(outfit.GetOutfitType()));
      var outfitUi = lootUiData.GetOutfitUi(outfit.GetOutfitType());
      outfit.SetItemUi(outfitUi);
      outfit.SetOutfitComponent(playerOutfitComponent);
      outfit.Use();
    }

    private void SetupPlayerInventory() {
      var inventory = playerInventory.GetInventory();
      if (ParameterManager.Instance.inventoryItems == null) {
        return;
      }

      var items = ParameterManager.Instance.inventoryItems;
      for (int i = 0; i < items.Count; ++i) {
        if (items[i].GetItemType() == "Outfit") {
          var outfit = items[i] as Outfit;
          outfit.SetAnimatorOverrider(outfitsAnimators.GetAnimators(outfit.GetOutfitType()));
        }
        int slotUiIndex = items[i].GetItemUi().GetItemUiSlotIndex();
        SetItemUi(items[i]);
        items[i].GetItemUi().SetItemUiSlotIndex(slotUiIndex);
        inventory.AddItem(items[i]);
      }
    }

    private void SetUpPlayerCharacteristics() {
      var experience = ParameterManager.Instance.experience;
      var freePoints = ParameterManager.Instance.freePoints;
      var healthLimit = ParameterManager.Instance.healthLimit;
      var damageBuff = ParameterManager.Instance.damageBuff;
      var accuracy = ParameterManager.Instance.accuracy;
      playerCharacteristicsComponent.SetUpCharacteristics(experience, freePoints, damageBuff, healthLimit, accuracy);
    }

    private void SetItemUi(Item item) {
      IItemUi itemUi = null;
      switch (item.GetItemType()) {
        case "Gun":
          var gun = item as Gun;
          itemUi = lootUiData.GetGunUi(gun.GetGunType());
          break;
        case "Outfit":
          var outfit = item as Outfit;
          itemUi = lootUiData.GetOutfitUi(outfit.GetOutfitType());
          break;
        case "MedKit":
          var medKit = item as MedKit;
          itemUi = lootUiData.GetMedKitUi(medKit.GetMedKitType());
          break;
        case "Ammo":
          var ammo = item as Ammo;
          itemUi = lootUiData.GetAmmoUi();
          break;
      }
      item.SetItemUi(itemUi);
    }
  }

}
