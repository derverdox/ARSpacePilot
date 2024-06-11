import cv2
from cvzone.HandTrackingModule import HandDetector
import socket

#Parameters
width, height = 1280, 720

device_index = 0  # 0 is your standard cam, for external webcam change to 1
cap = cv2.VideoCapture(device_index)
cap.set(3, width)
cap.set(4, height)

# Hand Detector
detector = HandDetector(maxHands=1, detectionCon=0.85)

# UDP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM) # DGRAM for udp, STREAM for tcp
serverAddressPort = ("127.0.0.1", 5052) # specify a unoccupied port

while True:
    # Get webcam images
    success, img = cap.read()
    # Detect Hands
    hands, img = detector.findHands(img)

    data = []
    # We need to send 21 landmarks with each 3 components: Landmark values (x, y, z) * 21
    if hands:
        # Get the first hand detected
        hand = hands[0]
        # Get the landmark list
        lmList = hand['lmList']
        print(lmList)

        for lm in lmList:
            data.extend([lm[0], height - lm[1], lm[2]])
        print(data)
        # send data via udp
        sock.sendto(str.encode(str(data)), serverAddressPort)

    cv2.imshow("Image", img)
    cv2.waitKey(1)
