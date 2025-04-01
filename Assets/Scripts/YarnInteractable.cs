using UnityEngine;

public class YarnInteractable : MonoBehaviour, IConversable
{
    [SerializeField] private string nodeName = "";
    [SerializeField] private bool oneShot = false;
    private MinimalDialogueRunner dialogueRunner;

    bool interactable = true;
    bool isCurrentConversation = false;

    // this file is attached to every character in the scene and so will affect only
    // the targeted character object when functions are called

    // disable scene interaction, activate speaker indicator, and
    // run dialogue from {conversationStartNode}

    private void Awake()
    {
        dialogueRunner = FindFirstObjectByType<MinimalDialogueRunner>();
        dialogueRunner.DialogueComplete.AddListener(EndConversation);
    }

    public void StartConversation()
    {
        Debug.Log("Starting conversation with " + gameObject.name);
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(nodeName);
    }

    // reverse StartConversation's changes: 
    // re-enable scene interaction, deactivate indicator, etc.
    public void EndConversation()
    {
        if (!isCurrentConversation){return;}

        dialogueRunner.StopDialogue();
        if (oneShot)
        {
            DisableConversation();
            dialogueRunner.DialogueComplete.RemoveListener(this.EndConversation);
        }
    }

    // make character not able to be clicked on
    public void DisableConversation()
    {
        interactable = false;
    }
}
