using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileInfoExercise01 : MonoBehaviour
{
    private void Awake() {
    //인스턴스 변수로 선언, 매개변수(경로, 파일명)을 문자열로 작성한다.
    FileInfo fileInfo = new FileInfo("a1.dat");

    //파일을 생성한다.
    FileStream file = fileInfo.Create();
        
    //파일을 외부에서 다루기 위해 파일을 닫는다.
    file.Close();

    //파일을 복사한다.
    fileInfo.CopyTo("b1.dat");

    //파일을 잘라내기 후 붙여넣는다.
    fileInfo.MoveTo("c1.dat");

    //파일의 존재여부를 확인한다.
    bool isExists = fileInfo.Exists;
    Debug.Log($"{fileInfo.Name} 파일 존재 여부 : {isExists}");

    //파일의 속성을 확인한다.
    FileAttributes attr = fileInfo.Attributes;
    Debug.Log($"{fileInfo.Name}의 파일 속성 : {attr}");

    //파일 삭제
    fileInfo.Delete();
    }
}
