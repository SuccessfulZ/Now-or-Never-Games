using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryPicker
{
    private List<Story> unreadStories;

    public int GetStoryCount() => unreadStories.Count;

    public Story GetStory(int index) => unreadStories[index];

    public void ResetUnreadStories()
    {
        unreadStories = new List<Story>(stories.Select(x => new Story(x.id, x.title, x.text)));
    }

    public void LoadUnreadStories()
    {
        ResetUnreadStories();
        List<Story> storiesToRemove = new List<Story>();
        foreach (var story in unreadStories)
        {
            if (HasStory(story)) storiesToRemove.Add(story);
        }
        unreadStories = unreadStories.Except(storiesToRemove).ToList();
    }

    public void SaveStory(Story story)
    {
        string key = GetBatchKey(story);
        int batch = PlayerPrefs.GetInt(key, 0);
        batch |= 1 << (story.id % 32);
        PlayerPrefs.SetInt(key, batch);
    }

    public void ClearStories()
    {
        for (int i = 0; i < GetStoryCount() / 32; ++i)
        {
            string key = GetBatchKeyFromBatchID(i);
            PlayerPrefs.DeleteKey(key);
        }
    }

    public bool HasStory(Story story)
    {
        string key = GetBatchKey(story);
        int batch = PlayerPrefs.GetInt(key, 0);

        return (batch & (1 << (story.id % 32))) != 0;
    }

    public Story PickStory()
    {
        int storyID = Random.Range(0, GetStoryCount());
        var story = GetStory(storyID);
        story.picked = true;
        unreadStories.Remove(story);
        SaveStory(story);
        return story;
    }

    private string GetBatchKey(Story story) => GetBatchKey(story.id);
    private string GetBatchKey(int storyID) => GetBatchKeyFromBatchID(storyID / 32);
    private string GetBatchKeyFromBatchID(int batchID) => $"s{batchID}";

    private static List<Story> stories = new List<Story>
    {
        new Story(0, "a", "Hello"),
        new Story(1, "b", "World"),
        new Story(2, "c", "!!!"),
    };
}
