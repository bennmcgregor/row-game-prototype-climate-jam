
public enum RowingState {
    DRIVE,
    RECOVERY
}

public class RowingStateMachine
{
    private RowingState _state;

    public RowingState State => _state;

    public RowingStateMachine(RowingState initialState)
    {
        _state = initialState;
    }

    public void StateTransition()
    {
        switch(_state) {
            case RowingState.RECOVERY:
                _state = RowingState.DRIVE;
                break;
            case RowingState.DRIVE:
                _state = RowingState.RECOVERY;
                break;
            default:
                break;
        }
    }
}
