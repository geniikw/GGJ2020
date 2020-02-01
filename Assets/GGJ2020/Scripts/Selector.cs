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

    public Button playButton;

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
        if (current.x == x && current.y == y)
            return;
        
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

    public void Rotate()
    {
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

    public void OnGameStartClick(){
        GameManager.StartSetPhoneStep(m_playerList);
    }


    public  void OnEvent(EventData photonEvent){
        if(photonEvent.Code == 1){
            //game start
            Debug.Log("game start");

            waitMenu.SetActive(false);
            selectMenu.SetActive(true);
            currentSelectActor = m_playerList[0].ActorNumber;
        }
    }
}
