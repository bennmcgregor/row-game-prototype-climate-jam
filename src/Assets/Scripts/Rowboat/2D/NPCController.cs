using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class NPCController : MonoBehaviour
{
    [SerializeField] private RowboatSlider _slider;
    [SerializeField] private float _velocity = 1f;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _maximumSpeed = 2.5f;
    [SerializeField] private RowboatParamsProvider _rowboatParamsProvider;
    [SerializeField] private RowBoat2D _rowboat;
    [SerializeField] private bool _isEmpty = false;

    private RowingStateMachine _stateMachine;

    private float _playerMostRecentCatchPosition;
    private float _playerMostRecentFinishPosition;
    private bool _hasCatchUpdate;
    private bool _hasFinishUpdate;

    public RowboatSlider Slider => _slider;
    public RowingState CurrentState => _stateMachine.State;
    
    private void Awake()
    {
        _stateMachine = new RowingStateMachine(RowingState.RECOVERY);
        _playerController.OnCatchPositionUpdated += OnCatchPositionUpdated;
        _playerController.OnFinishPositionUpdated += OnFinishPositionUpdated;

        _hasCatchUpdate = false;
        _hasFinishUpdate = false;
    }

    private void Start()
    {
        _playerMostRecentCatchPosition = _playerController.CatchPosition;
        _playerMostRecentFinishPosition = _playerController.FinishPosition;
        
        // start the follow logic
        if (!_isEmpty)
        {
            StartCoroutine(FollowPlayer());
        }
    }

    private float GetSliderPosition()
    {
        return _slider.Value;
    }

    private void OnCatchPositionUpdated(float pos)
    {
        _playerMostRecentCatchPosition = pos;
        _hasCatchUpdate = true;
    }

    private void OnFinishPositionUpdated(float pos)
    {
        _playerMostRecentFinishPosition = pos;
        _hasFinishUpdate = true;
    }

    private void UpdateVelocityForRecovery()
    {
        // calculate and update current velocity
        float predictedTimeToCatchForPlayer = (_playerMostRecentCatchPosition 
            - _playerController.GetSliderPosition()) / _playerController.CurrentSpeed;
        
        float requiredSpeedToMatchPlayer = 0;
        // player has not yet reached predicted catch position
        if (predictedTimeToCatchForPlayer > 0)
        {
            requiredSpeedToMatchPlayer = (_playerMostRecentCatchPosition
                - GetSliderPosition()) / predictedTimeToCatchForPlayer;
            // should be greater than 0, otherwise player has just done a catch
            // Assert.IsTrue(requiredSpeedToMatchPlayer > 0);
            _velocity = requiredSpeedToMatchPlayer;
        }
        else // player has passed predicted catch position
        {
            // just keep following the player
            _velocity = _playerController.CurrentSpeed;
        }
    }

    private void UpdateVelocityForDrive()
    {
        // calculate and update current velocity
        float predictedTimeToFinishForPlayer = (_playerMostRecentFinishPosition 
            - _playerController.GetSliderPosition()) / _playerController.CurrentSpeed;
        float requiredSpeedToMatchPlayer = 0;
        // player has not yet reached predicted finish position
        if (predictedTimeToFinishForPlayer < 0)
        {
            requiredSpeedToMatchPlayer = (_playerMostRecentFinishPosition
                - GetSliderPosition()) / predictedTimeToFinishForPlayer;
            // should be greater than 0, otherwise player has just done a finish
            // Assert.IsTrue(requiredSpeedToMatchPlayer >= 0);
            _velocity = -1 * requiredSpeedToMatchPlayer;
        }
        else // player has passed predicted finish position
        {
            // just keep following the player
            _velocity = -1 * _playerController.CurrentSpeed;
        }
    }

    private IEnumerator FollowPlayer()
    {
        while (true)
        {
            // start in recovery, player also in recovery
            // Assert.IsTrue(_stateMachine.State == RowingState.RECOVERY);
            // Assert.IsTrue(_playerController.CurrentState == RowingState.RECOVERY);
            // use default velocity
            do
            {
                // the AI's position is behind the player
                if (_playerController.GetSliderPosition() >= GetSliderPosition())
                {
                    UpdateVelocityForRecovery();
                }
                else // the AI's position is ahead of the player
                {
                    // the AI has not yet reached the predicted catch position of the player
                    if (GetSliderPosition() < _playerMostRecentCatchPosition)
                    {
                        UpdateVelocityForRecovery();
                    }
                    else // the AI has passed the predicted catch position
                    {
                        // wait for the player
                        _velocity = 0;
                    }
                }
                // wait for the player to start the catch
                yield return new WaitForSeconds(_rowboatParamsProvider.GetTrustSystemParams().NPCDelayTime);
            } while (!_hasCatchUpdate && !_hasFinishUpdate);

            // _hasCatchUpdate will always be set first
            _hasCatchUpdate = false;
            if (_hasFinishUpdate) // the cycle has reset, jump to the top of the loop
            {
                _hasFinishUpdate = false;
                continue;
            }
            // the player must be in the drive at this point
            // Assert.IsTrue(_playerController.CurrentState == RowingState.DRIVE);

            // calculate drive velocity required to reach the finish at the same time as the player
            float predictedTimeToFinishForPlayer = (_playerMostRecentFinishPosition 
                - _playerController.GetSliderPosition()) / _playerController.CurrentSpeed;
            float requiredDriveSpeedToMatchPlayer = 0;
            // player has not yet reached predicted finish position
            if (predictedTimeToFinishForPlayer < 0)
            {
                requiredDriveSpeedToMatchPlayer = (_playerMostRecentFinishPosition
                    - GetSliderPosition()) / predictedTimeToFinishForPlayer;
            }
            else // player has passed predicted finish position
            {
                // assume player is going to the end of the slide (position 0)
                predictedTimeToFinishForPlayer = _playerController.GetSliderPosition() / _playerController.CurrentSpeed;
                requiredDriveSpeedToMatchPlayer = GetSliderPosition() / predictedTimeToFinishForPlayer;
            }
            // Assert.IsTrue(requiredDriveSpeedToMatchPlayer >= 0);

            // if required drive velocity is greater than the max velocity, keep going and meet the player at the catch
            if (requiredDriveSpeedToMatchPlayer > _maximumSpeed)
            {
                // calculate the speed required to reach the catch at the same time as the player
                do { // the AI is in recovery, player is in drive
                    predictedTimeToFinishForPlayer = (_playerMostRecentFinishPosition - 
                        _playerController.GetSliderPosition()) / _playerController.CurrentSpeed;

                    float predictedTimeFromFinishToCatch = 0;
                    // player has not yet reached predicted finish position
                    if (predictedTimeToFinishForPlayer < 0) {
                        predictedTimeFromFinishToCatch = (_playerMostRecentCatchPosition - 
                            _playerMostRecentFinishPosition) / _playerController.CurrentSpeed; // TODO: change this CurrentSpeed to the predicted recovery speed
                    }
                    else // player has passed predicted finish position
                    {
                        predictedTimeFromFinishToCatch = (_playerMostRecentCatchPosition -
                            _playerController.GetSliderPosition()) / _playerController.CurrentSpeed; // TODO: change this CurrentSpeed to the predicted recovery speed
                    }
                    // Assert.IsTrue(predictedTimeFromFinishToCatch >= 0);

                    float requiredSpeedToMatchPlayer = (_playerMostRecentCatchPosition - GetSliderPosition()) / 
                        (Math.Abs(predictedTimeToFinishForPlayer) + predictedTimeFromFinishToCatch);
                    if (requiredSpeedToMatchPlayer >= 0) // AI is not yet at the predicted catch position
                    {
                        _velocity = requiredSpeedToMatchPlayer;
                    }
                    else // AI has passed the predicted catch position
                    {
                        _velocity = 0;
                    }
                    yield return new WaitForSeconds(_rowboatParamsProvider.GetTrustSystemParams().NPCDelayTime);
                } while (!_hasFinishUpdate);
                _hasFinishUpdate = false;
                // Assert.IsTrue(_playerController.CurrentState == RowingState.RECOVERY);
                continue; // jump to state where both player and AI are in recovery
            }
            else // start drive
            {
                _velocity = -1 * requiredDriveSpeedToMatchPlayer;
                _stateMachine.StateTransition();
                // Assert.IsTrue(_stateMachine.State == RowingState.DRIVE);
                yield return new WaitForSeconds(_rowboatParamsProvider.GetTrustSystemParams().NPCDelayTime);
            }
            
            DRIVE:
            // start in drive, player also in drive
            // Assert.IsTrue(_stateMachine.State == RowingState.DRIVE);
            // Assert.IsTrue(_playerController.CurrentState == RowingState.DRIVE);
            // use default velocity
            do
            {
                // the AI's position is ahead of the player (behind the player on the drive)
                if (GetSliderPosition() >= _playerController.GetSliderPosition())
                {
                    UpdateVelocityForDrive();
                }
                else // the AI's position is behind the player (ahead of the player on the drive)
                {
                    // the AI has not yet reached the predicted finish position of the player
                    if (GetSliderPosition() > _playerMostRecentFinishPosition)
                    {
                        UpdateVelocityForDrive();
                    }
                    else // the AI has passed the predicted finish position
                    {
                        // wait for the player
                        _velocity = 0;
                    }
                }
                // wait for the player to start the finish
                yield return new WaitForSeconds(_rowboatParamsProvider.GetTrustSystemParams().NPCDelayTime);
            } while (!_hasFinishUpdate && !_hasCatchUpdate);

            // _hasFinishUpdate will always be set first
            _hasFinishUpdate = false;
            if (_hasCatchUpdate) // the cycle has reset, jump to the top of the loop
            {
                _hasCatchUpdate = false;
                goto DRIVE; // yes, I'm using a goto (it's less duplicated code!)
            }
            // the player must be in the recovery at this point
            // Assert.IsTrue(_playerController.CurrentState == RowingState.RECOVERY);

            // calculate recovery velocity required to reach the catch at the same time as the player
            float predictedTimeToCatchForPlayer = (_playerMostRecentCatchPosition 
                - _playerController.GetSliderPosition()) / _playerController.CurrentSpeed;
            float requiredRecoverySpeedToMatchPlayer = 0;
            // player has not yet reached predicted catch position
            if (predictedTimeToCatchForPlayer > 0)
            {
                // TODO: perhaps this is could be negative?
                requiredRecoverySpeedToMatchPlayer = (_playerMostRecentCatchPosition
                    - GetSliderPosition()) / predictedTimeToCatchForPlayer;
            }
            else // player has passed predicted finish position
            {
                // assume player is going to the end of the slide (position 100)
                predictedTimeToCatchForPlayer = (100 - _playerController.GetSliderPosition()) / _playerController.CurrentSpeed;
                requiredRecoverySpeedToMatchPlayer = (100 - GetSliderPosition()) / predictedTimeToCatchForPlayer;
            }
            // Assert.IsTrue(requiredRecoverySpeedToMatchPlayer >= 0);

            // if required recovery velocity is greater than the max velocity, keep going and meet the player at the finish
            if (requiredRecoverySpeedToMatchPlayer > _maximumSpeed)
            {
                // calculate the speed required to reach the finish at the same time as the player
                do { // the AI is in drive, player is in recovery
                    predictedTimeToCatchForPlayer = (_playerMostRecentCatchPosition - 
                        _playerController.GetSliderPosition()) / _playerController.CurrentSpeed;

                    float predictedTimeFromCatchToFinish = 0;
                    // player has not yet reached predicted catch position
                    if (predictedTimeToCatchForPlayer > 0) {
                        predictedTimeFromCatchToFinish = (_playerMostRecentCatchPosition - 
                            _playerMostRecentFinishPosition) / _playerController.CurrentSpeed; // TODO: change this CurrentSpeed to the predicted drive speed
                    }
                    else // player has passed predicted catch position
                    {
                        predictedTimeFromCatchToFinish = (_playerController.GetSliderPosition() -
                            _playerMostRecentFinishPosition) / _playerController.CurrentSpeed; // TODO: change this CurrentSpeed to the predicted drive speed
                    }
                    // Assert.IsTrue(predictedTimeFromCatchToFinish >= 0);

                    float requiredSpeedToMatchPlayer = (_playerMostRecentFinishPosition - GetSliderPosition()) / 
                        (Math.Abs(predictedTimeToCatchForPlayer) + predictedTimeFromCatchToFinish);
                    if (requiredSpeedToMatchPlayer < 0) // AI is not yet at the predicted finish position
                    {
                        _velocity = requiredSpeedToMatchPlayer;
                    }
                    else // AI has passed the predicted finish position
                    {
                        _velocity = 0;
                    }
                    yield return new WaitForSeconds(_rowboatParamsProvider.GetTrustSystemParams().NPCDelayTime);
                } while (!_hasCatchUpdate);
                _hasCatchUpdate = false;
                // Assert.IsTrue(_playerController.CurrentState == RowingState.DRIVE);
                goto DRIVE; // jump to state where both player and AI are in drive
            }
            else // start recovery
            {
                _velocity = requiredRecoverySpeedToMatchPlayer;
                _stateMachine.StateTransition();
                // Assert.IsTrue(_stateMachine.State == RowingState.RECOVERY);
                yield return new WaitForSeconds(_rowboatParamsProvider.GetTrustSystemParams().NPCDelayTime);
            }
        }
    }

    private void FixedUpdate()
    {
        _slider.AddValue(_velocity, _stateMachine.State);
    }
/*
On recovery:
- Status quo (both moving in same direction): AI is moving to get to the previous catch position of the player at the
same time as the player, assuming they keep their current speed.
    - i.e. given the player's previous catch position, their current distance from that position, and their current speed,
      what speed do I need to travel at to reach the same position at the same time as the player?
    - need to predict the time it will take the player to reach that position using their distance and current speed.
      Then using the time, predict the speed the AI needs to reach that position given their current distance from it.
- Player changes catch position while AI still moving: AI needs to decide where and when to change direction to get in time 
the fastest without surpassing power limits. It has 2 options: immediately change direction, or keep going and wait for 
the player at their previous catch position.
    - Calculate the drive velocity required to reach the player's previous finish position at the same time as them.
      If it's greater than the maximum drive velocity (by a margin of error), then keep going. Otherwise, start drive.
    - If decision is to keep going, then calculate the speed required to reach the finish at the same time as the player,
      given the player's previous (predicted) finish position and current speed, and their predicted catch position and
      predicted speed (previous recovery speed).
- Player doesn't change catch position before the AI expects it: The AI should keep going to the limit and then wait for a 
change from the player, recording this as the new "previous" catch position. ("previous" really means "most recent").
- Player changes catch position, then changes finish position (while AI is still in recovery): this is the status quo case.

On drive:
- Status quo (both moving in same direction): AI is moving to get to the previous finish position of the player at the
same time as the player, assuming they keep their current speed.
    - i.e. given the player's previous finish position, their current distance from that position, and their current speed,
      what speed do I need to travel at to reach the same position at the same time as the player?
    - need to predict the time it will take the player to reach that position using their distance and current speed.
      Then using the time, predict the speed the AI needs to reach that position given their current distance from it.
- Player changes finish position: AI needs to decide where and when to change direction to get in time the fastest without
surpassing power limits (or more helpfully/more complicated, boat speed reduction limits). It has 2 options: immediately 
change direction, or keep going and wait for the player at their previous finish position.
    - Calculate the magnitude of the force in the opposite direction of motion of the boat required to exit drive immediately
      and reach the catch at the same time as the player. Also calculate the magnitude of the force in the same direction
      as the boat lost by continuing the drive to the player's previous finish position at the same time as them.
      Pick the option with the least magnitude.
    - If decision is to switch, calculate velocity required to reach player's previous catch position at the same time as them.
    - If decision is to keep going, then calculate the speed required to reach the finish at the same time as the player,
      given the player's previous (predicted) catch position and current speed, and their predicted finish position and
      predicted speed (previous drive speed).
- Player doesn't change finish position before the AI expects it: The AI should keep going to the limit and then wait for a
change from the player, recording this as the new previous finish position.
- Player changes finish position, then changes catch position (while AI is still in drive ): this is the status quo case.
*/
}
