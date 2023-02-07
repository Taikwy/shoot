using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    
    public TextMeshProUGUI numScrap;

    // public TextMeshProUGUI movementSpeed;
    // public TextMeshProUGUI dashDistance;
    // public TextMeshProUGUI dashSpeed;
    // public TextMeshProUGUI isDashing;
    // public TextMeshProUGUI maxHealth;
    // public TextMeshProUGUI currentHealth;
    // public TextMeshProUGUI currentDefaultAmmo;
    // public TextMeshProUGUI currentSkillAmmo;
    
    public void SetText(float scrap){
        numScrap.text = scrap.ToString();
    }

    // public void SetText(float moveSpd, float dashDist, float dashSpd, bool isDash, float maxHP, float currentHP, float currentDefault, float currentSkill){
    //     movementSpeed.text = moveSpd.ToString();
    //     dashDistance.text = dashDist.ToString();
    //     dashSpeed.text = dashSpd.ToString();
    //     isDashing.text = isDash.ToString();
    //     maxHealth.text = maxHP.ToString();
    //     currentHealth.text = currentHP.ToString();
    //     currentDefaultAmmo.text = currentDefault.ToString();
    //     currentSkillAmmo.text = currentSkill.ToString();
    // }

    public void UpdateText(TextMeshProUGUI textMesh, string value){
        textMesh.text = value;
    }
}
