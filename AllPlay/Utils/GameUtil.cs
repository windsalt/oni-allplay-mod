using UnityEngine;

namespace AllPlay.Utils
{
    // 一些关于游戏的一些设置
    public class GameUtil
    {
        // TODO 设置游戏运行倍数?
        // TODO 添加建造挖掘任务?
        // TODO 消毒任务开关
        // TODO 游戏日程安排

        // TODO 设置游戏保存周期
        private readonly SpeedControlScreen[] speedControl =
            Object.FindObjectsByType<SpeedControlScreen>(FindObjectsSortMode.InstanceID);

        private readonly SaveGame[] save = Object.FindObjectsByType<SaveGame>(
            FindObjectsSortMode.InstanceID
        );

        /// <summary>
        /// 获取游戏当前倍速,如果使用了倍速mod,一样的,依旧获取到的并不是实际倍数,而是游戏本身有的3档速度
        /// </summary>
        /// <returns cref="int">速度值:0,1,2,对应的游戏3档速度</returns>
        public int GetSpeed()
        {
            foreach (var c in speedControl)
            {
                return c.GetSpeed();
            }
            return 0;
        }

        /// <summary>
        /// 设置游戏自动保存间隔周期
        /// </summary>
        /// <param name="cycle">间隔周期</param>
        /// <returns cref="bool">是否成功</returns>
        public bool SetSaveTime(int cycle)
        {
            foreach (var s in save)
            {
                s.AutoSaveCycleInterval = cycle;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取游戏自动保存间隔
        /// </summary>
        /// <returns cref="int">间隔周期</returns>
        public int GetSave()
        {
            foreach (var c in save)
            {
                return c.AutoSaveCycleInterval;
            }
            return 0;
        }
    }
}
