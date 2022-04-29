public enum DirectionState {
    FORWARD,
    REVERSE
}

public class DirectionStateMachine
{
    private DirectionState _state;
    private DirectionState _prevState;

    public DirectionState State => _state;
    public DirectionState PrevState => _prevState;

    public DirectionStateMachine(DirectionState initialState)
    {
        _state = initialState;
        _prevState = initialState;
    }

    public void StateTransition()
    {
        _prevState = _state;
        if (_state == DirectionState.FORWARD)
        {
            _state = DirectionState.REVERSE;
        }
        else
        {
            _state = DirectionState.FORWARD;
        }
    }
}