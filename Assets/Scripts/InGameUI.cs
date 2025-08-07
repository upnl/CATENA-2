using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;
    [SerializeField] private Image currentIconImage;
    [SerializeField] private Image skill1IconImage;
    [SerializeField] private Image skill2IconImage;

    private void Start()
    {
        /* ===== Debug Code Starts ===== */
        // InputSystem.actions.FindAction("Skill1").SubscribeAllPhases(_ =>
        // {
        //     SetSkill1IconCooldown(3f);
        // });
        // InputSystem.actions.FindAction("Skill2").SubscribeAllPhases(_ =>
        // {
        //     SetSkill2IconCooldown(1f);
        // });
        /* ===== Debug Code Ends ===== */
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

    public void SetSkill1Icon(Sprite icon)
    {
        skill1IconImage.sprite = icon;
    }

    public void SetSkill2Icon(Sprite icon)
    {
        skill2IconImage.sprite = icon;
    }

    private IEnumerator SkillCooldownCoroutine(Image skillIcon, float cooldown)
    {
        if (cooldown <= 0)
        {
            yield break;
        }

        var elapsedTime = 0f;

        while (elapsedTime < cooldown)
        {
            var progress = elapsedTime / cooldown;
            skillIcon.fillAmount = progress;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        skillIcon.fillAmount = 1f;
    }

    #pragma warning disable CS0162  // TODO: remove this later
    public void SetSkill1IconCooldown(float cooldown)
    {
        if (false) // TODO: Check if skillIconImage is in cooldown
        {
            return;
        }

        StartCoroutine(SkillCooldownCoroutine(skill1IconImage, cooldown));
    }
    
    public void SetSkill2IconCooldown(float cooldown)
    {
        if (false) // TODO: Check if skillIconImage is in cooldown
        {
            return;
        }

        StartCoroutine(SkillCooldownCoroutine(skill2IconImage, cooldown));
    }
    #pragma warning restore CS0162  // TODO: remove this later
}
