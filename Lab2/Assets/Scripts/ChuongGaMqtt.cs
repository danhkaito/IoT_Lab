using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json;

namespace ChuongGa
{
    public class Status_Data
    {
        public float temperature { get; set; }
        public float humidity { get; set; }
    }



    public class ControlLED_Data
    {
        public string status { get; set; }
        public string device;
        // public int device_status { get; set; }

    }
    public class ControlPUMP_Data
    {
        public string status { get; set; }
        // public int device_status { get; set; }
        public string device;


    }

    public class ChuongGaMqtt : M2MqttUnityClient
    {
        public List<string> topics = new List<string>();


        public string msg_received_from_topic_status = "";
        public string msg_received_from_topic_control = "";


        private List<string> eventMessages = new List<string>();
        [SerializeField]
        public Status_Data _status_data;
        [SerializeField]
        private ControlLED_Data _controlLED_data;
        private ControlPUMP_Data _controlPUMP_data;

        public InputField username;
        public InputField password;
        public InputField add;

        public Manager manager;

        // public void PublishPUMP()
        // {
        //     _config_data = new Config_Data();
        //     GetComponent<ChuongGaManager>().Update_Config_Value(_config_data);
        //     string msg_config = JsonConvert.SerializeObject(_config_data);
        //     client.Publish(topics[1], System.Text.Encoding.UTF8.GetBytes(msg_config), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        //     Debug.Log("publish config");
        // }

        public void PublishLED()
        {
            _controlLED_data = new ControlLED_Data();
            _controlLED_data = GetComponent<Manager>().Update_ControlLED_Value(_controlLED_data);
            string msg_config = JsonConvert.SerializeObject(_controlLED_data);
            Debug.Log(msg_config);
            try{
                client.Publish(topics[1], System.Text. Encoding.UTF8.GetBytes(msg_config), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Debug.Log("publish LED");
            }
            catch(Exception err){

            }
        }

        public void PublishPUMP()
        {
            _controlPUMP_data = new ControlPUMP_Data();
            _controlPUMP_data = GetComponent<Manager>().Update_ControlPUMP_Value(_controlPUMP_data);
            string msg_config = JsonConvert.SerializeObject(_controlPUMP_data);
            try{
                client.Publish(topics[2], System.Text. Encoding.UTF8.GetBytes(msg_config), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Debug.Log("publish PUMP");
            }
            catch(Exception err){}
        }
        public void SetEncrypted(bool isEncrypted)
        {
            this.isEncrypted = isEncrypted;
        }


        protected override void OnConnecting()
        {
            base.OnConnecting();
            //SetUiMessage("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
        }

        protected override void OnConnected()
        {
            
            base.OnConnected();

            // SubscribeTopics();
        }

        public override void Connect()
        {
            mqttUserName="bkiot";
            mqttPassword="12345678";
            brokerAddress="mqttserver.tk";
            // mqttUserName=username.text;
            // mqttPassword=password.text;
            // brokerAddress=add.text;
            base.Connect();
            manager.SwitchLayer();

            // SubscribeTopics();
        }
        protected override void SubscribeTopics()
        {
            // Debug.Log("BBBBBBBBBBBBBB");
            // Debug.Log(topics);
            foreach (string topic in topics)
            {
                // Debug.Log(new string[] { topic });
                if (topic != "")
                {
                    client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

                }
            }
        }

        protected override void UnsubscribeTopics()
        {
            foreach (string topic in topics)
            {
                if (topic != "")
                {
                    client.Unsubscribe(new string[] { topic });
                }
            }

        }

        protected override void OnConnectionFailed(string errorMessage)
        {
            Debug.Log("CONNECTION FAILED! " + errorMessage);
        }

        protected override void OnDisconnected()
        {
            Debug.Log("Disconnected.");
        }

        protected override void OnConnectionLost()
        {
            Debug.Log("CONNECTION LOST!");
        }



        protected override void Start()
        {

            base.Start();
        }

        protected override void DecodeMessage(string topic, byte[] message)
        {
            string msg = System.Text.Encoding.UTF8.GetString(message);
            Debug.Log("Received: " + msg+ "from "+topic);
            // Debug.Log("BBBBBBBBBBB");

            //StoreMessage(msg);
            if (topic == topics[0])
                ProcessMessageStatus(msg);

            if (topic == topics[1])
                ProcessMessageControlLED(msg);
            if(topic==topics[2])
                ProcessMessageControlPUMP(msg);

        }

        private void ProcessMessageStatus(string msg)
        {
             _status_data = JsonConvert.DeserializeObject<Status_Data>(msg);
            msg_received_from_topic_status = msg;
            GetComponent<Manager>().Update_Status(_status_data);

        }

        private void ProcessMessageControlLED(string msg)
        {
            ControlLED_Data _control_data=new ControlLED_Data();
            _control_data = JsonConvert.DeserializeObject<ControlLED_Data>(msg);
            msg_received_from_topic_control = msg;
            GetComponent<Manager>().Update_ControlLED(_control_data);

        }
        private void ProcessMessageControlPUMP(string msg)
        {
            ControlPUMP_Data _control_data=new ControlPUMP_Data();
            _control_data = JsonConvert.DeserializeObject<ControlPUMP_Data>(msg);
            msg_received_from_topic_control = msg;
            GetComponent<Manager>().Update_ControlPUMP(_control_data);

        }

        private void OnDestroy()
        {
            Disconnect();
        }

        private void OnValidate()
        {
            //if (autoTest)
            //{
            //    autoConnect = true;
            //}
        }

        public void UpdateConfig()
        {
           
        }

        public void UpdateControl()
        {

        }
    }
}