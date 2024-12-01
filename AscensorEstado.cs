namespace AscensorApi
{
    public class AscensorEstado
    {
        public int CurrentFloor { get; set; }
        public bool IsMoving { get; set; }
        public bool DoorsOpen { get; set; }
        public string description { get; set;}
    }
}
