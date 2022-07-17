using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeper : MonoBehaviour
{
    public AudioClip unlockDoorSd;

    private AudioSource _aud;

    //----- ������� ������ � ��������� �������
    private const int LockedR = 95;
    private const int LockedUr = 81;
    private const int LockedUl = 80;
    private const int LockedL = 100;
    private const int LockedDL = 101;
    private const int LockedDr = 102;

    //----- ������� ������ � ��������� �������
    private const int OpenR = 48;
    private const int OpenUr = 93;
    private const int OpenUl = 92;
    private const int OpenL = 51;
    private const int OpenDL = 26;
    private const int OpenDr = 27;


    private IKeyMaster keys;

    private void Awake()
    {
        _aud = GetComponent<AudioSource>();
        keys = GetComponent<IKeyMaster>();
    }

    private void OnCollisionStay(Collision coll)
    {
        // ���� ������ ���, ����� �� ����������
        if (keys.keyCount < 1) return;

        // ������� ������������ ������ ������
        var ti = coll.gameObject.GetComponent<Tile>();
        if (ti == null) return;

        // ���������, ������ ���� ���� ������� ����� � ����� (������������� �������� ������������� �����)
        var facing = keys.GetFacing();
        // ���������, �������� �� ������ �������� ������
        Tile ti2;
        switch (ti.tileNum)
        {
            case LockedR:
                if (facing != 0) return;
                ti.SetTile(ti.x, ti.y, OpenR);
                break;
            case LockedUr:
                if (facing != 1) return;
                ti.SetTile(ti.x, ti.y, OpenUr);
                ti2 = TileCamera.TILES[ti.x - 1, ti.y];
                ti2.SetTile(ti2.x, ti2.y, OpenUl);
                break;
            case LockedUl:
                if (facing != 1) return;
                ti.SetTile(ti.x, ti.y, OpenUl);
                ti2 = TileCamera.TILES[ti.x + 1, ti.y];
                ti2.SetTile(ti2.x, ti2.y, OpenUr);
                break;
            case LockedL:
                if (facing != 2) return;
                ti.SetTile(ti.x, ti.y, OpenL);
                break;
            case LockedDL:
                if (facing != 3) return;
                ti.SetTile(ti.x, ti.y, OpenDL);
                ti2 = TileCamera.TILES[ti.x + 1, ti.y];
                ti2.SetTile(ti2.x, ti2.y, OpenDr);
                break;
            case LockedDr:
                if (facing != 3) return;
                ti.SetTile(ti.x, ti.y, OpenDr);
                ti2 = TileCamera.TILES[ti.x - 1, ti.y];
                ti2.SetTile(ti2.x, ti2.y, OpenDL);
                break;
            default:
                return; // �����, ����� ��������� ���������� �������� ������
        }
        _aud.PlayOneShot(unlockDoorSd);
        keys.keyCount--;
    }

}
