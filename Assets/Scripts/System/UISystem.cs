using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISystem : BaseSystem
{
    public override void SetEvent()
    {
        _gameState.resetButton.onClick.AddListener(OnClickedResetButton);
        _gameState.playButton.onClick.AddListener(OnClickedPlayButton);
    }

    private void OnClickedPlayButton()
    {
        string text = _gameState.inputField.text;
        int length = text.Length;
        if (length == 0) return;
        _gameEvent.startRotation(text);

    }

    private void OnClickedResetButton()
    {
        SceneManager.LoadScene("CubeDemo");
    }
}
