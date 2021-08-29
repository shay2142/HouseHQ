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
    """ create a table from the create_table_sql statement
    :param conn: Connection object
    :param create_table_sql: a CREATE TABLE statement
    :return:
    """
    try:
        c = conn.cursor()
        c.execute(create_table_sql)
    except Error as e:
        print(e)
