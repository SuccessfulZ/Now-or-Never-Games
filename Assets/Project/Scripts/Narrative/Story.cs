[System.Serializable]
public class Story
{
    public string title;
    public string text;
    public int id;
    public bool picked;

    public Story(int id, string title, string text)
    {
        this.id = id;
        this.title = title;
        this.text = text;
        picked = false;
    }
    public Story(string title, string text) : this(0, title, text) { }
}
