using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MySql.Data;
using MySql.Data.MySqlClient;

using System.IO; //Input Ouput
using System;

using LitJson;

public class UserInfo
{
    public string userName { get; private set; }
    public string userPW { get; private set; }

    public UserInfo(string name, string password)
    {
        userName = name;
        userPW = password;
    }

    public void SetName(string name)
    {
        userName = name;
    }
}

public class SQLManager : MonoBehaviour
{
    public MySqlConnection connection;
    public MySqlDataReader reader;

    public string DBPath = string.Empty;

    public UserInfo info;

    public static SQLManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DBPath = Application.dataPath + "/Database";
        string serverInfo = SetServer(DBPath);

        try
        {
            if (serverInfo.Equals(string.Empty))
            {
                Debug.Log("Sql server is not connected");
                return;
            }
            connection = new MySqlConnection(serverInfo);
            connection.Open();
            Debug.Log("Sql open completely");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    string SetServer(string path)
    {
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string jsonString = File.ReadAllText(path + "/config.json");
        JsonData itemData = JsonMapper.ToObject(jsonString);
        string serverInfo = $"Server={itemData[0]["IP"]}; Database={itemData[0]["TableName"]}; Uid={itemData[0]["ID"]}; Pwd={itemData[0]["PW"]}; Port={itemData[0]["PORT"]}; CharSet=utf8;";

        return serverInfo;
    }

    private bool CheckConnection(MySqlConnection c)
    {
        if (c.State != System.Data.ConnectionState.Open)
        {
            c.Open();
            if (c.State != System.Data.ConnectionState.Open)
            {
                return false;
            }
        }
        return true;
    }

    public bool Login(string id, string password)
    {
        try
        {
            if (!CheckConnection(connection)) Debug.Log("Login failed");

            string SQLCommand = string.Format(@"SELECT Name, Password 
            FROM member WHERE Name = '{0}' AND Password = '{1}';", id, password);

            MySqlCommand cmd = new MySqlCommand(SQLCommand, connection);
            reader = cmd.ExecuteReader();

            if (reader.HasRows) //Reader에서 읽은 데이터가 1개 이상 존재하는가?
            {
                while (reader.Read()) //읽은 데이터를 나열하는 메서드
                {
                    string name = (reader.IsDBNull(0) ? string.Empty : reader["Name"].ToString());
                    string pwd = (reader.IsDBNull(1) ? string.Empty : reader["Password"].ToString());

                    if (!name.Equals(string.Empty) || !pwd.Equals(string.Empty))
                    {
                        info = new UserInfo(name, pwd);

                        if (!reader.IsClosed) reader.Close();
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }

                if (!reader.IsClosed) reader.Close();
            }
            return false;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            if (!reader.IsClosed) reader.Close();
            return false;
        }
    }

    public bool Join(string id, string password)
    {
        try
        {
            if (!CheckConnection(connection)) return false;

            string SQLCommand = string.Format(@"INSERT member (Name, Password) Values ('{0}', '{1}');", id, password);

            MySqlCommand cmd = new MySqlCommand(SQLCommand, connection);
            reader = cmd.ExecuteReader();

            if (reader.HasRows) //Reader에서 읽은 데이터가 1개 이상 존재하는가?
            {
                while (reader.Read()) //읽은 데이터를 나열하는 메서드
                {
                    string name = (reader.IsDBNull(0) ? string.Empty : reader["Name"].ToString());
                    string pwd = (reader.IsDBNull(1) ? string.Empty : reader["Password"].ToString());

                    if (!name.Equals(string.Empty) || !pwd.Equals(string.Empty))
                    {
                        info = new UserInfo(name, pwd);

                        if (!reader.IsClosed) reader.Close();
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }

                if (!reader.IsClosed) reader.Close();
            }
            return false;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            if (!reader.IsClosed) reader.Close();
            return false;
        }
    }
}
