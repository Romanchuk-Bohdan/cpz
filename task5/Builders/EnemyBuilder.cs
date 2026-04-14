using BuilderTask.Interfaces;
using BuilderTask.Models;

namespace BuilderTask.Builders
{
    public class EnemyBuilder : ICharacterBuilder
    {
        private Character _character = new Character { Role = "Enemy" };

        public ICharacterBuilder SetName(string name) { _character.Name = name; return this; }
        public ICharacterBuilder SetHeight(double height) { _character.Height = height; return this; }
        public ICharacterBuilder SetBodyType(string bodyType) { _character.BodyType = bodyType; return this; }
        public ICharacterBuilder SetHairColor(string hairColor) { _character.HairColor = hairColor; return this; }
        public ICharacterBuilder SetEyeColor(string eyeColor) { _character.EyeColor = eyeColor; return this; }
        public ICharacterBuilder SetClothing(string clothing) { _character.Clothing = clothing; return this; }
        public ICharacterBuilder AddInventoryItem(string item) { _character.Inventory.Add(item); return this; }
        
        public ICharacterBuilder AddDeed(string deed) 
        { 
            _character.Deeds.Add($"Evil Deed: {deed}"); 
            return this; 
        }

        public Character GetCharacter()
        {
            Character result = _character;
            _character = new Character { Role = "Enemy" };
            return result;
        }
    }
}