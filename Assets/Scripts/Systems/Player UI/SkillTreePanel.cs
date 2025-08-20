using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    ClassSkillTree skillTree;
    [SerializeField] List<SkillTreeButton> skillTreeButtons = new List<SkillTreeButton>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerController.instance != null) skillTree = PlayerController.instance.SkillManager().GetSkillTree();
        EventsManager.instance.onSkillPointSpent.AddListener(UpdateSkillTree);
    }

    void OnDestroy()
    {
        EventsManager.instance.onSkillPointSpent.RemoveListener(UpdateSkillTree);
    }

    void OnEnable()
    {
        if (skillTree != null) UpdateSkillTree();
        else
        {
            if (PlayerController.instance != null)
            {
                skillTree = PlayerController.instance.SkillManager().GetSkillTree();
                UpdateSkillTree();
            }
        }
    }

    void UpdateSkillTree()
    {
        int i = 0;
        foreach (SkillTreeButton button in skillTreeButtons)
        {
            if (button != null)
            {
                if (i < skillTree.list.Count && skillTree.list[i] != null)
                {
                    button.gameObject.SetActive(true);
                    button.UpdateButton(skillTree.list[i]);
                }
                else
                {
                    button.gameObject.SetActive(false);
                }
            }
            i++;
            if (i >= skillTreeButtons.Count)
            {
                // more skills than buttons in the active class skill manager 
                i--;
            }
        }
    }

    public void HideSkillTreePanel()
    {
        if (UIManager.instance != null) UIManager.instance.HideSkillTreePanel();
    }
}
