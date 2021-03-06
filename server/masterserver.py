import socket
import os
from  multiprocessing import Process
import threading
import json
import time
from sys import platform





global games
games = []


HOST = "localhost"
PORT = 7755
global currentPort
currentPort =  8000

def sendToConnect(socket,port):
    data = json.dumps({"code": 301,"game_port": port})
    socket.send(data.encode());


def startGameServer(port,socket,name):
    if platform == "linux" or platform == "linux2":
        os.system(f"./server.x86_64 {port} {name} &")
    else:
        os.system(f"start cmd /k bloons_tower_offence.exe {port} {name}")

    global currentPort
    currentPort += 1
    print("\n sleeping")
    time.sleep(2)
    print("\n awake")
    sendToConnect(socket,port)


def sendgames(socket):
    global games
    data = json.dumps({"code": 1000,"games" : games})
    socket.send(data.encode());






def dataHandle(socket,data):
    global games

    print(f"got {data} from {socket}")

    #data = json.dumps({"lst": [1] })
    socket.send(data)

    data = json.loads(data)
    if(data["code"] == 500):
        print("adding game")
        games.append(data)
        print(str(games))
        sendgames(socket)
    elif(data["code"] == 69):
        sendgames(socket)
    elif(data["code"] == 300 ): #host game
        print("hosting new game...")
        startGameServer(currentPort,socket,data["message"])

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
s.bind(('',PORT))

if(s.listen(5) == None):
    print("Server Started")
print("started listening on" + HOST)




while True:
        conn,addr = s.accept() # HALTS
        print(f"connection from {addr}")
        sendconfirm(conn)
        listenThread = threading.Thread(target=thread_listen_data,args=(conn,))
        listenThread.start()