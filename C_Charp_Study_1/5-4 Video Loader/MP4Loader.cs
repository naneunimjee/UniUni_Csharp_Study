using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class MP4Loader : MonoBehaviour
{
    [Header("MP4 Player Panel, File Name")]
    [SerializeField]
    private GameObject panelMP4Player; //영상 재생 제어를 위한 Panel
    [SerializeField]
    private TextMeshProUGUI textFileName; // 파일 이름

    [Header("MP4 Play Time (Slider, Text)")]
    [SerializeField]
    private Slider sliderPlaybar; //재생시간을 나타내는 Slider
    [SerializeField]
    private TextMeshProUGUI textCurrentPlaytime; //현재 재생시간
    [SerializeField]
    private TextMeshProUGUI textMaxPlaytime; // 총 재생시간

    [Header("Play Video & Audio")]
    [SerializeField]
    private RawImage rawImageDrawVideo; //영상 출력을 위한 RawImage
    [SerializeField]
    private VideoPlayer videoPlayer; // 영상 재생용 viedoPlayer
    [SerializeField]
    private AudioSource audioSource; // 사운드 재생용 AudioSource
    
    public void OnLoad(System.IO.FileInfo file)
    {
        // Panel 활성화
        panelMP4Player.SetAction("true");

        //MP4 파일 이름 출력
        textFileName.text = file.Name;

        //재생시간 표시에 사용되는 Slider, Text 초기화
        ResetPlaytimeUI();

        //Mp4 파일을 불러와서 재생
        StartCoroutine(LoadVideo(file.FullName));
    }


    private IEnumerator LoadVideo(string fullPath)
    {
        videoPlayer.url = fullPath; //url에 동영상 파일의 경로 저장

        
        /*
        //동영상 소리 재생 모드 : AudioSource
        videoPlayer.auidoOutputMode = VideoAudioOutputMpde.AudioSource;

        //동영상 소리를 재생할 AudioSource 설정
        //오디오트랙 디코딩 활성/비활성화 (VideoPlayer가 재생중이 아닐 때 설정)
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //videoPlayer에 등록된 영상의 사운드 재생으로 사용하기 때문에,
        //audioSource.clip은 비워둔다.
        audioSource.clip = null;
         이 주석은, inspector 창에서 구현을 했기 때문에 굳이 안 적어도 된다.*/

        //동영상에 출력되는 이미지를 imageDrawTexture에 설정
        //설정이 안 되어 있으면 코드로 설정한다.
        rawImageDrawVideo.texture = videoPlayer.targetTexture;

        //clip 정보를 동적으로 변경할 때는 Prepare() 호출 후 Prepare가 완료 되어야 재생 가능
        videoPlayer.Prepare();

        while ( !videoPlayer.isPrepared)
        {
            yield return null;
        }

        //MP4 동영상/사운드 재생
        Play();
    }

    public void OffLoad()
    {
        Stop();

        //Panel 비활성화
        panelMP4Player.SetActive(false);

    }

    public void Play()
    {
        //동영상, 사운드 재생
        videoPlayer.Play();
        audioSource.Play();

        //Slider, Text에 재생시간 정보를 업데이트
        StartCoroutine("OnPlaytimeUI");
    }

    public void Pause()
    {

        //동영상, 사운드 재생 일시 정지
        videoPlayer.Pause();
        audioSource.Pause();
    }

    public void Stop()
    {

        //동영상/사운드 재생 중지
        videoPlayer.Stop();
        audioSource.Stop();

        //Slider, text에 재생시간 정보 업데이트 중지
        StopCoroutine("OnPlaytimeUI");
        //재생시간 표시에 사용되는 Slider, Text 초기화
        ResetPlaytimeUI();
    }

    private void ResetPlaytimeUI()
    {
        sliderPlaybar.value = 0;
        textCurrentPlaytime.text = "00:00:00";
        textMaxPlaytime.text = "00:00:00";
    }

    private IEnumerator OnPlaytimeUI()
    {
        int hour = 0;
        int minutes = 0;
        int seconds = 0;
        while (true)
        {
            //현재 재생시간 표시 (Text UI)
            hour = (int)videoPlayer.time/3600;
            minutes = (int)(videoPlayer.time%3600)/60;
            seconds = (int)(videoPlayer.time%3600)%60;
            textCurrentPlaytime.text = $"{hour:D2}:{minutes:D2}:{seconds:D2}";

            //총 재생시간 표시 (Text UI)
            hour = (int)videoPlayer.length/3600;
            minutes = (int)(videoPlayer.length%3600)/60;
            seconds = (int)(videoPlayer.length%3600)%60;
            textMaxPlaytime.text = $"{hour:D2}:{minutes:D2}:{seconds:D2}";

            //Slider에 표시되는 재생 시간 설정
            sliderPlaybar.value = (float)(videoPlayer.time / videoPlayer.length);

            yield return new WaitForSeconds(1);

        }
    }
}
