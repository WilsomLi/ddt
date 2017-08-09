using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

namespace LuaFramework {
    public class NetworkManager : Manager {
        private SocketClient socket;
        static readonly object m_lockObject = new object();
        static Queue<KeyValuePair<int, ByteBuffer>> mEvents = new Queue<KeyValuePair<int, ByteBuffer>>();

		private uint m_messageIndex = 0;

        SocketClient SocketClient {
            get { 
                if (socket == null)
                    socket = new SocketClient();
                return socket;                    
            }
        }

        void Awake() {
            Init();
        }

        void Init() {
            SocketClient.OnRegister();
        }

        public void OnInit() {
            CallMethod("Start");
        }

        public void Unload() {
            CallMethod("Unload");
        }

        /// <summary>
        /// 执行Lua方法
        /// </summary>
        public object[] CallMethod(string func, params object[] args) {
            return Util.CallMethod("Network", func, args);
        }

        ///------------------------------------------------------------------------------------
        public static void AddEvent(int _event, ByteBuffer data) {
            lock (m_lockObject) {
                mEvents.Enqueue(new KeyValuePair<int, ByteBuffer>(_event, data));
            }
        }

        /// <summary>
        /// 交给Command，这里不想关心发给谁。
        /// </summary>
        void Update() {
            if (mEvents.Count > 0) {
                while (mEvents.Count > 0) {
                    KeyValuePair<int, ByteBuffer> _event = mEvents.Dequeue();
                    facade.SendMessageCommand(NotiConst.DISPATCH_MESSAGE, _event);
                }
            }
        }

        /// <summary>
        /// 发送链接请求
        /// </summary>
        public void SendConnect() {
            SocketClient.SendConnect();
        }

        /// <summary>
        /// 发送SOCKET消息
        /// </summary>
        public void SendMessage(ByteBuffer buffer) {
            SocketClient.SendMessage(buffer);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        public void OnDestroy() {
            SocketClient.OnRemove();
            Debug.Log("~NetworkManager was destroy");
        }

		public void Send(int id, string name, LuaTable tb)
		{
			if (SocketClient.State == SocketClient.ConnectState.DisConnect)
				return;
			var msg = ProtocolGroup.GetMsg(name);
			if(msg == null) {
				Debug.Log(String.Format("protocol {0} not exist",name));
				return
			}
			
			m_messageIndex ++;
			ByteBuffer protoBuff = new ByteBuffer();
			ProtocolReadWriter.Req(msg.Id,buff,tb,ProtocolGroup);
			byteBuffer buff = new ByteBuffer();
			buff.WriteUshort(msg.Id);
			buff.WriteBytes(protoBuff.ToBytes());
			protoBuff.Close();
			
			byte[] buffBytes = buff.ToBytes();
			ByteBuffer contentBuff = new ByteBuffer();
			contentBuff.WriteVchar((uint)buffBytes.Length);
			contentBuff.WriteBytes(buffBytes);
			
			ByteBuffer sendBuff = new ByteBuffer();
			sendBuff.WriteUint(Convert.ToUInt32(buffBytes.Length) + 16);
			
			//CRC
			uint crc = CRC.GetCrc32(buffBytes);
			sendBuffer.WriteUint(crc);
			
			//Time
			uint timeUint = TimeSpanUtils.ConvertDateTimeToUInt(DateTime.Now);
			ByteBuffer desBuff = new ByteBuffer();
			desBuff.WriteBytes(BitConverter.GetBytes(timeUint));
			desBuff.WriteBytes(BitConverter.GetBytes(m_messageIndex));
			
			//DES
			ulong des = BitConverter.ToUInt64(desBuff.ToBytes(),0);
			byte[] desBytes = DES.Encrypt(bitConverter.GetBytes(des));
			
			sendBuff.WriteUlong(BitConverter.ToUInt64(desBytes,0));
			sendBuff.WriteBytes(buffBytes);
			client.SendMessage(sendBuff);
			
			contentBuff.Close();
			desBuff.Close();
			buff.Close();
		}
    }
}
