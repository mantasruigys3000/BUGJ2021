import socket
import os
from  multiprocessing import Process
import threading



HOST = "localhost"
PORT = 7755
currentPort =  8000


def startGameServer(port):
    os.system(f"start cmd /k .\\bloons_tower_offencet.exe {port}")



def dataHandle(socket,data):
    print(f"got {data} from {socket}")
    socket.send(data)

def thread_listen_data(socket):
    while True:
        try:
            data = socket.recv(1024)
            if data:
                dataHandle(socket,data)
        except Exception as e:
            print(e)



#START APPLICATION

s = socket.socket(socket.AF_INET,socket.SOCK_STREAM) 
s.bind((HOST,PORT))

if(s.listen(5) == None):
    print("Server Started")
print("started listening on" + HOST)



while True:
        conn,addr = s.accept() # HALTS
        print(f"connection from {addr}")
        listenThread = threading.Thread(target=thread_listen_data,args=(conn,))
        listenThread.start()