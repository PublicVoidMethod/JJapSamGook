using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothManager : MonoBehaviour
{
    enum States
    {
        None,
        Scan,
        Connect,
        Subscribe,
        Unsubscribe,
        Disconnect,
        Communication,
    }

    public static BluetoothManager instance;

    States currentState = States.None;

    public string deviceName = "OrgBoard";
    public string bltServiceUUID = "6e400001-b5a3-f393-e0a9-e50e24dcca9e";
    public string bltSendUUID = "6e400002-b5a3-f393-e0a9-e50e24dcca9e";
    public string bltReceiveUUID = "6e400003-b5a3-f393-e0a9-e50e24dcca9e";
    string bltAdress = "";    

    void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        BluetoothLEHardwareInterface.Initialize(true, false, () => {
            StartCoroutine(ChangeState(States.Scan, 0.1f));
            print("블루투스 초기화 성공");
        }, (error) => {
            print("블루투스 초기화 실패");
        });
    }

    IEnumerator ChangeState(States state, float delayTime)
    {
        currentState = state;

        yield return new WaitForSeconds(delayTime);

        switch(currentState)
        {
            case States.Scan:
                StartScan();
                break;
            case States.Connect:
                StartConnect();
                break;
            case States.Subscribe:
                StartSubscribe();
                break;            
            default:
                break;
        }
    }

    void StartScan()
    {
        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) => {

            if (name.Contains(deviceName))
            {
                print("블루투스 찾기 성공");
                bltAdress = address;
                BluetoothLEHardwareInterface.StopScan();
                StartCoroutine(ChangeState(States.Connect, 0.5f));
            }

        }, null, false, false);
    }

    void StartConnect()
    {
        BluetoothLEHardwareInterface.ConnectToPeripheral(bltAdress, null, null, (address, serviceUUID, sendUUID) => {

            if (IsEqual(serviceUUID, bltServiceUUID))
            {
                if (IsEqual(sendUUID, bltSendUUID))
                {
                    print("블루투스 연결 성공");
                    StartCoroutine(ChangeState(States.Subscribe, 2));
                }
            }
        }, (disconnectedAddress) => {
            print("블루투스 연결 끊김");    
        });
    }

    void StartSubscribe()
    {
        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(bltAdress, bltServiceUUID, bltReceiveUUID, null, (address, receiveUUID, bytes) => {
            print("블루투스로 데이터 수신 성공");
            print("수신 : " + Encoding.UTF8.GetString(bytes) + ", " + receiveUUID);            
        });
    }

    public void SendData(string value)
    {
        var data = Encoding.UTF8.GetBytes(value);
   
        BluetoothLEHardwareInterface.WriteCharacteristic(bltAdress, bltServiceUUID, bltSendUUID, data, data.Length, false, (sendUUID) => {
            
        });
    }

    void DisConnect()
    {
        if (currentState == States.Subscribe)
        {
            BluetoothLEHardwareInterface.DisconnectPeripheral(bltAdress, (address) => {
                BluetoothLEHardwareInterface.DeInitialize(() => {

                    StartCoroutine(ChangeState(States.None, 0));
                });
            });
        }
        else
        {
            BluetoothLEHardwareInterface.DeInitialize(() => {

                StartCoroutine(ChangeState(States.None, 0));
            });
        }
    }

    string FullUUID(string uuid)
    {
        return "0000" + uuid + "-0000-1000-8000-00805F9B34FB";
    }

    bool IsEqual(string uuid1, string uuid2)
    {
        if (uuid1.Length == 4)
            uuid1 = FullUUID(uuid1);
        if (uuid2.Length == 4)
            uuid2 = FullUUID(uuid2);

        return (uuid1.ToUpper().Equals(uuid2.ToUpper()));
    }

    private void OnDestroy()
    {
        DisConnect();
        print("종료");
    }
}
