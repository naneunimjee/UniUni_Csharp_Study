using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextFileStreamExercise : MonoBehaviour
{   
    
    private void Awake()
    {
        //파일 생성 모드로 writeStream 생성
    Stream writeStream = new FileStream("TextFileStream.dat",FileMode.Create);
    StreamWriter streamWriter = new StreamWriter(writeStream);
    //BinaryWriter의 경우와 마찬가지로
    //매개변수로 스트림 클래스의 파생 클래스 인스턴스를 받는다.

    streamWriter.Write(22);
    streamWriter.WriteLine("naneunimjee1014");
    streamWriter.WriteLine("안뇽~");
    //WriteLine()의 경우에는 뒤에 한 줄 띄어서 입력된다.

    //사용완료후, 스트림을 닫는다.
    streamWriter.Close();

    //파일 읽기 모드로 readStream 생성
    Stream readStream = new FileStream("TextFileStream.dat",FileMode.Open);
    StreamReader streamReader = new StreamReader(readStream);

    //한 줄씩 읽어서 처리해야할 때는 while 반복문, EndOfStream 프로퍼티
    while (streamReader.EndOfStream == false)
    {
        Debug.Log(streamReader.ReadLine());
    }

    //한꺼번에 읽어서 처리해야할 때에는, ReadToEnd() 메소드 사용

    //스트림 닫기
    streamReader.Close();

    }
}
