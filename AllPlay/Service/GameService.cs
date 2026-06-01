namespace AllPlay.Service
{
    public class GameService
    {
        // TODO 挖掘建造任务
        private readonly Utils.GameUtil gameUtil = new Utils.GameUtil();

        public int GetSaveTime()
        {
            return gameUtil.GetSave();
        }

        public bool SetSaveTime(int i)
        {
            // 设置自动保存间隔,-1,1
            if (i < 0)
            {
                i = -1;
            }
            else if (i > 1 & i < 5)
            {
                i = 1;
            }
            else if (i > 50)
            {
                i = 50;
            }
            gameUtil.SetSaveTime(i);
            return true;
        }
    }
}
