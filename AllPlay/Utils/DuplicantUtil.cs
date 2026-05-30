using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AllPlay.Utils
{
    // TODO 复制人相关工具
    public class DuplicantUtil
    {
        private readonly MinionIdentity[] minions = Object.FindObjectsByType<MinionIdentity>(
            FindObjectsSortMode.InstanceID
        );

        // TODO 获取角色的可行动路径坐标
        public void GetActionPath(int id)
        {
            // 获取角色的路径坐标
        }

        // 对复制人进行改名
        public bool ReName(int id, string newName)
        {
            var flag = false;
            var minion = GetMinion(id);
            if (minion != null)
            {
                minion.SetName(newName);
                flag = true;
            }
            return flag;
        }

        // 对复制人重命名
        public bool ReName(MinionIdentity minion, string newName)
        {
            var flag = false;
            if (minion != null)
            {
                minion.SetName(newName);
                flag = true;
            }
            return flag;
        }

        public string GetName(int id)
        {
            // 获取角色的名字
            var minion = GetMinion(id);
            return minion?.GetProperName();
        }

        // 获取角色的当前坐标
        public Model.Coor GetXY(int id)
        {
            var minion = GetMinion(id);
            var coor = minion.transform.position;
            return new Model.Coor(coor.x, coor.y);
        }

        // TODO 获取所有复制人信息
        public List<Model.DuplicantInfo> GetAllInfo()
        {
            var info = new List<Model.DuplicantInfo>(minions.Count());
            for (int i = 0; i < minions.Count(); i++)
            {
                var user = new Model.DuplicantInfo();
                user.Id = minions[i].GetInstanceID();
                user.Name = minions[i].GetProperName();
                user.coor = new Model.Coor(
                    minions[i].transform.position.x,
                    minions[i].transform.position.y
                );

                info.Add(user);
            }

            return info;
        }

        // TODO 判断复制人是否被困住
        public bool IsStuck(int id)
        {
            return false;
        }

        public MinionIdentity GetMinion(int id) =>
            minions.FirstOrDefault(m => m.GetInstanceID() == id);

        public MinionIdentity GetMinion(string name) =>
            minions.FirstOrDefault(m => m.GetProperName() == name);
    }
}
