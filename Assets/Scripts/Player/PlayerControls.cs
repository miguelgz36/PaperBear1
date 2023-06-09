//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Scripts/Player/Player controls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player controls"",
    ""maps"": [
        {
            ""name"": ""Build"",
            ""id"": ""34db57a1-6046-45f5-9e4e-5f565e492898"",
            ""actions"": [
                {
                    ""name"": ""PlaceUnit"",
                    ""type"": ""Button"",
                    ""id"": ""633f0632-cc21-4a04-9d31-842ef7725e27"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e0911b89-f17f-4198-9144-711e8b0b4db1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceUnit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Build
        m_Build = asset.FindActionMap("Build", throwIfNotFound: true);
        m_Build_PlaceUnit = m_Build.FindAction("PlaceUnit", throwIfNotFound: true);
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

    // Build
    private readonly InputActionMap m_Build;
    private List<IBuildActions> m_BuildActionsCallbackInterfaces = new List<IBuildActions>();
    private readonly InputAction m_Build_PlaceUnit;
    public struct BuildActions
    {
        private @PlayerControls m_Wrapper;
        public BuildActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlaceUnit => m_Wrapper.m_Build_PlaceUnit;
        public InputActionMap Get() { return m_Wrapper.m_Build; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BuildActions set) { return set.Get(); }
        public void AddCallbacks(IBuildActions instance)
        {
            if (instance == null || m_Wrapper.m_BuildActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BuildActionsCallbackInterfaces.Add(instance);
            @PlaceUnit.started += instance.OnPlaceUnit;
            @PlaceUnit.performed += instance.OnPlaceUnit;
            @PlaceUnit.canceled += instance.OnPlaceUnit;
        }

        private void UnregisterCallbacks(IBuildActions instance)
        {
            @PlaceUnit.started -= instance.OnPlaceUnit;
            @PlaceUnit.performed -= instance.OnPlaceUnit;
            @PlaceUnit.canceled -= instance.OnPlaceUnit;
        }

        public void RemoveCallbacks(IBuildActions instance)
        {
            if (m_Wrapper.m_BuildActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBuildActions instance)
        {
            foreach (var item in m_Wrapper.m_BuildActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BuildActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BuildActions @Build => new BuildActions(this);
    public interface IBuildActions
    {
        void OnPlaceUnit(InputAction.CallbackContext context);
    }
}
