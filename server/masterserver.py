import socket
import os
from  multiprocessing import Process
import threading
import json
import time





games = []


HOST = "localhost"
PORT = 7755
global currentPort
currentPort =  8000

def sendToConnect(socket,port):
    data = json.dumps({"code": 301,"game_port": port})
    socket.send(data.encode());


def startGameServer(port,socket):
    os.system(f"start cmd /k bloons_tower_offence.exe {port}")
    global currentPort
    currentPort =  8000
    print("\n sleeping")
    time.sleep(2)
    print("\n awake")
    sendToConnect(socket,port)


def sendgames(socket):
    data = json.dumps({"code": 1000,"games" : games})
    socket.send(data.encode());






def dataHandle(socket,data):
    print(f"got {data} from {socket}")

    #data = json.dumps({"lst": [1] })
    socket.send(data)

    data = json.loads(data)
    if(data["code"] == 500):
        games.append(data)
        print(str(games))
        sendgames(socket)
    elif(data["code"] == 69):
        sendgames(socket)
    elif(data["code"] == 300 ): #host game
        print("hosting new game...")
        startGameServer(currentPort,socket)

def thread_listen_data(socket):
    while True:
        try:
            data = socket.recv(1024)
            if data:
                dataHandle(socket,data)
            else:
                print(f"{socket} disconnected")
                socket.close();
                return;

                
        except Exception as e:
            print(e)

def sendconfirm(socket):
    data = {"codee": 0}
    data = json.dumps({"code": 200})
    socket.send(data.encode())



#START APPLICATION

s = socket.socket(socket.AF_INET,socket.SOCK_STREAM) 
s.bind((HOST,PORT))

if(s.listen(5) == None):
    print("Server Started")
print("started listening on" + HOST)




while True:
        conn,addr = s.accept() # HALTS
        print(f"connection from {addr}")
        sendconfirm(conn)
        listenThread = threading.Thread(target=thread_listen_data,args=(conn,))
        listenThread.start()