namespace AllPlay.Model
{
    public struct DuplicantInfo
    {
        public int Id { get; set; }
        public string Name;
        public Coor coor;
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
