public enum DirectionState {
    FORWARD,
    REVERSE,
    STOPPING
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

    public void SetState(DirectionState state)
    {
        _prevState = _state;
        _state = state;
    }
}