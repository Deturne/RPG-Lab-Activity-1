using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : MonoBehaviour, IConversable
{
    [SerializeField] private string nodeName = "";
    [SerializeField] private bool oneShot = false;
    private MinimalDialogueRunner minimalDialogueRunner;

    private bool interactable = true;
    private bool isCurrentConversation = false;
    void Start()
    {
        minimalDialogueRunner = FindFirstObjectByType<MinimalDialogueRunner>();
        minimalDialogueRunner.DialogueComplete.AddListener(EndConversation);
    }

    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
    }

    public void EndConversation()
    {
        if (!isCurrentConversation) { return; }
  
        if (oneShot)
        {
            DisableConversation();
            minimalDialogueRunner.DialogueComplete.RemoveListener(EndConversation);
        }
    }

    public void StartConversation()
    {
        isCurrentConversation = true;
        minimalDialogueRunner.StartDialogue(nodeName);
    }
}
