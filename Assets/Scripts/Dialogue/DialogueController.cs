using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueController 
{
    private readonly EventBus _eventBus;
    private readonly DialoguePresenter _dialoguePresenter;
    private readonly Input _input;
    private readonly InputActions.IDialogueActions _dialogueActions;

    private Dialogue _currentDialogue;
    private int _currentLineIndex;
    
    public DialogueController(EventBus eventBus, DialoguePresenter dialoguePresenter, Input input)
    {
        _eventBus = eventBus;
        _dialoguePresenter = dialoguePresenter;
        _input = input;
        _dialogueActions = new DialogueActionsCallbacks.Builder()
            .AddOnSkip(OnSkip, InputActionPhase.Performed)
            .Build();
        
        _input.Add(_dialogueActions);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _eventBus.Publish(new DialogueStartedEvent(dialogue));
        _currentDialogue = dialogue;
        _currentLineIndex = 0;
        _dialoguePresenter.ShowLine(_currentDialogue.Lines[0]);
        _input.SetInputContext(InputContext.Dialogue);
    }

    private void NextLine()
    {
        _currentLineIndex++;

        if (_currentLineIndex < _currentDialogue.Lines.Count)
            _dialoguePresenter.ShowLine(_currentDialogue.Lines[_currentLineIndex]);
        
        else
            EndDialogue();
        
    }

    private void EndDialogue()
    {
        if (_currentDialogue == null)
            return;
        
        _dialoguePresenter.EndDialogue();
        _eventBus.Publish(new DialogueFinishedEvent(_currentDialogue));
        _currentDialogue = null;
        _input.Remove(_dialogueActions);
    }

    private void OnSkip(InputAction.CallbackContext _)
    {
        if (_dialoguePresenter.IsPlaying)
            _dialoguePresenter.FinishTextAnimation();
        else
            NextLine();
    }
}
