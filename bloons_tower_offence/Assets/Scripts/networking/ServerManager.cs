using JetBrains.Annotations;
using Mirror;
using Mirror.Websocket;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ServerManager : MonoBehaviour {
	public string ip = "178.62.91.41";
	public int port = 7755;

	public NetworkManager manager;
	public TelepathyTransport transport;

	public List<gameInfo> currentGames;


	bool shouldConnect = false;
	public ushort connectToPort = 8000;

	bool startServer = false;







	#region private members 	
	private TcpClient socketConnection;
	private Thread clientReceiveThread;
	#endregion

	// Start is called before the first frame update
	void Start() {
		
			Debug.Log("Conencting this device to master server");
			ConnectToTcpServer();
		

	}

	public void ConnectToTcpServer() {
		try {
			clientReceiveThread = new Thread(new ThreadStart(ListenForData));
			clientReceiveThread.IsBackground = true;
			clientReceiveThread.Start();
				
			} catch (Exception e) {
			Debug.Log("On client connect exception " + e);
		}
	}

	private void ListenForData() {
		try {

			socketConnection = new TcpClient(ip, 7755);
			//SendMessage();

			Byte[] bytes = new Byte[1024];
			while (true) {
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream()) {
					int length;
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) {
						var incommingData = new byte[length];
						Array.Copy(bytes, 0, incommingData, 0, length);
						// Convert byte array to string message. 						
						string serverMessage = Encoding.ASCII.GetString(incommingData);
						Debug.Log("server message received as: " + serverMessage);
						handlePacket(serverMessage);
						/*
						manager.networkAddress = "localhost";
						connectToPort = ushort.Parse(serverMessage);
						transport.port = connectToPort;
						shouldConnect = true;
						*/





					}
				}
			}
		} catch (SocketException socketException) {
			Debug.Log("Socket exception: " + socketException);
		}
	}

	private new void SendMessage(string message) {
		if (socketConnection == null) {
			return;
		}
		try {
			// Get a stream object for writing. 			
			NetworkStream stream = socketConnection.GetStream();
			if (stream.CanWrite) {
				string clientMessage = message;
				// Convert string message to byte array.                 
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
				// Write byte array to socketConnection stream.                 
				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
				Debug.Log("Client sent his message - should be received by server");
			}
		} catch (SocketException socketException) {
			Debug.Log("Socket exception: " + socketException);
		}
	}

	public void getGameList() {
		string message = JsonUtility.ToJson(
			new codePacket(69)
		 );
		//sm.ConnectToTcpServer();
		Debug.Log("getting games list");
		SendMessage(message);
	}

	private void Update() {

		if (Input.GetKeyDown(KeyCode.Return)) {
			string jsonMessage = JsonUtility.ToJson(new basicPacket(0, "Hello this is a packet from unity being sent"));



			SendMessage(jsonMessage);
		}

		if (shouldConnect) {
			connect();
			shouldConnect = false;

		}

        if (startServer) {
			if (SystemInfo.graphicsDeviceID == 0) {
				startServer = false;
				manager.StartServer();
            } else {
				startServer = false;

            }
				

        }
	}

	public void host() {
		string message = JsonUtility.ToJson(
			new codePacket(300)
		 );
		//sm.ConnectToTcpServer();
		Debug.Log("hosting");
		SendMessage(message);
	}

	public void connectGameServerToMaster() {
		string message = JsonUtility.ToJson(
			new gameInfo(
				UnityEngine.Random.value.ToString(),
				manager.networkAddress,
				transport.port,
				500
				)
		 );
		//sm.ConnectToTcpServer();
		Debug.Log("Sending game to server");
		SendMessage(message);
	}

	public  void connect() {
		transport.port = connectToPort;
		manager.StartClient();

	}


	

	private void handlePacket(string message) {
		
		codePacket data = (codePacket)JsonUtility.FromJson(message, typeof(codePacket));
		
		 
			if (data.code == 200) {

				startServer = true;



			}
		
		

		if(data.code == 1000) {
			gameList games = JsonUtility.FromJson<gameList>(message);
			Debug.Log(JsonUtility.ToJson(games));

			int j;
			currentGames = games.games;
			/*
			for( j = 0; j < games.games.Count; j++) {
				Debug.Log(games.games[j].game_name);
            }
			*/

		}

        if (data.code == 301) {
			gameInfo _data = (gameInfo)JsonUtility.FromJson(message, typeof(gameInfo));
			connectToPort = _data.game_port;
			shouldConnect = true;
        }
		



	}

    private void OnApplicationQuit() {
		socketConnection.Close();
    }

    public class basicPacket {
		public int code;
		public string message;

		public basicPacket(int code, string message) {
			this.code = code;
			this.message = message;

		}

	}

	public class codePacket {
		public int code;
		public List<int> lst;


		public codePacket(int code) {
			this.code = code;
		}
	}

	[System.Serializable]
	public class gameInfo {
		public string game_name;
		public string game_ip;
		public ushort game_port;
		public int code;

		public gameInfo(string _name, string _ip, ushort _port,int _code = 0) {
			this.game_name = _name;
			this.game_ip = _ip;
			this.game_port = _port;
			this.code = _code;

		}
	}

	[System.Serializable]
	public class gameList {
		public int code;
		public List<gameInfo> games;

    }





}

