using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using RPG.CameraUI;
using UnityEditor;
using UnityEngine.UI;

// for mouse events

namespace RPG.Characters
{
    public class PlayerControl : MonoBehaviour
    {
        Character character;
        SpecialAbilities abilities;
        WeaponSystem weaponSystem;
        Interactable interactable;

        void Start()
        {
            character = GetComponent<Character>();
            abilities = GetComponent<SpecialAbilities>();
            weaponSystem = GetComponent<WeaponSystem>();
            interactable = GetComponent<Interactable>();

            BindManaBarToPlayerHud();
            RegisterForMouseEvents();
        }

        private void BindManaBarToPlayerHud()
        {
            if (GameObject.Find("Core Game Elements"))
                abilities.energyBar = GameObject.FindGameObjectWithTag("PlayerSpecialBar").GetComponent<Image>();
            else
                Debug.LogWarning("There is a player in the scene but no Core Game Elements with mana bar exist");
        }

        private void RegisterForMouseEvents()
        {
            var cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
            cameraRaycaster.onMouseOverPotentiallyWalkable += OnMouseOverPotentiallyWalkable;
            cameraRaycaster.onMouseOverInteractable += OnMouseOverInteractable;
        }

        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            ScanForAbilityKeyDown();

            #region debug keys
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                GetComponent<Character>().moveSpeedMultiplier += 0.5f;
                Debug.Log("Player speed set to " + GetComponent<Character>().moveSpeedMultiplier);
            }

            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                GetComponent<Character>().moveSpeedMultiplier -= 0.5f;
                Debug.Log("Player speed set to " + GetComponent<Character>().moveSpeedMultiplier);
            }

            GetComponent<Character>().moveSpeedMultiplier = Mathf.Clamp(GetComponent<Character>().moveSpeedMultiplier, 0f, 10f);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Time.timeScale += 0.1f;
                Debug.Log("Time scale set to " + Time.timeScale);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Time.timeScale -= 0.1f;
                Debug.Log("Time scale set to " + Time.timeScale);
            }

            GetComponent<Character>().moveSpeedMultiplier = Mathf.Clamp(GetComponent<Character>().moveSpeedMultiplier, 0f, 2f);

            if (Input.GetKeyDown(KeyCode.G))
            {
                character.godMode = !character.godMode;
                Debug.Log("GodMode set to " + character.godMode);
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                character.unlimitedMana = !character.unlimitedMana;
                Debug.Log("Unlimited mana set to " + character.unlimitedMana);
            }

            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                GetComponent<Character>().moveSpeedMultiplier = 1f;
                Time.timeScale = 1f;
                Debug.Log("Reset");
            }

            #endregion
        }

        void ScanForAbilityKeyDown()
        {
            for (int keyIndex = 1; keyIndex < abilities.GetNumberOfAbilities(); keyIndex++)
            {
                if (Input.GetKeyDown(keyIndex.ToString()))
                {
                    abilities.AttemptSpecialAbility(keyIndex);
                }
            }
        }

        void OnMouseOverPotentiallyWalkable(Vector3 destination)
        {
            if (Input.GetMouseButton(0))
            {
                weaponSystem.StopAttacking();
                character.SetDestination(destination);
            }
        }

        bool IsTargetInRange(GameObject target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= weaponSystem.GetCurrentWeapon().GetMaxAttackRange();
        }

        void OnMouseOverEnemy(EnemyAI enemy)
        {
            if (Input.GetMouseButton(0) && IsTargetInRange(enemy.gameObject))
            {
                weaponSystem.AttackTarget(enemy.gameObject);
            }
            else if (Input.GetMouseButton(0) && !IsTargetInRange(enemy.gameObject))
            {
                StartCoroutine(MoveAndAttack(enemy));
            }
            else if (Input.GetMouseButtonDown(1) && IsTargetInRange(enemy.gameObject))
            {
                abilities.AttemptSpecialAbility(0, enemy.gameObject);
            }
            else if (Input.GetMouseButtonDown(1) && !IsTargetInRange(enemy.gameObject))
            {
                StartCoroutine(MoveAndPowerAttack(enemy));
            }
        }

        void OnMouseOverInteractable(Interactable interactable)
        {
            if (Input.GetMouseButtonDown(1) && IsTargetInRange(interactable.gameObject))
            {
                weaponSystem.StopAttacking();
                // TODO Intearct();
                interactable.Interact();
            }
            else if (Input.GetMouseButtonDown(1) && !IsTargetInRange(interactable.gameObject))
            {
                weaponSystem.StopAttacking();
                // TODO MoveAndIntearct();
                StartCoroutine(MoveAndInteract(interactable));
            }
        }

        IEnumerator MoveToTarget(GameObject target)
        {
            character.SetDestination(target.transform.position);
            while (!IsTargetInRange(target))
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }

        IEnumerator MoveAndAttack(EnemyAI enemy)
        {
            yield return StartCoroutine(MoveToTarget(enemy.gameObject));
            weaponSystem.AttackTarget(enemy.gameObject);
        }

        IEnumerator MoveAndPowerAttack(EnemyAI enemy)
        {
            yield return StartCoroutine(MoveToTarget(enemy.gameObject));
            abilities.AttemptSpecialAbility(0, enemy.gameObject);
        }

        IEnumerator MoveAndInteract(Interactable interactable)
        {
            yield return StartCoroutine(MoveToTarget(interactable.gameObject));
            interactable.Interact();
        }
    }
}