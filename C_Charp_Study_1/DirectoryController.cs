using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //DirectoryInfo, FileInfo class
using System; //Environment class


public class DirectoryController : MonoBehaviour
{

    private DirectoryInfo currentDirectory;

    private void Awake()
    {
        //프로그램이 최상단에 활성화 상태가 아니어도 플레이
        Application.runInBackground = true;

        //최초 경로를 바탕화면으로 설정한다.
        //Environment.GetFolderPath() : 윈도우에 존재하는 폴더 경로를 얻어오는 메소드
        //Environment.SpecialFolder.[이름] : 윈도우에 존재하는 특수 폴더 열거형 (descktop은 바탕화면 경로_)
        string desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        currentDirectory = new DirectoryInfo(desktopFolder); //메모리 할당, 디렉토리 위치를 바탕화면으로

        //현재 폴더에 존재하는 디렉토리, 파일 생성
        UpdateDirectory(currentDirectory);
        //바탕화면에 존재하는 모든 정보 출력
    }

    /// <summary>
    /// 현재 폴더 정보 업데이트 함수
    /// </summary>
    
    private void UpdateDirectory(DirectoryInfo dir)
    {
        //현재 경로 설정
        currentDirectory = dir;
        
        //현재 폴더 이름 출력
        Debug.Log($"현재 폴더명 : {currentDirectory.Name}");

        //현재 폴더에 존재하는 모든 폴더 이름 출력
        foreach ( DirectoryInfo d in currentDirectory.GetDirectories())
        {
            Debug.Log(d.Name);
        }

        //현재 폴더에 존재하는 모든 파일 이름 출력
        foreach ( FileInfo f in currentDirectory.GetFiles()){
            Debug.Log(f.Name);
        }
    }
}
