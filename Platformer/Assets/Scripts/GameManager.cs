using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMPro.TMP_Dropdown diffDropdown;
    [SerializeField] private Toggle muteToggle;
    private int volume;
    private bool mute;
    private byte difficulty;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void SetMute() {
        this.mute = muteToggle.isOn;
    }

    public void SetVolume() {
        this.volume = (int)volumeSlider.value;
    }

    public void SetDifficulty() {
        this.difficulty = (byte)diffDropdown.value;
    }
    
}
