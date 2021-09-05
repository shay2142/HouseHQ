from flask import Flask, request
import requests
import urllib
import urllib.request
import json

import db

app = Flask(__name__)

CONN = object
servers = []
prodact_key = "W269N-WFGWX-YVC9B-4J6C9-T83GX"  #for now...


@app.route('/', methods=['GET', 'POST'])
def updateServer():
    if request.method == 'POST':
        print(request.data)
        x = request.data.decode('utf-8')
        server = json.loads(x)

        if server["prodact_key"] != prodact_key:
                return "prodact key in incorrect"

        for ser in servers:
            if ser["ip"] == server["ip"] and ser["port"] == server["port"]:
                return ""
            elif ser["dominServer"] == server["dominServer"]:
                return "This domain is already occupied"

        servers.append(server)
        return json.dumps(servers)

    return "hello_world"


@app.route('/servers')
def getServers():
    return json.dumps(servers)


if __name__ == '__main__':
    conn = db.create_connection()
    db.create_tables(conn)
    db.insertValueToUsers(conn, ('shay1', '12345', 'shay@gmail.com', 'admin', prodact_key))
    db.getUserInformation(conn, "shay1")
    db.closeDB(conn)
    #app.run(host='0.0.0.0', port=8080)
