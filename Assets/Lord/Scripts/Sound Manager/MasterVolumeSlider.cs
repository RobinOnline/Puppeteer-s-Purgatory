using UnityEngine.UI;
using UnityEngine;
public class MasterVolumeSlider : MonoBehaviour
{
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
        SoundManager.Instance.ChangeMasterVolume(slider.value);
        slider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeMasterVolume(value));
    }
}
