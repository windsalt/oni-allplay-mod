using System.Collections.Generic;
using System.ComponentModel;
using utils;

namespace service
{
    public class DuplicantService
    {
        private readonly DuplicantUtil duplicantUtil = new DuplicantUtil();

        // TODO 判断是否有复制人被困住,返回复制人信息
        public void IsStuck()
        {
            duplicantUtil.GetAllInfo();
        }

        public List<model.DuplicantInfo> GetAllInfo()
        {
            return duplicantUtil.GetAllInfo();
        }

        public bool ReName(int id, string newName)
        {
            var flag = false;
            if (newName != "")
            {
                var minion = duplicantUtil.GetMinion(id);
                Debug.Log("获取minion");
                if (minion != null)
                {
                    Debug.Log("minion不为空");
                    minion.SetName(newName);
                    flag = true;
                }
            }
            return flag;
        }
    }
}
