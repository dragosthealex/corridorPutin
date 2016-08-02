using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour {

	public AudioMixerSnapshot intro;
	public AudioMixerSnapshot inGame;
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot outro;
	public AudioMixerSnapshot mute;

	private AudioSource introSource;
	private AudioSource inGameSource;
	private AudioSource outroSource;
	private AudioMixerSnapshot currentSnap;

	private bool isMute = false;

	public int bpm = 128;

	private float timeTransition;
	private float timeQuarterNote;
	
	// Use this for initialization
	void Awake () {
		timeQuarterNote = 60 / bpm;
		timeTransition = 4 * timeQuarterNote;

		AudioSource[] sources = gameObject.GetComponents<AudioSource> ();
		introSource = sources [0];
		inGameSource = sources [1];
		outroSource = sources [2];
	}

	public void transitionTo(int snapshot) {
		switch (snapshot) {
		case(1):
			introSource.Play ();
			intro.TransitionTo (timeTransition);
			currentSnap = intro;
			break;
		case(2):
			inGameSource.Play ();
			inGame.TransitionTo (timeTransition);
			currentSnap = inGame;
			break;
		case(3):
			paused.TransitionTo (timeTransition);
			currentSnap = paused;
			break;
		case(4):
			outroSource.Play ();
			outro.TransitionTo (timeTransition);
			currentSnap = outro;
			break;
		}
	}

	public void Update() {
		if (Input.GetKeyDown (KeyCode.M))
			ToggleMute ();
	}

	private void ToggleMute() {
		isMute = !isMute;

		if (isMute) {
			// Mute
			mute.TransitionTo (4 * timeTransition);
		} else {
			currentSnap.TransitionTo (4 * timeTransition);
		}
	}
}
