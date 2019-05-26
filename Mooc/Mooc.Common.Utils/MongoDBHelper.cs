using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;

namespace Mooc.Common.Utils
{
    public class MongoDBHelper
    {
        private static MongoServer server=null;
        private static string dbConnectionString =MemcachedHelper.GetConfigValue("MongoDBServer");       
        private static MongoServer ServerInstance()
        {
            if (server == null)
            {
                server = MongoServer.Create(dbConnectionString);
            }
            return server;
        }

        public static bool ExistsFile(string dbName, string filename) {
            if (string.IsNullOrEmpty(filename))
                return false;
            MongoDatabase db = MongoDBHelper.ServerInstance().GetDatabase(dbName);
            return db.GridFS.Exists(filename.ToUpper());
        }

        private static MongoServer server2 = null;
        public static string CheckDB(string dbConnectionString, string dbName)
        {
            if (server2 == null)
            {
                server2 = MongoServer.Create(dbConnectionString);
            }
            MongoDatabase db = server2.GetDatabase(dbName);
            return db.GridFS.DatabaseName;
        }

        public static MemoryStream GetFileByName(string dbName, string filename)
        {
            //MongoGridFSStream stream = null;
            MemoryStream ms = new MemoryStream();
            //byte[]file=null;
            try
            {
                MongoDatabase db = MongoDBHelper.ServerInstance().GetDatabase(dbName);
                //MongoGridFSFileInfo fileinfo = db.GridFS.FindOne(filename.ToUpper());
                //stream = fileinfo.Open(System.IO.FileMode.Open);
                //file = new byte[stream.Length];
                //stream.Read(file, 0, (int)stream.Length);
                db.GridFS.Download(ms, filename.ToUpper());
            }
            catch { }
            finally {
                //if (null != stream)
                //    stream.Close();
            }
            return ms;
        }

        public static void SetFileByName(string dbName,string filepath,string filename)
        {
            if (!File.Exists(filepath))
                return;
            MongoDatabase db = MongoDBHelper.ServerInstance().GetDatabase(dbName);
            if (db.GridFS.Exists(filename.ToUpper()))
                db.GridFS.Delete(filename.ToUpper());
            MongoGridFSFileInfo fileInfo = db.GridFS.Upload(filepath, filename.ToUpper());
        }

        private static MongoCredential GetCredential(bool isWriteRight)
        {
            MongoCredential credential = null;
            if(isWriteRight)
            {
                credential = MongoCredential.CreateGssapiCredential("writeuser", "password");
            }
            else
            {
                credential = MongoCredential.CreateGssapiCredential("readuser", "password");
            }
            return credential;
        }

        public static MongoCollection GetCollection(string dbName, string collectionName)
        {
            MongoDatabase db = MongoDBHelper.ServerInstance().GetDatabase(dbName);
            MongoCollection collection = db.GetCollection(collectionName);
            return collection;
        }

        public static bool Insert<T>(string dbName, string collectionName, T model)
        {
            bool isInsert = false;
            if (model == null)
                return isInsert;
            MongoCollection collection = GetCollection(dbName, collectionName);
            WriteConcernResult result = collection.Insert<T>(model);
            isInsert = result.Ok;
            return isInsert;
        }

        public static bool Update(string dbName, string collectionName, MongoDB.Driver.IMongoQuery query, MongoDB.Driver.Builders.UpdateBuilder update)
        {
            MongoCollection collection = GetCollection(dbName, collectionName);
            WriteConcernResult result = collection.Update(query, update);
            return result.Ok;
        }
    }
}