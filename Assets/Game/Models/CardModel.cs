namespace Game.Models
{
    public class CardModel
    {
        public int Id { get; }
        public string SpriteKey { get; }

        public CardModel(int id, string spriteKey)
        {
            Id = id;
            SpriteKey = spriteKey;
        }
    }
}
