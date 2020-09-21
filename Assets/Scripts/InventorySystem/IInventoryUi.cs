﻿using Utility;

namespace InventorySystem {
  public interface IInventoryUi {
    void SetItem(IItemUi itemUi, Identifier itemIdentifier);
    void RemoveItem(Identifier identifier);
  }
}