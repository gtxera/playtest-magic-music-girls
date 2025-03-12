using System.Collections.Generic;

public class Party
{
    private HashSet<Character> _characters = new HashSet<Character>();

    public float Experience { get; private set; }

    public int Level
    {
        get
        {
            var experience = 0f;
            var level = 0;
            do
            {
                level++;
                experience = LevelCalculator.GetXpRequirementFor(level);
            }
            while (experience < Experience);

            return level;
        }
    }

    public IReadOnlyCollection<Character> Characters => _characters;

    public void AddCharacter(Character character)
    {
        _characters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        _characters.Remove(character);
    }

    public void AddXp(float xp)
    {
        var experienceBefore = Experience;
        var levelBefore = Level;
        Experience += xp;

        var eventBus = EventBus.Instance;

        eventBus.Publish(new ExperienceGainedEvent(experienceBefore, Experience));
        if (levelBefore != Level)
        {
            eventBus.Publish(new LevelGainedEvent(levelBefore, Level));
        }
    }
}
