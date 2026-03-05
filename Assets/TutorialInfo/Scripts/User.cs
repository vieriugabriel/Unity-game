public class User
{
    public string controlMethod;
    public int[] carUnlocked;
    public int selectedCarIndex;
    public float scrollPos;
    public int mapIndex;
    public int[] mapUnlocked;
    public float mapScrollPos;
    public int coins;
    public float sound;
    public User(string controlMethod, int[] carUnlocked, int selectedCarIndex, float scrollPos, int mapIndex, int[] mapUnlocked, float mapScrollPos, int coins, float sound)
    {
        this.controlMethod = controlMethod;
        this.carUnlocked = carUnlocked;
        this.selectedCarIndex = selectedCarIndex;
        this.scrollPos = scrollPos;
        this.mapIndex = mapIndex;
        this.mapUnlocked = mapUnlocked;
        this.mapScrollPos = mapScrollPos;
        this.coins = coins;
        this.sound = sound;
    }
}
