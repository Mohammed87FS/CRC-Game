using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider timerSlider;
    private MapGenerator mapGenerator;

    void Start()
    {
        mapGenerator = FindObjectOfType<MapGenerator>();
        SetLevelDuration(mapGenerator.GetLevelDuration());
    }

    void Update()
    {
        UpdateTimerSlider();
    }

    public void SetLevelDuration(float duration)
    {
        timerSlider.maxValue = duration;
        timerSlider.value = duration;
    }

    void UpdateTimerSlider()
    {
        float remainingTime = mapGenerator.GetLevelTimer();
        timerSlider.value = remainingTime;
    }
}
