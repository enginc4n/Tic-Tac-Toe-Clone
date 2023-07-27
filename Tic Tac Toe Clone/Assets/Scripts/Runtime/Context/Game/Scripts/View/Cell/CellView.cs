using Runtime.Context.Game.Scripts.Enums;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Context.Game.Scripts.View.Cell
{
  public class CellView : EventView
  {
    [Header("Cell Properties")]
    [SerializeField]
    private TextMeshProUGUI cellLabel;

    [SerializeField]
    private Button cellButton;

    public void SetCellLabel(int turn, TeamType playerOneTeamType, TeamType playerTwoTeamType)
    {
      bool isPlayerOneTurn = turn % 2 == 0;
      if (isPlayerOneTurn)
      {
        cellLabel.color = Color.red;
        ProcessPlayerOneTurn(playerOneTeamType);
      }
      else
      {
        cellLabel.color = Color.black;
        ProcessPlayerTwoTurn(playerTwoTeamType);
      }
    }

    private void ProcessPlayerOneTurn(TeamType playerOneTeamType)
    {
      if (playerOneTeamType == TeamType.Cross)
      {
        cellLabel.text = "X";
      }
      else
      {
        cellLabel.text = "O";
      }
    }

    private void ProcessPlayerTwoTurn(TeamType playerTwoTeamType)
    {
      if (playerTwoTeamType == TeamType.Cross)
      {
        cellLabel.text = "X";
      }
      else
      {
        cellLabel.text = "O";
      }
    }

    public void SetCellInteractable(bool interactable)
    {
      cellButton.interactable = interactable;
    }

    public void OnCellClicked(string key)
    {
      dispatcher.Dispatch(CellEvent.CellClicked, key);
    }
  }
}
