namespace AllPlay.Model
{
    // 复制人信息
    public struct DuplicantInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int o2 { get; set; }
        public int yali { get; set; }
        public int Calorie { get; set; }
        public Coor Coor { get; set; }
    }

    public struct Coor
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Coor(float x, float y)
        {
            X = (int)x;
            Y = (int)y;
            Z = 1;
        }
    }
}
