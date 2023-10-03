using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject panelIamgeViewer; //이미지 정보를 출력하는 panel

    [SerializeField]
    private Image imageDrawTexture; //파일이 나타내는 이미지 출력
    [SerializeField]
    private TextMeshProUGUI textFileData; //파일 이름, 해상도, 용량

    private float maxWidth = 800; //Image UI의 최대 크기
    private float maxHeight = 600;

    public void OnLoad(FileInfo file)
    {
        //이미지 정보 출력하는 패널 활성화
        panelIamgeViewer.SetActive(true);

        //파일의 경로를 Bytes 데이터로 전환한다.
        byte[] byteTexture = File.ReadAllBytes(file.FullName);

        //byteTexture에 있는 byte 배열 정보를 바탕으로 Texture2D 이미지 파일 데이터 생성
        Texture2D texture2D = new Texture2D(0, 0);
        if ( byteTexture.Length > 0)
        {
            texture2D.LoadImage(byteTexture);
        }
        /// 이미지 출력하는 Image UI의 크기 설정
        /// 원본 텍스처의 width 크기가 image UI의 최대 width 크기보다 크면
        if (texture2D.width > maxWidth){
            imageDrawTexture.rectTransform.sizeDelta = new Vector2(maxWidth, maxWidth/texture2D.width*texture2D.height);
        }
        else if (texture2D.height > maxHeight){
            imageDrawTexture.rectTransform.sizeDelta = new Vector2(maxHeight/texture2D.height*texture2D.width, maxHeight);
        }
        else{
            imageDrawTexture.rectTransform.sizeDelta = new Vector2(texture2D.width, texture2D.height);
        }

        //Texture2D -> sprite 변환
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        
        //imageDrawTexture image UI에 보여지는 이미지를 sprite로 설정
        imageDrawTexture.sprite = sprite;

        //이미지 파일 정보 출력
        textFileData.text = $"{file.Name} ({texture2D.width} x {texture2D.height}, {file.Length}Bytes)";
        
    }

    public void OffLoad()
    {
        //이미지 정보를 출력하는 panelImageViewer를 비활성화
        panelIamgeViewer.SetActive(false);
    }

}
