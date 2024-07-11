using TMPro;
using UnityEngine;

public interface ICommand
{
    void Execute();
}

public class ShowPanelCommand : ICommand
{
    private GameObject panel;

    public ShowPanelCommand(GameObject panel)
    {
        this.panel = panel;
    }

    public void Execute()
    {
        panel.SetActive(true);
    }
}

public class HidePanelCommand : ICommand
{
    private GameObject panel;

    public HidePanelCommand(GameObject panel)
    {
        this.panel = panel;
    }

    public void Execute()
    {
        panel.SetActive(false);
    }
}

public class UpdateTextCommand : ICommand
{
    private TextMeshProUGUI textComponent;
    private string newText;

    public UpdateTextCommand(TextMeshProUGUI textComponent, string newText)
    {
        this.textComponent = textComponent;
        this.newText = newText;
    }

    public void Execute()
    {
        textComponent.text = newText;
    }
}
