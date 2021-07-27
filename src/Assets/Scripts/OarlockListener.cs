using System;
using UnityEngine;

public class OarlockListener : MonoBehaviour
{
	public HingeJoint PortOarlock;
	public HingeJoint StarboardOarlock;
	public InputListener InputListener;
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

	private void Start()
	{
		InputListener.OnPortStickChange += stick => _portStick = stick;
        InputListener.OnStarboardStickChange += stick => _starboardStick = stick;
	}

    public void Update()
	{
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

		_portEffortScalingFactor = CalculateScalingFactor(PortOarlock.angle);
		_starboardEffortScalingFactor = CalculateScalingFactor(StarboardOarlock.angle);
	}

	private float CalculateScalingFactor(float angle)
	{
		double radians = Math.PI * angle / 180.0f;
		return (float) Math.Cos(radians);
	}
}