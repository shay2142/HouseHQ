from socket import socket
from zlib import decompress

import pygame

WIDTH = 1920
HEIGHT = 1080
host = "192.168.0.128"
port = 5001


def recvall(conn, length):
    """ Retreive all pixels. """

    buf = b''
    while len(buf) < length:
        data = conn.recv(length - len(buf))
        if not data:
            return data
        buf += data
    return buf


def main():
    #pygame.init()
    #screen = pygame.display.set_mode((WIDTH, HEIGHT))
    #clock = pygame.time.Clock()
    #watching = True
    #watching = False

    sock = socket()
    sock.connect((host, port))
    print("[+] Connected.")
    pygame.init()
    #screen = pygame.display.set_mode((WIDTH, HEIGHT))
    #screen = pygame.display.set_mode((0, 0), pygame.FULLSCREEN)
    screen = pygame.display.set_mode((WIDTH, HEIGHT), pygame.RESIZABLE)
    clock = pygame.time.Clock()
    watching = True
    try:
       
        while watching:
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    watching = False
                    break

            # Retreive the size of the pixels length, the pixels length and pixels
            size_len = int.from_bytes(sock.recv(1), byteorder='big')
            size = int.from_bytes(sock.recv(size_len), byteorder='big')
            pixels = decompress(recvall(sock, size))

            # Create the Surface from raw pixels
            img = pygame.image.fromstring(pixels, (WIDTH, HEIGHT), 'RGB')

            # Display the picture
            screen.blit(img, (0, 0))
            pygame.display.flip()
            clock.tick(60)
    finally:
        sock.close()


if __name__ == '__main__':
    main()