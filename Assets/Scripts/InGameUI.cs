using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [Header("Current Character")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;
    [SerializeField] private Image currentIconImage;

    [Header("Skill Icons")]
    [SerializeField] private Slider skill1IconSlider;
    [SerializeField] private Image skill1IconImage;
    [SerializeField] private Image skill1BackgroundImage;

    [Header("Ready Characters")]
    [SerializeField] private Image ready1IconImage;
    [SerializeField] private Image ready1HpImage;
    [SerializeField] private Slider ready1HpSlider;
    [Space(10)]
    [SerializeField] private Image ready2IconImage;
    [SerializeField] private Image ready2HpImage;
    [SerializeField] private Slider ready2HpSlider;

    public Image ready1BarImage;
    public Image ready2BarImage;

    public Sprite[] characterIcons;
    public Sprite[] currentCharacterIcons;
    public Sprite[] currentSkillIcons;
    public Sprite[] barSprites;
    
    private PartyController _partyController;

    private void Start()
    {
        _partyController = FindObjectOfType<PartyController>();
    }
    
    private void Update()
    {
        hpSlider.value = _partyController.GetCurrentCharacterController().hp /
                         _partyController.GetCurrentCharacterController().maxHp;
        mpSlider.value = _partyController.GetCurrentCharacterController().mp /
                         _partyController.GetCurrentCharacterController().maxMp;

        ready1HpSlider.value = _partyController.GetCurrentCharacterController(0).hp /
                               _partyController.GetCurrentCharacterController(0).maxHp;
        ready2HpSlider.value = _partyController.GetCurrentCharacterController(1).hp /
                               _partyController.GetCurrentCharacterController(1).maxHp;

        ready1IconImage.sprite = characterIcons[_partyController.GetNumCharacter(0)];
        ready2IconImage.sprite = characterIcons[_partyController.GetNumCharacter(1)];

        currentIconImage.sprite = currentCharacterIcons[_partyController.GetCurrentCharacterIndex()];
        skill1IconImage.sprite = currentSkillIcons[_partyController.GetCurrentCharacterIndex()];
        
        ready1BarImage.sprite = barSprites[_partyController.GetNumCharacter(0)];
        ready2BarImage.sprite = barSprites[_partyController.GetNumCharacter(1)];
    }

    public void OnReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetHpSlider(float value)
    {
        hpSlider.value = value;
    }

    public void SetMpSlider(float value)
    {
        mpSlider.value = value;
    }

    public void SetCurrentIcon(Sprite icon)
    {
        currentIconImage.sprite = icon;
    }

    public void SetSkill1Icon(Sprite icon, Sprite background)
    {
        skill1IconImage.sprite = icon;
        skill1BackgroundImage.sprite = background;
    }

    private static IEnumerator SkillCooldownCoroutine(Slider slider, float cooldown)
    {
        if (cooldown <= 0)
        {
            yield break;
        }

        var elapsedTime = 0f;

        while (elapsedTime < cooldown)
        {
            var progress = elapsedTime / cooldown;
            slider.value = progress;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        slider.value = 1f;
    }

    public void SetSkill1IconCooldown(float cooldown)
    {
        // TODO: Check if skillIconImage is in cooldown
        // if (false)
        // {
        //     return;
        // }

        StartCoroutine(SkillCooldownCoroutine(skill1IconSlider, cooldown));
    }
    
    public void SetReady1Character(Sprite icon, Sprite hpImage, float hpValue)
    {
        ready1IconImage.sprite = icon;
        ready1HpImage.sprite = hpImage;
        ready1HpSlider.value = hpValue;
    }

    public void SetReady2Character(Sprite icon, Sprite hpImage, float hpValue)
    {
        ready2IconImage.sprite = icon;
        ready2HpImage.sprite = hpImage;
        ready2HpSlider.value = hpValue;
    }
}
