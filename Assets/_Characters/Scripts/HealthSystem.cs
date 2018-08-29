using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RPG.Characters
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] float maxHealthPoints = 100f;
        [SerializeField] AudioClip[] damageSounds;
        [SerializeField] AudioClip[] deathSounds;
        [SerializeField] float deathVanishSeconds = 2.0f;

        public Image healthBar;
        public Image healthBarOptional;

        const string DEATH_TRIGGER = "Death";

        float currentHealthPoints;
        Animator animator;
        AudioSource audioSource;
        Character character;

        public float healthAsPercentage
        {
            get { return currentHealthPoints / maxHealthPoints; }
        }

        void Start()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            character = GetComponent<Character>();

            currentHealthPoints = maxHealthPoints;
            BindHealthBarToCharacterCanvas();
        }

        void Update()
        {
            UpdateHealthBar();
        }

        void UpdateHealthBar()
        {
            if (healthBar) // Enemies may not have health bars to update
            {
                if (currentHealthPoints >= maxHealthPoints && tag != "Player")
                {
                    healthBar.enabled = false;
                }
                else
                {
                    healthBar.enabled = true;
                    if (healthBarOptional) healthBarOptional.fillAmount = healthAsPercentage;
                    healthBar.fillAmount = healthAsPercentage;
                }
            }
        }

        public void TakeDamage(float damage)
        {
            if (character.godMode == true) return;

            bool characterDies = (currentHealthPoints - damage <= 0);

            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);

            var clip = damageSounds[UnityEngine.Random.Range(0, damageSounds.Length)];
            audioSource.PlayOneShot(clip);

            if (characterDies)
            {
                StartCoroutine(KillCharacter());
            }
        }

        public void Heal(float points)
        {
            currentHealthPoints = Mathf.Clamp(currentHealthPoints + points, 0f, maxHealthPoints);
        }

        IEnumerator KillCharacter()
        {
            character.Kill();
            animator.SetTrigger(DEATH_TRIGGER);

            audioSource.clip = deathSounds[UnityEngine.Random.Range(0, deathSounds.Length)];
            audioSource.Play(); // override any existing sounds
            yield return new WaitForSecondsRealtime(audioSource.clip.length);

            var playerComponent = GetComponent<PlayerControl>();
            if (playerComponent && playerComponent.isActiveAndEnabled) // relying on lazy evaluation
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else // assume is enemy for now, reconsider on other NPCs
            {
                Destroy(gameObject, deathVanishSeconds);
            }
        }

        private void BindHealthBarToCharacterCanvas()
        {
            if (tag != "Player")
            {
                GetComponent<HealthSystem>().healthBar =
                    transform.Find("Character Canvas").Find("Health Bar").GetComponent<Image>();
            }

            if (tag == "Player")
            {
                if (GameObject.Find("Core Game Elements"))
                {
                    healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<Image>();
                    healthBarOptional = transform.Find("Character Canvas").Find("Health Bar").GetComponent<Image>();
                }
                else
                    Debug.LogWarning("There is a player in the scene but no Core Game Elements with health bar exist");
            }
        }
    }
}