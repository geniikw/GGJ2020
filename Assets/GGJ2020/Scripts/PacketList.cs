using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//코드
public enum PK{

    SelectStart = 1,
    MoveSelecter = 2,//int x, int y
    RotateSelecter = 3,
    ConfirmPosition = 4,
    StartSetItem = 5,
    CompleteItem = 6,//정보 잘가는지 확인
    Play = 7,
    Next = 8,
    GameOver = 9, //확인누르면 로비로
}