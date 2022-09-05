using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovementInfo : MonoBehaviour
{
    public TextMeshProUGUI movementSpeed;
    public TextMeshProUGUI dashDistance;
    public TextMeshProUGUI dashSpeed;
    public TextMeshProUGUI isDashing;

    public void SetText(float moveSpd, float dashDist, float dashSpd, bool isDash){
        movementSpeed.text = moveSpd.ToString();
        dashDistance.text = dashDist.ToString();
        dashSpeed.text = dashSpd.ToString();
        isDashing.text = isDash.ToString();
    }

    public void UpdateText(TextMeshProUGUI textMesh, string value){
        textMesh.text = value;
    }
}
