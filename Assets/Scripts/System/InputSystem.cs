using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : BaseSystem,IPreUpdateSystem
{
    public void PreUpdate()
    {
        _gameState.onSpaceKeyDown = Input.GetKeyDown(KeyCode.Space);
    }
}
