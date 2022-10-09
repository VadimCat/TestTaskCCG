using UnityEngine;

namespace Client
{
    public class CardViewData
    {
        public CardViewData(string title, string description, Sprite art)
        {
            Title = title;
            Description = description;
            Art = art;
        }
    
        public string Title { get; }
        public string Description { get; }
        public Sprite Art { get; }
    }
}