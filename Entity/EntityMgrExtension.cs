using PFramework;
using UnityEngine;

namespace GameMain
{
    public static class EntityMgrExtension
    {
        private static int IdPlus = 0;
        private static int IdMinus = 0;

        public static int GetSerialID(this EntityMgr obj, bool isInServer = true)
        {
            if (isInServer)
                return IdPlus++;
            else
                return IdMinus--;
        }

        public static T ShowEntity<T>(this EntityMgr obj, Vector3 pos = default, Quaternion quaternion = default,
            Transform parent = null, bool isInServer = true) where T : Entity
        {
            return obj.ShowEntity<T>(GetSerialID(obj, isInServer), pos, quaternion, parent);
        }

        public static T ShowEntity<T>(this EntityMgr obj,IEntityData entityData, Vector3 pos = default, Quaternion quaternion = default,
            Transform parent = null, bool isInServer = true) where T : Entity
        {
            return obj.ShowEntity<T>(GetSerialID(obj, isInServer), entityData, pos, quaternion, parent);
        }
        
        public static T ShowEntity<T>(this EntityMgr obj,string key,IEntityData entityData, Vector3 pos = default, Quaternion quaternion = default,
            Transform parent = null, bool isInServer = true) where T : Entity
        {
            return obj.ShowEntity<T>(GetSerialID(obj, isInServer), key, entityData, pos, quaternion, parent);
        }
    }
}