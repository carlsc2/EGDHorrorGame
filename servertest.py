# Send UDP broadcast packets

import sys, time
import socket
from datetime import datetime
##s = socket(AF_INET, SOCK_DGRAM)
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM, socket.IPPROTO_UDP) # UDP


sock.bind(('', 0))
sock.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)

MESSAGE = "TEST"
UDP_IP = '<broadcast>'
UDP_PORT = 50000

for i in range(10):
#while 1:
    data = repr(time.time()) + '\n'
    ##s.sendto(data, ('<broadcast>', MYPORT))

    sock.sendto(bytes(str(datetime.now())+": TEST", "utf-8"), (UDP_IP, UDP_PORT))
    
    time.sleep(.1)
