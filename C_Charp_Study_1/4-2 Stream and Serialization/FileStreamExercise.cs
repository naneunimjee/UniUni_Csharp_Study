using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // FileStream
using System; //BitConverter

public class FileStreamExercise : MonoBehaviour
{
    private void Awake()
    {
        int WriteData = 22;
        //파일 생성 모드로 새로운 파일 스트림 생성
        Stream writeStream = new FileStream("FileStream.dat",FileMode.Create);
        //바이트로 변환
        byte[] writeBytes = BitConverter.GetBytes(WriteData);
        //변환한 바이트를 파일 스트림을 통해 파일에 기록한다.
        writeStream.Write(writeBytes, 0, writeBytes.Length);
        //파일 스트림을 닫는다.
        writeStream.Close();

        //파일로부터 데이터 읽기, 스트림 내의 바이트를 저장할 공간부터 만들어야한다.
        byte[] ReadBytes = new byte[8];
        //파일 열기 모드로 파일 생성
        Stream ReadStream = new FileStream("FileStream.dat",FileMode.Open);
        //bytes 길이만큼 데이터를 읽어 readStream에 저장한다.
        ReadStream.Read(ReadBytes, 0, ReadBytes.Length);
        //BitConverter를 통해 bytes에 담겨 있는 값을 int 형식으로 변환한다.
        int ReadData = BitConverter.ToInt32(ReadBytes, 0);
        //읽기 스트림을 닫는다.
        ReadStream.Close();

        //결과 출력
        Debug.Log($"파일에 저장되어 있던 데이터 : {ReadData}");
    }
}
