using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class Selector : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public static Selector i;

    public List<Player> m_playerList = new List<Player>();

    public GameObject selectMenu;
    public GameObject waitMenu;



    public Vector2Int current;

    public Text userCount;
    public Text roomNumber;
    public Text guide;

    public Button playButton;

    public Button gameStartButton;

    public Cell[][] grid;

    public int currentRotation;

    public bool isVertical = true;

    public int currentSelectActor = -1;



    public override void OnEnable()
    {
        base.OnEnable();
        i = this;
        PhotonNetwork.AddCallbackTarget(this);

    }

    public override void OnDisable()
    {
        base.OnEnable();
        PhotonNetwork.RemoveCallbackTarget(this);

    }

    public void Start()
    {
        roomNumber.text = PhotonNetwork.CurrentRoom.Name;
        UpdateUser();
        playButton.gameObject.SetActive(m_playerList[0].NickName.Equals(PhotonNetwork.NickName));
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateUser();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) => UpdateUser();

    public void UpdateUser()
    {
        m_playerList.Clear();
        m_playerList.AddRange(PhotonNetwork.CurrentRoom.Players.Values);
        m_playerList = m_playerList.OrderBy(user => user.ActorNumber).ToList();

        userCount.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        playButton.interactable = PhotonNetwork.CurrentRoom.PlayerCount > 1;
    }

    public void SetXY(int x, int y)
    {
        if (MonoPlayer.i.actorNumber != currentSelectActor)
            return;

        if (current.x == x && current.y == y)
            return;

        GameManager.SendEvent(PK.MoveSelecter, new object[] { x, y });
        MovePoint(x, y);
    }

    public void MovePoint(int x, int y)
    {
        current = new Vector2Int(x, y);

        var phoneSize = GameData.ins.phoneSize;

        if (isVertical ? ValidationVertical(current, phoneSize) : ValidationHorizontal(current, phoneSize))
        {
            if (isVertical)
                ColoringVertical(current, phoneSize);
            else
                ColoringHorizontal(current, phoneSize);
        }
    }

    public bool ValidationHorizontal(Vector2Int point, Vector2Int size)
    {

        for (int y = point.y; y > point.y - size.x; y--)
        {
            for (int x = point.x; x < point.x + size.y; x++)
            {
                if (y < 0 || x < 0)
                {
                    return false;
                }
                if (y >= grid.Length || x >= grid[0].Length)
                {
                    return false;
                }

                if (grid[y][x].isConfirm)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public bool ValidationVertical(Vector2Int point, Vector2Int size)
    {
        for (int y = point.y; y < point.y + size.y; y++)
        {
            for (int x = point.x; x < point.x + size.x; x++)
            {
                if (y < 0 || x < 0)
                {
                    return false;
                }
                if (y >= grid.Length || x >= grid[0].Length)
                {
                    return false;
                }

                if (grid[y][x].isConfirm)
                {

                    return false;
                }
            }
        }
        return true;
    }


    public void ColoringVertical(Vector2Int point, Vector2Int size)
    {
        bool isFirst = true;
        for (int y = 0; y < grid.Length; y++)
        {
            for (int x = 0; x < grid[0].Length; x++)
            {
                if (grid[y][x].state == 1 || grid[y][x].isConfirm)
                    continue;

                if (point.y <= y && y < point.y + size.y && point.x <= x && x < point.x + size.x)
                {
                    grid[y][x].SetState(isFirst ? 3 : 2);
                    isFirst = false;
                }
                else
                    grid[y][x].SetState(0);
            }
        }
    }

    public void ColoringHorizontal(Vector2Int point, Vector2Int size)
    {
        for (int y = 0; y < grid.Length; y++)
        {
            for (int x = 0; x < grid[0].Length; x++)
            {
                if (grid[y][x].state == 1 || grid[y][x].isConfirm)
                    continue;

                if (point.y >= y && y > point.y - size.x && point.x <= x && x < point.x + size.y)
                {
                    grid[y][x].SetState(2);
                    if (y == point.y && x == point.x)
                        grid[y][x].SetState(3);
                }
                else
                    grid[y][x].SetState(0);
            }
        }
    }

    void ConfirmPosition()
    {
        var point = current;
        var size = GameData.ins.phoneSize;

        if (isVertical)
        {
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[0].Length; x++)
                {
                    if (grid[y][x].state == 1 || grid[y][x].isConfirm)
                        continue;

                    if (point.y <= y && y < point.y + size.y && point.x <= x && x < point.x + size.x)
                    {
                        grid[y][x].isConfirm = true;
                    }
                }
            }

        }
        else
        {
            for (int y = 0; y < grid.Length; y++)
            {
                for (int x = 0; x < grid[0].Length; x++)
                {
                    if (grid[y][x].state == 1 || grid[y][x].isConfirm)
                        continue;

                    if (point.y >= y && y > point.y - size.x && point.x <= x && x < point.x + size.y)
                    {
                        grid[y][x].isConfirm = true;
                    }

                }
            }
        }
    }

    public void OnConfirm()
    {
        GameManager.SendEvent(PK.ConfirmPosition);
    }


    public void Rotate()
    {
        if (MonoPlayer.i.actorNumber == currentSelectActor)
            GameManager.SendEvent(PK.RotateSelecter, null);
        isVertical = !isVertical;
        if (!isVertical)
            current = new Vector2Int(0, 2);
        else
            current = Vector2Int.zero;

        var phoneSize = GameData.ins.phoneSize;

        if (isVertical)
            ColoringVertical(current, phoneSize);
        else
            ColoringHorizontal(current, phoneSize);
    }

    public void OnGameStartClick()
    {
        GameManager.SendEvent(PK.SelectStart, null);

    }

    public void OnStartSetItem()
    {
        GameManager.SendEvent(PK.StartSetItem);
    }


    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 1)
        {
            //game start
            GameManager.InitPlayerData(m_playerList);
            StartUserPosition();
        }
        else if (photonEvent.Code == 2)
        {
            var xy = (object[])photonEvent.CustomData;
            var x = (int)xy[0];
            var y = (int)xy[1];
            Debug.Log("Receive MoveEvent " + x + " " + y);
            if (MonoPlayer.i.actorNumber != currentSelectActor)
            {
                MovePoint(x, y);
            }
        }
        else if (photonEvent.Code == 3)
        {
            if (MonoPlayer.i.actorNumber != currentSelectActor)
            {
                Rotate();
            }
        }

        else if (photonEvent.Code == 4)
        {
            ConfirmPosition();

            var selector = m_playerList.First(p => p.ActorNumber == currentSelectActor);
            var idx = m_playerList.IndexOf(selector);
            var me = m_playerList.First(p => p.NickName == PhotonNetwork.NickName);
            var myIdx = m_playerList.IndexOf(me);


            if (idx == m_playerList.Count - 1)
            {
                if (myIdx == 0)
                    gameStartButton.gameObject.SetActive(true);
                selectMenu.SetActive(false);
                waitMenu.SetActive(false);
            }
            else
            {
                idx++;
                selectMenu.SetActive(myIdx == idx);
                guide.text = (idx + 1) + "번 유저가 선택중입니다.";
                currentSelectActor = m_playerList[idx].ActorNumber;
            }
        }
        else if (photonEvent.Code == 5)
        {
            GameManager.StartItemSetStep();
        }
    }


    public void StartUserPosition()
    {
        var me = m_playerList.First(p => p.NickName == PhotonNetwork.NickName);
        var myIdx = m_playerList.IndexOf(me);

        waitMenu.SetActive(false);
        selectMenu.SetActive(myIdx == 1);
        gameStartButton.gameObject.SetActive(false);
        currentSelectActor = m_playerList[1].ActorNumber;
        guide.text = "2번 유저가 선택중입니다.";

        FindObjectOfType<MonoPlayer>().Setup(myIdx == 0, me.ActorNumber, myIdx);
    }
}
