using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Begin_Menu: MonoBehaviour
{
    private AudioSource click;
    [SerializeField]
    private GameObject _playButton;
    [SerializeField]
    private GameObject _quitButton;
    [SerializeField]
    private GameObject _optionsButton; 
    [SerializeField]
    private GameObject _slider;
    [SerializeField]
    private GameObject _backButton;
    [SerializeField]
    private GameObject _volumeText;    
    [SerializeField]
    private AudioSource _backMusic;
    private void Start()
    {
        click = gameObject.GetComponent<AudioSource>();
        _backMusic = GameObject.Find("Singleton_Skill_Controller").GetComponent<AudioSource>();

        _slider.GetComponent<Slider>().value = Singleton_Skills_Manager.use.AudioVolume;
        _backMusic.volume = Singleton_Skills_Manager.use.AudioVolume;
    }
    public void LoatTo(int level)
    {
       // Debug.Log("aga");
        if (!click.isPlaying)
            click.Play();
        SceneManager.LoadScene(level);
    }

    public void AppQuick()
    {
        if (!click.isPlaying)
        {
            click.Play();
        }
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Options()
    {
        _slider.SetActive(true);
        _volumeText.SetActive(true);
        _backButton.SetActive(true);

        _optionsButton.SetActive(false);
        _playButton.SetActive(false);
        _quitButton.SetActive(false);
    }

    public void BackToMenu()
    {
        _slider.SetActive(false);
        _volumeText.SetActive(false);
        _backButton.SetActive(false);

        _optionsButton.SetActive(true);
        _playButton.SetActive(true);
        _quitButton.SetActive(true);
    }

    public void SetVolume(float volume)
    {

        if (_backMusic != null)
        {
            _backMusic.volume = volume;
            Singleton_Skills_Manager.use.AudioVolume = volume;
            PlayerPrefs.SetFloat(Singleton_Skills_Manager.use.Str_AudioVolume, volume);
        }
    }
}
