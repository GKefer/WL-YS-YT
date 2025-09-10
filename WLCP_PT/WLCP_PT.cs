using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using WLCP_Library;
using WLCP_PT.ExtensionMethod;
using WLCP_PT.MES;
using WLCP_PT.MES.Message;
using WLCP_PT.PLC;
using WLCP_PT.PLC.EquipEnum;
using WLCP_PT.PLC.EquipEnum.EquipList;
using WLCP_PT.PLC.Model;
using WLCP_PT.RMS;
using WLCP_PT.RTM;
using static System.Net.Mime.MediaTypeNames;

namespace WLCP_PT
{
    public class WLCP_PT
    {
        #region 宣告公用參數
        //1.1 宣告WLCP公用參數
        private HandlePipe handlePipe;
        private HandleRabbitMQ handleRabbitMQ_MES, handleRabbitMQ_MES2, handleRabbitMQ_RTM, handleRabbitMQ_RMS;
        private HandleText handleText;
        private readonly string GV_SERVICE_NAME = Assembly.GetExecutingAssembly().GetName().Name;
        #endregion

        //設備清單
        private Dictionary<string, PLCEquip> plcEquips;
        //對各系統邏輯元件
        private PLCAccess plcAccess;
        private RTMAccess rtmAccess;
        private MESAccess mesAccess;
        private RMSAccess rmsAccess;
        //Thread => tPLC：PLC讀取, tRTM：RTM上報, tMES：MES上報
        //int = > plcFreq：PLC讀取頻率, rtmFreq：RTM上報頻率, mesFreq：MES上報頻率, 單位：秒
        private Thread tPLC;
        private int plcFreq = 1;
        //PT L2
        private Thread tMES, tMES2, tMES3;
        private Thread tRTM, tRTM2;
        private Thread tRMS;
        private int mesFreq = 30, mesFreq2 = 1, mesFreq3 = 1200;
        private int rmsFreq = 1;
        private int rtmFreq = 7;
        //能源 L2
        private Thread tMES4, tMES5;
        private int mesFreq4 = 300;

        public void StartService()
        {
            //1. 設定初始化
            Init();
        }

        private void Init()
        {
            while (true)
            {
                try
                {
                    Log("程式初始化...");

                    //2.1 WLCP元件初始化
                    handleText = new HandleText();
                    handlePipe = new HandlePipe(GV_SERVICE_NAME);
                    handleRabbitMQ_MES = new HandleRabbitMQ(handleText.GetMQSettings(@"APServerSetup_MES.ini"));
                    handleRabbitMQ_MES2 = new HandleRabbitMQ(handleText.GetMQSettings(@"APServerSetup_MES2.ini"));
                    handleRabbitMQ_RTM = new HandleRabbitMQ(handleText.GetMQSettings(@"APServerSetup_RTM.ini"), "2");
                    handleRabbitMQ_RMS = new HandleRabbitMQ(handleText.GetMQSettings(@"APServerSetup_RMS.ini"));

                    //存取元件初始化
                    GenerateData();

                    //PLC
                    plcAccess = new PLCAccess(plcEquips);
                    tPLC = new Thread(PLCReadValues);
                    tPLC.Start();
                    Thread.Sleep(5000);

                    //MES
                    mesAccess = new MESAccess(plcEquips);
                    tMES = new Thread(MESDataPublish_L2902);
                    tMES2 = new Thread(MESDataPublish_L2602);
                    tMES3 = new Thread(MESDataPublish_L2604);
                    tMES4 = new Thread(MESDataPublish_L2901_1);
                    tMES5 = new Thread(MESDataPublish_L2901_2);
                    tMES.Start();
                    tMES2.Start();
                    tMES3.Start();
                    tMES4.Start();
                    tMES5.Start();

                    //RTM
                    rtmAccess = new RTMAccess(plcEquips);
                    tRTM = new Thread(RTMDataPublish_L2902);
                    tRTM2 = new Thread(RTMDataPublish_L2901);
                    tRTM.Start();
                    tRTM2.Start();

                    //RMS
                    rmsAccess = new RMSAccess(plcEquips);
                    tRMS = new Thread(RMSDataPublish);
                    tRMS.Start();

                    Log("程式初始化成功");
                    break;
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException e)
                {
                    Log("程式初始化失敗，因MQ連線異常", e);
                }
                catch (Exception e)
                {
                    Log("程式初始化失敗，因不明異常", e);
                }
                Thread.Sleep(5000);
            }
        }

        //初始化站別/設備/參數清單
        private void GenerateData()
        {
            plcEquips = new Dictionary<string, PLCEquip>();

            //依照EQUIP_CODE產生設備清單
            foreach (string name in Enum.GetNames(typeof(EquipList)))
            {
                PLCEquip plcEquip = new PLCEquip();
                plcEquip.EQUIP_CODE = name;
                plcEquip.SHOP_CODE = ((int)Enum.Parse(typeof(EquipList), name)).ToString();
                plcEquips.Add(name, plcEquip);
            }

            //將該設備PARAMETER的名稱與位址加入
            foreach (KeyValuePair<string, PLCEquip> plcEquip in plcEquips)
            {
                //Engineering Data
                Type type = Type.GetType("WLCP_PT.PLC.EquipEnum.ParmList." + plcEquip.Key);
                if (type != null)
                {
                    foreach (string name in Enum.GetNames(type))
                    {
                        PLCParam plcParam = new PLCParam();
                        plcParam.PARAMETER_NAME = name;
                        plcParam.PARAMETER_ADDRESS = name.GetStringValue(type)[0];
                        plcParam.Source = name.GetEnumValue(type);
                        plcParam.Type = (int)Enum.Parse(type, name);
                        plcParam.FileName_MES = name.GetStringValue(type)[1];
                        plcEquip.Value.PARAM_LIST.Add(plcParam);
                    }
                }

                //EQUIP_STATUS
                type = Type.GetType("WLCP_PT.PLC.EquipEnum.StatusList." + plcEquip.Key);
                if (type != null)
                {
                    foreach (string name in Enum.GetNames(type))
                    {
                        PLCStatus plcStatus = new PLCStatus();
                        plcStatus.PARAMETER_ADDRESS = name.GetStringValue(type)[0];
                        plcStatus.Source = name.GetEnumValue(type);
                        plcStatus.Type = (int)Enum.Parse(type, name);
                        plcEquip.Value.EQUIP_STATUS = plcStatus;
                    }
                }
            }
        }

        //PLC資料讀取
        private void PLCReadValues()
        {
            plcAccess.MQTTReadValuesAsync();

            while (true)
            {
                try
                {
                    plcAccess.ReadValues();
                }
                catch (Exception e)
                {
                    Log("PLC資料讀取失敗，因不明異常", e);
                }

                Thread.Sleep(100 * plcFreq);
            }
        }

        //MES
        private void MESDataPublish_L2902()
        {
            //以固定頻率上報L2902
            while (true)
            {
                try
                {
                    //依據MES需求每秒產生一個L2902並在30秒後統一發送
                    List<L2902> l2902_List = new List<L2902>();
                    for (int i = 0; i < mesFreq; i++)
                    {
                        l2902_List.Add(mesAccess.GenerateData_L2902());
                        Thread.Sleep(1000);
                    }

                    //統一訊息時間
                    string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    foreach (L2902 l2902 in l2902_List)
                    {
                        l2902.MESSAGE_TIME = now;
                    }

                    handleRabbitMQ_MES.Publish(JsonConvert.SerializeObject(l2902_List));
                    Log("MES L2902事件上報成功");
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException e)
                {
                    Log("MES L2902事件上報失敗，因MQ連線異常", e);
                }
                catch (Exception e)
                {
                    Log("MES L2902事件上報失敗，因不明異常", e);
                }
            }
        }

        private void MESDataPublish_L2602()
        {
            //以固定頻率上報L2602
            while (true)
            {
                try
                {
                    string json = mesAccess.GenerateData_L2602();
                    if (json != null)
                    {
                        handleRabbitMQ_MES.Publish(json);
                        Log("MES L2602事件上報成功");
                    }
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException e)
                {
                    Log("MES L2602事件上報失敗，因MQ連線異常", e);
                }
                catch (Exception e)
                {
                    Log("MES L2602事件上報失敗，因不明異常", e);
                }

                Thread.Sleep(1000 * mesFreq2);
            }
        }

        private void MESDataPublish_L2604()
        {
            //以固定頻率上報L2604
            while (true)
            {
                try
                {
                    string json = mesAccess.GenerateData_L2604();
                    if (json != null)
                    {
                        handleRabbitMQ_MES.Publish(json);
                        Log("MES L2604事件上報成功");
                    }
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException e)
                {
                    Log("MES L2604事件上報失敗，因MQ連線異常", e);
                }
                catch (Exception e)
                {
                    Log("MES L2604事件上報失敗，因不明異常", e);
                }

                Thread.Sleep(1000 * mesFreq3);
            }
        }

        //Public
        private void MESDataPublish_L2901_1()
        {
            //以固定頻率上報L2901_01
            while (true)
            {
                try
                {
                    string json = mesAccess.GenerateData_L2901_01();
                    if (json != null)
                    {
                        Console.WriteLine(json);
                        handleRabbitMQ_MES2.Publish(json);
                        Log("MES L2901_01事件上報成功");
                    }
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException e)
                {
                    Log("MES L2901_01事件上報失敗，因MQ連線異常", e);
                }
                catch (Exception e)
                {
                    Log("MES L2901_01事件上報失敗，因不明異常", e);
                }

                Thread.Sleep(1000 * mesFreq4);
            }
        }

        private void MESDataPublish_L2901_2()
        {
            //以固定頻率上報L2901_02
            while (true)
            {
                try
                {
                    string json = mesAccess.GenerateData_L2901_02();
                    if (json != null)
                    {
                        Console.WriteLine(json);
                        handleRabbitMQ_MES2.Publish(json);
                        Log("MES L2901_02事件上報成功");
                    }
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException e)
                {
                    Log("MES L2901_02事件上報失敗，因MQ連線異常", e);
                }
                catch (Exception e)
                {
                    Log("MES L2901_02事件上報失敗，因不明異常", e);
                }

                Thread.Sleep(1000 * mesFreq4);
            }
        }

        //RTM
        private void RTMDataPublish_L2902()
        {
            //程式啟動時上報L2B01
            while (true)
            {
                try
                {
                    handleRabbitMQ_RTM.Publish(rtmAccess.GenerateData_L2B01());
                    Log("RTM L2B01事件上報成功");
                    break;
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException e)
                {
                    Log("RTM L2B01事件上報失敗，因MQ連線異常", e);
                }
                catch (Exception e)
                {
                    Log("RTM L2B01事件上報失敗，因不明異常", e);
                }

                Thread.Sleep(1000 * rtmFreq);
            }

            //以固定頻率上報L2902
            while (true)
            {
                try
                {
                    string json = rtmAccess.GenerateData_L2902();
                    if (json != null)
                    {
                        //Console.WriteLine(json);
                        handleRabbitMQ_RTM.Publish(json);
                        Log("RTM L2902事件上報成功");
                    }
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException e)
                {
                    Log("RTM L2902事件上報失敗，因MQ連線異常", e);
                }
                catch (Exception e)
                {
                    Log("RTM L2902事件上報失敗，因不明異常", e);
                }

                Thread.Sleep(100 * rtmFreq);
            }
        }

        private void RTMDataPublish_L2901()
        {
            while (true)
            {
                try
                {
                    string json = rtmAccess.GenerateData_L2901();
                    if (json != null)
                    {
                        handleRabbitMQ_RTM.Publish(json);
                        Log("RTM L2901事件上報成功");
                    }
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException e)
                {
                    Log("RTM L2901事件上報失敗，因MQ連線異常", e);
                }
                catch (Exception e)
                {
                    Log("RTM L2901事件上報失敗，因不明異常", e);
                }

                Thread.Sleep(100 * rtmFreq);
            }
        }

        //RMS
        private void RMSDataPublish()
        {
            //程式啟動時上報L2C62
            while (true)
            {
                try
                {
                    handleRabbitMQ_RMS.Publish(rmsAccess.GenerateData_L2C62());
                    Log("RMS L2C62事件上報成功");
                    break;
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException e)
                {
                    Log("RMS L2C62事件上報失敗，因MQ連線異常", e);
                }
                catch (Exception e)
                {
                    Log("RMS L2C62事件上報失敗，因不明異常", e);
                }

                Thread.Sleep(1000 * rmsFreq);
            }
        }

        //打印Log
        private void Log(string log)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff：") + log);
        }

        //打印Error Log
        private void Log(string log, Exception e)
        {
            string logE = log + "\n" + e.Message;
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff：") + logE);
            handleText.WriteDebugLog(GV_SERVICE_NAME, logE);
        }
    }
}