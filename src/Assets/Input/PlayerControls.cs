// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""66860b0f-d7d1-438c-9f00-ceb79274806a"",
            ""actions"": [
                {
                    ""name"": ""StarboardOar"",
                    ""type"": ""Value"",
                    ""id"": ""65863f7e-8a33-4aed-9c30-fed2777106f9"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PortOar"",
                    ""type"": ""Value"",
                    ""id"": ""787c145c-ab2d-4c4f-bcff-6dc39cf6fe7d"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GrabOars"",
                    ""type"": ""Button"",
                    ""id"": ""02006ea7-687e-47d1-b007-f2c5189ad200"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LiftPortOar"",
                    ""type"": ""Button"",
                    ""id"": ""b7168b8d-3db2-46e8-ab0f-25c57224c425"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LiftStarboardOar"",
                    ""type"": ""Value"",
                    ""id"": ""048f8ad9-4dac-4891-8b22-ad5f9922e9ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d8d66a3e-5f96-4596-b743-9c155441fe33"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StarboardOar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8500c88d-01f5-4ce3-b5df-4eea9cb1f1dc"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StarboardOar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Analog"",
                    ""id"": ""0cc6ff40-52b0-4927-9b14-5be506a2102e"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PortOar"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a981e0e8-d29b-42a3-94af-0425c79ed48c"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PortOar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a679dc7f-3149-40f0-a0b5-6a602f844ed9"",
                    ""path"": ""<Joystick>/rz"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PortOar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dfff3372-0164-4a19-af20-818d875f0b70"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PortOar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""33f09680-f59b-48c1-92f1-e767ce9c2538"",
                    ""path"": ""<Joystick>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PortOar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""05fb7102-9536-494f-bc5a-8921c5a184f8"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PortOar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b64f388-65e2-47f8-ad25-90492bf62940"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GrabOars"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f19a929b-cc9d-4388-a1cf-4cb29f979c11"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GrabOars"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b75c2b0-195b-4162-9a12-7247e4676109"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LiftPortOar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a58faee-39ee-4100-9b00-978df3270033"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/button6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LiftPortOar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""362b61cf-95f3-454d-ad81-d79292c84daa"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LiftStarboardOar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e850da39-f2fc-4844-bda3-599cdddd5bb7"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/button5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LiftStarboardOar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_StarboardOar = m_Gameplay.FindAction("StarboardOar", throwIfNotFound: true);
        m_Gameplay_PortOar = m_Gameplay.FindAction("PortOar", throwIfNotFound: true);
        m_Gameplay_GrabOars = m_Gameplay.FindAction("GrabOars", throwIfNotFound: true);
        m_Gameplay_LiftPortOar = m_Gameplay.FindAction("LiftPortOar", throwIfNotFound: true);
        m_Gameplay_LiftStarboardOar = m_Gameplay.FindAction("LiftStarboardOar", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_StarboardOar;
    private readonly InputAction m_Gameplay_PortOar;
    private readonly InputAction m_Gameplay_GrabOars;
    private readonly InputAction m_Gameplay_LiftPortOar;
    private readonly InputAction m_Gameplay_LiftStarboardOar;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @StarboardOar => m_Wrapper.m_Gameplay_StarboardOar;
        public InputAction @PortOar => m_Wrapper.m_Gameplay_PortOar;
        public InputAction @GrabOars => m_Wrapper.m_Gameplay_GrabOars;
        public InputAction @LiftPortOar => m_Wrapper.m_Gameplay_LiftPortOar;
        public InputAction @LiftStarboardOar => m_Wrapper.m_Gameplay_LiftStarboardOar;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @StarboardOar.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStarboardOar;
                @StarboardOar.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStarboardOar;
                @StarboardOar.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStarboardOar;
                @PortOar.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPortOar;
                @PortOar.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPortOar;
                @PortOar.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPortOar;
                @GrabOars.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrabOars;
                @GrabOars.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrabOars;
                @GrabOars.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrabOars;
                @LiftPortOar.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLiftPortOar;
                @LiftPortOar.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLiftPortOar;
                @LiftPortOar.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLiftPortOar;
                @LiftStarboardOar.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLiftStarboardOar;
                @LiftStarboardOar.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLiftStarboardOar;
                @LiftStarboardOar.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLiftStarboardOar;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @StarboardOar.started += instance.OnStarboardOar;
                @StarboardOar.performed += instance.OnStarboardOar;
                @StarboardOar.canceled += instance.OnStarboardOar;
                @PortOar.started += instance.OnPortOar;
                @PortOar.performed += instance.OnPortOar;
                @PortOar.canceled += instance.OnPortOar;
                @GrabOars.started += instance.OnGrabOars;
                @GrabOars.performed += instance.OnGrabOars;
                @GrabOars.canceled += instance.OnGrabOars;
                @LiftPortOar.started += instance.OnLiftPortOar;
                @LiftPortOar.performed += instance.OnLiftPortOar;
                @LiftPortOar.canceled += instance.OnLiftPortOar;
                @LiftStarboardOar.started += instance.OnLiftStarboardOar;
                @LiftStarboardOar.performed += instance.OnLiftStarboardOar;
                @LiftStarboardOar.canceled += instance.OnLiftStarboardOar;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnStarboardOar(InputAction.CallbackContext context);
        void OnPortOar(InputAction.CallbackContext context);
        void OnGrabOars(InputAction.CallbackContext context);
        void OnLiftPortOar(InputAction.CallbackContext context);
        void OnLiftStarboardOar(InputAction.CallbackContext context);
    }
}
