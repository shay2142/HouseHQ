import sqlite3
from sqlite3 import Error

def create_connection():
    """ create a database connection to the SQLite database
        specified by db_file
    :param db_file: database file
    :return: Connection object or None
    """
    conn = None
    try:
        conn = sqlite3.connect('HHQservice.db')
        return conn
    except Error as e:
        print(e)

    return conn

def create_tables(conn):

    create_users_table = """
    CREATE TABLE IF NOT EXISTS USERS(
        usersID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
        USERNAME TEXT NOT NULL,
        PASSWORD TEXT NOT NULL,
        EMAIL TEXT NOT NULL,
        LEVEL_KEY TEXT,
        prodact_keyID INTEGER);
    """

    create_prodact_key_table = """
    CREATE TABLE IF NOT EXISTS PRODACT_KEY(
        prodact_keyID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
        prodact_key TEXT NOT NULL,
        production_date TEXT NOT NULL,
        expiry_date TEXT NOT NULL,
        server_limit INTEGER NOT NULL);
    """

    create_server_table = """
    CREATE TABLE IF NOT EXISTS SERVER(
        serverID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
        ip TEXT NOT NULL,
        port INTEGER NOT NULL,
        dominServer TEXT NOT NULL,
        version TEXT NOT NULL,
        STATUS TEXT);
    """

    create_servers_table = """
    CREATE TABLE IF NOT EXISTS SERVERS(
        serversID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
        usersID INTEGER,
        serverID INTEGER,
        FOREIGN KEY(usersID) REFERENCES USERS(usersID), FOREIGN KEY(serverID) REFERENCES SERVER(serverID));
    """

    # create tables
    if conn is not None:
        create_table(conn, create_users_table)

        create_table(conn, create_prodact_key_table)

        create_table(conn, create_server_table)

        create_table(conn, create_servers_table)
    else:
        print("Error! cannot create the database connection.")


def create_table(conn, create_table_sql):
    """ 
    create a table from the create_table_sql statement
    :param conn: Connection object
    :param create_table_sql: a CREATE TABLE statement
    :return:
    """
    try:
        c = conn.cursor()
        c.execute(create_table_sql)
    except Error as e:
        print(e)

def insertValueToServer(conn, values):
    """
    insert value to SERVER
    :param conn:
    :param project:
    :return: project id
    """
    if not domainIsExists(conn, values[2]) and not ipAndPortNotExists(conn, values[0], values[1]):

        sql = ''' INSERT INTO SERVER(ip, port, dominServer, version, STATUS)
                VALUES(?,?,?,?,?) '''
        cur = conn.cursor()
        cur.execute(sql, values)
        conn.commit()
        return cur.lastrowid
    else:
        print("the domain already exists")
        return ""

def insertValueToUsers(conn, values):
    """
    insert value to Users
    :param conn:
    :param project:
    :return: project id
    """
    if not userNameIsExists(conn, values[0]):

        sql = ''' INSERT INTO USERS(USERNAME, PASSWORD, EMAIL, LEVEL_KEY, prodact_keyID)
                VALUES(?,?,?,?,?) '''
        cur = conn.cursor()
        cur.execute(sql, values)
        conn.commit()
        return cur.lastrowid
    else:
        print("the user name already exists")

def ipAndPortNotExists(conn, ip, port):
    cur = conn.cursor()
    cur.execute("SELECT dominServer FROM SERVER WHERE USERNAME=?", (ip,))

    rows = cur.fetchall()

    for row in rows:
        if row[0] == ip and row[1] == port:
            return True
    return False
    
def domainIsExists(conn, domain):
    """
    Query users by domain
    :param conn: the Connection object
    :param domain:
    :return:
    """
    cur = conn.cursor()
    cur.execute("SELECT dominServer FROM SERVER WHERE USERNAME=?", (domain,))

    rows = cur.fetchall()

    for row in rows:
        if row[0] == domain:
            return True
    return False

def userNameIsExists(conn, userName):
    """
    Query users by userName
    :param conn: the Connection object
    :param userName:
    :return:
    """
    cur = conn.cursor()
    cur.execute("SELECT USERNAME FROM USERS WHERE USERNAME=?", (userName,))

    rows = cur.fetchall()

    for row in rows:
        if row[0] == userName:
            return True
    return False

def deleteTable(conn, tableName):
    """
    Delete all rows in the table
    :param conn: Connection to the SQLite database
    :return:
    """
    sql = 'DELETE FROM ' + tableName
    cur = conn.cursor()
    cur.execute(sql)
    conn.commit()

def passwordIsCorrect(conn, userName, password):
    cur = conn.cursor()
    cur.execute("SELECT PASSWORD FROM USERS WHERE USERNAME=?", (userName,))

    rows = cur.fetchall()

    for row in rows:
        if row[0] == password:
            return True
    return False

def getUserInformation(conn, userName):
    cur = conn.cursor()
    cur.execute("SELECT * FROM USERS WHERE USERNAME=?", (userName,))

    rows = cur.fetchall()

    for row in rows:
        print(row)
        return row
    return -1

def updateUserInformation(conn, values):
    sql = ''' UPDATE USERS
              SET PASSWORD = ? ,
                  EMAIL = ? ,
                  LEVEL_KEY = ? ,
                  prodact_keyID = ?,
              WHERE usersID = ?''' #! prodact key ID
    cur = conn.cursor()
    cur.execute(sql, values)
    conn.commit()

def updateStatusServer(conn, values):
    sql = ''' UPDATE SERVER
              SET STATUS = ?,
              WHERE serverID = ?''' #! prodact key ID
    cur = conn.cursor()
    cur.execute(sql, values)
    conn.commit()

def updateServer(conn, values):
    sql = ''' UPDATE SERVER
              SET ip = ? ,
                  port = ? ,
                  dominServer = ? ,
                  version = ?,
                  STATUS = ?,
              WHERE serverID = ?''' #! prodact key ID
    cur = conn.cursor()
    cur.execute(sql, values)
    conn.commit()

def getServerInformation(conn, domain):
    cur = conn.cursor()
    cur.execute("SELECT * FROM SERVER WHERE dominServer=?", (domain,))

    rows = cur.fetchall()

    for row in rows:
        print(row)
        return row
    return -1

def closeDB(conn):
    conn.close()