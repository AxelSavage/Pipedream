[System.Serializable]
public class Pipe
{
    public int Id { get; set; }
    public float Diameter { get; set; }
    public float Length { get; set; }
    public float FlowRate { get; set; }
    public float PressureDrop { get; set; }
    public bool HasPump { get; set; }
    public float PumpPower { get; set; }
}
