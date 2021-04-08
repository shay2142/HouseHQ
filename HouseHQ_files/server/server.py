from socket import socket
from threading import Thread
from zlib import compress
import mss.tools
from mss import mss
from PIL import Image


WIDTH = 1920
HEIGHT = 1080
SERVER_HOST = "0.0.0.0"
SERVER_PORT = 5001

def retreive_screenshot(conn):
    with mss() as sct:
        # The region to capture
        rect = {'top': 0, 'left': 0, 'width': WIDTH, 'height': HEIGHT}
        num = 0
        while 'recording':
            num += 1
            # Capture the screen
            img = sct.grab(rect)
            #img1 = img
            #img1 = Image.frombytes("RGB", img.size, img.bgra, "raw", "BGRX")
            #output = "monitor-{}.png".format(num)
            #img1.save(output)

            #mss.tools.to_png(img.rgb, img.size, 'screenshot.png')
            #print(img)
            # Tweak the compression level here (0-9)
            pixels = compress(img.rgb, 6)

            # Send the size of the pixels length
            size = len(pixels)
            size_len = (size.bit_length() + 7) // 8
            conn.send(bytes([size_len]))

            # Send the actual pixels length
            size_bytes = size.to_bytes(size_len, 'big')
            conn.send(size_bytes)

            # Send pixels
            conn.sendall(pixels)


def main():
    port = 5000
    ip = "0.0.0.0"
    s = socket()
    s.bind((SERVER_HOST, SERVER_PORT))

    try:
        s.listen(5)
        print('Server started.')

        while 'connected':
            conn, addr = s.accept()
            print('Client connected IP:', addr)
            print(conn)
            thread = Thread(target=retreive_screenshot, args=(conn,))
            thread.start()
    finally:
        s.close()


if __name__ == '__main__':
    main()