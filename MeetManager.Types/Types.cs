﻿using System.Data;

namespace MeetManager.Types
{

    public class Parm
    {
        public string? Name { get; set; }
        public SqlDbType DataType { get; set; }
        public object? Value { get; set; }
        public int Size { get; set; }
        public ParameterDirection Direction { get; set; }

        public Parm(string? name, SqlDbType dataType, object? value, int size = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            Name = name;
            DataType = dataType;
            Value = value;
            Size = size;
            Direction = direction;
        }

       
    }

    public enum MeetType
    {
        Local = 1,
        Provincial = 2,
        National = 3,
        International = 4,
    }
}