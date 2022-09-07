using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public enum States
    {
        MENU,
        PLAYING,
        PAUSED,
        RESET_POSITION,
        GAMEOVER
    }

    public Dictionary<States, StateBase> dictionaryState;

    private StateBase _currentState;
    private States _state;
    public float timeToStartGame = 1f;

    public static StateMachine instance;

    private void Awake() {
        instance = this;

        dictionaryState = new Dictionary<States, StateBase>();
        dictionaryState.Add(States.MENU, new MenuState());
        dictionaryState.Add(States.PLAYING, new PlayingState());
        dictionaryState.Add(States.PAUSED, new PausedState());
        dictionaryState.Add(States.RESET_POSITION, new ResetPositionState());
        dictionaryState.Add(States.GAMEOVER, new GameOverState());

        SwitchState(States.MENU);
    }

    private void StartGame() {
        SwitchState(States.MENU);
    }

    private void SwitchState(States state, string[] args = null) {
        if (_currentState != null) {
            _currentState.Exit();
        }
        _currentState = dictionaryState[state];
        _state = state;
        _currentState.Enter(args);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState != null)
        {
            _currentState.Update();
        }
    }

    public void ResetPosition() {
        SwitchState(States.RESET_POSITION);
    }

    public void GameOver() {
        SwitchState(States.GAMEOVER);
    }

    public void Menu() {
        SwitchState(States.MENU);
    }

    public void Start() {
        SwitchState(States.PLAYING, new string[] { "reset" });
    }

    public void Play() {
        SwitchState(States.PLAYING);
    }

    public void Pause() {
        SwitchState(States.PAUSED);
    }

    public States GetState() {
        return _state;
    }
}
