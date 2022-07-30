using UnityEngine;
using Message;
using System.Collections;
using UnityEngine.XR.WSA;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class myPlayerController : MonoBehaviour, IMessageReceiver
{
    protected static myPlayerController s_Instance;
    public static myPlayerController instance { get { return s_Instance; } }

    // Called automatically by Unity when the script is first added to a gameobject or is reset from the context menu.
    void Reset()
    {

    }

    // Called automatically by Unity when the script first exists in the scene.
    void Awake()
    {
        s_Instance = this;
    }

    // Called automatically by Unity after Awake whenever the script is enabled. 
    void OnEnable()
    {
        // SceneLinkedSMB<PlayerController>.Initialise(m_Animator, this);

        //m_Damageable = GetComponent<Damageable>();
        //m_Damageable.onDamageMessageReceivers.Add(this);

        //m_Damageable.isInvulnerable = true;

        //m_Renderers = GetComponentsInChildren<Renderer>();
    }

    // Called automatically by Unity whenever the script is disabled.
    void OnDisable()
    {
        //m_Damageable.onDamageMessageReceivers.Remove(this);
    }

    // Called automatically by Unity once every Physics step.
    void FixedUpdate()
    {

    }

    // Called at the start of FixedUpdate to record the current state of the base layer of the animator.
    void CacheAnimatorState()
    {

    }

    // Called after the animator state has been cached to determine whether this script should block user input.
    void UpdateInputBlocking()
    {
        //bool inputBlocked = m_CurrentStateInfo.tagHash == m_HashBlockInput && !m_IsAnimatorTransitioning;
        //inputBlocked |= m_NextStateInfo.tagHash == m_HashBlockInput;
        //m_Input.playerControllerInputBlocked = inputBlocked;
    }

    // Called after the animator state has been cached to determine whether or not the staff should be active or not.
    /*bool IsWeaponEquiped()
    {
        bool equipped = m_NextStateInfo.shortNameHash == m_HashEllenCombo1 || m_CurrentStateInfo.shortNameHash == m_HashEllenCombo1;
        equipped |= m_NextStateInfo.shortNameHash == m_HashEllenCombo2 || m_CurrentStateInfo.shortNameHash == m_HashEllenCombo2;
        equipped |= m_NextStateInfo.shortNameHash == m_HashEllenCombo3 || m_CurrentStateInfo.shortNameHash == m_HashEllenCombo3;
        equipped |= m_NextStateInfo.shortNameHash == m_HashEllenCombo4 || m_CurrentStateInfo.shortNameHash == m_HashEllenCombo4;

        return equipped;
    }*/

    // Called each physics step with a parameter based on the return value of IsWeaponEquiped.
    void EquipMeleeWeapon(bool equip)
    {
        /*meleeWeapon.gameObject.SetActive(equip);
        m_InAttack = false;
        m_InCombo = equip;

        if (!equip)
            m_Animator.ResetTrigger(m_HashMeleeAttack);*/
    }

    // Called each physics step.
    void CalculateForwardMovement()
    {/*
            // Cache the move input and cap it's magnitude at 1.
            Vector2 moveInput = m_Input.MoveInput;
            if (moveInput.sqrMagnitude > 1f)
                moveInput.Normalize();

            // Calculate the speed intended by input.
            m_DesiredForwardSpeed = moveInput.magnitude * maxForwardSpeed;

            // Determine change to speed based on whether there is currently any move input.
            float acceleration = IsMoveInput ? k_GroundAcceleration : k_GroundDeceleration;

            // Adjust the forward speed towards the desired speed.
            m_ForwardSpeed = Mathf.MoveTowards(m_ForwardSpeed, m_DesiredForwardSpeed, acceleration * Time.deltaTime);

            // Set the animator parameter to control what animation is being played.
            m_Animator.SetFloat(m_HashForwardSpeed, m_ForwardSpeed);*/
    }

    // Called each physics step.
    void CalculateVerticalMovement()
    {/*
            // If jump is not currently held and Ellen is on the ground then she is ready to jump.
            if (!m_Input.JumpInput && m_IsGrounded)
                m_ReadyToJump = true;

            if (m_IsGrounded)
            {
                // When grounded we apply a slight negative vertical speed to make Ellen "stick" to the ground.
                m_VerticalSpeed = -gravity * k_StickingGravityProportion;

                // If jump is held, Ellen is ready to jump and not currently in the middle of a melee combo...
                if (m_Input.JumpInput && m_ReadyToJump && !m_InCombo)
                {
                    // ... then override the previously set vertical speed and make sure she cannot jump again.
                    m_VerticalSpeed = jumpSpeed;
                    m_IsGrounded = false;
                    m_ReadyToJump = false;
                }
            }
            else
            {
                // If Ellen is airborne, the jump button is not held and Ellen is currently moving upwards...
                if (!m_Input.JumpInput && m_VerticalSpeed > 0.0f)
                {
                    // ... decrease Ellen's vertical speed.
                    // This is what causes holding jump to jump higher that tapping jump.
                    m_VerticalSpeed -= k_JumpAbortSpeed * Time.deltaTime;
                }

                // If a jump is approximately peaking, make it absolute.
                if (Mathf.Approximately(m_VerticalSpeed, 0f))
                {
                    m_VerticalSpeed = 0f;
                }

                // If Ellen is airborne, apply gravity.
                m_VerticalSpeed -= gravity * Time.deltaTime;
            }*/
    }

    // Called each physics step to set the rotation Ellen is aiming to have.
    void SetTargetRotation()
    {

    }

    // Called each physics step to help determine whether Ellen can turn under player input.
    /*
    bool IsOrientationUpdated()
    {
        bool updateOrientationForLocomotion = !m_IsAnimatorTransitioning && m_CurrentStateInfo.shortNameHash == m_HashLocomotion || m_NextStateInfo.shortNameHash == m_HashLocomotion;
        bool updateOrientationForAirborne = !m_IsAnimatorTransitioning && m_CurrentStateInfo.shortNameHash == m_HashAirborne || m_NextStateInfo.shortNameHash == m_HashAirborne;
        bool updateOrientationForLanding = !m_IsAnimatorTransitioning && m_CurrentStateInfo.shortNameHash == m_HashLanding || m_NextStateInfo.shortNameHash == m_HashLanding;

        return updateOrientationForLocomotion || updateOrientationForAirborne || updateOrientationForLanding || m_InCombo && !m_InAttack;
    }*/

    // Called each physics step after SetTargetRotation if there is move input and Ellen is in the correct animator state according to IsOrientationUpdated.
    void UpdateOrientation()
    {/*
            m_Animator.SetFloat(m_HashAngleDeltaRad, m_AngleDiff * Mathf.Deg2Rad);

            Vector3 localInput = new Vector3(m_Input.MoveInput.x, 0f, m_Input.MoveInput.y);
            float groundedTurnSpeed = Mathf.Lerp(maxTurnSpeed, minTurnSpeed, m_ForwardSpeed / m_DesiredForwardSpeed);
            float actualTurnSpeed = m_IsGrounded ? groundedTurnSpeed : Vector3.Angle(transform.forward, localInput) * k_InverseOneEighty * k_AirborneTurnSpeedProportion * groundedTurnSpeed;
            m_TargetRotation = Quaternion.RotateTowards(transform.rotation, m_TargetRotation, actualTurnSpeed * Time.deltaTime);

            transform.rotation = m_TargetRotation;*/
    }

    // Called each physics step to check if audio should be played and if so instruct the relevant random audio player to do so.
    void PlayAudio()
    {

    }

    // Called each physics step to count up to the point where Ellen considers a random idle.
    void TimeoutToIdle()
    {

    }

    // Called each physics step (so long as the Animator component is set to Animate Physics) after FixedUpdate to override root motion.
    void OnAnimatorMove()
    {

    }

    // This is called by an animation event when Ellen swings her staff.
    public void MeleeAttackStart(int throwing = 0)
    {

    }

    // This is called by an animation event when Ellen finishes swinging her staff.
    public void MeleeAttackEnd()
    {

    }

    // Called by a state machine behaviour on Ellen's animator controller.
    public void RespawnFinished()
    {

    }

    // Called by Ellen's Damageable when she is hurt.
    public void OnReceiveMessage(MessageType type, object sender, object data)
    {
        /*
            switch (type)
            {
                case MessageType.DAMAGED:
                    {
                        Damageable.DamageMessage damageData = (Damageable.DamageMessage)data;
                        Damaged(damageData);
                    }
                    break;
                case MessageType.DEAD:
                    {
                        Damageable.DamageMessage damageData = (Damageable.DamageMessage)data;
                        Die(damageData);
                    }
                    break;
            }*/
    }
}