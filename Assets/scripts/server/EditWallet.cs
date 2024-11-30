using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

public class EditWallet : MonoBehaviour
{
    public TMP_InputField Name;
    public TMP_InputField Balance;
    public TMP_Dropdown Currency;
    public int wallet_id;
    public string URL = "http://195.2.79.241:5000/api_app/wallet_edit";
    public GameObject OBJWithReloadSceneScript;
    public GameObject CreateWalletCanvas;
    public GameObject MainCanvas;
    public TMP_Text ErrorText;

    [System.Serializable]
    public class WalletEditClass
    {
        public int id_wallet;
        public string name;
        public string balance;
        public string currency;
    }
    [System.Serializable]
    public class ServerResponseAddWallet
    {
        public string existance;
        public string edition;
    }

    public void SaveWalletEdit()
    {
        WalletEditClass WalletEditClassOBJ = new WalletEditClass();
        WalletEditClassOBJ.name = Name.text;
        WalletEditClassOBJ.id_wallet = wallet_id;
        WalletEditClassOBJ.balance = Balance.text;
        WalletEditClassOBJ.currency = Currency.captionText.text;
        string WalletEditClassString = JsonUtility.ToJson(WalletEditClassOBJ);
        byte[] WalletEditDataRaw = Encoding.UTF8.GetBytes(WalletEditClassString);
        StartCoroutine(EditWalletCOR(WalletEditDataRaw));
    }

    IEnumerator EditWalletCOR(byte[] WalletEditDataRaw)
    {
        UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(URL, "POST");
        webRequest.SetRequestHeader("Content-Type", "application/json");

        webRequest.uploadHandler = new UploadHandlerRaw(WalletEditDataRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        yield return webRequest.SendWebRequest();
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("������: " + webRequest.error);
        }
        else
        {
            ServerResponseAddWallet response = JsonUtility.FromJson<ServerResponseAddWallet>(webRequest.downloadHandler.text);
            Debug.Log(webRequest.downloadHandler.text);
            Debug.Log(response.existance + " " + response.edition + "");
            if (response.existance == "True" && response.edition == "True")
            {
                MainCanvas.SetActive(true);
                ErrorText.text = "";
                CreateWalletCanvas.SetActive(false);
            }
            else
            {
                ErrorText.text = "���� ������� ������������ �������� ��������. �������� �������� ������ ���� ����������";
            }
        }
    }
}