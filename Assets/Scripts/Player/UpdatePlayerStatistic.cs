using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdatePlayerStatistic 
{
    Vector3 lastPositon;

    public void ShowAngle(TextMeshProUGUI angleText, float angle)
    {
        angleText.text = $"angle: {angle:F0}";
    }

    public void ShowPosition(TextMeshProUGUI positionText, Transform player)
    {
        positionText.text = $"coords: {player.position.x:F1}" + ":" + $"{player.position.y:F1}";
    }

    public void ShowSpeed(TextMeshProUGUI speedText, Transform player)
    {
        float speed = (player.position - lastPositon).magnitude / Time.deltaTime;
        lastPositon = player.position;
        speedText.text = $"speed: {speed:F1}";
    }

    public void ShowLaserCharges(TextMeshProUGUI chargesText, int amount)
    {
        chargesText.text = $"charges: {amount}";
    }

    public void ShowLaserColdown(TextMeshProUGUI coldown, float time)
    {
        coldown.text = $"coldown: {time:F1}";
    }

}
