using UnityEngine.UI;
using UnityEngine;
public class EffectVolumeSlider : MonoBehaviour
{
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
        SoundManager.Instance.ChangeEffectsVolume(slider.value);
        slider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeEffectsVolume(value));
    }
}
