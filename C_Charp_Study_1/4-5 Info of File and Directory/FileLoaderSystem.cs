using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class FileLoaderSystem : MonoBehaviour
{
    //확장자별 파일 처리 (Load or Play)
    private FileLoader fileLoader;
    private void Awake()
    {
        fileLoader = GetComponent<FileLoader>();
    }

    public void LoadFile(FileInfo file)
    {
        OffAllPanel(); //파일 정보가 출력되는 모든 패널을 우선 비활성화
        
        //선택한 파일이 문서 파일일 경우 문서 프로그램을 실행
        if (file.FullName.Contains("pdf") || file.FullName.Contains("xlsx") || file.FullName.Contains("doc") ||
        file.FullName.Contains(".pptx") || file.FullName.Contains(".hwp") || file.FullName.Contains(".text"))
        {
            fileLoader.OnLoad(file);
        }

        //나머지 모든 확장자는 문서와 동일하게 파일 정보 출력
        else{
            fileLoader.OnLoad(file);
        }
    }

    private void OffAllPanel()
    {
        fileLoader.OffLoad();
    }
}