using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class FileLoader : MonoBehaviour
{
 [SerializeField]
 private GameObject panelFileViewer; //파일 정보를 출력하는 Panel, 활성 비활성화에 사용됨

[SerializeField]   
private TextMeshProUGUI textFileName; //파일 이름
[SerializeField]   
private TextMeshProUGUI textFileSize; //파일 크기
[SerializeField]   
private TextMeshProUGUI textFileCreationTime; //파일 생성 시간
[SerializeField]   
private TextMeshProUGUI  textLastWirteTime; //파일 최종 수정 시간
[SerializeField]   
private TextMeshProUGUI textDirectory; //파일 경로
[SerializeField]   
private TextMeshProUGUI textFullName; //전체 경로 (디렉토리, 파일이름)

private FileInfo fileInfo; //전체 파일을 나타내는 FileInfo

public void OnLoad(FileInfo file)
{
    //파일 정보를 출력하는 Panel 활성화
    panelFileViewer.SetActive(true);

    fileInfo = file;

    //파일의 정보를 Text UI에 출력
    textFileName.text = $"파일 이름 : {fileInfo.Name}";
    textFileSize.text = $"파일 크기 : {fileInfo.Length} Bytes";
    textFileCreationTime.text = $"파일 생성 시간 : {fileInfo.CreationTime}";
    textDirectory.text = $"파일 경로 : {fileInfo.Directory}";
    textFullName.text = $"전체 경로 : {fileInfo.FullName}";
}

    public void OpenFile()
    {
        //파일 열기
        Application.OpenURL("file:///"+fileInfo.FullName);

    }

    public void OffLoad()
    {
    panelFileViewer.SetActive(false);
    }

}
