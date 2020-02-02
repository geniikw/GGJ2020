using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> _clips;
    public AudioSource source;
    public static AudioManager i;
  
  private void Awake() {
      i = this;
  }
    public void PlaySound(int idx){
        source.PlayOneShot(_clips[idx]);
    }
}
