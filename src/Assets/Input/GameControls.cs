//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Input/GameControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GameControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""RowingTest"",
            ""id"": ""9ea4a343-59ec-41bf-b12f-7d2f88525c3c"",
            ""actions"": [
                {
                    ""name"": ""Catch"",
                    ""type"": ""Button"",
                    ""id"": ""422b4de6-a9cd-4b0b-9f2f-ff29ff08c972"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""170d6c5e-221a-4abd-a74f-450a156b594a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RudderUp"",
                    ""type"": ""Button"",
                    ""id"": ""bbb434c4-eaea-46e9-aee2-df03574d2e65"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RudderDown"",
                    ""type"": ""Button"",
                    ""id"": ""6c3f8857-a884-44aa-9fb6-62ed11018266"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reverse"",
                    ""type"": ""Button"",
                    ""id"": ""7ace1d4d-477a-48b0-95b6-8df52609edda"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2233b83e-8fea-437a-bd57-4ac3921ad223"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Catch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0b06e15-762b-497a-805f-be14369d05c3"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58c517f4-f1e4-4860-b854-17fdafeefcf1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RudderUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a18d17e4-b42d-430c-8a57-5cbc55feabc2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RudderDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eecab7b1-cdd4-4ddc-9024-00767effc0e0"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reverse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // RowingTest
        m_RowingTest = asset.FindActionMap("RowingTest", throwIfNotFound: true);
        m_RowingTest_Catch = m_RowingTest.FindAction("Catch", throwIfNotFound: true);
        m_RowingTest_Inventory = m_RowingTest.FindAction("Inventory", throwIfNotFound: true);
        m_RowingTest_RudderUp = m_RowingTest.FindAction("RudderUp", throwIfNotFound: true);
        m_RowingTest_RudderDown = m_RowingTest.FindAction("RudderDown", throwIfNotFound: true);
        m_RowingTest_Reverse = m_RowingTest.FindAction("Reverse", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // RowingTest
    private readonly InputActionMap m_RowingTest;
    private IRowingTestActions m_RowingTestActionsCallbackInterface;
    private readonly InputAction m_RowingTest_Catch;
    private readonly InputAction m_RowingTest_Inventory;
    private readonly InputAction m_RowingTest_RudderUp;
    private readonly InputAction m_RowingTest_RudderDown;
    private readonly InputAction m_RowingTest_Reverse;
    public struct RowingTestActions
    {
        private @GameControls m_Wrapper;
        public RowingTestActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Catch => m_Wrapper.m_RowingTest_Catch;
        public InputAction @Inventory => m_Wrapper.m_RowingTest_Inventory;
        public InputAction @RudderUp => m_Wrapper.m_RowingTest_RudderUp;
        public InputAction @RudderDown => m_Wrapper.m_RowingTest_RudderDown;
        public InputAction @Reverse => m_Wrapper.m_RowingTest_Reverse;
        public InputActionMap Get() { return m_Wrapper.m_RowingTest; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RowingTestActions set) { return set.Get(); }
        public void SetCallbacks(IRowingTestActions instance)
        {
            if (m_Wrapper.m_RowingTestActionsCallbackInterface != null)
            {
                @Catch.started -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnCatch;
                @Catch.performed -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnCatch;
                @Catch.canceled -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnCatch;
                @Inventory.started -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnInventory;
                @RudderUp.started -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnRudderUp;
                @RudderUp.performed -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnRudderUp;
                @RudderUp.canceled -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnRudderUp;
                @RudderDown.started -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnRudderDown;
                @RudderDown.performed -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnRudderDown;
                @RudderDown.canceled -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnRudderDown;
                @Reverse.started -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnReverse;
                @Reverse.performed -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnReverse;
                @Reverse.canceled -= m_Wrapper.m_RowingTestActionsCallbackInterface.OnReverse;
            }
            m_Wrapper.m_RowingTestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Catch.started += instance.OnCatch;
                @Catch.performed += instance.OnCatch;
                @Catch.canceled += instance.OnCatch;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @RudderUp.started += instance.OnRudderUp;
                @RudderUp.performed += instance.OnRudderUp;
                @RudderUp.canceled += instance.OnRudderUp;
                @RudderDown.started += instance.OnRudderDown;
                @RudderDown.performed += instance.OnRudderDown;
                @RudderDown.canceled += instance.OnRudderDown;
                @Reverse.started += instance.OnReverse;
                @Reverse.performed += instance.OnReverse;
                @Reverse.canceled += instance.OnReverse;
            }
        }
    }
    public RowingTestActions @RowingTest => new RowingTestActions(this);
    public interface IRowingTestActions
    {
        void OnCatch(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnRudderUp(InputAction.CallbackContext context);
        void OnRudderDown(InputAction.CallbackContext context);
        void OnReverse(InputAction.CallbackContext context);
    }
}