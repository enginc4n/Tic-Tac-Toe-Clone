using System.Collections;
using Runtime.Context.Game.Scripts.Enums;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Context.Game.Scripts.View.PlayerRegisterMenu
{
  public class PlayerRegisterMenuView : EventView
  {
    [Header("Container")]
    [SerializeField]
    private GameObject container;

    [Header("Input Fieds")]
    [SerializeField]
    private TMP_InputField playerOneNameInputField;

    [SerializeField]
    private TMP_InputField playerTwoNameInputField;

    [Header("Labels")]
    [SerializeField]
    private TextMeshProUGUI errorLabel;

    [SerializeField]
    private float errorLabelDuration = 2f;

    [Header("Buttons")]
    [SerializeField]
    private Button playerOneCrossButton;

    [SerializeField]
    private Button playerOneCircleButton;

    [SerializeField]
    private Button playerTwoCrossButton;

    [SerializeField]
    private Button playerTwoCircleButton;

    public bool isCoroutineRunning { get; private set; }

    public string GetPlayerOneName()
    {
      return playerOneNameInputField.text;
    }

    public string GetPlayerTwoName()
    {
      return playerTwoNameInputField.text;
    }

    public IEnumerator SetErrorLabel(ErrorTypes error)
    {
      isCoroutineRunning = true;
      string errorMessage = string.Empty;
      switch (error)
      {
        case ErrorTypes.NoPlayerName:
          errorMessage = "Please enter a name for both players";
          break;

        case ErrorTypes.SamePlayerName:
          errorMessage = "Please enter different names for both players";
          break;
      }

      errorLabel.gameObject.SetActive(true);
      errorLabel.text = errorMessage;
      yield return new WaitForSeconds(errorLabelDuration);
      errorLabel.gameObject.SetActive(false);
      isCoroutineRunning = false;
    }

    public void TogglePlayerRegisterMenu(bool isActive)
    {
      container.SetActive(isActive);
    }

    public void SetPlayerTwoButtonsInteractable(TeamType teamType)
    {
      bool isCross = teamType == TeamType.Cross;
      playerTwoCrossButton.interactable = !isCross;
      playerTwoCircleButton.interactable = isCross;
    }

    public void SetPlayerOneButtonsInteractable(TeamType teamType)
    {
      bool isCross = teamType == TeamType.Cross;
      playerOneCrossButton.interactable = !isCross;
      playerOneCircleButton.interactable = isCross;
    }

    public void OnPlayerOneClickCross()
    {
      dispatcher.Dispatch(PlayerRegisterMenuEvents.PlayerOneTeamTypeChanged, TeamType.Cross);
    }

    public void OnPlayerOneClickCircle()
    {
      dispatcher.Dispatch(PlayerRegisterMenuEvents.PlayerOneTeamTypeChanged, TeamType.Circle);
    }

    public void OnPlayerTwoClickCross()
    {
      dispatcher.Dispatch(PlayerRegisterMenuEvents.PlayerTwoTeamTypeChanged, TeamType.Cross);
    }

    public void OnPlayerTwoClickCircle()
    {
      dispatcher.Dispatch(PlayerRegisterMenuEvents.PlayerTwoTeamTypeChanged, TeamType.Circle);
    }

    public void OnRegisterClicked()
    {
      dispatcher.Dispatch(PlayerRegisterMenuEvents.RegisterClicked);
    }
  }
}
