using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateWalletCheckInPutField : MonoBehaviour
{

    public TMP_InputField _Name; //�������� ��������
    public TMP_InputField _Balance; // ���� ��� ������
    public Button Button; // ������ �������� ��������
    public GameObject CreateWalletButton; //������ �������� �������� ��� gameobject
    public GameObject _NameGameObject; //���� name ��� gameobject
    public GameObject _BalanceGameObject; // ���� Balance ��� gameobject
    private bool IfNameCheckPassed = false;
    private bool IfBalanceCheckPassed = false;
    public Sprite InActiveButton;
    public Sprite IsActiveButton;
    public Sprite InputField;
    public Sprite InputFieldWrong;
    public TMP_Text NameErrorText;

    public void NameCheck()
    {
        IfNameCheckPassed = true;
        if (_Name.text.Length < 5 || _Name.text.Length > 10)
        {
            IfNameCheckPassed = false;
            _NameGameObject.GetComponent<Image>().sprite = InputFieldWrong;
            NameErrorText.text = "�������� ������ ����� ����� �� 5 �� 10 �������.";
        }
        else
        {
            _NameGameObject.GetComponent<Image>().sprite = InputField;
        }
        SetActiveButton();
    }

    public void BalanceCheck()
    {
        IfBalanceCheckPassed = true;
        if (_Balance.text.Length < 1 || _Balance.text.Length > 1000000)
        {
            IfBalanceCheckPassed = false;
            _BalanceGameObject.GetComponent<Image>().sprite = InputFieldWrong;
        }
        else
        {
            _BalanceGameObject.GetComponent<Image>().sprite = InputField;
        }
        SetActiveButton();
    }

    public void SetActiveButton()
    {
        if (IfNameCheckPassed == true && IfBalanceCheckPassed == true )
        {
            Button.interactable = true;
            CreateWalletButton.GetComponent<Image>().sprite = IsActiveButton;
        }
        else
        {
            Button.interactable = false;
            CreateWalletButton.GetComponent<Image>().sprite = InActiveButton;
        }
    }
}
