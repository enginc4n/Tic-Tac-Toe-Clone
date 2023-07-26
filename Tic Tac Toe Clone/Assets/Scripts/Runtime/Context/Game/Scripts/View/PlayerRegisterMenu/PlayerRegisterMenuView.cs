using System.Collections;
using Runtime.Context.Game.Scripts.Enums;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

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

    private bool _isCoroutineRunning;

    public string GetPlayerOneName()
    {
      return playerOneNameInputField.text;
    }

    public string GetPlayerTwoName()
    {
      return playerTwoNameInputField.text;
    }

    public IEnumerator SetErrorLabel(string error)
    {
      _isCoroutineRunning = true;
      errorLabel.gameObject.SetActive(true);
      errorLabel.text = error;

      yield return new WaitForSeconds(errorLabelDuration);

      errorLabel.gameObject.SetActive(false);
      _isCoroutineRunning = false;
    }

    public bool GetIsCoroutineRunning()
    {
      return _isCoroutineRunning;
    }

    public void DisablePlayerRegisterMenu()
    {
      container.SetActive(false);
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
