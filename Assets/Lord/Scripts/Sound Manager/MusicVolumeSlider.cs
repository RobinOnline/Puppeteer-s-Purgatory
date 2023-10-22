using UnityEngine.UI;
using UnityEngine;
public class MusicVolumeSlider : MonoBehaviour
{
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
        SoundManager.Instance.ChangeMusicVolume(slider.value);
        slider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeMusicVolume(value));
    }
}
