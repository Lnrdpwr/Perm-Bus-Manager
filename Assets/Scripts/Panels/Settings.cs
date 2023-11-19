using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Settings : MonoBehaviour{
	[Header("Музыка и звуки")]
	[SerializeField] private AudioSource _musicSource;
	[SerializeField] private AudioSource _soundSource;

	[Header("Постобработка")]
	[SerializeField] private PostProcessVolume _postProcessVolume;

	public void SetSoundsVolume(float volume){
		_soundSource.volume = volume;
	}

	public void SetMusicVolume(float volume){
		_musicSource.volume = volume;
	}

	public void ToggleEffects(bool value){
		_postProcessVolume.enabled = value;
	}
}
