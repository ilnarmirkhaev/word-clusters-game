﻿namespace Core.Serialization
{
    public interface ISerializer
    {
        public string Serialize<T>(T data);
        public T Deserialize<T>(string data);
    }
}