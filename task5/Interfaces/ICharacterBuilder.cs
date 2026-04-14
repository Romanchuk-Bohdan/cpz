using BuilderTask.Models;

namespace BuilderTask.Interfaces
{
    public interface ICharacterBuilder
    {
        ICharacterBuilder SetName(string name);
        ICharacterBuilder SetHeight(double height);
        ICharacterBuilder SetBodyType(string bodyType);
        ICharacterBuilder SetHairColor(string hairColor);
        ICharacterBuilder SetEyeColor(string eyeColor);
        ICharacterBuilder SetClothing(string clothing);
        ICharacterBuilder AddInventoryItem(string item);
        ICharacterBuilder AddDeed(string deed); 
        Character GetCharacter();
    }
}