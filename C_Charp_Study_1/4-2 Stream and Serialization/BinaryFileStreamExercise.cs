using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class BinaryFileStreamExercise : MonoBehaviour
{
    private void Awake()
    {
        //BinaryWirter 클래스 인스턴스를 생성할 때, 매개변수로 stream 클래스를
        //기반으로 하는 파생 클래스 인스턴스를 받는다.
        Stream writeStream = new FileStream("BinaryFileStream.dat", FileMode.Create);
        BinaryWriter binaryWriter = new BinaryWriter(writeStream);

        //BinaryWriter 클래스의 Write 메소드로 파일에 작성
        binaryWriter.Write("안녕하세요");
        binaryWriter.Write(22);
        binaryWriter.Write(1.23f);

        //파일 닫기
        binaryWriter.Close();

        //BinaryReader 클래스 인스턴스도 마찬가지로, 매개변수로 stream 클래스의
        //파생 클래스 인스턴스를 받는다.
        Stream readStream = new FileStream("BinaryFileStream.dat", FileMode.Open);
        BinaryReader binaryReader = new BinaryReader(readStream);

        //BinaryFileStream.dat 파일의 크기를 출력
        Debug.Log($"File Size : {readStream.Length} Bytes");

        //BinaryReader의 ReadXX() 메소드는 읽을 데이터 형식별로 메소드를 제공한다.
        Debug.Log(binaryReader.ReadString());
        Debug.Log(binaryReader.ReadInt32());
        Debug.Log(binaryReader.ReadSingle());


        //읽기 스트림 닫기
        binaryReader.Close();
    }
}
