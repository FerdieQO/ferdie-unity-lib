using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace FerdieUnityLib.Helpers
{
    public static class IOHelpers
    {
        private const string localPersistancePathFileFormat = "/{0}.dat";

        public static bool PersistDataLocal<T>(T data, string id)
        {
            bool result = true;
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream file = File.Create(Application.persistentDataPath + string.Format(localPersistancePathFileFormat, id)))
                    {
                        formatter.Serialize(file, data);
                        file.Close();
                    }

                }
                catch (Exception exc)
                {
                    result = false;
                    Debug.LogError("IOHelpers: " + exc);
                }
            }
            return result;
        }

        public static T RetreiveDataLocal<T>(string id, string persistentDataPathFile)
        {
            T result = default(T);
            string dataPath = Application.persistentDataPath + string.Format(localPersistancePathFileFormat, id);
            if (File.Exists(dataPath))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream file = File.Open(dataPath, FileMode.Open))
                    {
                        result = (T)formatter.Deserialize(file);
                        file.Close();
                    }
                }
                catch (Exception exc)
                {
                    Debug.LogError(string.Format("Error in IOHelpers.RetreiveDataLocal with path ({0}): " + exc, dataPath));
                }
            }
            return result;
        }
    }
}
