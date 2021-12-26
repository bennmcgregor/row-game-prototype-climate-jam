using System;
using UnityEngine;

public class OarlockListener : MonoBehaviour
{
	// this class keeps track of the pull state of each oar (can it be pulled or not) and
	// and the component of the force on the oar in the direction of the boat

	// TEMP actions
	public Action<PullState> OnPortPullStateChanged;
	public Action<PullState> OnStarboardPullStateChanged;

	public HingeJoint PortOarlock;
	public HingeJoint StarboardOarlock;
	public RowingInputListener InputListener;
	public float AngleDelta = 5.0f;

    // getter
    public PullState PortPullState => _portPullState;
    public PullState StarboardPullState => _starboardPullState;
	public float PortEffortScalingFactor => _portEffortScalingFactor;
	public float StarboardEffortScalingFactor => _starboardEffortScalingFactor;

    //internal Properties
    private PullState _starboardPullState = PullState.CanPull;
    private PullState _portPullState = PullState.CanPull;
	private float _starboardEffortScalingFactor = -1f;
	private float _portEffortScalingFactor = -1f;
	private Vector2 _starboardStick;
    private Vector2 _portStick;

	// TEMP internal properties
	private PullState _prevStarboardPullState;
	private PullState _prevPortPullState;

	private void Start()
	{
		InputListener.OnPortStickChange += stick => _portStick = stick;
        InputListener.OnStarboardStickChange += stick => _starboardStick = stick;
	}

    public void Update()
	{
		_prevPortPullState = _portPullState;
		_prevStarboardPullState = _starboardPullState;

		if (_portStick.y > 0 && PortOarlock.angle <= PortOarlock.limits.min + AngleDelta || // pushed all the way to the catch
			_portStick.y < 0 && PortOarlock.angle >= PortOarlock.limits.max - AngleDelta) // or pulled all the way into the finish
		{
			
			_portPullState = PullState.CannotPull;
		}
		else
		{
			_portPullState = PullState.CanPull;
		}

		if (_starboardStick.y > 0 && StarboardOarlock.angle >= StarboardOarlock.limits.max - AngleDelta || 
			_starboardStick.y < 0 && StarboardOarlock.angle <= StarboardOarlock.limits.min + AngleDelta)
		{
			_starboardPullState = PullState.CannotPull;
		}
		else
		{
			_starboardPullState = PullState.CanPull;
		}

		if (_prevPortPullState != _portPullState) {
			OnPortPullStateChanged?.Invoke(_portPullState);
		}
		if (_prevStarboardPullState != _starboardPullState) {
			OnStarboardPullStateChanged?.Invoke(_starboardPullState);
		}

		_portEffortScalingFactor = CalculateScalingFactor(PortOarlock.angle);
		_starboardEffortScalingFactor = CalculateScalingFactor(StarboardOarlock.angle);
	}

	// get the component of the force on the oars that is in the direction of the boat
	private float CalculateScalingFactor(float angle)
	{
		double radians = Math.PI * angle / 180.0f;
		return (float) Math.Cos(radians);
	}
}