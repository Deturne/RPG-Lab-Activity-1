using UnityEngine;
using Yarn.Unity;

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
        Debug.Log("DialogueRunner  and Subscribed");
    }

    public void StartConversation()
    {
        // If the character is not interactable or a conversation is already running, return
        Debug.Log("Starting conversation with " + gameObject.name);
        if(!interactable || dialogueRunner.isRunning) { return; }

        isCurrentConversation = true;
        dialogueRunner.StartDialogue(nodeName);
    }

    // reverse StartConversation's changes: 
    // re-enable scene interaction, deactivate indicator, etc.
    public void EndConversation()
    {
        if (!isCurrentConversation){return;}

        isCurrentConversation = false;

        if (oneShot)
        {
            DisableConversation();
            dialogueRunner.DialogueComplete.RemoveListener(EndConversation);
        }

    }

    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
    }
}
