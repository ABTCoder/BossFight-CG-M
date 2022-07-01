//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Script/MovementController.inputactions
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

public partial class @MovementController : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MovementController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MovementController"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""e667d9d6-102a-4f9f-b636-6c5db4e5b533"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""36aa9334-2875-4dff-9776-eee8e31be7fb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Move Camera"",
                    ""type"": ""Value"",
                    ""id"": ""c60c5d47-d0a6-4936-9bbd-effd003d64a0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ShieldBlock"",
                    ""type"": ""Button"",
                    ""id"": ""6ee84591-d16e-4269-a31b-ea679302af96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""BaseAttack"",
                    ""type"": ""Button"",
                    ""id"": ""8304a417-978f-489b-a93f-df4e8eccebf2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Fireball"",
                    ""type"": ""Button"",
                    ""id"": ""4f00b8fb-1f2d-41c9-9142-1203e39a8f7d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LockOn"",
                    ""type"": ""Button"",
                    ""id"": ""10a5cae7-fd09-47f0-a69a-5f66a114b4e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Lock On Target Left"",
                    ""type"": ""Button"",
                    ""id"": ""643ef5da-ffd9-4ab4-bdaf-62686faa7c59"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Lock On Target Right"",
                    ""type"": ""Button"",
                    ""id"": ""2b6403ad-e23f-42f7-986e-da217d47e046"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DodgeRoll"",
                    ""type"": ""Button"",
                    ""id"": ""49909745-3cbd-41eb-bb1d-cfd2690ff6a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""HealthUp"",
                    ""type"": ""Button"",
                    ""id"": ""c54a162c-7f0e-4463-b608-1d788b1c4c54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchLockOnTarget"",
                    ""type"": ""Value"",
                    ""id"": ""5eaa564c-a40f-47e0-b84e-624611edab20"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7533080f-e6fd-4f0a-8dbc-36961a604fa2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3be991cb-6c35-4f42-96cf-e7682418ebdc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e6ca2147-8d9c-4342-95f8-b5328ad1da4c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d859fccc-2419-45d8-aea8-a34b78fd43b8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1338ee86-063f-4421-8ac8-1f0fc275fdb8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""61e3b172-a4ff-4d5a-b536-4beaf4a7afe7"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d36089a-daf3-4daf-bfc6-868fec5772ca"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BaseAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""654fd3dd-79d7-47f8-87c8-fc0059e3d8cf"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fireball"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44d0e65c-5a69-45e9-bb3a-3800bda7afc0"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""517d4b13-4d74-4554-8394-918c567bbcc8"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Target Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5a537de-02f4-4d00-a702-b82be4aa44cc"",
                    ""path"": ""<Mouse>/forwardButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Target Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d941f15f-a52b-4cab-a391-d29fa96da4d6"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShieldBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aaddf609-60d3-43f5-8d27-8115e3291fb9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DodgeRoll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a200a0aa-fe05-41be-871e-94005352b521"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HealthUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a9668b0-0e02-4c46-8ba8-bd53d4447017"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchLockOnTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_Move = m_Main.FindAction("Move", throwIfNotFound: true);
        m_Main_MoveCamera = m_Main.FindAction("Move Camera", throwIfNotFound: true);
        m_Main_ShieldBlock = m_Main.FindAction("ShieldBlock", throwIfNotFound: true);
        m_Main_BaseAttack = m_Main.FindAction("BaseAttack", throwIfNotFound: true);
        m_Main_Fireball = m_Main.FindAction("Fireball", throwIfNotFound: true);
        m_Main_LockOn = m_Main.FindAction("LockOn", throwIfNotFound: true);
        m_Main_LockOnTargetLeft = m_Main.FindAction("Lock On Target Left", throwIfNotFound: true);
        m_Main_LockOnTargetRight = m_Main.FindAction("Lock On Target Right", throwIfNotFound: true);
        m_Main_DodgeRoll = m_Main.FindAction("DodgeRoll", throwIfNotFound: true);
        m_Main_HealthUp = m_Main.FindAction("HealthUp", throwIfNotFound: true);
        m_Main_SwitchLockOnTarget = m_Main.FindAction("SwitchLockOnTarget", throwIfNotFound: true);
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

    // Main
    private readonly InputActionMap m_Main;
    private IMainActions m_MainActionsCallbackInterface;
    private readonly InputAction m_Main_Move;
    private readonly InputAction m_Main_MoveCamera;
    private readonly InputAction m_Main_ShieldBlock;
    private readonly InputAction m_Main_BaseAttack;
    private readonly InputAction m_Main_Fireball;
    private readonly InputAction m_Main_LockOn;
    private readonly InputAction m_Main_LockOnTargetLeft;
    private readonly InputAction m_Main_LockOnTargetRight;
    private readonly InputAction m_Main_DodgeRoll;
    private readonly InputAction m_Main_HealthUp;
    private readonly InputAction m_Main_SwitchLockOnTarget;
    public struct MainActions
    {
        private @MovementController m_Wrapper;
        public MainActions(@MovementController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Main_Move;
        public InputAction @MoveCamera => m_Wrapper.m_Main_MoveCamera;
        public InputAction @ShieldBlock => m_Wrapper.m_Main_ShieldBlock;
        public InputAction @BaseAttack => m_Wrapper.m_Main_BaseAttack;
        public InputAction @Fireball => m_Wrapper.m_Main_Fireball;
        public InputAction @LockOn => m_Wrapper.m_Main_LockOn;
        public InputAction @LockOnTargetLeft => m_Wrapper.m_Main_LockOnTargetLeft;
        public InputAction @LockOnTargetRight => m_Wrapper.m_Main_LockOnTargetRight;
        public InputAction @DodgeRoll => m_Wrapper.m_Main_DodgeRoll;
        public InputAction @HealthUp => m_Wrapper.m_Main_HealthUp;
        public InputAction @SwitchLockOnTarget => m_Wrapper.m_Main_SwitchLockOnTarget;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMove;
                @MoveCamera.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMoveCamera;
                @ShieldBlock.started -= m_Wrapper.m_MainActionsCallbackInterface.OnShieldBlock;
                @ShieldBlock.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnShieldBlock;
                @ShieldBlock.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnShieldBlock;
                @BaseAttack.started -= m_Wrapper.m_MainActionsCallbackInterface.OnBaseAttack;
                @BaseAttack.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnBaseAttack;
                @BaseAttack.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnBaseAttack;
                @Fireball.started -= m_Wrapper.m_MainActionsCallbackInterface.OnFireball;
                @Fireball.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnFireball;
                @Fireball.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnFireball;
                @LockOn.started -= m_Wrapper.m_MainActionsCallbackInterface.OnLockOn;
                @LockOn.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnLockOn;
                @LockOn.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnLockOn;
                @LockOnTargetLeft.started -= m_Wrapper.m_MainActionsCallbackInterface.OnLockOnTargetLeft;
                @LockOnTargetLeft.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnLockOnTargetLeft;
                @LockOnTargetLeft.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnLockOnTargetLeft;
                @LockOnTargetRight.started -= m_Wrapper.m_MainActionsCallbackInterface.OnLockOnTargetRight;
                @LockOnTargetRight.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnLockOnTargetRight;
                @LockOnTargetRight.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnLockOnTargetRight;
                @DodgeRoll.started -= m_Wrapper.m_MainActionsCallbackInterface.OnDodgeRoll;
                @DodgeRoll.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnDodgeRoll;
                @DodgeRoll.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnDodgeRoll;
                @HealthUp.started -= m_Wrapper.m_MainActionsCallbackInterface.OnHealthUp;
                @HealthUp.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnHealthUp;
                @HealthUp.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnHealthUp;
                @SwitchLockOnTarget.started -= m_Wrapper.m_MainActionsCallbackInterface.OnSwitchLockOnTarget;
                @SwitchLockOnTarget.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnSwitchLockOnTarget;
                @SwitchLockOnTarget.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnSwitchLockOnTarget;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @MoveCamera.started += instance.OnMoveCamera;
                @MoveCamera.performed += instance.OnMoveCamera;
                @MoveCamera.canceled += instance.OnMoveCamera;
                @ShieldBlock.started += instance.OnShieldBlock;
                @ShieldBlock.performed += instance.OnShieldBlock;
                @ShieldBlock.canceled += instance.OnShieldBlock;
                @BaseAttack.started += instance.OnBaseAttack;
                @BaseAttack.performed += instance.OnBaseAttack;
                @BaseAttack.canceled += instance.OnBaseAttack;
                @Fireball.started += instance.OnFireball;
                @Fireball.performed += instance.OnFireball;
                @Fireball.canceled += instance.OnFireball;
                @LockOn.started += instance.OnLockOn;
                @LockOn.performed += instance.OnLockOn;
                @LockOn.canceled += instance.OnLockOn;
                @LockOnTargetLeft.started += instance.OnLockOnTargetLeft;
                @LockOnTargetLeft.performed += instance.OnLockOnTargetLeft;
                @LockOnTargetLeft.canceled += instance.OnLockOnTargetLeft;
                @LockOnTargetRight.started += instance.OnLockOnTargetRight;
                @LockOnTargetRight.performed += instance.OnLockOnTargetRight;
                @LockOnTargetRight.canceled += instance.OnLockOnTargetRight;
                @DodgeRoll.started += instance.OnDodgeRoll;
                @DodgeRoll.performed += instance.OnDodgeRoll;
                @DodgeRoll.canceled += instance.OnDodgeRoll;
                @HealthUp.started += instance.OnHealthUp;
                @HealthUp.performed += instance.OnHealthUp;
                @HealthUp.canceled += instance.OnHealthUp;
                @SwitchLockOnTarget.started += instance.OnSwitchLockOnTarget;
                @SwitchLockOnTarget.performed += instance.OnSwitchLockOnTarget;
                @SwitchLockOnTarget.canceled += instance.OnSwitchLockOnTarget;
            }
        }
    }
    public MainActions @Main => new MainActions(this);
    public interface IMainActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnMoveCamera(InputAction.CallbackContext context);
        void OnShieldBlock(InputAction.CallbackContext context);
        void OnBaseAttack(InputAction.CallbackContext context);
        void OnFireball(InputAction.CallbackContext context);
        void OnLockOn(InputAction.CallbackContext context);
        void OnLockOnTargetLeft(InputAction.CallbackContext context);
        void OnLockOnTargetRight(InputAction.CallbackContext context);
        void OnDodgeRoll(InputAction.CallbackContext context);
        void OnHealthUp(InputAction.CallbackContext context);
        void OnSwitchLockOnTarget(InputAction.CallbackContext context);
    }
}
