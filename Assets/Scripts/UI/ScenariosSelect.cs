using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class ScenariosSelect : MonoBehaviour
{
    public RectTransform scenariosListRect;
    public List<ScenarioItem> scenarioItems;
    public float shiftOffset = 500f;
    public float moveTime = 0.4f;

    private int selectedIndex = 0;
    private Ease ease = Ease.InOutCubic;
    

    public void MoveRight()
    {
        if (selectedIndex < scenarioItems.Count -1)
        {
            scenariosListRect.DOBlendableLocalMoveBy(new Vector3(-shiftOffset, 0, 0), moveTime).SetEase(ease);
            selectedIndex++;
        }
        else
        {
            scenariosListRect.DOBlendableLocalMoveBy(new Vector3(-shiftOffset/4, 0, 0), moveTime).SetLoops(2, LoopType.Yoyo) .SetEase(ease);
        }
    }
    public void MoveLeft()
    {
        if (selectedIndex > 0)
        {
            scenariosListRect.DOBlendableLocalMoveBy(new Vector3(shiftOffset, 0, 0), moveTime).SetEase(ease);
            selectedIndex--;
        }
        else
        {
            scenariosListRect.DOBlendableLocalMoveBy(new Vector3(shiftOffset/4, 0, 0), moveTime).SetLoops(2, LoopType.Yoyo) .SetEase(ease);
        }
    }

    public string GetScenario()
    {
        return scenarioItems[selectedIndex].scenarioKey;
    }
}
