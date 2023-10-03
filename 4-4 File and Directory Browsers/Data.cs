using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//파일의 속성(폴더, 파일) - 아이콘 출력용 (파일의 확장자에 따라 세부 구분할 경우 타입 추가)
public enum DataType { Directory = 0, File } 
public class Data : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spriteIcons; //아이콘에 적용할 수 있는 Sprite 이미지

    private Image imageIcon; //파일의 속성에 따라 아이콘 출력
    private TextMeshProUGUI textDataName; //파일의 이름 출력

    private DataType dataType; //파일의 속성
    
    private string fileName; //파일 이름
    public string FileName => fileName; //외부에서 확인하기 위한 Get 프로퍼티

    private int maxFileNameLength = 25; //파일 이름 최대 길이

    public void Setup(string fileName, DataType dataType)
    {
        //만약 Panel 오브젝트의 Image 컴포넌트를 삭제하지않으면 PanelData가 Get된다.
        imageIcon = GetComponentInChildren<Image>();
        textDataName = GetComponentInChildren<TextMeshProUGUI>();

        this.fileName = fileName;
        this.dataType = dataType;

        //아이콘 이미지 설정
        imageIcon.sprite = spriteIcons[(int)this.dataType];

        //파일 이름 출력
        textDataName.text = this.fileName;
        //파일의 이름의 최대 길이 maxFileNameLength를 넘어가면 이름의 뒷부분 잘라내고 ".."추가
        if ( fileName.Length >= maxFileNameLength)
        {
            textDataName.text = fileName.Substring(0, maxFileNameLength);
            textDataName.text += "..";
        } 
        
        //파일 이름 색상 설정 (폴더 노랑, 파일 하양)
        SetTextColor();
    }

    private void SetTextColor()
    {
        if (dataType == DataType.Directory)
        {
            textDataName.color = Color.yellow;
        }
        else
        {
            textDataName.color = Color.white;
        }
    }

}
