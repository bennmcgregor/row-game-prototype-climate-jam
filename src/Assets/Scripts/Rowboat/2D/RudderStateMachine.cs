public enum RudderState {
    UP,
    DOWN,
    NONE
}

public class RudderStateMachine
{
    private RudderState _state;
    private bool _isPressingUp = false;
    private bool _isPressingDown = false;

    public RudderState State => _state;

    public RudderStateMachine(RudderState initialState)
    {
        _state = initialState;
    }

    public void OnRudderUp()
    {
        if (_isPressingUp)
        {
            OnCancelUp();
        }
        else
        {
            OnAimUp();
        }
        UpdateState();
    }

    public void OnRudderDown()
    {
        if (_isPressingDown)
        {
            OnCancelDown();
        }
        else
        {
            OnAimDown();
        }
        UpdateState();
    }

    private void OnAimUp()
    {
        _isPressingUp = true;
    }

    private void OnCancelUp()
    {
        _isPressingUp = false;
    }

    private void OnAimDown()
    {
        _isPressingDown = true;
    }

    private void OnCancelDown()
    {
        _isPressingDown = false;
    }

    private void UpdateState()
    {
        if (_isPressingUp && _isPressingDown)
        {
            _state = RudderState.NONE;
        }
        else if (_isPressingUp && !_isPressingDown)
        {
            _state = RudderState.UP;
        }
        else if (!_isPressingUp && _isPressingDown)
        {
            _state = RudderState.DOWN;
        }
        else
        {
            _state = RudderState.NONE;
        }
    }
}