import serial
import socket

# serial connection settings for reading Nucleo data:
ser = serial.Serial('/dev/ttyACM0', 9600, timeout=2)

# TCP connection settings for sending sensor data to PC:
TCP_IP = ''
TCP_PORT = 5000
BUFFER_SIZE = 1024

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((TCP_IP, TCP_PORT))
s.listen()
print("Waiting for connection...\n")
conn, addr = s.accept()
print('Connection address: ', addr)

while 1:
	try:
		data = ser.readline() # read serial data
		conn.send(data)
		print(data)
	except BrokenPipeError:
		print("Broken pipe error! Attempting to re-establish...")
		s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
		s.bind((TCP_IP, TCP_PORT))
		s.listen()
		print("Waiting for connection...\n")
		conn, addr = s.accept()
		print('Connection address: ', addr)
conn.close()