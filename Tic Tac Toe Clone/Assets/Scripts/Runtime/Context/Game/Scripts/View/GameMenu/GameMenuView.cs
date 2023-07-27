using Runtime.Context.Game.Scripts.Enums;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

namespace Runtime.Context.Game.Scripts.View.GameMenu
{
  public class GameMenuView : EventView
  {
    [Header("Container")]
    [SerializeField]
    private GameObject container;

    [SerializeField]
    private GameObject gameBoardContainer;

    [Header("Player One")]
    [SerializeField]
    private TextMeshProUGUI playerOneNameLabel;

    [SerializeField]
    private GameObject playerOneCrossButton;

    [SerializeField]
    private GameObject playerOneCircleButton;

    [Header("Player Two")]
    [SerializeField]
    private TextMeshProUGUI playerTwoNameLabel;

    [SerializeField]
    private GameObject playerTwoCrossButton;

    [SerializeField]
    private GameObject playerTwoCircleButton;

    [Header("Labels")]
    [SerializeField]
    private TextMeshProUGUI orderLabel;

    public void ToggleGameMenuPanel(bool isActive)
    {
      container.SetActive(isActive);
    }

    private void SetPlayerNames(string playerOneName, string playerTwoName)
    {
      playerOneNameLabel.text = playerOneName;
      playerTwoNameLabel.text = playerTwoName;
    }

    private void ShowPlayerTeamTypes(TeamType playerOneTeamType, TeamType playerTwoTeamType)
    {
      bool isPlayerOneCross = playerOneTeamType == TeamType.Cross;
      playerOneCrossButton.SetActive(isPlayerOneCross);
      playerOneCircleButton.SetActive(!isPlayerOneCross);

      bool isPlayerTwoCross = playerTwoTeamType == TeamType.Cross;
      playerTwoCrossButton.SetActive(isPlayerTwoCross);
      playerTwoCircleButton.SetActive(!isPlayerTwoCross);
    }

    public void SetGameMenu(string playerOneName, string playerTwoName, TeamType playerOneTeamType, TeamType playerTwoTeamType)
    {
      ToggleGameMenuPanel(true);
      SetPlayerNames(playerOneName, playerTwoName);
      ShowPlayerTeamTypes(playerOneTeamType, playerTwoTeamType);
    }

    public Transform GetGameBoardContainerTransform()
    {
      return gameBoardContainer.transform;
    }

    public void SetOrderLabel(string playerName, string label = "'s Turn")
    {
      orderLabel.text = playerName + label;
    }
  }
}
