using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class FileExercise : MonoBehaviour
{
    private void Awake(){
    //새로운 파일을 생성한다. (경로와 파일명을 매개변수로 입력)
    FileStream file = File.Create("a.dat");

    //파일을 생성하면 자동으로 파일이 열리게 된다.
    //파일을 닫아주지(Close)않으면,
    //Copy(), Move(), Delete()와 같은 외부에서의 제어가 불가능하다.

    file.Close();

    //파일 복사 붙여넣기
    File.Copy("a.dat", "b.dat");

    //파일 잘라내기 후 붙여넣기
    File.Move("a.dat", "c.dat");

    //파일의 존재여부 확인
    bool isExist = File.Exists("c.dat");
    Debug.Log($"c.dat 파일 존재 여부 : {isExist}");

    //매개변수에 입력된 파일의 속성 확인
    FileAttributes attr = File.GetAttributes("b.dat");
    Debug.Log($"c.dat 파일 속성 : {attr}");

    //파일을 삭제한다.
    File.Delete("b.dat");
    File.Delete("c.dat");
    }
}

