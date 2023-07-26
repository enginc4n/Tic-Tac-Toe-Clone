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

    public void SetCellLabel(string label)
    {
      cellLabel.text = label;
    }

    public void SetCellInteractable(bool interactable)
    {
      cellButton.interactable = interactable;
    }

    public void SetCellLabelColor(Color color)
    {
      cellLabel.color = color;
    }

    public void OnCellClicked()
    {
      dispatcher.Dispatch(CellEvent.CellClicked);
    }
  }
}
