using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryPicker
{
    public StoryPicker()
    {
        LoadUnreadStories();
    }

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
        int count = GetStoryCount();
        if (count > 0)
        {
            int storyID = Random.Range(0, count);
            var story = GetStory(storyID);
            story.picked = true;
            unreadStories.Remove(story);
            SaveStory(story);
            return story;
        }
        else
        {
            var story = new Story("Developers", "You have seen all stories!");
            return story;
        }
    }

    private string GetBatchKey(Story story) => GetBatchKey(story.id);
    private string GetBatchKey(int storyID) => GetBatchKeyFromBatchID(storyID / 32);
    private string GetBatchKeyFromBatchID(int batchID) => $"s{batchID}";

    private static List<Story> stories = new List<Story>
    {
        new Story( 0, "Oleksandr, a serviceman, 21 years old", "The first day after self-isolation we practiced on the training yard. I ran the first lap with the thought that I was about to die. I don’t know how to explain it, but I could fully feel my lungs. I thought it was out of habit after lying for a long time. I knew I was checked, and that there was no pneumonia. Although we, conscripts, are checked carelessly. I ran the second lap and I felt every vessel of my lungs. Wow…"),
        new Story( 1, "said Maryna, an 58-year-old office worker", "I didn't have the symptoms we were told about from the TV, I just had a very bad headache for a few days. I thought it was a migraine and calmly went to work,"),
        new Story( 2, "Serhii, programmer, 35 years old", "It's hard, very hard mentally. To be honest, I was afraid to die. It was hard for me to breathe, only pessimistic thoughts came to mind, I became very irritable and just waited when I could continue to live at my own pace."),
        new Story( 3, "Valeriia, music teacher, 47 years old", "Yes, I taught children to play the piano on Viber, believe me, it's possible. To spread the bacilli where there are enough without me, I thought this prospect was not very bright ... It seemed to me that I had increased intracranial pressure. I read about it. It felt like my face was tearing."),
        new Story( 4, "Vladyslav, businessman, 42 years old", "For about a week I had a temperature of 39°C, I had a slight cough, weakness, I also lost my sense of smell and taste. At this time I stopped training, moved only a little, drank more water and slept a lot. In fact, not as terrible as they say. Why panic so much? You just have to be careful. Well, or I'm lucky."),
        new Story( 5, "Oleksii, dentist, 62 years old", "I was suffocating, as if someone was pressing on my chest. It's good that a nurse was on duty nearby. She called a doctor, and I was taken away. I lost consciousness, I remember only the inscription \"Resuscitation Area\" on the door, and then everything is fuzzy"),
        new Story( 6, "Ruslan, MP, 41 years old", "I want to tell all the people of the country: take this virus more seriously, take all precautions. It is possible that young people, for the most part, overcome the disease, but it is necessary to think about relatives, parents, and others."),
        new Story( 7, "Yana, beauty queen, 29 years old", "In those days all kinds of fears awoke in me, and first of all, the fear of death. When I saw the eyes of doctors who did not know what to do with me, I completely revaluated my life.\r\nIn this situation, you begin to fight all the fears, appreciate your health and the health of your loved ones. I wanted to protect them and see them at the same time. Illness was a kind of a rebirth."),
        new Story( 8, "Nataliia, public figure, 41 years old", "I would advise people not to panic. If you have symptoms, be sure to contact your family doctor. But it is not necessary to run to doctors on every occasion. We must follow all the precautions recommended by the Ministry of Health. These are simple rules that help you keep yourself in the right condition and not get sick."),
        new Story( 9, "Vadym, businessman, 50 years old", "At first I was treated like a leper. But at the same time I met a huge number of good, sensitive and intelligent people. They supported me and restored my faith in people.\r\nThe poet Joseph Brodsky once said: \"If it is impossible to save the world, let's save at least one person.\" Probably, that's about me."),
        new Story(10, "Andrew, businessman, 46 years old", "You should not be afraid. We need to draw the right conclusions. The first thing is to go to self-isolation to protect relatives and friends. Do not stay at home, do not hide that you have a virus and do not self-medicate. Immediately turn to doctors and trust them completely."),
        new Story(11, "Maryna, 30 years old", "And even if you are young and healthy, there is a chance to infect somebody weaker. For example, people who do not wear a mask in transport, in the subway - how does this person know that he will not be guilty of someone's death? This is a collective irresponsibility."),
        new Story(12, "Rostyslav, student, 22 years old", "It's not as scary as it is described. It's just a disease that needs to be experienced. You have to wash your hands for sure. After coming back from the street. When the symptoms kick in, see a doctor. No one will scold you, but will only do better,"),
        new Story(13, "Yuliia, 26, and Mykhailo, 33", "I wish those who are ill to endure the time of sitting at home. When you go down to meet the courier, you have to go back up. It's hard. It's hard not to communicate with friends. I want healthy people not to think that the virus will pass. When you are sick, the worst thing is not being sick, but infect the others,"),
        new Story(14, "Tonya, 19", "Your mental health depends on your physical. When I stopped being nervous and measuring my temperature every hour, it started to get easier,"),
    };
}
